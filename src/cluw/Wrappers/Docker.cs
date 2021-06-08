using System;
using System.Collections.Generic;
using System.Text;

namespace cluw.Wrappers
{
    public class Docker : WrapperBase
    {
        public void Restart(string containerName)
        {
            this.RunCommand2("docker", $"restart {containerName}");
        }

        public void Exec(string containerName, string command)
        {
            this.RunCommand2("docker", $"exec -it {containerName} {command}");
        }
    }
}
