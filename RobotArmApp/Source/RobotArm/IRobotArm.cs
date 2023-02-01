using System.IO.Ports;
using System.Threading.Tasks;

using RobotArmApp.Source.Commands;

namespace RobotArmApp.Source.RobotArm
{
    public interface IRobotArm
    {
        enum Axis : byte
        {
            Base, Shoulder, Elbow, WristVertical, WristRotation, Gripper, Count
        }

        const int AxisCount = (int)Axis.Count;

        T? SendCommand<T>(ICommand<T?> command);

        Task<T?> SendCommandAsync<T>(ICommand<T?> command);

        ICommand<object?> CreateMoveCommand(Axis axis, float value);

        ICommand<object?> CreateMoveAllCommand(float[] values);

        void SendBytes(byte[] bytes);
    }
}
