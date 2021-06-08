using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cluw
{
    public class CommandResult
    {
        public bool HadErrors { get; set; }
        public List<string> Output { get; set; }

        public void Show()
        {
            foreach (var line in Output)
            {
                Console.WriteLine(line);
            }
        }

        public bool ShowReturnHadErrors()
        {
            foreach (var line in Output)
            {
                Console.WriteLine(line);
            }

            return this.HadErrors;
        }

        public bool ShowNoWarnReturnHadErrors()
        {
            foreach (var line in Output.Where(i => !i.ToLower().Contains("warning")))
            {
                Console.WriteLine(line);
            }

            return this.HadErrors;
        }
    }
}
