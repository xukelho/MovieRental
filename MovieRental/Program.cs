using MovieRental.Data;
using MovieRental.Rental;
using MovieRental.PaymentProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<MovieRentalDbContext>();

// Use scoped so RentalFeatures can consume the scoped DbContext
builder.Services.AddScoped<IRentalFeatures, RentalFeatures>();

// Register payment providers — DI will inject all IPaymentProvider implementations
builder.Services.AddTransient<IPaymentProvider, PayPalProvider>();
builder.Services.AddTransient<IPaymentProvider, MbWayProvider>();

// Register factory as scoped so it can safely depend on transient providers and be injected into scoped services.
builder.Services.AddScoped<IPaymentProviderFactory, PaymentProviderFactory>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Create a scope and get the DbContext from DI to ensure migrations/DB creation run correctly.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MovieRentalDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
