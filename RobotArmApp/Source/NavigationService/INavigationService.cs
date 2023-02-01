namespace RobotArmApp.Source.NavigationService
{
    public interface INavigationService
    {
        public System.Windows.Navigation.NavigationService? Navigation { get; set; }

        void ControllerSteeringViewNavigate();

        void CreateProgramViewNavigate(string programBody = "");

        void CreateProgramViewNavigate(Program program);

        void MainMenuViewNavigate();

        void ProgramListViewNavigate();

        void RecordProgramViewNavigate();
    }
}
