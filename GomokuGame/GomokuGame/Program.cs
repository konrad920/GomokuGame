using GomokuGame.AplicationServices;
using GomokuGame.UI;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IGomokuEngine, GomokuEngine>();

var serviceBuilder = services.BuildServiceProvider();
var app = serviceBuilder.GetService<IApp>();
app.Run();