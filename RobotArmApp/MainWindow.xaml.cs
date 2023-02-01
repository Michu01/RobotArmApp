using System.Windows;

using Microsoft.Extensions.DependencyInjection;

using RobotArmApp.Source;
using RobotArmApp.Source.NavigationService;

namespace RobotArmApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            INavigationService navigationService = ((App)Application.Current).ServiceProvider.GetRequiredService<INavigationService>();
            navigationService.Navigation = mainFrame.NavigationService;
            
        }
    }
}
