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
    
    public class ArgsParser
    {
        public BuildArgs Args
        {
            get;
            set;
        }
        
        public bool Parse(string[] args)
        {
            this.Args = new BuildArgs();

            if (args.Length != 3)
            {
                return false;
            }

            int currentArg = 0;
            this.Args.SolutionFile = args[currentArg];
            ++currentArg;
            this.Args.BuildSwitch = args[currentArg];
            ++currentArg;
            this.Args.Configuration = args[currentArg];
            
            return true;
        }
    }
}
