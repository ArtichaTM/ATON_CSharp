using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddControllers();
builder.Services.AddDbContext<AtonApi.Models.UserContext>(opt => opt.UseInMemoryDatabase("MemoryDB"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.IncludeXmlComments(Path.Combine(
        AppContext.BaseDirectory
        , "AtonApi.xml"
    ));
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Aton API",
        Description = "Aton Web API",
        // TermsOfService = new Uri("https://example.com/terms"),
        // Contact = new OpenApiContact
        // {
        //     Name = "Example Contact",
        //     Url = new Uri("https://example.com/contact")
        // },
        // License = new OpenApiLicense
        // {
        //     Name = "Example License",
        //     Url = new Uri("https://example.com/license")
        // }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Logger.LogInformation("Use token " + AtonApi.Controllers.UserController.token + " to access the site");
app.Run();
