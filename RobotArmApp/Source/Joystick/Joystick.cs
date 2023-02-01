using System;

using RobotArmApp.Source.Joystick;

namespace RobotArmApp.Source.Joystick
{
    public class Joystick : IJoystick
	{
		private readonly uint index;

		public IAxisConfig[] AxisConfigs { get; set; } = new AxisConfig[]
		{
			AxisConfig.CreateKnobAxisConfig(),
			AxisConfig.CreateKnobAxisConfig(),
			AxisConfig.CreateKnobAxisConfig(),
			AxisConfig.CreateKnobAxisConfig(),
			AxisConfig.CreateLR2AxisConfig(),
			AxisConfig.CreateLR2AxisConfig(),
			AxisConfig.CreateDPadAxisConfig(),
			AxisConfig.CreateDPadAxisConfig()
		};

		public Joystick(uint index = 0)
		{
			this.index = index;
		}

		public static void Update()
		{
			SFML.Window.Joystick.Update();
		}

		public float GetRawAxisPosition(IJoystick.Axis axis)
		{
			return SFML.Window.Joystick.GetAxisPosition(index, (SFML.Window.Joystick.Axis)axis);
		}

		public float[] GetRawAxisPositions()
		{
			float[] axes = new float[IJoystick.AxisCount];

			for (uint n = 0; n < IJoystick.AxisCount; ++n)
			{
				axes[n] = GetRawAxisPosition((IJoystick.Axis)n);
			}

			return axes;
		}

		public float GetAxisPosition(IJoystick.Axis axis)
		{
			float raw = GetRawAxisPosition(axis);

            IAxisConfig axisConfig = AxisConfigs[(uint)axis];

			FloatRange range = new(axisConfig.Range);

			if (axisConfig.DeadZone.HasValue)
			{
				if (Math.Abs(raw - axisConfig.Neutral) < axisConfig.DeadZone.Value)
				{
					return axisConfig.Neutral;
				}

				raw += raw < 0.0f ? axisConfig.DeadZone.Value : -axisConfig.DeadZone.Value;
				range.Maximum -= axisConfig.DeadZone.Value;
				range.Minimum += axisConfig.DeadZone.Value;
			}

			return range.MapValue(raw, axisConfig.TargetRange);
		}

		public float[] GetAxisPositions()
		{
			float[] axes = new float[IJoystick.AxisCount];

			for (uint n = 0; n < IJoystick.AxisCount; ++n)
			{
				axes[n] = GetAxisPosition((IJoystick.Axis)n);
			}

			return axes;
		}

		public bool GetIsButtonPressed(IJoystick.Button button)
		{
			return GetIsButtonPressed((uint)button);
		}

		public bool[] GetButtons()
		{
			bool[] buttons = new bool[IJoystick.ButtonCount];

			for (uint n = 0; n < IJoystick.ButtonCount; ++n)
			{
				buttons[n] = GetIsButtonPressed(n);
			}

			return buttons;
		}

		private bool GetIsButtonPressed(uint button)
        {
			return SFML.Window.Joystick.IsButtonPressed(index, button);
		}
	}
}
