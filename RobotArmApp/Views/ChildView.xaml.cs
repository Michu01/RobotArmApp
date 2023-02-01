using System.Windows.Controls;

using RobotArmApp.ViewModels;

namespace RobotArmApp.Views
{
    public partial class ChildView : Page
    {
        public ChildView(ChildViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
