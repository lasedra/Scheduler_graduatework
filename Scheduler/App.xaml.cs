using Microsoft.Extensions.Configuration;
using Scheduler.Models;
using System.Windows;

namespace Scheduler
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appconfig.json", optional: false, reloadOnChange: true);

            SchedulerDbContext.dbContext = new SchedulerDbContext(builder.Build());
        }
    }
}
