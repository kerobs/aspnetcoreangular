using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreSpa.Web
{
    public class Startup
    {
        // Order or run
        //1) Constructor
        //2) Configure services
        //3) Configure
        private IHostingEnvironment HostingEnvironment { get; }
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            HostingEnvironment = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            //   services.AddPreRenderDebugging(HostingEnvironment);

            services.AddOptions();



            services.AddMemoryCache();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                // base-address of your identityserver
                                options.Authority = Configuration["StsAuthority"];
                                // name of the API resource
                                options.Audience = "spa-api";
                            });



            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist/aspnetcorespa";
            });


        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseHealthChecks("/health");

            // app.AddCustomSecurityHeaders();



            app.UseHttpsRedirection();

            // https://github.com/openiddict/openiddict-core/issues/518
            // And
            // https://github.com/aspnet/Docs/issues/2384#issuecomment-297980490
            var forwarOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            forwarOptions.KnownNetworks.Clear();
            forwarOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwarOptions);

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseSpaStaticFiles();

            app.UseSpa(spa =>
                      {
                          spa.Options.SourcePath = "ClientApp";
                          if (env.IsDevelopment())
                          {
                               spa.UseAngularCliServer(npmScript: "start");
                          }
                      });
        }
    }
}
