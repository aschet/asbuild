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
    using System.Diagnostics;

    public class StudioRunner
    {
        private ProcessStartInfo startInfo = new ProcessStartInfo();
        
        public StudioRunner(BuildArgs args)
        {
            this.startInfo.CreateNoWindow = true;
            this.startInfo.UseShellExecute = false;
            this.startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            this.startInfo.Arguments = args.ToString();

            StudioDetector studioDetector = new StudioDetector();
            this.startInfo.FileName = studioDetector.DetectPath();
        }

        public int Run()
        {
            int exitCode = ExitStatus.ToInt(ExitStatus.Code.Error);
            
            try
            {
                Console.WriteLine("Invoking \"{0}\" {1}", this.startInfo.FileName, this.startInfo.Arguments);
                using (Process process = Process.Start(this.startInfo))
                {
                    process.WaitForExit();
                    exitCode = process.ExitCode;
                }
            }
            catch
            {
            }

            return exitCode;
        }
    }
}
