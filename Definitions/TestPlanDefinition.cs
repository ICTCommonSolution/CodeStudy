using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using Newtonsoft.Json.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodingStudy
{
    public class Settings
    {
        #region Predefined Tags: TestPlan
        public string SettingTags_Sequence = string.Empty;
        public string SettingTags_Properties = string.Empty;
        public string SettingTags_TestName = string.Empty;
        public string SettingTags_ResourceName = string.Empty;
        public string SettingTags_MethodName = string.Empty;
        public string SettingTags_Input = string.Empty;
        public string SettingTags_Output = string.Empty;
        public string SettingTags_MeasPointID = string.Empty;
        public string SettingTags_MeasPointName = string.Empty;
        public string SettingTags_Limit1 = string.Empty;
        public string SettingTags_Limit2 = string.Empty;
        //public string m_SettingTags_ = string.Empty;
        //public string m_SettingTags_ = string.Empty;
        //public string m_SettingTags_ = string.Empty;
        #endregion Predefined Tags: TestPlan

        public Settings()
        {
            Load("settings.json");            
        }
        public void Load(string FileName)
        {
            try
            {
                if (false == System.IO.File.Exists(FileName))
                {
                    throw new Exception(string.Format("The file \"{0}\"does not exist.", FileName));
                }
                else
                {//do nothing
                }
                #region Load Tags
                StreamReader file = File.OpenText(FileName);
                JsonTextReader reader = new JsonTextReader(file);
                JObject joSettings = (JObject)JToken.ReadFrom(reader);

                foreach (JToken jt in joSettings.Children())
                {
                    var property = jt as JProperty;
                    if (true == property.Name.ToLower().Equals("testplan_tag"))
                    {
                        LoadTags_Testplan((JObject)joSettings["Testplan_Tag"]);
                    }
                    //else if (true == property.Name.ToLower().Equals("testplan_tag"))
                    //{
                    
                    //}
                    else { //do nothing
                    }
                }

                reader.Close();
                file.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            #endregion Load Tags

        }
        private void LoadTags_Testplan(JObject TPSec)
        {
            foreach (JToken jt in TPSec.Children())
            {
                if (jt.Type == JTokenType.Object && ((JObject)jt).Count > 1)
                {
                    LoadTags_Testplan((JObject)jt);
                }
                else { //do nothing 
                }

                var jPrpt = jt as JProperty;
                if (true == jPrpt.Name.Equals("SettingTags_Sequence"))
                {
                    SettingTags_Sequence = (string)jPrpt.Value;
                }
                else if (true == jPrpt.Name.Equals("SettingTags_Properties"))
                {
                    SettingTags_Properties = (string)jPrpt.Value;
                }
                else if (true == jPrpt.Name.Equals("SettingTags_TestName"))
                {
                    SettingTags_TestName = (string)jPrpt.Value;
                }
                else if (true == jPrpt.Name.Equals("SettingTags_ResourceName"))
                {
                    SettingTags_ResourceName = (string)jPrpt.Value;
                }
                else if (true == jPrpt.Name.Equals("SettingTags_MethodName"))
                {
                    SettingTags_MethodName = (string)jPrpt.Value;
                }
                else if (true == jPrpt.Name.Equals("SettingTags_Input"))
                {
                    SettingTags_Input = (string)jPrpt.Value;
                }
                else if (true == jPrpt.Name.Equals("SettingTags_Output"))
                {
                    SettingTags_Output = (string)jPrpt.Value;
                }
                else if (true == jPrpt.Name.Equals("SettingTags_MeasPointID"))
                {
                    SettingTags_MeasPointID = (string)jPrpt.Value;
                }
                else if (true == jPrpt.Name.Equals("SettingTags_MeasPointName"))
                {
                    SettingTags_MeasPointName = (string)jPrpt.Value;
                }
                else if (true == jPrpt.Name.Equals("SettingTags_Limit1"))//low limit or the value to equal to.
                {
                    SettingTags_Limit1 = (string)jPrpt.Value;
                }
                else if(true == jPrpt.Name.Equals("SettingTags_Limit2"))//high limit
                {
                    SettingTags_Limit1 = (string)jPrpt.Value;
                }
                //else if (true == jPrpt.Name.Equals("SettingTags_"))
                //{
                //    m_SettingTags_ = (string)jPrpt.Value;
                //}
                //else if (true == jPrpt.Name.Equals("SettingTags_"))
                //{
                //    m_SettingTags_ = (string)jPrpt.Value;
                //}
                else
                { //do nothing
                }
            }
        }
    }
}
