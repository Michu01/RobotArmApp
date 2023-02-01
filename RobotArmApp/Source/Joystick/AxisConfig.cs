using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArmApp.Source.Joystick
{
	public class AxisConfig : IAxisConfig
	{
		public float? DeadZone { get; set; } = 10.0f;

		public float Neutral { get; set; } = 0.0f;

		public FloatRange Range { get; set; } = new(-100.0f, 100.0f);

		public FloatRange TargetRange { get; set; } = new(-1.0f, 1.0f);

		public AxisConfig() { }

		public static AxisConfig CreateKnobAxisConfig()
		{
			return new AxisConfig();
		}

		public static AxisConfig CreateLR2AxisConfig()
		{
			return new AxisConfig()
			{
				DeadZone = null,
				Neutral = -100.0f,
				Range = new(-100.0f, 100.0f),
				TargetRange = new(0.0f, 1.0f)
			};
		}

		public static AxisConfig CreateDPadAxisConfig()
		{
			return new AxisConfig();
		}
	};
}
