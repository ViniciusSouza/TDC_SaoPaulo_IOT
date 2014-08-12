using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TDC2014WP.Resources;
using TDC2014WP.DataObjects;
using Microsoft.WindowsAzure.MobileServices;
using TDC2014WP.ViewModel;
using Hammock.Web;
using TDC2014WP.Twitter;
using Hammock;
using System.IO;
using Hammock.Authentication.OAuth;
using Windows.Networking.Connectivity;
using Microsoft.Phone.Net.NetworkInformation;
using TDC2014WP.Helper;

namespace TDC2014WP
{
    public partial class MainPage : PhoneApplicationPage
    {

        private String strMensagemTwitte = "Troquei a cor do stand Microsoft no #TDC2014 por {0} @iot4dx";

        private MicrosoftColors CorSelecionada;

        string OAuthTokenKey = string.Empty;
        string tokenSecret = string.Empty;
        string accessToken = string.Empty;
        string accessTokenSecret = string.Empty;
        string userID = string.Empty;
        string userScreenName = string.Empty;
        Uri lasturl;

        private AgendaViewModel agendaVM;

        private MobileServiceCollection<IoTActivity, IoTActivity> items;

        private IMobileServiceTable<IoTActivity> atividadeTable = App.MobileService.GetTable<IoTActivity>();

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;

            CheckInternet();
                

        }

        private void CheckInternet()
        {
            if (!DeviceNetworkInformation.IsNetworkAvailable)
            {

                //Dispatcher.BeginInvoke(() =>
                //{
                    MessageBox.Show("O Aplicativo necessita de internet para funcionar!");
                //});
                
                Application.Current.Terminate();
            }
        }

        private void NetworkInformation_NetworkStatusChanged(object sender)
        {
            CheckInternet();
            
        }

       

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            SettingsManager sm = new SettingsManager();
            DateTime now = DateTime.Now;
            if (sm.GetValue("DateLastColorChanged", now) == now)
            {
                App.DateLastColorChanged = now.AddMinutes(-1);
            }
            else
            {
                App.DateLastColorChanged = sm.GetValue("DateLastColorChanged", now);
            }
            base.OnNavigatedTo(e);


            agendaVM = new AgendaViewModel();
            await agendaVM.GetAgendaRecords();


            var lista_agenda = agendaVM.Agendas.OrderBy(agenda => agenda.HoraInicio);
            

            foreach (var agenda in lista_agenda)
            {
                agenda.HoraInicio = DateHelper.ToBrazil(agenda.HoraInicio);
                agenda.HoraTermino = DateHelper.ToBrazil(agenda.HoraTermino);
            }

