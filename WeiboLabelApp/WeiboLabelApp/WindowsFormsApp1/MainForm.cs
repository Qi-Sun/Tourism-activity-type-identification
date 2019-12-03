using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;

namespace Weibo_Label_App
{
    public partial class MainForm : Form
    {
        public GlobalParameter gPara = null;
        public MainForm()
        {
            gPara = new GlobalParameter();
            InitializeComponent();
        }

        //Mysql
        public string sql_connection_str = "";

        // Label
        public string label_is_tourism = "";
        public string label_act_type = "";
        public string label_purpose = "";
        public string label_wid = "";
        public string label_user = "";
        public string label_time = "";
        public int label_duration = 0;
        public DateTime label_show_time = DateTime.Now;
        public DateTime label_end_time = DateTime.Now;
        public Dictionary<string, object> label_weibo = null;

        //Gmap 
        public GMapOverlay map_overlay_weibo = new GMapOverlay("WeiboMarker");

        //Classification system 
        public Dictionary<string, Dictionary<string, List<string>>> classification_act = new Dictionary<string, Dictionary<string, List<string>>>();
        public Dictionary<string, List<string>> classification_purpose = new Dictionary<string, List<string>>();

        public void Initialize_Classification_Act()
        {
            classification_act = new Dictionary<string, Dictionary<string, List<string>>>();
            classification_act.Add("游玩", new Dictionary<string, List<string>>());
            classification_act.Add("餐饮", new Dictionary<string, List<string>>());
            classification_act.Add("住宿", new Dictionary<string, List<string>>());
            classification_act.Add("出行", new Dictionary<string, List<string>>());
            classification_act.Add("购物", new Dictionary<string, List<string>>());
            classification_act.Add("娱乐", new Dictionary<string, List<string>>());
            classification_act.Add("事务", new Dictionary<string, List<string>>());
            classification_act.Add("其他", new Dictionary<string, List<string>>());
            // 游玩
            classification_act["游玩"].Add("定位", new List<string>() { "TBD" });
            classification_act["游玩"].Add("赏景", new List<string>() { "TBD" });
            classification_act["游玩"].Add("感慨", new List<string>() { "TBD" });
            classification_act["游玩"].Add("休闲", new List<string>() { "TBD" });
            classification_act["游玩"].Add("夜游", new List<string>() { "TBD" });
            classification_act["游玩"].Add("戏曲", new List<string>() { "TBD" });
            classification_act["游玩"].Add("追星", new List<string>() { "TBD" });
            // 餐饮
            classification_act["餐饮"].Add("基础", new List<string>() { "快餐", "TBD" });
            classification_act["餐饮"].Add("观光", new List<string>() { "景区", "TBD" });
            classification_act["餐饮"].Add("休闲", new List<string>() { "TBD" });
            classification_act["餐饮"].Add("特色", new List<string>() { "TBD" });
            // 住宿
            classification_act["住宿"].Add("青旅", new List<string>() { "TBD" });
            classification_act["住宿"].Add("宾馆", new List<string>() { "TBD" });
            classification_act["住宿"].Add("酒店", new List<string>() { "TBD" });
            // 出行
            classification_act["出行"].Add("城市间", new List<string>() { "离开" ,"抵达" ,"途中" ,"TBD" });
            classification_act["出行"].Add("市内", new List<string>() { "公共交通", "出租车", "TBD" });
            classification_act["出行"].Add("景区内", new List<string>() { "TBD" });
            // 购物
            classification_act["购物"].Add("日常", new List<string>() { "TBD" });
            classification_act["购物"].Add("特产", new List<string>() { "TBD" });
            classification_act["购物"].Add("定位", new List<string>() { "TBD" });
            // 娱乐
            classification_act["娱乐"].Add("唱歌", new List<string>() { "TBD" });
            classification_act["娱乐"].Add("电影", new List<string>() { "TBD" });
            classification_act["娱乐"].Add("温泉", new List<string>() { "TBD" });
            classification_act["娱乐"].Add("游乐园", new List<string>() { "TBD" });
            // 事务
            classification_act["事务"].Add("婚礼", new List<string>() { "TBD" });
            classification_act["事务"].Add("会议", new List<string>() { "TBD" });
            classification_act["事务"].Add("扫墓", new List<string>() { "TBD" });
            classification_act["事务"].Add("聚会", new List<string>() { "TBD" });
            classification_act["事务"].Add("其他", new List<string>() { "TBD" });
            // 其他
            classification_act["其他"].Add("生活琐事", new List<string>() { "TBD" });
            classification_act["其他"].Add("人生感慨", new List<string>() { "TBD" });
            classification_act["其他"].Add("笑话段子", new List<string>() { "TBD" });
            classification_act["其他"].Add("心灵鸡汤", new List<string>() { "TBD" });
            classification_act["其他"].Add("广告", new List<string>() { "TBD" });
            classification_act["其他"].Add("其他", new List<string>() { "TBD" });
        }

