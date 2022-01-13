using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CommunityProject.Swagger{
    
        public static class SwaggerConfiger
        {
            internal static void SwaggerSetup(this IServiceCollection services, OpenApiInfo settings)
            {
                if (settings.Version != null)
                {
                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc(settings.Version, settings);
                        c.OperationFilter<AddAuthHeaderOperationFilter>();
                        c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                        {
                            Description = "`Token only!!!` - without `Bearer_` prefix or {} or \"\"",
                            Type = SecuritySchemeType.Http,
                            BearerFormat = "JWT",
                            In = ParameterLocation.Header,
                            Scheme = "bearer"
                        });
                    });
                }
            }
        }
   
}
