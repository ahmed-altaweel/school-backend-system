using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure.Context;

namespace SchoolProject.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddServiceRegistrationDenpndency(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;


            }).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

            var jwtSettings = new JwtSettings();
            configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
            service.AddSingleton(jwtSettings);
            service.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
          .AddJwtBearer(x =>
          {
              x.RequireHttpsMetadata = false;
              x.SaveToken = true;

              x.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidAudience = jwtSettings.Audience,
                  ValidIssuer = jwtSettings.Issuer,
                  ValidateAudience = jwtSettings.ValidateAudience,
                  ValidateIssuer = jwtSettings.ValidateIssuer,
                  ValidateLifetime = jwtSettings.ValidateLifetime,
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                  ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSingingKey
                  //  ClockSkew = TimeSpan.Zero



              };

          });

            service.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                x.EnableAnnotations();

                x.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {

                    Description = "JWT Authorization header using the Bearer Scheme (Example:'Bearer 12345hjsdf')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id=JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    Array.Empty<string>()
                }
                });
            });

            service.AddAuthorization(opt =>
            {
                opt.AddPolicy("CreateStudent", policy =>
                {
                    policy.RequireClaim("Create Sutdent", "True");
                });
            });



        }
    }
}
