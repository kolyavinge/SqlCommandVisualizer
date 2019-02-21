using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCommandVisualizer
{
    public class TempFileManager
    {
        private static List<string> _tmpFilePathCollection = new List<string>();

        ~TempFileManager()
        {
            _tmpFilePathCollection.ForEach(File.Delete);
        }

        public string GetTempFile(string fileContent, string fileExtension)
        {
            var tempFilePath = Path.GetTempFileName() + "." + fileExtension;
            File.WriteAllText(tempFilePath, fileContent);
            _tmpFilePathCollection.Add(tempFilePath);

            return tempFilePath;
        }
    }
}
