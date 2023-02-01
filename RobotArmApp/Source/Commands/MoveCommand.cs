using System;
using System.Linq;

using RobotArmApp.Source.RobotArm;

namespace RobotArmApp.Source.Commands
{
    public class MoveCommand : Command<object?>
    {
        private readonly IRobotArm.Axis axis;

        private readonly char angle;

        public MoveCommand(IRobotArm.Axis axis, char angle) : 
            base(ICommand<object?>.Type.Move)
        {
            this.axis = axis;
            this.angle = angle;
        }

        public override byte[] GetBytes()
        {
            return base
                .GetBytes()
                .Append((byte)axis)
                .Append((byte)angle)
                .ToArray();
        }

        public override string ToString()
        {
            return base.ToString() + " " + axis + " " + angle;
        }
    }
}
