using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Timeouts;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Feature
{
    public static class FeatureExtensions
    {
        public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration)
        {
            string myPolicy = "policyApiEcommerce";

            services.AddMvc();
            services.AddControllers().AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
            });

            services.AddRequestTimeouts(options => {
                options.DefaultPolicy =
                    new RequestTimeoutPolicy { Timeout = TimeSpan.FromMilliseconds(1500) };
                options.AddPolicy("CustomPolicy", TimeSpan.FromMilliseconds(2000));
            });

            return services;
        }
    }
}
