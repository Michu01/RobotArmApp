using System;
using System.Configuration;
using System.Diagnostics;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;

using RobotArmApp.Source.Joystick;
using RobotArmApp.Source.NavigationService;
using RobotArmApp.Source.RobotArm;

namespace RobotArmApp
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; }

        private static IServiceProvider CreateServiceProvider()
        {
            ServiceCollection serviceDescriptors = new();

            if (ConfigurationManager.AppSettings["arduinoPort"] is not string port)
            {
                Debug.WriteLine("No arduino port specified!");
                Current.Shutdown();
            }
            else if (!int.TryParse(ConfigurationManager.AppSettings["baudRate"], out int baudRate))
            {
                Debug.WriteLine("No baud rate specified!");
                Current.Shutdown();
            }
            else
            {
                serviceDescriptors.AddSingleton<IRobotArm, RobotArm>(serviceProvider => new RobotArm(port, baudRate));
            }

            serviceDescriptors.AddSingleton<IJoystick, Joystick>();
            serviceDescriptors.AddSingleton<INavigationService, NavigationService>();

            return serviceDescriptors.BuildServiceProvider();
        }

        public App()
        {
            InitializeComponent();

            ServiceProvider = CreateServiceProvider();

            try
            {
                ServiceProvider.GetRequiredService<IRobotArm>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Current.Shutdown();
            }
        }
    }
}
