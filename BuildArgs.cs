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
    using System.IO;
    
    public class BuildArgs
    {
        private string solutionFile;

        private string logFile;

        public BuildArgs()
        {
            this.SolutionFile = string.Empty;
            this.logFile = Path.GetTempFileName();
            this.BuildSwitch = string.Empty;
            this.Configuration = string.Empty;
        }

        public string SolutionFile
        {
            get
            {
                return Path.GetFullPath(this.solutionFile);
            }

            set
            {
                this.solutionFile = value;
            }
        }

        public string LogFile
        {
            get
            {
                return this.logFile;
            }
        }

        public string BuildSwitch
        {
            get;
            set;
        }

        public string Configuration
        {
            get;
            set;
        }

        public override string ToString()
        {
            return "\"" + this.SolutionFile + "\" /" + this.BuildSwitch + " " + this.Configuration + " /out \"" + this.LogFile + "\"";
        }
    }
}
