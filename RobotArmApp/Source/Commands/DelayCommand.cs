using System;
using System.Linq;

namespace RobotArmApp.Source.Commands
{
    public class DelayCommand : Command<object?>
    {
        private readonly ushort milliseconds;

        public DelayCommand(TimeSpan delay) : 
            base(ICommand<object?>.Type.Delay)
        {
            milliseconds = (ushort)delay.TotalMilliseconds;
        }

        public override byte[] GetBytes()
        {
            return base
                .GetBytes()
                .Concat(BitConverter.GetBytes(milliseconds))
                .ToArray();
        }

        public override string ToString()
        {
            return base.ToString() + " " + milliseconds;
        }
    }
}
