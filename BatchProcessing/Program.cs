using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                printManual();
                return;
            }
            string fileWithCommands = args[0];
            string[] commands = readCommandsFromFile(fileWithCommands);
            for (int i= 0; i < commands.Length; i++)
            {
                string command = commands[i];
                if (command.Length > 2)
                {
                    try
                    {
                        System.Console.WriteLine((i + 1).ToString() + "  " + command);
                        runProcess(command);
                    }
                    catch(Exception e)
                    {
                        System.Console.WriteLine("Error");
                        System.Console.WriteLine(e.ToString());
                    }
                }
            }
        }

        static void printManual()
        {
            string manual = "Manual:\nBatchProcessing.exe fileWithProcessesInLines";
            System.Console.WriteLine(manual);
        }

        static string[] readCommandsFromFile(string file)
        {
            return System.IO.File.ReadAllLines(file);
        }

        static void runProcess(string processTxtLine)
        {
            string[] fullLineArgs = processTxtLine.Split(' ');
            string executionFile = fullLineArgs[0];
            string onlyArgs = "";
            for(int i=1; i< fullLineArgs.Length; i++)
            {
                if (i != 1)
                {
                    onlyArgs += " ";
                }
                onlyArgs += fullLineArgs[i];
            }

            

            Process cmd = System.Diagnostics.Process.Start(executionFile, onlyArgs);
            cmd.WaitForExit(480000);
            try
            {
                cmd.Kill();
            }
            catch(Exception e)
            {
                e.ToString();
            }
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }
        static void printArgs(string[] args)
        {
            string info = "Running: ";
            System.Console.WriteLine(info);
            foreach (string s in args)
            {
                System.Console.Write(s + " ");
            }
        }
    }
}
