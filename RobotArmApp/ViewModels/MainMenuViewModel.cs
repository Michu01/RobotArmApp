using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;

using RobotArmApp.Source.NavigationService;

namespace RobotArmApp.ViewModels
{
    public class MainMenuViewModel : ViewModel
    {
        private readonly INavigationService navigationService = ((App)Application.Current).ServiceProvider.GetRequiredService<INavigationService>();

        public RelayCommand ControllerSteeringViewNavigateCommand { get; }
        public RelayCommand ProgramListViewNavigateCommand { get; }
        public RelayCommand RecordProgramViewNavigateCommand { get; }

        public MainMenuViewModel()
        {
            ControllerSteeringViewNavigateCommand = new(() => navigationService.ControllerSteeringViewNavigate());
            ProgramListViewNavigateCommand = new(() => navigationService.ProgramListViewNavigate());
            RecordProgramViewNavigateCommand = new(() => navigationService.RecordProgramViewNavigate());
        }
    }
}
