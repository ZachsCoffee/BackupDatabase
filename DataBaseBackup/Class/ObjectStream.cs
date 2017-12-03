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
        private int index = -1, step;
        private StreamReader stepReader;

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

        public void EditLine(int lineForEdit, object newLine)
        {
            ArrayList list = ReadLines();
            list.Insert(lineForEdit, newLine);
            Write(false, list.ToArray());
        }

        public void EditLines(int[] linesForEdit, object[] newLines)
        {
            if (linesForEdit.Length != newLines.Length)
            {
                throw new ArgumentException("The two arrays must have same length.");
            }

            ArrayList list = ReadLines();
            for (int i = 0; i < linesForEdit.Length; i++)
            {
                list.Insert(linesForEdit[i], newLines[i]);
            }
            Write(false, list.ToArray());
        }

        /// <summary>
        /// Uses a delegate in order to decied, which lines will be overwriten with the new ones.
        /// </summary>
        /// <param name="where">Delegate method, when it is match return true and a new line overwrite the old.</param>
        /// <param name="newLines">The new lines.</param>
        /// <returns>True if at least one line is match, with the delegate method, overwise false.</returns>
        public bool EditLines(Where where, params object[] newLines)
        {
            bool flag = false;
            int count = 0;
            ArrayList list = ReadLines();
            for (int i = 0; i < list.Count; i++)
            {

                if (where(i, list[i]))
                {
                    if (i == newLines.Length)
                    {
                        break;
                    }
                    flag = true;
                    list.Insert(i, newLines[count++]);
                }
            }

            if (flag)
            {
                Write(false, list.ToArray());
            }
            return flag;
        }

        public void DeleteLines(params int[] lines)
        {
            ArrayList list = ReadLines();
            for (int i = 0; i < lines.Length; i++)
            {
                list.RemoveAt(lines[i]);
            }

            Write(false, list.ToArray());
        }

        /// <summary>
        /// Opens the stream for reading.
        /// </summary>
        /// <param name="step">The amount of lines for read.</param>
        public void StartReading(int step = 20)
        {
            if (index != -1)
            {
                throw new Exception("You must first end reading, before call StartReading().");
            }

            if (step <= 0)
            {
                throw new ArgumentException("Argument at pos: 1, can't be <= 0 .");
            }

            this.step = step;
            stepReader = new StreamReader(filePath, Encoding.UTF8);
        }

        /// <summary>
        /// Read the next lines from the file. The amount of readed lines, is defined at StartReading() method.
        /// </summary>
        /// <returns>Return a ListArray with readed lines. If end of stream reached, returns empty ArrayList.</returns>
        public ArrayList ReadNext()
        {
            ArrayList result = new ArrayList();
            string temp;
            int i = -1;
            while ((temp = stepReader.ReadLine()) != null && ++i < step)
            {
                result.Add(temp);
            }

            return result;
        }

        /// <summary>
        /// Close the stream. End of reading.
        /// </summary>
        public void EndReading()
        {
            if (stepReader != null)
            {
                stepReader.Close();
                index = -1;
            }
        }

        public void ClearFile()
        {
            File.WriteAllText(filePath, string.Empty);
        }

        public delegate bool Where(int pos, object line);

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