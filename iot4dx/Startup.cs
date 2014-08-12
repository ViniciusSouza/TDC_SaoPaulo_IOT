using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iot4dx.Startup))]
namespace iot4dx
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        //private void setRoles()
        //{
        //    using (var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext())))
        //    using (var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        //        foreach (var item in userRoles)
        //        {
        //            if (!rm.RoleExists(item.Value))
        //            {
        //                var roleResult = rm.Create(new IdentityRole(item.Value));
        //                if (!roleResult.Succeeded)
        //                    throw new ApplicationException("Creating role " + item.Value + "failed with error(s): " + roleResult.Errors);
        //            }
        //            var user = um.FindByName(item.Key);
        //            if (!um.IsInRole(user.Id, item.Value))
        //            {
        //                var userResult = um.AddToRole(user.Id, item.Value);
        //                if (!userResult.Succeeded)
        //                    throw new ApplicationException("Adding user '" + item.Key + "' to '" + item.Value + "' role failed with error(s): " + userResult.Errors);
        //            }
        //        }
        //}
       
    }
}
