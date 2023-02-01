using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.ComponentModel;

using RobotArmApp.Source.Commands;
using RobotArmApp.Source.Joystick;
using RobotArmApp.Source.RobotArm;

namespace RobotArmApp.ViewModels
{
    public class ControllerSteeringViewModel : ViewModel
    {
        public class Button : ObservableObject
        {
            public Button(string name)
            {
                Name = name;
            }

            public string Name { get; }

            private bool isPressed;

            public bool IsPressed
            {
                get => isPressed;
                set => SetProperty(ref isPressed, value);
            }
        }

        public class Axis : ObservableObject
        {
            private readonly IAxisConfig axisConfig;

            public Axis(string name, IAxisConfig axisConfig)
            {
                Name = name;
                this.axisConfig = axisConfig;
            }

            public string Name { get; }

            public float Minimum => axisConfig.TargetRange.Minimum;

            public float Maximum => axisConfig.TargetRange.Maximum;

            private float position;

            public float Position
            {
                get => position;
                set => SetProperty(ref position, value);
            }
        }

        private readonly DispatcherTimer timer = new()
        {
            Interval = TimeSpan.FromMilliseconds(20)
        };

        private readonly IJoystick joystick = ((App)Application.Current).ServiceProvider.GetRequiredService<IJoystick>();

        private readonly IRobotArm robotArm = ((App)Application.Current).ServiceProvider.GetRequiredService<IRobotArm>();

        public Axis[] Axes { get; } = new Axis[IJoystick.AxisCount];

        public Button[] Buttons { get; } = new Button[IJoystick.ButtonCount];

        public ControllerSteeringViewModel()
        {
            for (int i = 0; i < IJoystick.AxisCount; i++)
            {
                string name = Enum.GetName(typeof(IJoystick.Axis), (IJoystick.Axis)i) ?? "Unknown";

                Axes[i] = new Axis(name, joystick.AxisConfigs[i]);
            }

            for (int i = 0; i < IJoystick.ButtonCount; i++)
            {
                string name = Enum.GetName(typeof(IJoystick.Button), (IJoystick.Button)i) ?? "Unknown";

                Buttons[i] = new Button(name);
            }

            timer.Tick += OnTick;
            timer.Start();

            UpdateJoystickState();
        }

        private void UpdateJoystickState()
        {
            Joystick.Update();

            float[] positions = joystick.GetAxisPositions();

            for (int i = 0; i < IJoystick.AxisCount; i++)
            {
                Axes[i].Position = positions[i];
            }

            bool[] buttons = joystick.GetButtons();

            for (int i = 0; i < IJoystick.ButtonCount; i++)
            {
                Buttons[i].IsPressed = buttons[i];
            }

            float[] values = new float[6];

            values[0] = positions[0];
            values[1] = positions[1];
            values[2] = positions[3];
            values[3] = positions[2];
            values[4] = positions[5] - positions[4];
            values[5] += buttons[(int)IJoystick.Button.L1] ? 1 : 0;
            values[5] -= buttons[(int)IJoystick.Button.R1] ? 1 : 0;

            ICommand<object?> command = robotArm.CreateMoveAllCommand(values);
            robotArm.SendCommand(command);
        }

        private void OnTick(object? sender, EventArgs args)
        {
            UpdateJoystickState();
        }

        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
            timer.Stop();
        }
    }
}
