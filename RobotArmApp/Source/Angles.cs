using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RobotArmApp.Source.RobotArm;

namespace RobotArmApp.Source
{
    public class Angles
    {
        private readonly byte[] bytes = new byte[IRobotArm.AxisCount];

        public byte[] Bytes => bytes;

        public const int Size = IRobotArm.AxisCount;

        public byte Base
        {
            get => bytes[(int)IRobotArm.Axis.Base];
            set => bytes[(int)IRobotArm.Axis.Base] = value;
        }

        public byte Shoulder
        {
            get => bytes[(int)IRobotArm.Axis.Shoulder];
            set => bytes[(int)IRobotArm.Axis.Shoulder] = value;
        }

        public byte Elbow
        {
            get => bytes[(int)IRobotArm.Axis.Elbow];
            set => bytes[(int)IRobotArm.Axis.Elbow] = value;
        }

        public byte WristVertical
        {
            get => bytes[(int)IRobotArm.Axis.WristVertical];
            set => bytes[(int)IRobotArm.Axis.WristVertical] = value;
        }

        public byte WristRotation
        {
            get => bytes[(int)IRobotArm.Axis.WristRotation];
            set => bytes[(int)IRobotArm.Axis.WristRotation] = value;
        }

        public byte Gripper
        {
            get => bytes[(int)IRobotArm.Axis.Gripper];
            set => bytes[(int)IRobotArm.Axis.Gripper] = value;
        }

        public Angles()
        {

        }

        public Angles(byte[] bytes)
        {
            if (bytes.Length != IRobotArm.AxisCount)
            {
                throw new ArgumentException("Invalid array length", paramName: nameof(bytes));
            }

            this.bytes = bytes;
        }
    }
}
