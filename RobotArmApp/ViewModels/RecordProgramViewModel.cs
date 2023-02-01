using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;

using RobotArmApp.Source;
using RobotArmApp.Source.Commands;
using RobotArmApp.Source.Joystick;
using RobotArmApp.Source.NavigationService;
using RobotArmApp.Source.RobotArm;

namespace RobotArmApp.ViewModels
{
    public class RecordProgramViewModel : ViewModel
    {
        private readonly DispatcherTimer timer = new()
        {
            Interval = TimeSpan.FromMilliseconds(20)
        };

        private readonly IJoystick joystick = ((App)Application.Current).ServiceProvider.GetRequiredService<IJoystick>();

        private readonly IRobotArm robotArm = ((App)Application.Current).ServiceProvider.GetRequiredService<IRobotArm>();

        private bool crossButtonPressed = false;

        public ObservableCollection<Angles> RecordedAngles { get; } = new();

        public AsyncRelayCommand AddCurrentAnglesCommand { get; }

        public RelayCommand StopRecordingCommand { get; }

        public RelayCommand<Angles> RemoveRecordedAngleCommand { get; }

        public RecordProgramViewModel()
        {
            AddCurrentAnglesCommand = new(AddCurrentAngles);
            StopRecordingCommand = new(StopRecording);
            RemoveRecordedAngleCommand = new(RemoveRecordedAngle);

            timer.Tick += OnTick;
            timer.Start();

            UpdateJoystickState();
        }

        private async Task AddCurrentAngles()
        {
            GetAllCommand command = new();
            Angles? angles = await robotArm.SendCommandAsync(command);

            if (angles is not null)
            {
                RecordedAngles.Add(angles);
            }
        }

        private void StopRecording()
        {
            string programBody = string.Join('\n', RecordedAngles.Select(e => new SetAllCommand(e).ToString()));

            INavigationService navigationService = ((App)Application.Current).ServiceProvider.GetRequiredService<INavigationService>();
            navigationService.CreateProgramViewNavigate(programBody);
        }

        private void RemoveRecordedAngle(Angles? angles)
        {
            if (angles != null)
            {
                RecordedAngles.Remove(angles);
            }
        }

        private void UpdateJoystickState()
        {
            Joystick.Update();

            float[] values = new float[6];

            values[0] = joystick.GetAxisPosition(IJoystick.Axis.LeftHorizontal);
            values[1] = joystick.GetAxisPosition(IJoystick.Axis.LeftVertical);
            values[2] = joystick.GetAxisPosition(IJoystick.Axis.RightVertical);
            values[3] = joystick.GetAxisPosition(IJoystick.Axis.RightHorizontal);
            values[4] = joystick.GetAxisPosition(IJoystick.Axis.R2) - joystick.GetAxisPosition(IJoystick.Axis.L2);
            values[5] += joystick.GetIsButtonPressed(IJoystick.Button.L1) ? 1 : 0;
            values[5] -= joystick.GetIsButtonPressed(IJoystick.Button.R1) ? 1 : 0;

            ICommand<object?> command = robotArm.CreateMoveAllCommand(values);
            robotArm.SendCommand(command);

            bool crossButtonPressed = joystick.GetIsButtonPressed(IJoystick.Button.Cross);
            if (!this.crossButtonPressed && crossButtonPressed)
            {
                _ = AddCurrentAngles();
            }
            this.crossButtonPressed = crossButtonPressed;
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
