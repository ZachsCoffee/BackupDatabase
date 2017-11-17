using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBackup.Class
{
    public class ObjectStream
    {
        private string filePath;

        public ObjectStream(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException("Argument at pos: 1, not null.");
        }

        public void WriteLines(params object[] objects)
        {
            Write(true, objects);
        }

        public ArrayList ReadLines()
        {
            ArrayList list = new ArrayList();
            using (var streamReader = new StreamReader(filePath, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            return list;
        }

        public void DeleteLines(params int[] lines)
        {
            ArrayList list = ReadLines();
            for (int i=0; i<lines.Length; i++)
            {
                list.RemoveAt(lines[i]);
            }

            Write(false, list.ToArray());
        }

        public void ClearFile()
        {
            File.WriteAllText(filePath, string.Empty);
        }

        private void Write(bool append, object[] objects)
        {
            using (var streamWriter = new StreamWriter(filePath, append, Encoding.UTF8))
            {
                foreach (object obj in objects)
                {
                    streamWriter.WriteLine(obj);
                }
            }
        }
    }
}
