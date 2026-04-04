using BuberDinner.Application;
using BuberDinner.InfraStructure;
using BuberDinner.Api.Filters;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddOpenApi();
builder.Services.AddControllers(Options =>
{
    Options.Filters.Add<ErrorHandlingFilterAttibute>();
});
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
}
