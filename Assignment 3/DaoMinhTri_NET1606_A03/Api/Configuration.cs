using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using BusinessLogic.Dto.Response;
using DataAccess.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

namespace Api
{
    public static class Configuration
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, string? jwtKey, string issuer, string audience)
        {
            services.AddControllers();
            
            var modelBuilder = new ODataConventionModelBuilder();
            modelBuilder.EntitySet<Supplier>("Suppliers");
            modelBuilder.EntitySet<Manufacturer>("Manufacturers");
             modelBuilder.EntitySet<RentingTransaction>("RentingTransactions")
                 .EntityType.HasKey(rt => rt.RentingTransationId);
            modelBuilder.EntitySet<CustomerDto>("Customers")
                .EntityType.HasKey(c => c.CustomerId);
            modelBuilder.EntitySet<RentingDetail>("RentingDetails")
                .EntityType.HasKey(rd => new {rd.RentingTransactionId, rd.CarId});

            services.AddControllers().AddOData(
                options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100).AddRouteComponents(
                    "odata",
                    modelBuilder.GetEdmModel()));
            
            services.AddCors(options =>
                {
                    options.AddPolicy(name: "_publicPolicy",
                        //Define cors URL 
                        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                    );
                }
            );
            services.AddSwaggerGen();

            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? "")),
                        ClockSkew = TimeSpan.Zero
                    };
                }
                    );

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "FU renting system API",
                        Version = "v1",
                        Description = "ASP NET core API for FU Renting system project."
                    });
                //string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //option.IncludeXmlComments(xmlPath);
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
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

            services.AddEndpointsApiExplorer();
            services.AddHttpContextAccessor();

            return services;
        }

    }
}
