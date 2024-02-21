using System.ComponentModel.DataAnnotations;
using Gray.Server.Services;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddSingleton<GrayQAvatarService>()
    .AddHttpClient<GrayQAvatarService>();

builder.Logging.AddConsole();

var app = builder.Build();

app.MapGet("/", (GrayQAvatarService service, string id, [Range(0, 100)] int? quality) => service.Generate(id, quality));

app.Run($"http://*:{app.Configuration.GetValue<int>("PORT")}/");
