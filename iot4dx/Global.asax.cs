using iot4dx.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace iot4dx
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Cria os usuários padrão
            CreateRoles();

        }

        private async void CreateRoles()
        {
            ApplicationDbContext dbc =  ApplicationDbContext.Create();

            //ClearDataBase(dbc);

            var roles = dbc.Roles.ToList();

            if (roles.Count() == 0)
            {
                IdentityResult ir;
                var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(dbc));
                ir = rm.Create(new IdentityRole("Administrators"));
                if (ir.Succeeded)
                {
                    await dbc.SaveChangesAsync();
                    CreatUser("vbs_mack@hotmail.com", "AcaoIoT!2014", "Administrators");
                    CreatUser("marlon.luz@live.com", "AcaoIoT!2014", "Administrators");
                    CreatUser("thiago@bessa.net.br", "AcaoIoT!2014", "Administrators");
                    CreatUser("microsoft@tdc.com", "AcaoIoT!2014", "Administrators");
                }
            }

            var users = dbc.Users.ToList();

        }

        private async  void ClearDataBase(ApplicationDbContext dbc)
        {
            var users = dbc.Users.ToList();
            foreach (var user in users)
            {
                dbc.Users.Remove(user);
            }
            await dbc.SaveChangesAsync();

            var roles = dbc.Roles.ToList();

            foreach (var role in roles)
            {
                dbc.Roles.Remove(role);
            }

            await dbc.SaveChangesAsync();
        }

        private async void CreatUser(string username, string password, string rolename)
        {
            ApplicationDbContext dbc = ApplicationDbContext.Create();
            IdentityResult ir;
            var user = new ApplicationUser()
            {
                UserName = username,
                Email = username,
            };
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(dbc));

            ir = um.Create(user, password);
            if (ir.Succeeded)
                ir = um.AddToRole(user.Id, rolename);

            await dbc.SaveChangesAsync();
        }


        private bool AddUserAndRole(ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "user1@contoso.com",
            };
            ir = um.Create(user, "P_assw0rd1");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }
    }
}
