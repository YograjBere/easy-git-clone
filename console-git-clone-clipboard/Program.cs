using console_git_clone_clipboard;
using console_git_clone_clipboard.configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

internal class Program
{

    [STAThread]
    private static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (IServiceScope scope = host.Services.CreateScope())
        {
            GitCloneApp p = scope.ServiceProvider.GetRequiredService<GitCloneApp>();
            p.Process();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((hostBuilderContext, configureDelegate) =>
           {
               configureDelegate.SetBasePath(Directory.GetCurrentDirectory());
               configureDelegate.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
               configureDelegate.AddJsonFile("foldermapping.json", optional: false, reloadOnChange: true);
           })
           .ConfigureServices((hostContext, services) =>
           {
               services.AddOptions();
               services.Configure<GitCloneAppSettings>(options => 
                    hostContext.Configuration.GetSection("gitCloneAppSettings").Bind(options));
               services.AddScoped<GitCloneApp>();
               services.AddTransient<IGitWrapper, GitWrapper>();
               services.AddSingleton<IValidateOptions<GitCloneAppSettings>, GitCloneAppSettingsValidator>();
           }).ConfigureLogging((hostBuilderContext, configureLogging) =>
           {
               configureLogging.ClearProviders();
               //    var logger = new LoggerConfiguration()
               //                 .Enrich.FromLogContext()
               //                 .WriteTo.Console()
               //                 .WriteTo.File("logs.txt", Serilog.Events.LogEventLevel.Information)
               //                 .CreateLogger();                
               //    configureLogging.AddSerilog(logger, dispose: true);
           }).UseSerilog((HostBuilderContext hostingContext,
                    IServiceProvider services,
                    LoggerConfiguration loggerConfiguration) =>
                        loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));
}