        public bool Update_Classification_Act()
        {
            string type1 = comboBox_type_1.Text;
            string type2 = comboBox_type_2.Text;
            string type3 = comboBox_type_3.Text;
            if (!classification_act.ContainsKey(type1))
                classification_act.Add(type1, new Dictionary<string, List<string>>());
            if (!classification_act[type1].ContainsKey(type2))
                classification_act[type1].Add(type2, new List<string>() { "TBD" });
            if (!classification_act[type1][type2].Contains(type3))
            {
                classification_act[type1][type2].Add(type3);
                return true;
            }
            return false;
        }

        public void Initialize_Classification_Purpose()
        {
            classification_purpose = new Dictionary<string, List<string>>();
            classification_purpose.Add("商务旅游", new List<string>() { "出差", "会议" });
            classification_purpose.Add("养生旅游", new List<string>() { "健身", "养老" });
            classification_purpose.Add("研学旅游", new List<string>() { "考试", "培训" });
            classification_purpose.Add("休闲旅游", new List<string>() { "观光", "度假" });
            classification_purpose.Add("情感旅游", new List<string>() { "婚庆", "探亲访友" });
            classification_purpose.Add("探奇", new List<string>() { "探险", "新奇体验" });
        }

        public bool Update_Classification_Purpose()
        {
            string type1 = comboBox_purpose_1.Text;
            string type2 = comboBox_purpose_2.Text;
            if (!classification_purpose.ContainsKey(type1))
                classification_purpose.Add(type1, new List<string>() { "TBD" });
            if (!classification_purpose[type1].Contains(type2)) 
            { 
                classification_purpose[type1].Add(type2);
                return true;
            }
            return false;
        }

        public void Refresh_ComboBox_Act_1()
        {
            comboBox_type_1.Items.Clear();
            foreach (var item in classification_act)
            {
                comboBox_type_1.Items.Add(item.Key);
            }
            comboBox_type_1.Text = "";
            comboBox_type_2.Text = "";
            comboBox_type_3.Text = "";
        }

        public void Refresh_Combox_Act_2()
        {
            if (comboBox_type_1.Text != "" && classification_act.ContainsKey(comboBox_type_1.Text))
            {
                comboBox_type_2.Items.Clear();
                foreach (var item in classification_act[comboBox_type_1.Text])
                {
                    comboBox_type_2.Items.Add(item.Key);
                }
                comboBox_type_2.Text = "";
            }
        }

        public void Refresh_ComboBox_Purpose_1()
        {
            comboBox_purpose_1.Items.Clear();
            foreach (var item in classification_purpose)
            {
                comboBox_purpose_1.Items.Add(item.Key);
            }
            comboBox_purpose_1.Text = "";
            comboBox_purpose_2.Text = "";
        }

        public void Refresh_ComboBox_Purpose_2()
        {
            if (comboBox_purpose_1.Text != "" && classification_purpose.ContainsKey(comboBox_purpose_1.Text))
            {
                comboBox_purpose_2.Items.Clear();
                foreach (var item in classification_purpose[comboBox_purpose_1.Text])
                {
                    comboBox_purpose_2.Items.Add(item);
                }
            }
            comboBox_purpose_2.Text = "";
        }

