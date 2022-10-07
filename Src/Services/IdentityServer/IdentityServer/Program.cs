using IdentityServer.Extensions;
using IdentityServer.GrpcServices;
using IdentityServer.Security;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using User.Grpc.Protos;

var builder = WebApplication.CreateBuilder(args);

//Inject the classes we just created
builder.Services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddCustomizeddentityServer();

// Grpc Configuration
builder.Services.AddGrpcClient<AuthenticationProtoService.AuthenticationProtoServiceClient>
    (o => o.Address = new Uri(builder.Configuration["GrpcSettings:UserUrl"]));
builder.Services.AddScoped<AuthenticationGrpcService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();
app.UseCustomizedIdentityServer();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/.well-known/openid-configuration")
    {
        context.Request.Path = "/.well-known/openid-configuration.json";
    }

    await next();
});
;
app.MapGet("/", () => "hello world!");

app.Run();
