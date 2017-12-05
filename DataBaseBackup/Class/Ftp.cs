using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBackup.Class
{
    class Ftp
    {
        private string serverType;
        private string domainName;
        private string port;
        private string username;

        public Ftp() { }

        public Ftp(string serverType, string domainName, string port, string username)
        {
            this.serverType = serverType;
            this.domainName = domainName;
            this.port = port;
            this.username = username;
        }
        public void setServerType(string serverType) { this.serverType = serverType; }
        public string getServerType() { return serverType; }

        public void setDomainName(string domainName) { this.domainName = domainName; }
        public string getDomainName() { return domainName; }

        public void setPort(string port) { this.port = port; }
        public string getPort() { return port; }

        public void setUsername(string username) { this.username = username; }
        public string getUsername() { return username; }


        public override string ToString()
        {
            return serverType + "," + domainName + "," + port + "," + username;
        }
    }
}
