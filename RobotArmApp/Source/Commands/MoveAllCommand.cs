using System;
using System.Linq;

using RobotArmApp.Source.RobotArm;

namespace RobotArmApp.Source.Commands
{
    public class MoveAllCommand : Command<object?>
    {
        private readonly char[] angles;

        public MoveAllCommand(char[] angles) :
            base(ICommand<object?>.Type.MoveAll)
        {
            if (angles.Length != IRobotArm.AxisCount)
            {
                throw new ArgumentException("Invalid array size", paramName: nameof(angles));
            }

            this.angles = angles;
        }

        public override byte[] GetBytes()
        {
            return base
                .GetBytes()
                .Concat(angles.Select(a => (byte)a))
                .ToArray();
        }

        public override string ToString()
        {
            return base.ToString() + " " + string.Join(' ', angles);
        }
    }
}
