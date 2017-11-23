using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBackup.Class
{
    class LogFile
    {
        private DateTime time;
        private String type="";
        private String desc="";

        private string startupPath = System.IO.Path.GetFullPath(@"..\..\LogFiles\test1.txt");
        private ObjectStream ObjStr;

        public LogFile()
        {
            ObjStr = new ObjectStream(startupPath);         
        }


        public ArrayList print()
        {
            ArrayList list = ObjStr.ReadLines();
            return list;
        }
       
    }
}
