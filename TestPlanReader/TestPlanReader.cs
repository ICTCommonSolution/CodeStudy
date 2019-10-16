using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace CodingStudy
{
    public class TPReader
    {
        string m_strFileName = string.Empty;
        TestPlan m_TP = new TestPlan();
        public TPReader() ////init a new file
        {
        }

        public TPReader(string fileName)
        {
            if (true == System.IO.File.Exists(fileName))
            {
                m_strFileName = fileName;
            }
            else
                throw new Exception(string.Format("The file {0} does not exist", fileName));

            try
            {
                StreamReader file = File.OpenText(m_strFileName);
                JsonTextReader reader = new JsonTextReader(file);
                JObject joTP = (JObject)JToken.ReadFrom(reader);
                if(false == ReadFile(m_strFileName))
                {
                    m_strFileName = string.Empty;
                    throw new Exception(string.Format("The file could not be parsed as test plan"));
                }
                reader.Close();
                file.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error in loading test plan: {0}", ex.Message));
            }
        }

        private bool ReadFile(string fileName)
        {
            try
            {
                StreamReader file = File.OpenText(m_strFileName);
                JsonTextReader reader = new JsonTextReader(file);
                JObject joTP = (JObject)JToken.ReadFrom(reader);

                JObject jo = (JObject)joTP.SelectToken("Sequence");
                ParseSequecne(ref m_TP, (JObject)joTP["Sequence"]);
            }
            catch (Exception ex)
            {
                //ToDo: send the error message to log.
                return false;
            }

            return true;
        }

        private void ParseSequecne(ref TestPlan tpSequence, JObject joSequence)
        {
            for (int i = 0; i < joSequence.Count; i++)
            {
                Dictionary<string, object> dictInput = null;
                Dictionary<string, object> dictOutput = null;
                JObject joItem = (JObject)joSequence[i];
                string strName = joItem["Name"].ToString();
                string strFielName = joItem["FielName"].ToString();
                string strMethod = joItem["Method"].ToString();
                if (true == joItem.ContainsKey("Input"))
                {
                    JObject joInput = (JObject)joItem["Input"];
                    dictInput = ParseInputSection(joInput);
                }
                else
                {
                    //ToDo: recheck later, add null to dictionary?
                }
                if (true == joItem.ContainsKey("Output"))
                {
                    JObject joOutput = (JObject)joItem["Output"];
                    dictOutput = ParseOutputSection(joOutput);
                }
                else
                {
                    //ToDo: recheck later, add null to dictionary?
                }
            }
        }
        private void ParseProperties(ref TestPlan tpSequence, JObject joProperties)
        {
            try
            {
                //assume there is no array data in this section
                foreach (JToken jt in joProperties.Children())
                {
                    if (jt.Type == JTokenType.Property)
                    {
                        KeyValuePair<string, object> kvpProperty = GetPropertyPair(jt);
                    }
                    else if (jt.Type == JTokenType.Array)
                    {

                    }
                    else if (jt.Type == JTokenType.Null)
                    {

                    }
                    else if (jt.Type == JTokenType.Object)
                    {
                    }
                    else
                    {
                        throw new Exception(string.Format("Unspported type:{0}", jt.Type.ToString()));
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(string.Format("", ex.Message));
            }
        }

        private KeyValuePair<string, object> GetPropertyPair(JToken DataPair)
        {
            //No sub branch allowed
            if (((JObject)DataPair).Count > 1)
            {
                throw new Exception(string.Format("Children exist. It's not a base data pair with {0}", DataPair.First.ToString()));            
            }
            try
            {
                var property = DataPair as JProperty;
                KeyValuePair<string, object> kvp = new KeyValuePair<string, object>(property.Name, property.Value);
                return kvp;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error in parse the data {0} with error message: {1}", DataPair.ToString(), ex.Message));
            }
        }

        private List<object> ParseArray(JObject ArrayData)//, ref Dictionary<string, object> DataDict)
        {
            try
            {
                JArray ja = JArray.Parse(ArrayData.ToString());
                List<object> listArray = new List<object>();
                for (int i = 0; i < ja.Count; i++)
                {
                    JToken jt = JToken.Parse(ja[i].ToString());
                }
                foreach (JToken jt in ArrayData.Children())
                {
                    if (jt.Type == JTokenType.Array)
                    {
                        listArray.Append(ParseArray((JObject)jt));
                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error in ParseArrary with error message:{0}", ex.Message));
            }
        }

        private Dictionary<string, object> ParseOutputSection(JObject OutputSection)
        {
            if (OutputSection.Count < 1) //no measure point assigned.
            {
                return null;
            }
            JArray jarOutput = JArray.Parse(OutputSection["Output"].ToString());
            Dictionary<string, object> dictOutput = new Dictionary<string, object>();
            for (int i = 0; i < jarOutput.Count; ++i)  //遍历JArray
            {
                JObject joPoint = JObject.Parse(jarOutput[i].ToString());
                Dictionary<string, object> dictPoint = new Dictionary<string, object>();
                string strID = joPoint["ID"].ToString();
                dictPoint.Add("Name", joPoint["Name"].ToString());
                locationitem._distance = joPoint["_distance"].ToString();
            }
            return ;
        }
        private Dictionary<string, object> ParseInputSection(JObject Section)
        {
            Dictionary<string, object> m_dictInput = null;
            if (false == Section.HasValues)
            {
                return null;
            }
            else
            {
                m_dictInput = new Dictionary<string, object>();
                //foreach (JToken jtSub in Section)
                {
                    //                    jtSub
                }
                return m_dictInput;
            }
        }
    }

    public class TestPlan
    {
        public List<Dictionary<string, string>> TPList = null;
        private Dictionary<string, Dictionary<string, object>> m_dictInput = null;
        private Dictionary<string, Dictionary<string, object>> m_dictOutput = null;
        //  item1
        //  item2
        private Dictionary<string, string> m_dictAnItem = null;
        //Example:  "GUID":1234-abcd-*-*    //unique id
        //          "Name":Test1            //test name shows on screen
        //          "File":abcd.dll         //the resource file
        //          "Method":edfg           //the method to run the test
        private Dictionary<string, object> m_dictAnInput = null; //key is the GUID:1234-abcd-*-*    //unique id linked to the item
        //Example:  "name1":300              // or a dictionary for multi-value as an input
        //          "name2":true
        private Dictionary<string, object> m_dictAnOutput = null; //key is the GUID "GUID":1234-abcd-*-*    //unique id linked to the item
        //Example:  "point1"                // the name shows on screen
        //          "limit1"                // the first limit. if the second is absent, use "=" to compare
        public TestPlan()
        {
            TPList = new List<Dictionary<string, string>>();
            m_dictInput = new Dictionary<string, Dictionary<string, object>>();
            m_dictOutput = new Dictionary<string, Dictionary<string, object>>();
        }

        public Dictionary<string, string> GetAnItemByGUID(string GUID)
        {
            if (TPList == null || TPList.Count == 0)
            {
                throw new Exception(string.Format("The TP list is null or empty."));
            }

            try
            {
                foreach (Dictionary<string, string> dictAnItem in TPList)
                {
                    string strGUID = dictAnItem["GUID"];
                    if (true == strGUID.Equals(GUID))
                    {
                        return dictAnItem;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error in get test item by GUID:{0} with error message {1}", GUID, ex.Message));
            }
            return null;
        }

        public Dictionary<string, string> GetAnItemByName(string Name)
        {
            if (TPList == null || TPList.Count == 0)
            {
                throw new Exception(string.Format("The TP list is null or empty."));
            }

            try
            {
                foreach (Dictionary<string, string> dictAnItem in TPList)
                {
                    string strName = dictAnItem["Name"];
                    if (true == strName.Equals(Name))
                    {
                        return dictAnItem;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error in get test item by name:{0} with error message {1}", Name, ex.Message));
            }
            return null;
        }

        public Dictionary<string, object> GetAnInputByGUID(string GUID)
        {
            if (TPList == null || TPList.Count == 0)
            {
                throw new Exception(string.Format("The TP list is null or empty."));
            }

            if (true == m_dictInput.ContainsKey(GUID))
            {
                return m_dictInput[GUID];
            }
            else
            {
                return null;
            }
        }

        public Dictionary<string, object> GetAnOutputByGUID(string GUID)
        {
            if (TPList == null || TPList.Count == 0)
            {
                throw new Exception(string.Format("The TP list is null or empty."));
            }

            if (true == m_dictOutput.ContainsKey(GUID))
            {
                return m_dictOutput[GUID];
            }
            else
            {
                return null;
            }
        }
        public Dictionary<string, object> GetAnOutByName(string Name)
        {
            if (TPList == null || TPList.Count == 0)
            {
                throw new Exception(string.Format("The TP list is null or empty."));
            }

            try
            {
                foreach (KeyValuePair<string, Dictionary<string, object>> kvpAnOutput in m_dictOutput)
                {
                    if (true == Convert.ToString(kvpAnOutput.Value["Name"]).Equals(Name))
                    {
                        return kvpAnOutput.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Error in get test item by name:{0} with error message {1}", Name, ex.Message));
            }
            return null;
        }
    }
}
