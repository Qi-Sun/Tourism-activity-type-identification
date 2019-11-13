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
        public string label_wid = "";
        public string label_user = "";
        public string label_time = "";
        public int label_duration = 0;
        public DateTime label_show_time = DateTime.Now;
        public DateTime label_end_time = DateTime.Now;
        public Dictionary<string, object> label_weibo = null;

        //Gmap 
        public GMapOverlay map_overlay_weibo = new GMapOverlay("WeiboMarker");

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
            label_wid = label_weibo["wid"].ToString();
            // Display weibo
            PointLatLng marker_latlng = new PointLatLng((double)one_weibo["latitude"], (double)one_weibo["longitude"]);
            gMap.Position = marker_latlng;
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
            label_time = label_end_time.ToString();
            label_is_tourism = Get_Label_Tourism();
            label_act_type = Get_Label_ActType();
            // Write to Database
            var sql_insert = gPara.SQL_Insert_LabelResult(label_wid, label_time,
                label_user, label_is_tourism, label_act_type, label_duration);
            Database.Execute_NonQuery(sql_connection_str, sql_insert);
            // UI
            button_labelit.Enabled = false;
            Initialize_Label_Tourism_State();
            Initialize_Label_ActType_State();
        }

        public Dictionary<string, object> Get_Random_Weibo()
        {
            Random randomObject = new Random();
            int random_index = randomObject.Next(1, 5399161);
            var sql_select_weibo = gPara.SQL_Select_OneWeibo(random_index);
            DataTable random_table = Database.DataTable_ExecuteReader(sql_connection_str, sql_select_weibo);
            if (random_table != null && random_table.Rows.Count == 0)
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
            gPara.MysqlIP = textBox_Server.Text;
            gPara.MysqlPort = textBox_Port.Text;
            gPara.MysqlTableLabel = textBox_table.Text;
            //Mysql
            sql_connection_str = Database.GetConnectionString("suzhou", "geosoft", "3702");
            // UI
            richTextBox_weibo.Font = new Font("Times New Roman", 12);
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
            radioButton_act_buy.Checked = false;
            radioButton_act_eat.Checked = false;
            radioButton_act_entertainment.Checked = false;
            radioButton_act_live.Checked = false;
            radioButton_act_notsure.Checked = false;
            radioButton_act_sightseeing.Checked = false;
            radioButton_act_transport.Checked = false;
            radioButton_other.Checked = false;
            comboBox_other.Text = "";
            comboBox_other.SelectedItem = null;
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
            if (radioButton_act_buy.Checked == true)
                return "购";
            else if (radioButton_act_eat.Checked == true)
                return "吃";
            else if (radioButton_act_entertainment.Checked == true)
                return "娱";
            else if (radioButton_act_live.Checked == true)
                return "住";
            else if (radioButton_act_notsure.Checked == true)
                return "不确定";
            else if (radioButton_act_sightseeing.Checked == true)
                return "游";
            else if (radioButton_act_transport.Checked == true)
                return "行";
            else if (radioButton_other.Checked == true)
            {
                return comboBox_other.Text.ToString();
            }
            else
                return "null";
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
    }
}
