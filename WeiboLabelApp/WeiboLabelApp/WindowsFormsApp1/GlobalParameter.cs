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

        public string SQL_Insert_LabelResult(string wid, string time, string user, string tourism, string actType, int durationTime)
        {
            return "REPLACE INTO " + MysqlSchema + "." + MysqlTableLabel + "(" +
                FieldWeiboID + "," + FieldTimestamp + "," + FieldUser + "," + FieldTourism + "," +
                FieldType + "," + FieldDuration +  ")" + " VALUES("  +
                wid + ",'" + time + "','" + user + "','" + tourism + "','" + actType + "'," + durationTime + ");";
        }
    }
}
