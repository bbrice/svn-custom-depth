/*
 * Copyright (C) 2021, Brian Brice. All rights reserved.
 *
 * This file is part of svn-custom-depth.
 *
 * svn-custom-depth is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * svn-custom-depth is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with svn-custom-depth.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace svn_custom_depth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] dirs = (args.Length > 0 ? args : new[] { "." });

            foreach (var dir in dirs)
            {
                try
                {
                    string workingDirectory = Path.GetFullPath(dir);

                    var procInfo = new ProcessStartInfo();
                    procInfo.WorkingDirectory = workingDirectory;
                    procInfo.FileName = "svn";
                    procInfo.Arguments = "info --xml -R";
                    procInfo.UseShellExecute = false;
                    procInfo.RedirectStandardOutput = true;

                    var svn = Process.Start(procInfo);
                    var xml = XDocument.Load(svn.StandardOutput);

                    Debug.Assert(svn.HasExited);

                    var elements = (IEnumerable)xml.XPathEvaluate("/info/entry[./wc-info/depth!=\"infinity\"]/@path | /info/entry/wc-info/depth[.!=\"infinity\"]/text()");
                    foreach (var element in elements)
                    {
                        if (element is XAttribute)
                            Console.WriteLine(Path.Combine(workingDirectory, ((XAttribute)element).Value));
                        else if (element is XText)
                            Console.WriteLine("    " + ((XText)element).Value);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("An exception was thrown: " + e.Message);
                }
            }
        }
    }
}
