using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cluw.Wrappers
{
    public class AwsS3 : WrapperBase
    {
        public async Task<CommandResult> Copy(string from, string to)
        {
            return await this.RunCommandAsync($"cmd /c aws --region us-east-1 s3 cp \"{from}\" \"{to}\"").ConfigureAwait(false);
        }
    }
}
