using System.Linq;

namespace RobotArmApp.Source.Commands
{
    public class SetAllCommand : Command<object?>
    {
        private readonly Angles angles;

        public SetAllCommand(Angles angles) : 
            base(ICommand<object?>.Type.SetAll)
        {
            this.angles = angles;
        }

        public override byte[] GetBytes()
        {
            return base
                .GetBytes()
                .Concat(angles.Bytes)
                .ToArray();
        }

        public override string ToString()
        {
            return base.ToString() + " " + string.Join(' ', angles.Bytes);
        }
    }
}
