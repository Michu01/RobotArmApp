using System;
using System.IO.Ports;
using System.Threading;

namespace RobotArmApp.Source.Commands
{
    public class GetAllCommand : Command<Angles?>
    {
        public GetAllCommand() : 
            base(ICommand<Angles?>.Type.GetAll) 
        {

        }

        public override Angles? Send(SerialPort port)
        {
            base.Send(port);

            while (port.BytesToRead < Angles.Size)
            {
                Thread.Sleep(1);
            }

            if (port.BytesToRead > Angles.Size)
            {
                throw new Exception();
            }

            Angles angles = new();

            port.Read(angles.Bytes, 0, Angles.Size);

            return angles;
        }
    }
}
