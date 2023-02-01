using System.Collections.ObjectModel;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using RobotArmApp.Source;
using RobotArmApp.Source.NavigationService;
using RobotArmApp.Source.RobotArm;

namespace RobotArmApp.ViewModels
{
    public class ProgramListViewModel : ViewModel
    {
        public ObservableCollection<Program> Programs { get; } = new(Program.GetPrograms());

        private readonly IRobotArm robotArm = ((App)Application.Current).ServiceProvider.GetRequiredService<IRobotArm>();

        public RelayCommand<Program> RunProgramCommand { get; }

        public RelayCommand<Program> DeleteProgramCommand { get; }

        public RelayCommand<Program> EditProgramCommand { get; }

        public RelayCommand CreateProgramViewNavigateCommand { get; }

        public ProgramListViewModel()
        {
            RunProgramCommand = new(RunProgram);
            DeleteProgramCommand = new(DeleteProgram);
            EditProgramCommand = new(EditProgram);
            CreateProgramViewNavigateCommand = new(CreateProgramViewNavigate);
        }

        private void RunProgram(Program? program)
        {
            program?.ExecuteAsync(robotArm);
        }

        private void DeleteProgram(Program? program)
        {
            if (program != null)
            {
                program.Delete();
                Programs.Remove(program);
            }
        }

        private void EditProgram(Program? program)
        {
            if (program != null)
            {
                INavigationService navigationService = ((App)Application.Current).ServiceProvider.GetRequiredService<INavigationService>();
                navigationService.CreateProgramViewNavigate(program);
            }
        }

        private static void CreateProgramViewNavigate()
        {
            INavigationService navigationService = ((App)Application.Current).ServiceProvider.GetRequiredService<INavigationService>();
            navigationService.CreateProgramViewNavigate();
        }
    }
}