        public void Refresh_Combox_Act_3()
        {
            if (comboBox_type_2.Text != "" && classification_act[comboBox_type_1.Text].ContainsKey(comboBox_type_2.Text))
            {
                comboBox_type_3.Items.Clear();
                foreach (var item in classification_act[comboBox_type_1.Text][comboBox_type_2.Text])
                {
                    comboBox_type_3.Items.Add(item);
                }
                
            }
            comboBox_type_3.Text = "";
        }

        private void button_RandomOne_Click(object sender, EventArgs e)
        {
            if (textBox_User.Text.Length == 0)
            {
                MessageBox.Show("请输入一个用户名", "Message Info");
                return;
            }
            // Clear
            map_overlay_weibo.Markers.Clear();
            label_show_time = DateTime.Now;
            button_showpic.Enabled = false;
            button_labelit.Enabled = true;
            richTextBox_weibo.Text = "";
            GMapOverlayClear();
            Initialize_Label_ActType_State();
            Initialize_Label_Tourism_State();
            // Random One Weibo
            label_weibo = Get_Random_Weibo();
            var one_weibo = label_weibo;
            if (one_weibo == null)
                return;
            label_wid = label_weibo["wid"].ToString();
            // Display weibo
            PointLatLng marker_latlng = new PointLatLng((double)one_weibo["latitude"], (double)one_weibo["longitude"]);
            gMap.Position = marker_latlng;
            gMap.Zoom = 13;
            GMapMarker marker_weibo = new GMarkerGoogle(marker_latlng, GMarkerGoogleType.blue);
            marker_weibo.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker_weibo.ToolTipText = Weibo_Info_ToString(one_weibo); 
            map_overlay_weibo.Markers.Add(marker_weibo);
            gMap.Refresh();
            // MessageBox.Show(Weibo_Info_ToString(one_weibo));
            // Show Picture
            if (one_weibo["pic_url"].ToString().Length > 0)
                button_showpic.Enabled = true;
            // Show Text in RichTextBox
            richTextBox_weibo.AppendText(Weibo_Info_ToString_Brief(one_weibo));
            // ComboBox
            Refresh_ComboBox_Act_1();
            Refresh_ComboBox_Purpose_1();
            return;
        }

        public string Weibo_Info_ToString(Dictionary<string, object> weibo_info)
        {
            string str_weibo = "";
            str_weibo += "WID:" + weibo_info["wid"].ToString() + "\n";
            str_weibo += "UID:" + weibo_info["userid"].ToString() + "\n";
            str_weibo += string.Format("GEO:({0},{1})\n", weibo_info["latitude"].ToString(),
                weibo_info["longitude"].ToString());
            str_weibo += "Home:" + weibo_info["source"].ToString() + "\n";
            str_weibo += "Time:" + weibo_info["time"].ToString() + "\n";
            str_weibo += "Text:" + weibo_info["text"].ToString() + "\n";
            if (weibo_info["poiid"].ToString().Length > 1)
            {
                str_weibo += "POI:" + weibo_info["poiid"].ToString() + "\n";
                str_weibo += "Place:" + weibo_info["poi_title"].ToString() + "\n";
            }
            return str_weibo;
        }

        public string Weibo_Info_ToString_Brief(Dictionary<string, object> weibo_info)
        {
            string str_weibo = "";
            str_weibo += "Home:" + weibo_info["source"].ToString() + "\n";
            str_weibo += "Time:" + weibo_info["time"].ToString() + "\n";
            str_weibo += "Text:" + weibo_info["text"].ToString() + "\n";
            if (weibo_info["poiid"].ToString().Length > 1)
            {
                str_weibo += "POI:" + weibo_info["poiid"].ToString() + "\n";
                str_weibo += "Place:" + weibo_info["poi_title"].ToString() + "\n";
            }
            return str_weibo;
        }

