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
        //public string SettingTags_ = string.Empty;
        //public string SettingTags_ = string.Empty;
        //public string SettingTags_ = string.Empty;
        #endregion Predefined Tags: TestPlan

        public Settings()
        {

        }
        public static void Load(string FileName)
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
                if(true == property.Name.Equals(Settings.))
            }

            #endregion Load Tags

        }

        private void LoadTags_Testplan()
        {
            
        }
    }
}
