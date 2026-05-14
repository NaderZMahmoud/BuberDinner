using BuberDinner.Api;
using BuberDinner.Application;
using BuberDinner.InfraStructure;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddOpenApi();

}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseExceptionHandler("/error");
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
}
