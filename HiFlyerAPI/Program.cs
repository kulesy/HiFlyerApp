using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddShopifyClient()
   .ConfigureHttpClient(client =>
   {
       client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("shopifyStoreUrl"));
       client.DefaultRequestHeaders.Add("X-Shopify-Storefront-Access-Token",
                                        builder.Configuration.GetValue<string>("storefrontConnection"));
   }
);
builder.Services.AddSwaggerGen(setup =>
{
    setup.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Portfolio API",
            Version = "v1"
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API v1");
    });
}
app.UseCors(policy =>
                policy.WithOrigins("http://localhost:5000", "https://localhost:5001")
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithHeaders(HeaderNames.ContentType));
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
