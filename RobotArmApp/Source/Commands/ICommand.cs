using System.IO.Ports;
using System.Threading.Tasks;

namespace RobotArmApp.Source.Commands
{
    public interface ICommand<T>
    {
        enum Type : byte
        {
            Delay, Set, Move, SetAll, MoveAll, GetAll
        }

        byte[] GetBytes();

        T? Send(SerialPort port);

        Task<T?> SendAsync(SerialPort port);
    }
}
