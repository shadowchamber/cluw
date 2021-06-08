using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace cluw.Wrappers
{
    public class DotNet : WrapperBase
    {
        public string Configuration { get; set; }
        public string RunTime { get; set; }
        public string DotNetExecutable { get; set; }

        public DotNet WithConfiguration(string configuration)
        {
            this.Configuration = configuration;

            return this;
        }

        public DotNet WithRunTime(string runTime)
        {
            this.RunTime = runTime;

            return this;
        }

        public DotNet WithDotNetExecutable(string dotNetExecutable)
        {
            this.DotNetExecutable = dotNetExecutable;

            return this;
        }

        public async Task<CommandResult> BuildAsync(string projectFile)
        {
            string args = "build";

            string app = string.IsNullOrEmpty(DotNetExecutable) ? "dotnet" : DotNetExecutable;

            args += (string.IsNullOrEmpty(Configuration) ? "" : " --configuration " + Configuration);
            args += (string.IsNullOrEmpty(RunTime) ? "" : " --runtime " + RunTime);
            args += (string.IsNullOrEmpty(projectFile) ? "" : " " + projectFile);

            return await this.RunCommandAsync(app, args).ConfigureAwait(false);
        }

        public async Task<CommandResult> PublishAsync(string projectFile, string publishdir)
        {
            string command = "publish";

            string app = string.IsNullOrEmpty(DotNetExecutable) ? "dotnet" : DotNetExecutable;

            command += (string.IsNullOrEmpty(Configuration) ? "" : " --configuration " + Configuration);
            command += (string.IsNullOrEmpty(RunTime) ? "" : " --runtime " + RunTime);
            command += (string.IsNullOrEmpty(projectFile) ? "" : " " + projectFile);
            command += (string.IsNullOrEmpty(publishdir) ? "" : " /p:PublishDir=\"" + publishdir + "\"");

            return await this.RunCommandAsync(app, command).ConfigureAwait(false);
        }
    }
}

