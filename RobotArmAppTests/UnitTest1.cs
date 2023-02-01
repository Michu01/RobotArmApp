using System;
using System.IO.Ports;
using System.Threading;

using RobotArmApp.Source;
using RobotArmApp.Source.Commands;
using RobotArmApp.Source.RobotArm;

using Xunit;

namespace RobotArmAppTests
{
    public class UnitTest1
    {
        private readonly SerialPort port = new("COM3")
        {
            ReadTimeout = 1000,
            WriteTimeout = 1000
        };

        public UnitTest1()
        {
            port.Open();
        }

        private void CommandTest<T>(Command<T> command)
        {
            command.Send(port);

            byte[] expected = command.GetBytes();

            while (port.BytesToRead < expected.Length)
            {
                Thread.Sleep(1);
            }
            Assert.Equal(expected.Length, port.BytesToRead);

            byte[] actual = new byte[expected.Length];

            port.Read(actual, 0, actual.Length);

            Assert.Equal(expected, actual);
            Assert.Equal(0, port.BytesToRead);
        }

        [Fact]
        public void DelayCommandTest()
        {
            DelayCommand command = new(TimeSpan.FromSeconds(1));
            CommandTest(command);
        }

        [Fact]
        public void MoveCommandTest()
        {
            MoveCommand command = new(IRobotArm.Axis.Shoulder, (char)30);
            CommandTest(command);
        }

        [Fact]
        public void SetCommandTest()
        {
            SetCommand command = new(IRobotArm.Axis.Elbow, 15);
            CommandTest(command);
        }

        [Fact]
        public void SetAllCommandTest()
        {
            SetAllCommand command = new(new(new byte[] { 1, 2, 3, 4, 5, 6 }));
            CommandTest(command);
        }

        [Fact]
        public void MoveAllCommandTest()
        {
            MoveAllCommand command = new(new char[] { (char)1, (char)2, (char)3, (char)4, (char)5, (char)6 });
            CommandTest(command);
        }

        [Fact]
        public void GetAllCommandTest()
        {
            GetAllCommand command = new();
            command.Send(port);

            while (port.BytesToRead < 7)
            {
                Thread.Sleep(1);
            }
            Assert.Equal(7, port.BytesToRead);

            port.Read(new byte[7], 0, 7);

            Assert.Equal(0, port.BytesToRead);
        }
    
        [Fact]
        public void MoveAllCommandLoopTest()
        {
            for (int i = 0; i < 100; ++i)
            {
                MoveAllCommandTest();
                Thread.Sleep(10);
            }
        }
    }
}