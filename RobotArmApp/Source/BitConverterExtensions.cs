using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArmApp.Source
{
    public static class BitConverterExtensions
    {
        public static byte[] GetBytes(short[] arr)
        {
            return arr.SelectMany(BitConverter.GetBytes).ToArray();
        }
    }
}
