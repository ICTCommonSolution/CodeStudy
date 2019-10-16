using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CodingStudy
{
    public class LoadJSon
    {
        string m_fileName = string.Empty;
        Dictionary<string, object> m_dictContentTree = new Dictionary<string, object>();
        public LoadJSon(string fileName)
        {
            if (true == System.IO.File.Exists(fileName))//read json file
            {
                m_fileName = fileName;
                ReadingFile();
            }
            else // file does not exist. Create a new one for writting
            {
                System.IO.File.Create(fileName);
                m_fileName = fileName;
            }

            WriteToFile(fileName);
        }

        private bool ReadingFile()
        {
            return true;
        }

        private bool WriteToFile(string fileName)
        {
            Dictionary<string, object> dictSubBranch = new Dictionary<string, object>();
            dictSubBranch.Add("a", "a");
            dictSubBranch.Add("b", 1);
            dictSubBranch.Add("c", 9.9);
            dictSubBranch.Add("d", true);

            m_dictContentTree.Add("section1", dictSubBranch);
            dictSubBranch.Clear();

            m_dictContentTree.Add("Hello", "world");

            dictSubBranch.Add("a", "aa");
            dictSubBranch.Add("b", 11);
            dictSubBranch.Add("c", 99.9999999);
            dictSubBranch.Add("d", false);

            m_dictContentTree.Add("section3", dictSubBranch);
            dictSubBranch.Clear();



            return true;
        }

    }
}
