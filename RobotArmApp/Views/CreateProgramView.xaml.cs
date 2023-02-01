using System.Windows.Controls;

using RobotArmApp.ViewModels;

namespace RobotArmApp.Views
{
    public partial class CreateProgramView : Page
    {
        public CreateProgramView(CreateProgramViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
