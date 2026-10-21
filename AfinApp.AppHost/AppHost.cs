var builder = DistributedApplication.CreateBuilder(args);

/// parameters/secrets for services
var keycloakRealm = builder.AddParameter("keycloak-realm", "afinapp");
var keycloakURL = builder.AddParameter("keycloak-url", "http://localhost:8080");
var keycloakAPIClientId = builder.AddParameter("KeycloakClientIdAfinAppAPI", "apiservice");
var keycloakClientSecret = builder.AddParameter("KeycloakClientSecretAfinAppAPI", secret: true);
var keycloakWebId = builder.AddParameter("web-keycloak-id", "webfrontend");
var keycloakWebSecret = builder.AddParameter("web-keycloak-secret", secret: true);
var keycloakScope = builder.AddParameter("keycloak-scope", "email openid");
var keycloakAudience = builder.AddParameter("keycloak-audience", "account");
var corsAPI = builder.AddParameter("afinapp-api-cors");
var betterAuthSecret = builder.AddParameter(
    "better-auth-secret",
    new GenerateParameterDefault { MinLength = 32, Special = false },
    secret: true,
    persist: true
);

var betterAuthUrl = builder.AddParameter(
    "better-auth-url",
    "http://localhost:3000"
);

var database = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("AfinAppDB", "AfinAppDB");

var keycloak = builder.AddKeycloak("keycloak", 8080)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .WithExternalHttpEndpoints()
    .WithRealmImport("./KeycloakRealms");

var worker = builder.AddProject<Projects.AfinApp_Worker>("afinapp-worker")
    .WithReference(database)
    .WithReference(keycloak)
    .WaitFor(keycloak)
    .WaitFor(database);

var apiService = builder.AddProject<Projects.AfinApp_ApiService>("apiservice")
    .WithHttpHealthCheck("/health")
    .WithReference(database)
    .WaitFor(keycloak)
    .WaitFor(database)
    .WaitFor(worker)
    .WithEnvironment("KEYCLOAK__URL", keycloakURL)
    .WithEnvironment("KEYCLOAK__REALM", keycloakRealm)
    .WithEnvironment("KEYCLOAK__CLIENTID", keycloakAPIClientId)
    .WithEnvironment("KEYCLOAK__SECRET", keycloakClientSecret)
    .WithEnvironment("KEYCLOAK__SCOPE", keycloakScope)
    .WithEnvironment("KEYCLOAK__AUDIENCE", keycloakAudience)
    .WithEnvironment("WEB_FRONTEND_CORS", corsAPI);

var frontend = builder.AddNpmApp("webfrontend", "../AfinApp.frontend/", "dev")
    .WithNpmPackageInstallation()
    .WithHttpEndpoint(3000, env: "PORT") // Make sure Next.js uses the port assigned by Aspire
    .WithExternalHttpEndpoints()
    .WithEnvironment("BETTER_AUTH_SECRET", betterAuthSecret)
    .WithEnvironment("BETTER_AUTH_URL", betterAuthUrl)
    .WithEnvironment("NEXT_PUBLIC_BASE_URL", betterAuthUrl)
    .WithEnvironment("NEXTAUTH_URL", betterAuthUrl)
    .WithEnvironment("NEXTAUTH_SECRET", betterAuthSecret)
    .WithEnvironment("KEYCLOAK_REALM", keycloakRealm)
    .WithEnvironment("KEYCLOAK_URL", keycloakURL)
    .WithEnvironment("KEYCLOAK_CLIENT_ID", keycloakWebId)
    .WithEnvironment("KEYCLOAK_CLIENT_SECRET", keycloakWebSecret)
    .WithEnvironment("KEYCLOAK_SCOPE", keycloakScope)
    .WaitFor(apiService)
    .WithReference(apiService)
    .WaitFor(keycloak)
    .WithReference(keycloak)
    .WithReference(database)
    .WaitFor(database)
    .PublishAsDockerFile();

if (builder.ExecutionContext.IsRunMode)
{
    frontend.WithEnvironment("NEXTAUTH_URL_INTERNAL", frontend.GetEndpoint("http"));
}

builder.Build().Run();