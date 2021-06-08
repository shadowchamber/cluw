using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cluw.Wrappers
{
    public class SevenZip : WrapperBase
    {
        public async Task<CommandResult> Pack(string archiveFilename, string source)
        {
            return await this.RunCommandAsync($"cmd /c \"c:\\Program Files\\7-Zip\\7z.exe\" a \"{archiveFilename}\" -w \"{source}\"").ConfigureAwait(false);
        }
    }
}
