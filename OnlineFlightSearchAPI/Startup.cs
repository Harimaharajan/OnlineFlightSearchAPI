using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OnlineFlightSearchAPI.DBContext;
using OnlineFlightSearchAPI.FlightServices;
using OnlineFlightSearchAPI.Repositories;
using OnlineFlightSearchAPI.Repositories.FlightRepository;
using OnlineFlightSearchAPI.Services.AuthenticationServices;
using OnlineFlightSearchAPI.Validator;

namespace OnlineFlightSearchAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<ISearchFlightService, FlightService>();
            services.AddTransient<IAirportServices, AirportServices>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFlightRepository, FlightRepository>();
            services.AddTransient<IAirportRepository, AirportRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<UserValidator, UserValidator>();
            services.AddDbContext<IFlightDBContext, FlightDBContext>();

            string secretKey = Configuration.GetSection("JWTParameter:SecretKey").Value;

            var jettokenparams = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = Configuration.GetSection("JWTParameter:Issuer").Value,
                ValidAudience = Configuration.GetSection("JWTParameter:Audience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtconfig =>
                    jwtconfig.TokenValidationParameters = jettokenparams);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
