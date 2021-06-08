using System;
using System.Collections.Generic;
using System.Text;

namespace cluw.Wrappers
{
    public class DockerCompose : WrapperBase
    {
        public string Version
        {
            get
            {
                this.RunCommand2("docker-compose", "-v");
                // docker-compose version 1.25.0, build unknown

                if (CommandOutput.Contains("Command 'docker-compose' not found") || CommandErrors.Contains("Command 'docker-compose' not found"))
                {
                    return "not installed";
                }

                string res = CommandOutput;

                res = res.Replace("docker-compose version ", "");

                res = res.Substring(0, res.IndexOf(','));

                return res;
            }
        }

        public bool Installed
        {
            get
            {
                return Version != "not installed";
            }
        }

        public void RequestCert(string domain)
        {
            string email = "andrii.pylypenko@recoded.com.ua";
            int staging = 0;
            string composefile = "docker-compose-entry.yml";
            int rsakeysize = 2048;

            this.RunCommand2("docker-compose", $"-f {composefile} run --rm --entrypoint \"certbot certonly --webroot -w /var/www/certbot {staging} {email} -d {domain} --rsa-key-size {rsakeysize} --agree-tos --force-renewal\" certbot");
        }

        public void ReUpNginx()
        {
            this.RunCommand2("docker-compose", "up --force-recreate -d nginx");
        }
    }
}
