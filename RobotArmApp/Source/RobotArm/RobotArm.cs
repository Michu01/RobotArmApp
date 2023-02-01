using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading.Tasks;

using RobotArmApp.Source.Commands;

namespace RobotArmApp.Source.RobotArm
{
    public class RobotArm : IRobotArm, IDisposable
    {
        private readonly SerialPort port;

        public float[] MoveVelocities { get; } = new float[]
        {
            3.0f, 3.0f, 3.0f, 3.0f, 3.0f, 3.0f
        };

        public RobotArm(string portName, int baudRate)
        {
            port = new(portName, baudRate);

            try
            {
                port.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public T? SendCommand<T>(ICommand<T?> command)
        {
            return port.IsOpen ? command.Send(port) : default;
        }

        public async Task<T?> SendCommandAsync<T>(ICommand<T?> command)
        {
            return port.IsOpen ? await command.SendAsync(port) : default;
        }

        public ICommand<object?> CreateMoveCommand(IRobotArm.Axis axis, float value)
        {
            char angle = (char)(value * MoveVelocities[(int)axis]);

            return new MoveCommand(axis, angle);
        }

        public ICommand<object?> CreateMoveAllCommand(float[] values)
        {
            if (values.Length != IRobotArm.AxisCount)
            {
                throw new ArgumentException("Invalid array size", paramName: nameof(values));
            }

            char[] angles = new char[IRobotArm.AxisCount];

            for (int n = 0; n < IRobotArm.AxisCount; ++n)
            {
                angles[n] = (char)(MoveVelocities[n] * values[n]);
            }

            return new MoveAllCommand(angles);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            port.Dispose();
        }

        public void SendBytes(byte[] bytes)
        {
            port.Write(bytes, 0, bytes.Length);
        }
    }
}