        private void button_labelit_Click(object sender, EventArgs e)
        {
            // Label result
            label_end_time = DateTime.Now;
            var delta_time = label_end_time - label_show_time;
            label_duration = (int) delta_time.TotalSeconds;
            label_time = label_end_time.ToString("yyyy/MM/dd HH:hh:ss");
            label_is_tourism = Get_Label_Tourism();
            label_act_type = Get_Label_ActType();
            label_purpose = Get_Label_Purpose();

            // Write to Database
            var sql_insert = gPara.SQL_Insert_LabelResult_1203(label_wid, label_time,
                label_user, label_is_tourism, label_act_type, label_purpose,
                comboBox_type_1.Text, comboBox_type_2.Text, comboBox_type_3.Text,
                comboBox_purpose_1.Text, comboBox_purpose_2.Text,
                label_duration);
            Database.Execute_NonQuery(sql_connection_str, sql_insert);
            // UI
            button_labelit.Enabled = false;
            Initialize_Label_Tourism_State();
            Initialize_Label_ActType_State();
            // ComboBox 
            Update_Classification_Act();
            Update_Classification_Purpose();
        }

        public Dictionary<string, object> Get_Random_Weibo()
        {
            Random randomObject = new Random();
            string sql_select_weibo = "";
            if (checkBox_only_waidi.Checked == false)
            {
                int random_index = randomObject.Next(1, 5399161);
                sql_select_weibo = gPara.SQL_Select_OneWeibo(random_index);
            }
            else
            {
                int random_index = randomObject.Next(1, 1067162);
                sql_select_weibo = gPara.SQL_Select_nolocal_OneWeibo(random_index);
            }
            DataTable random_table = Database.DataTable_ExecuteReader(sql_connection_str, sql_select_weibo);
            if (random_table == null || random_table.Rows.Count == 0)
            {
                MessageBox.Show("No data");
                return null;
            }
            var random_record = random_table.Rows[0];
            Dictionary<string, object> weibo_info = new Dictionary<string, object>();
            weibo_info["wid"] = random_record["id"].ToString();
            weibo_info["text"] = random_record["text"].ToString();
            weibo_info["latitude"] = double.Parse(random_record["latitude"].ToString());
            weibo_info["longitude"] = double.Parse(random_record["longitude"].ToString());
            weibo_info["userid"] = random_record["userid"].ToString();
            weibo_info["time"] = random_record["time"].ToString();
            weibo_info["source"] = random_record["user_source"] != null ? random_record["user_source"].ToString() : "<UNK>";
            weibo_info["poiid"] = random_record["checkin_poiid"] != null ? random_record["checkin_poiid"].ToString() : "";
            weibo_info["poi_title"] = random_record["checkin_title"] != null ? random_record["checkin_title"].ToString() : "";
            weibo_info["pic_url"] = random_record["original_pic"] != null ? random_record["original_pic"].ToString() : "";
            return weibo_info;
        }

        public void Insert_label_record()
        {
            var sql_insert = gPara.SQL_Insert_LabelResult(label_wid, label_time, label_user, label_is_tourism, label_act_type, label_duration);
            Database.Execute_NonQuery(sql_connection_str, sql_insert);
            return;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(DateTime.Now.ToString("yyyy/MM/dd HH:hh:ss"));
            // Load Map
            gMap.MapProvider = GMapProviders.GoogleChinaMap;
            gMap.Manager.Mode = AccessMode.ServerAndCache;//地图加载模式
            gMap.MinZoom = 1;   //最小比例
            gMap.MaxZoom = 23;  //最大比例
            gMap.DragButton = System.Windows.Forms.MouseButtons.Left;//左键拖拽地图
            gMap.Position = new PointLatLng(31.25, 120.5);
            gMap.Zoom = 13;
            gMap.Overlays.Add(this.map_overlay_weibo);
            // Read User Info
            gPara.MysqlIP = "222.29.117.240";
            gPara.MysqlPort = "6667";
            gPara.MysqlTableLabel = "suzhou_weibo_labeled_1112";
            //Mysql
            sql_connection_str = Database.GetConnectionString("suzhou", "geosoft", "3702");
            // UI
            richTextBox_weibo.Font = new Font("Times New Roman", 12);
            // Build classification
            Initialize_Classification_Act();
            Initialize_Classification_Purpose();
        }

