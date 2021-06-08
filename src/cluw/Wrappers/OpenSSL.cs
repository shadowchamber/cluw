using System;
using System.Collections.Generic;
using System.Text;

namespace cluw.Wrappers
{
    public class OpenSSL : WrapperBase
    {
        public void GenerateSelfSignedCertificate(int days, int rsaBits, string path)
        {
            this.RunCommand2("openssl", $"req -x509 -nodes -newkey rsa:{rsaBits} -days {days} -keyout '{path}/privkey.pem' -out '{path}/fullchain.pem' -subj '/CN=localhost'");
        }
    }
}
