using Base.Domain.Helpers;
using Base.Infrastructure.Repositories;
using Base.Services.BaseCommonsServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Threading.Tasks;
using Base.Domain.Entities.Customs;
using Base.Services.Mappers;
using Base.Infrastructure.Context;
using Base.Domain.Interfaces.BaseCommons.RepositoryInterface;
using Base.Domain.Interfaces.BaseCommons.ServiceInterface;
using Base.Services.DbAppServices;
using Base.Domain.Interfaces.DbApp.RepositoryInterface;
using Base.Infrastructure.Repositories.DbApp;
using WebApiSimple.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Base.Domain.Interfaces.DbApp.ServiceInterface;
using Base.Services.Helpers.ErrorHandler;
using Sieve.Models;
using Sieve.Services;
using Base.Services.Helpers.Pagination;
using System.IO;
using System.Reflection;
using System;

namespace WebApiSimple
{
    public class Startup
    {
        IWebHostEnvironment env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opts =>
            {
                opts.EnableEndpointRouting = false;
            });

            services.AddHttpContextAccessor();
            services.AddCors(options => options.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            services.AddScoped<Session>();
            services.AddHttpClient();

            #region DbCommonsService
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISecurityService, SecurityService>();
            #endregion

            #region DbAppService
            services.AddTransient<IExampleRepository, ExampleRepository>();
            services.AddTransient<IExampleService, ExampleService>();
            services.AddTransient<IHealthRepository, HealthRepository>();
            services.AddTransient<IHealthService, HealthService>();
            services.AddTransient<IExcelDocGeneratorExampleService, ExcelDocGeneratorExampleService>();
            #endregion


            //services.AddAutoMapper(c => c.AddProfile<Mapper>());
            services.AddAutoMapper(c => c.AddProfile<Mapper>(), typeof(Startup));



            #region Security Token Validation
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("Security:KeyEncryption")));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = System.TimeSpan.Zero
                };
                
            });
            #endregion Security Token Validation

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore); 
            services.AddDbContext<BaseCommonsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Base.Infrastructure")), ServiceLifetime.Transient);
            services.AddDbContext<DbAppContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbAppConnection"), b => b.MigrationsAssembly("Base.Infrastructure")), ServiceLifetime.Transient);

            #region Sieve Pagination
            services.Configure<SieveOptions>(Configuration.GetSection("Sieve"));

            services.AddScoped<ISieveCustomSortMethods, SieveCustomSortMethods>();
            services.AddScoped<ISieveCustomFilterMethods, SieveCustomFilterMethods>();
            services.AddScoped<ISieveProcessor, ApplicationSieveProcessor>();
     
            #endregion


            #region Health
            services.AddHealthChecks().AddCheck<HealthCheckDbAppContextCheck>("DbAppConnection")
            .AddCheck<HealthCheckExternalServices>("ExternalEndPoint");

            #endregion

         


            string aditionalInfo = string.Format("Environment: {0}", this.env.EnvironmentName);
            services.AddSwaggerGen(c =>
            {
				var xfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xpath = Path.Combine(AppContext.BaseDirectory, xfile);
				c.IncludeXmlComments(xpath);
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web.Api", Version = "v1", Description = aditionalInfo });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                }
                });


            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            ApplicationConfiguration.Current = new ApplicationConfiguration();
            ApplicationConfiguration.Current.EMail = Configuration.GetSection("EMail").Get<ApplicationConfiguration.ConfigurationEMail>();
            ApplicationConfiguration.Current.ContentRootPath = env.ContentRootPath;

            app.UseHealthChecks("/Health"); //basic healthy check

            // global error handler

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiSimple"));
            }


            // configure HTTP request pipeline
            {
                // global cors policy
                app.UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

                // global error handler
                app.UseMiddleware<ErrorHandlerMiddleware>();

               // app.map();
            }


            app.UseMvc();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
              
            });
        }
    }
}