        public void GMapOverlayClear()
        {
            foreach (GMapOverlay item in gMap.Overlays)
            {
                item.Markers.Clear();
            }
        }

        private void textBox_User_TextChanged(object sender, EventArgs e)
        {
            gPara.LabelUser = textBox_User.Text;
            label_user = textBox_User.Text;
        }

        public void Initialize_Label_Tourism_State()
        {
            radioButton_toursim_true.Checked = false;
            radioButton_toursim_false.Checked = false;
            radioButton_toursim_notsure.Checked = false;
        }

        public void Initialize_Label_ActType_State()
        {
            /*
            radioButton_act_buy.Checked = false;
            radioButton_act_eat.Checked = false;
            radioButton_act_entertainment.Checked = false;
            radioButton_act_live.Checked = false;
            radioButton_act_notsure.Checked = false;
            radioButton_act_sightseeing.Checked = false;
            radioButton_act_transport.Checked = false;
            radioButton_other.Checked = false;
            comboBox_other.Text = "";
            comboBox_other.SelectedItem = null;*/
        }

        public string Get_Label_Tourism()
        {
            if (radioButton_toursim_true.Checked == true)
                return "true";
            else if (radioButton_toursim_false.Checked == true)
                return "false";
            else if (radioButton_toursim_notsure.Checked == true)
                return "not_sure";
            else
                return "null";
        }

        public string Get_Label_ActType()
        {
            return textbox_type.Text;
        }

        public string Get_Label_Purpose()
        {
            return textBox_purpose.Text;
        }

        private void button_showpic_Click(object sender, EventArgs e)
        {
            ShowPictureForm form_show_picture = new ShowPictureForm(label_weibo["pic_url"].ToString());
            if(form_show_picture.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void comboBox_other_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox_other.SelectedItem.ToString());
        }

        private void comboBox_type_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_act_type_text();
            Refresh_Combox_Act_2();
        }

        private void comboBox_type_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_act_type_text();
            Refresh_Combox_Act_3();
        }

        private void comboBox_type_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_act_type_text();
        }

        public void update_act_type_text()
        {
            string type1 = comboBox_type_1.Text;
            string type2 = comboBox_type_2.Text;
            string type3 = comboBox_type_3.Text;
            type1 = type1 != "" ? type1 : "Null";
            type2 = type2 != "" ? type2 : "Null";
            type3 = type3 != "" ? type3 : "Null";
            textbox_type.Text = string.Format("{0}-{1}-{2}", type1, type2, type3);
        }

        private void comboBox_purpose_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_purpose_text();
            Refresh_ComboBox_Purpose_2();
        }

        private void comboBox_purpose_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            update_purpose_text();
        }

        public void update_purpose_text()
        {
            string type1 = comboBox_purpose_1.Text;
            string type2 = comboBox_purpose_2.Text;
            type1 = type1 != "" ? type1 : "Null";
            type2 = type2 != "" ? type2 : "Null";
            textBox_purpose.Text = string.Format("{0}-{1}", type1, type2);
        }

        private void comboBox_type_1_TextChanged(object sender, EventArgs e)
        {
            update_act_type_text();
        }

        private void comboBox_type_2_TextChanged(object sender, EventArgs e)
        {
            update_act_type_text();
        }

        private void comboBox_type_3_TextChanged(object sender, EventArgs e)
        {
            update_act_type_text();
        }

        private void comboBox_purpose_1_TextChanged(object sender, EventArgs e)
        {
            update_purpose_text();
        }

        private void comboBox_purpose_2_TextChanged(object sender, EventArgs e)
        {
            update_purpose_text();
        }
    }
}
