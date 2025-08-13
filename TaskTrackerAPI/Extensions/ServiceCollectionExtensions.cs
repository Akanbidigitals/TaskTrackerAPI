using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TaskTrackerAPI.DataAccess.DataContext;
using TaskTrackerAPI.DataAccess.Interface;
using TaskTrackerAPI.DataAccess.Repository;
using TaskTrackerAPI.Domain.Configurations;
using TaskTrackerAPI.Domain.Models;
using TaskTrackerAPI.Utility;

namespace TaskTrackerAPI.Extensions;

public static class ServiceCollectionExtention
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITokenService, TokenService>();
        return services;
        
    }
    
    public static IServiceCollection RegisterPersistenceService(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddValidatorsFromAssembly(typeof(ServiceCollectionExtention).Assembly)
            .AddFluentValidationAutoValidation();
         
        services.AddDbContext<ApplicationDbContext>(option => 
         option.UseSqlite(
             configuration.GetConnectionString("DefaultConnection")
             )
        );
        return services;
    }

    public static IServiceCollection RegisterAppConfigurations(this IServiceCollection services , IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        services.Configure<JwtSettings>(jwtSettings);
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateActor = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!))
                };
            }
            );
        return services;
    }

    public static IServiceCollection AddRoleBasedAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(PoliciesConstant.User , policy => policy.RequireRole(Role.User.ToString()));
            options.AddPolicy(PoliciesConstant.Manager , policy => policy.RequireRole(Role.Manager.ToString()));
            
        });
        return services;
    }
   

    public static IServiceCollection RegisterSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo{Title ="TaskTracker API", Version = "v1"} );
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new  OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });
        return services;
    }
    
    
}