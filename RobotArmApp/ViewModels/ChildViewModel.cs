using Microsoft.Toolkit.Mvvm.Input;

namespace RobotArmApp.ViewModels
{
    public class ChildViewModel : ViewModel
    {
        public RelayCommand BackCommand { get; }

        public object Content { get; }

        public ChildViewModel(RelayCommand backCommand, object content)
        {
            BackCommand = backCommand;
            Content = content;
        }
    }
}
