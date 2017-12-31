using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBackup.Class
{
    /// <summary>
    /// Is good for small number of variables (~20).
    /// </summary>
    public class VariableStorage
    {
        private string filePath;
        private ObjectStream fileStream;

        public VariableStorage(string filePath)
        {
            this.filePath = filePath ?? throw new ArgumentNullException("Argument at pos: 1, not null.");

            fileStream = new ObjectStream(filePath);
        }

        /// <summary>
        /// Save a new variable, in the storage.
        /// </summary>
        /// <param name="varName">The variable name, must be unique at the storage in order to work properly. The method will not search if the variable is unique!</param>
        /// <param name="value">The value of the variable.</param>
        public void NewVariable(string varName, object value)
        {
            fileStream.WriteLines(varName + "," + value);
        }

        /// <summary>
        /// If the variable already exist, the value is set to the variable, orvewise a new variable will be create with the specified value.
        /// </summary>
        /// <param name="varName">The variable name.</param>
        /// <param name="value">The value of the variable.</param>
        public void PutVariable(string varName, object value)
        {
            bool ok = fileStream.EditLines(delegate (int pos, object line)
            {
                string[] splitedLine = ((string)line).Split(',');

                return splitedLine[0].Equals(varName);
            }, varName + "," + value);

            if (!ok)
            {
                fileStream.WriteLines(varName + "," + value);
            }
        }

        /// <summary>
        /// Search the storage for the specified variable.
        /// </summary>
        /// <param name="varName">The variable name.</param>
        /// <returns>Return the value of the variable, or null if the variable not found.</returns>
        public object GetVariable(string varName)
        {
            fileStream.StartReading(10);
            ArrayList lines;
            int i = -1;
            string[] splitedLine;
            while ((lines = fileStream.ReadNext()).Count != 0)
            {
                while (++i < lines.Count)
                {
                    splitedLine = ((string)lines[i]).Split(',');
                    if (splitedLine[0].Equals(varName))
                    {
                        fileStream.EndReading();
                        return splitedLine[1];
                    }
                }
                i = -1;
            }
            fileStream.EndReading();
            return null;
        }
    }
}
