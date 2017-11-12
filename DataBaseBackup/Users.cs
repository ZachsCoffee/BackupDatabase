using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBackup
{
    class Users
    {
        private string serverType;
        private string domainName;
        private string port;
        private string username;


        public Users() { }

        public Users(string servetType,string domainName,string port,string username)
        {
            this.serverType = servetType;
            this.domainName = domainName;
            this.port = port;
            this.username = username;

        }

        public void setServerType(String serverType)
        {
            this.serverType = serverType;

        }

        public string getServerType() { return serverType; }


    }
}
