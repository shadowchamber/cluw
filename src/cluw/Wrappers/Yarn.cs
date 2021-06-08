using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cluw.Wrappers
{
    public class Yarn : WrapperBase
    {
        public async Task<CommandResult> YarnAsync()
        {
            return await this.RunCommandAsync("yarn").ConfigureAwait(false);
        }
    }
}
