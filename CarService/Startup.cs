using AutoMapper;
using CarService.Entities;
using CarService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Middleware;
using Microsoft.AspNetCore.Identity;
using CarService.Models;
using CarService.Models.Validators;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarService.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace CarService
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
            var authenticationSettings = new AuthenticationSettings();

            Configuration.GetSection("Authentication").Bind(authenticationSettings);
            services.AddSingleton(authenticationSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasPhoneNumber", builder => builder.RequireClaim("PhoneNumber"));
                options.AddPolicy("Atleast20", builder => builder.AddRequirements(new MinimumAgeRequirements(20)));
                options.AddPolicy("CreatedAtLeast2CarMarkets", builder => builder.AddRequirements(new MinimumCarMarketsCreatedRequirements(2)));
            });
            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementsHandler>();
            services.AddScoped<IAuthorizationHandler, MinimumCarMarketsCreatedRequirementsHandler>();
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
            services.AddRazorPages();
            services.AddControllers().AddFluentValidation();
            services.AddDbContext<CarServiceDbContext>();
            services.AddScoped<CarSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<Services.ICarService, Services.CarService>();
            services.AddScoped<Services.ICarMarketService, Services.CarMarketService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICarPartService, CarPartService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<RequestTimeMiddleware>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CarSeeder seeder)
        {
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car Service API");
            });

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
