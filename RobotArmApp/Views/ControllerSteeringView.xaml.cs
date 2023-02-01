using System.Windows.Controls;

using RobotArmApp.ViewModels;

namespace RobotArmApp.Views
{
    public partial class ControllerSteeringView : Page
    {
        public ControllerSteeringView(ControllerSteeringViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
