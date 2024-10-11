using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

var allowAngularOrigins = "angularOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAngularOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:4200");

    });
});
var app = builder.Build();
app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.MapPost("/api/checkout", (IOptions<StripeSettings> stripeSettings, IHttpContextAccessor contextAccessor, [FromBody] CheckoutRequest request) =>
{
    StripeConfiguration.ApiKey = stripeSettings.Value.SecretKey;

    var options = new PaymentIntentCreateOptions { Amount = 1099, Currency = "usd" };
    var service = new PaymentIntentService();
    var session = service.Create(options);


    return Results.Ok(new { clientSecret = session.ClientSecret, publishableKey = stripeSettings.Value.PublishableKey });

})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/api/hello", () => Results.Ok("Hello"));

app.Run("http://localhost:5000");

public class StripeSettings
{
    public string PublishableKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}

public class CheckoutRequest
{
    public decimal Price { get; set; }
}
