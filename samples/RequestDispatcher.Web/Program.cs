using RequestDispatcher.Core;
using RequestDispatcher.Core.Abstractions;
using RequestDispatcher.Web;
using RequestDispatcher.Web.Handlers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDispatcher();
builder.Services.AddRequestDispatcherWeb();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/test", async (IRequestDispatcher dispatcher, IServiceScopeFactory scopeFactory) => 
{
    //await using var scope = scopeFactory.CreateAsyncScope();
    //var dispatcher2 = scope.ServiceProvider.GetRequiredService<IRequestDispatcher>();
    var result = await dispatcher.Send(new RequestOne("Test"));
    return Results.Ok();
});

app.Run();


