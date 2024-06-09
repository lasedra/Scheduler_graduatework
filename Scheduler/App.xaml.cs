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
            SchedulerDbContext.DbContext = new SchedulerDbContext()
            {
                AppConfig = new ConfigurationBuilder().AddJsonFile("appconfig.json", optional: false, reloadOnChange: true).Build(),
                IsAppUpdated = true
            };
        }
    }
}