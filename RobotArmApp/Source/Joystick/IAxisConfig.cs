using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArmApp.Source.Joystick
{
    public interface IAxisConfig
	{
		float? DeadZone { get; set; }

		float Neutral { get; set; }

		FloatRange Range { get; set; }

		FloatRange TargetRange { get; set; }
	}
}
