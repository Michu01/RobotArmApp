using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using Microsoft.Extensions.DependencyInjection;

using RobotArmApp.Source.NavigationService;
using RobotArmApp.ViewModels;

namespace RobotArmApp.Views
{
    public partial class MainMenuView : Page
    {
        

        public MainMenuView()
        {
            InitializeComponent();

            DataContext = new MainMenuViewModel();
        }
    }
}
