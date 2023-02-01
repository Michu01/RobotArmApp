using Microsoft.Toolkit.Mvvm.Input;

using RobotArmApp.ViewModels;
using RobotArmApp.Views;

namespace RobotArmApp.Source.NavigationService
{
    public class NavigationService : INavigationService
    {
        public System.Windows.Navigation.NavigationService? Navigation { get; set; }

        private ViewModel? viewModel;

        public void ControllerSteeringViewNavigate()
        {
            DisposeViewModel();
            ControllerSteeringViewModel viewModel = new();
            ControllerSteeringView view = new(viewModel);
            ChildViewModel childViewModel = new(new RelayCommand(MainMenuViewNavigate), view);
            ChildView childView = new(childViewModel);
            this.viewModel = viewModel;
            Navigation?.Navigate(childView);
        }

        public void CreateProgramViewNavigate(string programBody = "")
        {
            DisposeViewModel();
            CreateProgramViewModel viewModel = new(programBody);
            CreateProgramView view = new(viewModel);
            ChildViewModel childViewModel = new(new RelayCommand(ProgramListViewNavigate), view);
            ChildView childView = new(childViewModel);
            this.viewModel = viewModel;
            Navigation?.Navigate(childView);
        }

        public void CreateProgramViewNavigate(Program program)
        {
            DisposeViewModel();
            CreateProgramViewModel viewModel = new(program.BodyToString(), program.Name);
            CreateProgramView view = new(viewModel);
            ChildViewModel childViewModel = new(new RelayCommand(ProgramListViewNavigate), view);
            ChildView childView = new(childViewModel);
            this.viewModel = viewModel;
            Navigation?.Navigate(childView);
        }

        public void MainMenuViewNavigate()
        {
            DisposeViewModel();
            MainMenuView view = new();
            Navigation?.Navigate(view);
        }

        public void ProgramListViewNavigate()
        {
            DisposeViewModel();
            ProgramListView view = new();
            ChildViewModel childViewModel = new(new RelayCommand(MainMenuViewNavigate), view);
            ChildView childView = new(childViewModel);
            Navigation?.Navigate(childView);
        }

        public void RecordProgramViewNavigate()
        {
            DisposeViewModel();
            RecordProgramViewModel viewModel = new();
            RecordProgramView view = new(viewModel);
            ChildViewModel childViewModel = new(new RelayCommand(MainMenuViewNavigate), view);
            ChildView childView = new(childViewModel);
            this.viewModel = viewModel;
            Navigation?.Navigate(childView);
        }

        private void DisposeViewModel()
        {
            viewModel?.Dispose();
            viewModel = null;
        }
    }
}
