using Microsoft.Owin;

using PenSamples.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace PenSamples.Web
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Owin;

    using PenSamples.Web.Data;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(NewUserHandler);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private static async Task NewUserHandler(IOwinContext context, Func<Task> next)
        {
            var user = context.Authentication.User;
            if (user != null)
            {
                var username = user.Identity.Name;

                using (var entities = new PenEntities())
                {
                    using (var transaction = entities.Database.BeginTransaction(IsolationLevel.Serializable))
                    {
                        try
                        {
                            var userEntity = await entities.Users.FirstOrDefaultAsync(x => x.Name == username);
                            if (userEntity == null)
                            {
                                entities.Users.Add(new User() { Name = username, Status = ""});

                                await entities.SaveChangesAsync();
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }

            await next.Invoke();
        }
    }
}