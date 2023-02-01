using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

using RobotArmApp.Source;

namespace RobotArmApp.ViewModels
{
    public class CreateProgramViewModel : ViewModel
    {
        private string programBody;

        public string ProgramBody 
        { 
            get => programBody; 
            set => SetProperty(ref programBody, value); 
        }

        private string programName;

        public string ProgramName
        {
            get => programName;
            set => SetProperty(ref programName, value);
        }

        public RelayCommand SaveProgramCommand { get; }

        public CreateProgramViewModel(string programBody = "", string programName = "")
        {
            this.programBody = programBody;
            this.programName = programName;
            SaveProgramCommand = new(SaveProgram);
        }

        private void SaveProgram()
        {
            Program.CreateFromText(ProgramName, ProgramBody).SaveToFile();
        }
    }
}
