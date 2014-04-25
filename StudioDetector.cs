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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Win32;

    public class StudioDetector
    {
        private IDictionary<Version, string> installPaths = new SortedDictionary<Version, string>();

        public string DetectPath()
        {
            this.RetrieveInstallPathsFromRegistry();
            
            if (this.installPaths.Count == 0)
            {
                throw new FileNotFoundException("No Atmel Studio installation is present on your system.");
            }

            return this.installPaths.Last().Value;
        }

        private void RetrieveInstallPathsFromRegistry()
        {
            if (this.installPaths.Count != 0)
            {
                return;
            }
            
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            {
                using (RegistryKey key = hklm.OpenSubKey(@"SOFTWARE\Atmel\AtmelStudio"))
                {
                    if (key != null)
                    {
                        string[] subKeys = key.GetSubKeyNames();
                        foreach (string version in subKeys)
                        {
                            using (RegistryKey subKey = key.OpenSubKey(version))
                            {
                                this.RetrieveInstallPathFromSubKey(version, subKey);
                            }
                        }
                    }
                }
            }
        }
           
        private void RetrieveInstallPathFromSubKey(string version, RegistryKey subKey)
        {
            if (subKey == null)
            {
                return;
            }

            object pathValue = subKey.GetValue("InstallDir");
            if (pathValue != null && pathValue is string)
            {
                string path = Path.Combine((string)pathValue, "atmelstudio.exe");
                if (File.Exists(path))
                {
                    this.installPaths.Add(Version.Parse(version), path);
                }
            }
        }
    }
}
