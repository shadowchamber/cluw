namespace cluw
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Management.Automation;

    public class WrapperBase
    {
        public string CommandOutput { get; set; }
        public string CommandErrors { get; set; }
        public string WorkingDirectory { get; set; }

        public WrapperBase WithWorkingDirectory(string workingDirectory)
        {
            this.WorkingDirectory = workingDirectory;

            return this;
        }

        public void RunCommandNoRedirect(string command, string arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = WorkingDirectory
            };

            Console.WriteLine("Exec: {0} {1}", command, arguments);

            Process proc = new Process() { StartInfo = startInfo, };

            bool res = proc.Start();

            if (!res)
            {
                Console.WriteLine("process not started");
            }

            proc.WaitForExit();
        }

        public async Task<CommandResult> RunCommandAsync(string command, string arguments = "")
        {
            Console.WriteLine("Exec: {0} {1}", command, arguments);
            var cres = new CommandResult();

            try
            {
                using (PowerShell PowerShellInst = PowerShell.Create())
                {
                    PowerShellInst.AddScript("Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy Unrestricted");
                    PowerShellInst.AddScript(command + " " + arguments);
                    PSDataCollection<PSObject> PSOutput = await PowerShellInst.InvokeAsync().ConfigureAwait(false);

                    List<string> res = new List<string>();

                    foreach (PSObject obj in PSOutput)
                    {
                        if (obj != null)
                        {
                            res.Add(obj.ToString());
                        }
                    }

                    bool bres = PowerShellInst.HadErrors;

                    cres.Output = res;
                    cres.HadErrors = bres;

                    if (PowerShellInst.HadErrors)
                    {
                        try
                        {
                            var errors = PowerShellInst.Streams.Error.ReadAll();

                            if (errors != null)
                            {
                                bool foundErrors = false;

                                foreach (var error in errors)
                                {
                                    Console.WriteLine("Error: " + error.ToString());

                                    if (error.ToString().ToLower().Contains("error"))
                                    {
                                        foundErrors = true;
                                    }
                                }

                                if (!foundErrors)
                                {
                                    cres.HadErrors = false;
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message + " " + exception.StackTrace);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                var ceres = new CommandResult();

                ceres.Output = new List<string>();

                ceres.Output.Add(exception.Message);
                ceres.Output.Add(exception.StackTrace);

                ceres.HadErrors = true;

                return ceres;
            }

            return cres;

            //ProcessStartInfo startInfo = new ProcessStartInfo()
            //{
            //    FileName = command,
            //    Arguments = arguments,
            //    RedirectStandardOutput = true,
            //    RedirectStandardError = true,
            //    UseShellExecute = false,
            //    CreateNoWindow = true
            //};

            //Console.WriteLine("Exec: {0} {1}", command, arguments);

            //Process proc = new Process() { StartInfo = startInfo, };

            //bool res = proc.Start();

            //if (!res)
            //{
            //    Console.WriteLine("process not started");
            //}

            //proc.OutputDataReceived += Proc_OutputDataReceived;
            //proc.ErrorDataReceived += Proc_ErrorDataReceived;

            //await proc.WaitForExitAsync().ConfigureAwait(false);

            ////CommandOutput = proc.StandardOutput.ReadToEnd();
            ////CommandErrors = proc.StandardOutput.ReadToEnd();

            ////Console.WriteLine("Output: {0}", CommandOutput);
            ////Console.WriteLine("Errors: {0}", CommandErrors);
        }

        private void Proc_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        private void Proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        public void RunCommand2(string command, string arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = command,
                Arguments = arguments + " > /tmp/cout",
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Console.WriteLine("Exec: {0} {1}", command, arguments);

            Process proc = new Process() { StartInfo = startInfo, };

            bool res = proc.Start();

            if (!res)
            {
                Console.WriteLine("process not started");
            }

            proc.WaitForExit();

            CommandOutput = System.IO.File.ReadAllText("/tmp/cout");

            Console.WriteLine("Output: {0}", CommandOutput);
            Console.WriteLine("Errors: {0}", CommandErrors);
        }

        public void RunCommand3(string command, string arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "/bin/bash",
                Arguments = command + " " + arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Console.WriteLine("Exec: {0} {1}", command, arguments);

            Process proc = new Process() { StartInfo = startInfo, };

            bool res = proc.Start();

            if (!res)
            {
                Console.WriteLine("process not started");
            }

            proc.WaitForExit();

            CommandOutput = proc.StandardOutput.ReadToEnd();
            CommandErrors = proc.StandardOutput.ReadToEnd();

            Console.WriteLine("Output: {0}", CommandOutput);
            Console.WriteLine("Errors: {0}", CommandErrors);
        }

        public void RunCommand4(string command, string arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "/bin/bash",
                Arguments = command + " " + arguments + " > /tmp/cout",
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Console.WriteLine("Exec: {0} {1}", command, arguments);

            Process proc = new Process() { StartInfo = startInfo, };

            bool res = proc.Start();

            if (!res)
            {
                Console.WriteLine("process not started");
            }

            proc.WaitForExit();

            CommandOutput = System.IO.File.ReadAllText("/tmp/cout");

            Console.WriteLine("Output: {0}", CommandOutput);
            Console.WriteLine("Errors: {0}", CommandErrors);
        }

        public void RunCommandPy3(string command, string arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "/usr/bin/python3",
                Arguments = command + " " + arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Console.WriteLine("Exec: {0} {1}", command, arguments);

            Process proc = new Process() { StartInfo = startInfo, };

            bool res = proc.Start();

            if (!res)
            {
                Console.WriteLine("process not started");
            }

            proc.WaitForExit();

            CommandOutput = proc.StandardOutput.ReadToEnd();
            CommandErrors = proc.StandardOutput.ReadToEnd();

            Console.WriteLine("Output: {0}", CommandOutput);
            Console.WriteLine("Errors: {0}", CommandErrors);
        }

        public void RunCommand3cout(string command, string arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "/usr/bin/python3",
                Arguments = command + " " + arguments + " > /tmp/cout",
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Console.WriteLine("Exec: {0} {1}", command, arguments);

            Process proc = new Process() { StartInfo = startInfo, };

            bool res = proc.Start();

            if (!res)
            {
                Console.WriteLine("process not started");
            }

            proc.WaitForExit();

            CommandOutput = System.IO.File.ReadAllText("/tmp/cout");

            Console.WriteLine("Output: {0}", CommandOutput);
            Console.WriteLine("Errors: {0}", CommandErrors);
        }

        public void Bash(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();

            CommandOutput = process.StandardOutput.ReadToEnd();
            CommandErrors = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            Console.WriteLine("Output: {0}", CommandOutput);
            Console.WriteLine("Errors: {0}", CommandErrors);
        }

    }
}


