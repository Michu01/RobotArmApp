namespace RobotArmApp.Source.Joystick
{
    public interface IJoystick
    {
        enum Axis : byte
		{
			LeftHorizontal, LeftVertical, RightHorizontal, RightVertical, L2, R2, DPadHorizontal, DPadVertical, Count
		};

		enum Button : byte
		{
			Square, Cross, Circle, Triangle, L1, R1, L2, R2, Share, Options, L3, R3, PlayStation, Touchpad, Count
		};

		const int AxisCount = (int)Axis.Count;

		const int ButtonCount = (int)Button.Count;

		float[] GetAxisPositions();

		float GetAxisPosition(Axis axis);

		float GetRawAxisPosition(Axis axis);

		float[] GetRawAxisPositions();

		bool GetIsButtonPressed(Button button);

		bool[] GetButtons();

		IAxisConfig[] AxisConfigs { get; set; }
	}
}
