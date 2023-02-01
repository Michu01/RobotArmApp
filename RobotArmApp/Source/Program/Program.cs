using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using RobotArmApp.Source.Commands;
using RobotArmApp.Source.RobotArm;

namespace RobotArmApp.Source
{
    public class Program : IProgram
    {
        private readonly IEnumerable<ICommand<object?>> commands;

        public string Name { get; }

        public Program(string name, IEnumerable<ICommand<object?>> commands)
        {
            Name = name;
            this.commands = commands;
        }

        public string BodyToString()
        {
            return string.Join('\n', commands.Select(c => c.ToString()));
        }

        public static Program CreateFromText(string name, string text)
        {
            string[] lines = text.Split('\n');

            IEnumerable<ICommand<object?>> commands = ParseLines(lines);

            return new(name, commands);
        }

        public void SaveToFile()
        {
            File.WriteAllLines(GetProgramDirectory() + Name + ".txt", commands.Select(c => c.ToString() ?? string.Empty));
        }

        public static Program LoadFromFile(string path)
        {
            string name = Path.GetFileNameWithoutExtension(path);

            string[] lines = File.ReadAllLines(path);

            IEnumerable<ICommand<object?>> commands = ParseLines(lines);

            return new(name, commands);
        }

        public void Delete()
        {
            File.Delete(GetProgramDirectory() + Name + ".txt");
        }

        public void Execute(IRobotArm robotArm)
        {
            foreach (ICommand<object?> command in commands)
            {
                robotArm.SendCommand(command);
            }
        }

        public static IEnumerable<Program> GetPrograms()
        {
            return Directory.EnumerateFiles(GetProgramDirectory()).Select(s => LoadFromFile(s));
        }

        public static bool Exists(string name)
        {
            return File.Exists(GetProgramDirectory() + name + ".txt");
        }

        public static bool IsValid(string[] lines)
        {
            try
            {
                ParseLines(lines);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private static string GetProgramDirectory()
        {
            string pwd = Directory.GetCurrentDirectory();
            string? dir = new DirectoryInfo(pwd)?.Parent?.Parent?.Parent?.FullName;
            return dir + "/Programs/";
        }

        private static ICommand<object?> ParseLine(string line)
        {
            string[] parts = line.Split(' ');

            if (parts.Length == 0 || !Enum.TryParse(parts[0], true, out ICommand<object>.Type type))
            {
                throw new ArgumentException("Error in line: \"" + line + "\"");
            }

            if (type == ICommand<object>.Type.Delay && parts.Length == 2 && int.TryParse(parts[1], out int delay))
            {
                return new DelayCommand(TimeSpan.FromMilliseconds(delay));
            }

            if (type == ICommand<object>.Type.Set)
            {
                if (parts.Length == 3 && Enum.TryParse(parts[1], true, out IRobotArm.Axis axis) && byte.TryParse(parts[2], out byte angle))
                {
                    return new SetCommand(axis, angle);
                }
                throw new ArgumentException("Error in line: \"" + line + "\"");
            }

            if (type == ICommand<object>.Type.Move)
            {
                if (parts.Length == 3 && Enum.TryParse(parts[1], true, out IRobotArm.Axis axis) && char.TryParse(parts[2], out char angle))
                {
                    return new MoveCommand(axis, angle);
                }
                throw new ArgumentException("Error in line: \"" + line + "\"");
            }

            if (type == ICommand<object>.Type.SetAll && parts.Length == 7)
            {
                byte[] angles = new byte[6];

                int i = 0;
                for (; i < 6 && byte.TryParse(parts[i + 1], out angles[i]); i++) { }

                if (i == 6)
                {
                    return new SetAllCommand(new Angles(angles));
                }
            }

            throw new ArgumentException("Error in line: \"" + line + "\"");
        }

        private static IEnumerable<ICommand<object?>> ParseLines(string[] lines)
        {
            return lines.Select(l => ParseLine(l));
        }

        public Task ExecuteAsync(IRobotArm robotArm)
        {
            return Task.Run(() => Execute(robotArm));
        }
    }
}
