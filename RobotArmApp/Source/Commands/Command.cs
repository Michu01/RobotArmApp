using System.IO.Ports;
using System.Threading.Tasks;

namespace RobotArmApp.Source.Commands
{
    public class Command<T> : ICommand<T>
    {
        private readonly ICommand<T>.Type type;

        public Command(ICommand<T>.Type type)
        {
            this.type = type;
        }

        public virtual byte[] GetBytes()
        {
            return new byte[] { (byte)type };
        }

        public virtual T? Send(SerialPort port)
        {
            byte[] bytes = GetBytes();

            port.Write(bytes, 0, bytes.Length);

            return default;
        }

        public virtual Task<T?> SendAsync(SerialPort port)
        {
            return Task.Run(() => Send(port));
        }

        public override string ToString()
        {
            return type.ToString();
        }
    }
}
