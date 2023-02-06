var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/Home", () => "Home!!!!");
app.MapGet("/Ivan", () => "Hello Ivan!!!");
app.MapGet("/GetData", () => "Something...");
app.Run();
