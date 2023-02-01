using System.Linq;

using RobotArmApp.Source.RobotArm;

namespace RobotArmApp.Source.Commands
{
    public class SetCommand : Command<object?>
    {
        public IRobotArm.Axis Axis { get; set; }

        public byte Angle { get; set; }

        public SetCommand(IRobotArm.Axis axis, byte angle) : 
            base(ICommand<object?>.Type.Set)
        {
            Axis = axis;
            Angle = angle;
        }

        public override byte[] GetBytes()
        {
            return base.GetBytes().Append((byte)Axis).Append(Angle).ToArray();
        }

        public override string ToString()
        {
            return base.ToString() + " " + Axis + " " + Angle;
        }
    }
}
