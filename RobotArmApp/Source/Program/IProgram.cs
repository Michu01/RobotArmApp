using System.Threading.Tasks;

using RobotArmApp.Source.RobotArm;

namespace RobotArmApp.Source
{
    public interface IProgram
    {
        string Name { get; }

        string BodyToString();

        void Execute(IRobotArm robotArm);

        Task ExecuteAsync(IRobotArm robotArm);

        void SaveToFile();
    }
}