using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDC2014WP.DataObjects;

namespace TDC2014WP.ViewModel
{
    public class AgendaViewModel 
    {
        private IMobileServiceTable<AgendaEvento> agendaTable = App.MobileService.GetTable<AgendaEvento>();
        public ObservableCollection<AgendaEvento> Agendas;


        public async Task GetAgendaRecords()
        {
            List<AgendaEvento> itens = (await agendaTable.ToCollectionAsync()).OrderBy(agenda => agenda.HoraInicio).ToList<AgendaEvento>();

            if (itens.Count() == 0)
            {
                CreateAgenda();
            }

            Agendas = new ObservableCollection<AgendaEvento>(itens);
        }

        private async void CreateAgenda()
        {

            List<AgendaEvento> agenda = new List<AgendaEvento>();

            //Quinta-feira
            //Cloud Computing
            AgendaEvento Danilo_cloud = new AgendaEvento() { Trilha = "Cloud Computing", Data = new DateTime(2014, 8, 6), HoraInicio = new DateTime(2014, 8, 6, 13, 10, 0, 0), HoraTermino = new DateTime(2014, 8, 6, 14, 0, 0), Titulo = "Microsoft Azure: Opção de Nuvem para Todo o Desenvolvedor", Descricao = "Sabemos que quando a palavra \"MIcrosoft\" é citada, logo se pensa em uma plataforma fechada e proprietária. Entenda como isso mudou ao longo dos anos e como hoje o Microsoft Azure é uma solução de Nuvem que suporta diversas soluções e cenários open source. Abordaremos as principais funcionalidades existentes e o que há no roadmap para o futuro.", Palestrante = "Danilo Bordini", TwitterPalestrante = "@dbordini", UrlFotoPalestrante = "http://dbordini.azurewebsites.net/wp-content/uploads/2013/06/smartcard-2012_09_27-14_59_08-UTC-237x300.jpg", UrlSitePalestrante = "http://dbordini.azurewebsites.net/", MiniBio = "Danilo Bordini é graduado em Tecnologia da Informação pela FATEC ? Sorocaba, com Especialização em Marketing pela FGV. Certificado como MCSE e MCSA em Windows 2000 / 2003 / 2008, trabalha na trabalha na área de informática desde 1996, passando pelas disciplinas de Desenvolvimento de Software e Redes de Computadores. Hoje, ocupa a função de \"Technical Evangelist Manager\", liderando um time de Especialistas em Desenvolvimento de Software & Infraestrutura." };
            agenda.Add(Danilo_cloud);

            //Software Livre
            AgendaEvento vinicius_OpenSource = new AgendaEvento() { Trilha = "Software Livre", Data = new DateTime(2014, 8, 6), HoraInicio = new DateTime(2014, 8, 6, 13, 10, 0, 0), HoraTermino = new DateTime(2014, 8, 6, 14, 0, 0), Titulo = "Microsoft e OpenSource - Por que e como contribuir?", Descricao = "Nesta sessão falaremos da importância da iniciativa Open Souce na Microsoft, apresentando alguns dos projetos que estamos trabalhando e como você pode contribuir.", Palestrante = "Vinícius Souza", TwitterPalestrante = "@vbs_br", UrlFotoPalestrante = "http://viniciussouza.azurewebsites.net/wp-content/uploads/2012/08/favicon.png", UrlSitePalestrante = "http://viniciussouza.azurewebsites.net/", MiniBio = "Vinícius é Bacharel em Ciência da computação com mais de 10 anos de experiência em desenvolvimento utilizando diferentes plataformas. Atualmente trabalha como evangelista de desenvolvimento no time evangelistas da Microsoft Brasil. Além da paixão por tecnologia, pratica automodelismo off road, IoT, joga muito game e está aprendendo a tocar guitarra." };
            agenda.Add(vinicius_OpenSource);

            //15:40 às 16:30
            AgendaEvento Debate_Opensource_Cloud = new AgendaEvento() { Trilha = "Software Livre", Data = new DateTime(2014, 8, 6), HoraInicio = new DateTime(2014, 8, 6, 15, 40, 0, 0), HoraTermino = new DateTime(2014, 8, 6, 16, 30, 0), Titulo = "Software Livre e Computação em Nuvem (Debate)", Descricao = "Debate com os palestrantes sobre a utilização de nuvem e suas implicações", Palestrante = "Vinícius Souza", TwitterPalestrante = "@vbs_br", UrlFotoPalestrante = "http://viniciussouza.azurewebsites.net/wp-content/uploads/2012/08/favicon.png", UrlSitePalestrante = "http://viniciussouza.azurewebsites.net/", MiniBio = "Vinícius é Bacharel em Ciência da computação com mais de 10 anos de experiência em desenvolvimento utilizando diferentes plataformas. Atualmente trabalha como evangelista de desenvolvimento no time evangelistas da Microsoft Brasil. Além da paixão por tecnologia, pratica automodelismo off road, IoT, joga muito game e está aprendendo a tocar guitarra." };
            agenda.Add(Debate_Opensource_Cloud);


            foreach (AgendaEvento agenda_item in agenda)
            {
                await agendaTable.InsertAsync(agenda_item);
            }

        }
    }
}
