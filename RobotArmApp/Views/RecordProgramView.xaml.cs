using System.Windows.Controls;

using RobotArmApp.ViewModels;

namespace RobotArmApp.Views
{
    public partial class RecordProgramView : Page
    {
        public RecordProgramView(RecordProgramViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
