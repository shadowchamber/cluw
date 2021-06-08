using System;
using System.Collections.Generic;
using System.Text;

namespace cluw.Wrappers
{
    public class CertBot : WrapperBase
    {
        public void CertOnly(string email, string domain)
        {
            this.Bash($"/usr/bin/certbot certonly --webroot -w /var/www/certbot -m {email} -d {domain} --rsa-key-size 2048 --agree-tos --force-renewal");
        }
    }
}
