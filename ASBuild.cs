/*
 * ASBuild - Atmel Studio Command Line Builder
 * Copyright (C) 2014 Thomas Ascher
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
 */
namespace ASBuild
{
    using System;
    using System.IO;
    using System.Reflection;

    public class ASBuild
    {
        public static int Main(string[] args)
        {
            ArgsParser argParser = new ArgsParser();

            PrintGreeting();
            if (!argParser.Parse(args))
            {
                PrintUsage();
                return ExitStatus.ToInt(ExitStatus.Code.Success);
            }

            if (!File.Exists(argParser.Args.SolutionFile))
            {
                Console.WriteLine("Solution or project file '{}' does not exist.", argParser.Args.SolutionFile);
                return ExitStatus.ToInt(ExitStatus.Code.SolutionDoesNotExists);
            }

            int exitCode = ExitStatus.ToInt(ExitStatus.Code.Success);
            try
            {
                StudioRunner studioRunner = new StudioRunner(argParser.Args);
                exitCode = studioRunner.Run();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return ExitStatus.ToInt(ExitStatus.Code.StudioNotFound); 
            }

            LogPrinter.Print(argParser.Args.LogFile);

            return exitCode;
        }

        private static void PrintGreeting()
        {
            Console.WriteLine("{0} v{1} - Atmel Studio Command Line Builder by Thomas Ascher",
                Assembly.GetExecutingAssembly().GetName().Name,
                Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine("This program is licensed under the terms of the GNU GPL.");
            Console.WriteLine();
        }

        private static void PrintUsage()
        {
            Console.WriteLine("usage: <solution file> <build switch> <configuration>");
            Console.WriteLine("<build switch> := build | rebuild");
            Console.WriteLine("<configuration> := debug | release");
        }
    }
}