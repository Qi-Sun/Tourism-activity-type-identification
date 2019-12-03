using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weibo_Label_App
{
    public class GlobalParameter
    {
        public string MysqlIP = "";
        public string MysqlPort = "";

        public string MysqlTableWeibo = "suzhou_weibos_1112";
        public string MysqlTableWeiboNoLocal = "suzhou_weibos_nolocal_1112";
        public string MysqlTableLabel = "suzhou_weibo_labeled_1112";
        public string MysqlSchema = "suzhou";

        public string MysqlUser = "";
        public string MysqlPassword = "";

        public string FieldWeiboID = "wid";
        public string FieldTimestamp = "time";
        public string FieldUser = "label_user";
        public string FieldTourism = "label_is_tourism";
        public string FieldType = "label_act_type";
        public string FieldDuration = "label_duration_time";

        public string LabelUser = "NULL";
        public string SQL_Select_OneWeibo(int random_index)
        {
            return "SELECT * FROM " + MysqlSchema + "." + MysqlTableWeibo + " WHERE pkey = " + random_index + " ;";
        }

        public string SQL_Select_nolocal_OneWeibo(int random_index)
        {
            return "SELECT * FROM " + MysqlSchema + "." + MysqlTableWeiboNoLocal + " WHERE pkey = " + random_index + " ;";
        }

        public string SQL_Insert_LabelResult(string wid, string time, string user, string tourism, string actType, int durationTime)
        {
            return "REPLACE INTO " + MysqlSchema + "." + MysqlTableLabel + "(" +
                FieldWeiboID + "," + FieldTimestamp + "," + FieldUser + "," + FieldTourism + "," +
                FieldType + "," + FieldDuration +  ")" + " VALUES("  +
                wid + ",'" + time + "','" + user + "','" + tourism + "','" + actType + "'," + durationTime + ");";
        }

        public string SQL_Insert_LabelResult_1203(string wid, string time, string user, string tourism, string actType,string purpose,
            string act_1,string act_2,string act_3,string purpose_1,string purpose_2, int durationTime)
        {
            return string.Format(@"REPLACE INTO suzhou.suzhou_weibo_labeled_1203 (wid,time,label_user,label_is_tourism,label_act_type,label_purpose,label_act_level_1,
label_act_level_2,label_act_level_3,label_purpose_level_1,label_purpose_level_2,label_duration_time) VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}');",
wid, time, user, tourism, actType, purpose, act_1, act_2, act_3, purpose_1, purpose_2, durationTime);
        }
    }
}