            AgendaLista.DataContext = lista_agenda;

        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            SettingsManager sm = new SettingsManager();
            sm.SetValue("DateLastColorChanged", App.DateLastColorChanged);
            base.OnNavigatedFrom(e);
        }

        private async void ChangeColor(MicrosoftColors microsoftColors, String user, String strTweet)
        {
            if (DateTime.Now.CompareTo(App.DateLastColorChanged.AddMinutes(App.MinutesToChangeColor)) >= 0)
            {
                //Armazenando o instante da mensagem
                App.DateLastColorChanged = DateTime.Now;

                btOverlay.Visibility = System.Windows.Visibility.Visible;

                IoTActivity atividade = new IoTActivity();
                atividade.Color = microsoftColors;
                atividade.User = "@" + userScreenName;
                await atividadeTable.InsertAsync(atividade);

                //RefreshItemsAsync();

                //Removendo o Overlap
                btOverlay.Visibility = System.Windows.Visibility.Collapsed;

                //Faz um tweet da aplicação em  nome do usuário.
                PostTweet(user, strTweet);

                MessageBox.Show("Solicitação de troca de cor do stand enviada!", "MICROSOFT @ TDC 2014", MessageBoxButton.OK);

            }
            else
            {
                MessageBox.Show("Você pode solicitar uma mudança de cor a cada 1 minuto", "MICROSOFT @ TDC 2014", MessageBoxButton.OK);
            }
        }

        private async void RefreshItemsAsync()
        {
            //Lista
            try
            {
                items = await atividadeTable
                    .Where(iot => iot.Complete == false)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
            }

        }


        private void AutenticaChangeColor()
        {
            string corlor_name = string.Empty;
            if (isAlreadyLoggedIn())
            {
                userLoggedIn();

                switch (CorSelecionada)
                {
                    case MicrosoftColors.Blue: corlor_name = "Azul"; break;
                    case MicrosoftColors.Green: corlor_name = "Verde"; break;
                    case MicrosoftColors.Orange: corlor_name = "Amarelo"; break;
                    case MicrosoftColors.Red: corlor_name = "Vermelho"; break;
                }
                ChangeColor(CorSelecionada, userScreenName, String.Format(this.strMensagemTwitte, corlor_name));
            }
            else
            {
                loginBrowserControl.Visibility = System.Windows.Visibility.Visible;
                ContentPanel.Visibility = System.Windows.Visibility.Collapsed;

                var requestTokenQuery = OAuthUtil.GetRequestTokenQuery();
                requestTokenQuery.RequestAsync(TwitterSettings.RequestTokenUri, null);
                requestTokenQuery.QueryResponse += new EventHandler<WebQueryResponseEventArgs>(requestTokenQuery_QueryResponse);
            }
        }

        private void btVermelho_Click(object sender, RoutedEventArgs e)
        {
            CheckInternet();
            CorSelecionada = MicrosoftColors.Red;
            AutenticaChangeColor();

            


        }

        private void btVerde_Click(object sender, RoutedEventArgs e)
        {
            CheckInternet();
            CorSelecionada = MicrosoftColors.Green;
            //ChangeColor(DataObjects.MicrosoftColors.Green);
            AutenticaChangeColor();
        }

        private void btAmarelo_Click(object sender, RoutedEventArgs e)
        {

            CheckInternet();
            CorSelecionada = MicrosoftColors.Orange;
            //ChangeColor(DataObjects.MicrosoftColors.Orange);
            AutenticaChangeColor();
        }

        private void btAzul_Click(object sender, RoutedEventArgs e)
        {
            CheckInternet();
            CorSelecionada = MicrosoftColors.Blue;
            //ChangeColor(DataObjects.MicrosoftColors.Blue);
            AutenticaChangeColor();
        }



        private Boolean isAlreadyLoggedIn()
        {
            accessToken = MainUtil.GetKeyValue<string>("AccessToken");
            accessTokenSecret = MainUtil.GetKeyValue<string>("AccessTokenSecret");
            userScreenName = MainUtil.GetKeyValue<string>("ScreenName");
            userID = MainUtil.GetKeyValue<string>("UserID");

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(accessTokenSecret))
                return false;
            else
                return true;
        }

        private void userLoggedIn()
        {
            Dispatcher.BeginInvoke(() =>
            {
                // var SignInMenuItem = (Microsoft.Phone.Shell.ApplicationBarMenuItem)this.ApplicationBar.MenuItems[0];
                // SignInMenuItem.IsEnabled = false;

                // var SignOutMenuItem = (Microsoft.Phone.Shell.ApplicationBarMenuItem)this.ApplicationBar.MenuItems[1];
                //SignOutMenuItem.IsEnabled = true;

                //TweetPanel.Visibility = System.Windows.Visibility.Visible;
                //txtUserName.Text = "Welcome " + userScreenName;
            });
        }

        private void PostTweet(string userid, string mensagem)
        {
            if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(mensagem))
                return;

            try
            {
                var credentials = new OAuthCredentials
                {
                    Type = OAuthType.ProtectedResource,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                    ParameterHandling = OAuthParameterHandling.HttpAuthorizationHeader,
                    ConsumerKey = TwitterSettings.consumerKey,
                    ConsumerSecret = TwitterSettings.consumerKeySecret,
                    Token = this.accessToken,
                    TokenSecret = this.accessTokenSecret,
                    Version = "1.0"
                };

                var restClient = new RestClient
                {
                    Authority = "https://api.twitter.com",
                    HasElevatedPermissions = true
                };

                var restRequest = new RestRequest
                {
                    Credentials = credentials,
                    Path = "/1.1/statuses/update.json",
                    Method = WebMethod.Post
                };

                restRequest.AddParameter("status", mensagem);
                restClient.BeginRequest(restRequest, new RestCallback(PostTweetRequestCallback));
            }
            catch
            {
            }
        }
        private void PostTweetRequestCallback(RestRequest request, RestResponse response, object obj)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                //if (response.StatusCode == HttpStatusCode.OK)
                //{
                //    MessageBox.Show("TWEET_POSTED_SUCCESSFULLY");
                //}
                //else if (response.StatusCode == HttpStatusCode.Forbidden)
                //{
                //    MessageBox.Show("TWEET_POST_ERR_UPDATE_LIMIT");
                //}
                //else
                //{
                //    MessageBox.Show("WEET_POST_ERR_FAILED");
                //}
            });

            //var requestTokenQuery = OAuthUtil.GetRequestTokenQuery();
            //requestTokenQuery.RequestAsync(TwitterSettings.RequestTokenUri, null);
            //requestTokenQuery.QueryResponse += new EventHandler<WebQueryResponseEventArgs>(requestTokenQuery_QueryResponse);
        }

        void requestTokenQuery_QueryResponse(object sender, WebQueryResponseEventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader(e.Response);
                string strResponse = reader.ReadToEnd();
                var parameters = MainUtil.GetQueryParameters(strResponse);
                OAuthTokenKey = parameters["oauth_token"];
                tokenSecret = parameters["oauth_token_secret"];
                var authorizeUrl = TwitterSettings.AuthorizeUri + "?oauth_token=" + OAuthTokenKey;

                Dispatcher.BeginInvoke(() =>
                {
                    this.loginBrowserControl.Navigate(new Uri(authorizeUrl, UriKind.RelativeOrAbsolute));
                });
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show(ex.Message);
                });
            }
        }


        private void loginBrowserControl_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            lasturl = e.Uri;
            this.loginBrowserControl.Visibility = Visibility.Visible;
            ContentPanel.Visibility = System.Windows.Visibility.Collapsed;

            this.loginBrowserControl.Navigated -= loginBrowserControl_Navigated;
        }

        private void loginBrowserControl_Navigating(object sender, NavigatingEventArgs e)
        {
            try
            {
                if (e.Uri.ToString().StartsWith(TwitterSettings.CallbackUri))
                {
                    var AuthorizeResult = MainUtil.GetQueryParameters(e.Uri.ToString());
                    var VerifyPin = AuthorizeResult["oauth_verifier"];
                    this.loginBrowserControl.Visibility = Visibility.Collapsed;
                    ContentPanel.Visibility = System.Windows.Visibility.Visible;

                    var AccessTokenQuery = OAuthUtil.GetAccessTokenQuery(OAuthTokenKey, tokenSecret, VerifyPin);

                    AccessTokenQuery.QueryResponse += new EventHandler<WebQueryResponseEventArgs>(AccessTokenQuery_QueryResponse);
                    AccessTokenQuery.RequestAsync(TwitterSettings.AccessTokenUri, null);
                }
            }
            catch
            {
            }
        }

        void AccessTokenQuery_QueryResponse(object sender, WebQueryResponseEventArgs e)
        {
            try
            {
                StreamReader reader = new StreamReader(e.Response);
                string strResponse = reader.ReadToEnd();
                var parameters = MainUtil.GetQueryParameters(strResponse);
                accessToken = parameters["oauth_token"];
                accessTokenSecret = parameters["oauth_token_secret"];
                userID = parameters["user_id"];
                userScreenName = parameters["screen_name"];

                MainUtil.SetKeyValue<string>("AccessToken", accessToken);
                MainUtil.SetKeyValue<string>("AccessTokenSecret", accessTokenSecret);
                MainUtil.SetKeyValue<string>("ScreenName", userScreenName);
                MainUtil.SetKeyValue<string>("UserID", userID);

                userLoggedIn();

                Dispatcher.BeginInvoke(() =>
                {
                    AutenticaChangeColor();
                    //MessageBox.Show("Você está autenticado! Agora escolha a cor e acompanhe no twitter a conta @iot4dx", "MICROSOFT @ TDC 2014", MessageBoxButton.OK);
                });


            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show(ex.Message);
                });
            }
        }

    }
}