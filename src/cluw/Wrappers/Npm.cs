using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cluw.Wrappers
{
    public class Npm : WrapperBase
    {
        public async Task<CommandResult> InstallYarnAsync()
        {
            return await this.RunCommandAsync("npm", "install --global yarn").ConfigureAwait(false);
        }
    }
}
