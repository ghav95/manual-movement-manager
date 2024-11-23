using System.Web.Http;
using WebActivatorEx;
using Manual.Movement.Manager.Api;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Manual.Movement.Manager.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {                       
                        c.SingleApiVersion("v1", "Manual.Movement.Manager.Api");
                        c.PrettyPrint();
                })
                .EnableSwaggerUi(c =>
                {
                    c.DisableValidator();
                });
        }
    }
}
