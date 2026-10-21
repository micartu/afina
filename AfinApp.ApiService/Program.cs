using System.IdentityModel.Tokens.Jwt;
using AfinApp.Data.Context;
using AfinApp.Data.Shared;
using Carter;
using FluentValidation;
using Scalar.AspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        var assembly = typeof(Program).Assembly;
        var builder = WebApplication.CreateBuilder(args);
        var corsPolicy = "AllowFrontend";

        var configuration = builder.Configuration;
        // Add services to the container.
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        // Add services to the container.
        builder.Services.AddProblemDetails();

        // Add CORS configuration
        builder.Services
            .AddCors(options =>
                {
                    options.AddPolicy(corsPolicy, policy =>
                            {
                                var cors = Environment.GetEnvironmentVariable("WEB_FRONTEND_CORS");
                                if (cors != null)
                                {
                                    var urls = cors.Split(",");
                                    policy.WithOrigins(urls)
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                                }
                            });
                });

        builder.Services.AddCarter();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication()
            .AddKeycloakJwtBearer("keycloak", realm: "cartechma", options =>
                    {
                        var cfg = configuration.GetSection("Keycloak");
                        var keycloak = cfg["Url"] ?? "";
                        var keycloakRealm = cfg["Realm"] ?? "";
                        var keycloakURL = keycloak + "/realms/" + keycloakRealm;
                        var clientSecret = cfg["Secret"];
                        cfg["BaseEndpoint"] = keycloakURL;
                        cfg["ClientSecret"] = clientSecret;
                        // For development only - disable HTTPS metadata validation
                        // In production, use explicit Authority configuration instead
                        if (builder.Environment.IsDevelopment())
                        {
                            options.RequireHttpsMetadata = false;
                        }
                        options.Authority = keycloakURL;
                        options.Audience = cfg["Audience"];
                        options.TokenValidationParameters.NameClaimType = JwtRegisteredClaimNames.Name;
                    });

        builder.Services.AddAuthorizationBuilder();

        builder.Services.AddValidatorsFromAssembly(assembly);

        builder.AddServiceDefaults();
        builder.AddNpgsqlDbContext<AfinAppContext>(Const.db);
        builder.Services.AddScoped<IUsersContext, AfinAppContext>();
        builder.Services.AddScoped<IFamilyRelevantContext, AfinAppContext>();
        builder.Services.AddScoped<IFamilyContext, AfinAppContext>();
        builder.Services.AddScoped<IAccountContext, AfinAppContext>();
        builder.Services.AddScoped<IAccountRelevantContext, AfinAppContext>();
        builder.Services.AddScoped<ITransactionContext, AfinAppContext>();
        builder.Services.AddScoped<ITransactionRelevantContext, AfinAppContext>();
        builder.Services.AddScoped<ICategoryContext, AfinAppContext>();
        builder.Services.AddScoped<ICategoryRelevantContext, AfinAppContext>();
        builder.Services.AddScoped<IAssetContext, AfinAppContext>();
        builder.Services.AddScoped<IAssetRelevantContext, AfinAppContext>();
        builder.Services.AddScoped<IHoldingContext, AfinAppContext>();
        builder.Services.AddScoped<IHoldingRelevantContext, AfinAppContext>();

        builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Transient);
        builder.Services.AddHttpContextAccessor();

        var app = builder.Build();

        // resolve problem with DateTime not being added to Postgresql
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }
        else // prod inv
        {
            app.UseHttpsRedirection();
        }

        // Add CORS middleware - should be before authentication/authorization
        app.UseCors(corsPolicy);

        app.MapControllers();
        app.MapCarter();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapDefaultEndpoints();

        app.Run();
    }
}