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

        public string label_is_tourism = "";
        public string label_act_type = "";
        public string label_wid = "";
        public string label_user = "";
        public string label_time = "";
        public DateTime label_show_time = DateTime.Now;
        public DateTime label_end_time = DateTime.Now;


        private void button_RandomOne_Click(object sender, EventArgs e)
        {
            if (textBox_User.Text.Length == 0)
            {
                MessageBox.Show("请输入一个用户名", "Message Info");
                return;
            }
            label_show_time = DateTime.Now;
        }

        private void button_labelit_Click(object sender, EventArgs e)
        {
            label_end_time = DateTime.Now;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load Map
            gMap.Zoom = 17;
            gMap.MapProvider = GMapProviders.GoogleChinaMap;
            gMap.Manager.Mode = AccessMode.ServerAndCache;//地图加载模式
            gMap.MinZoom = 1;   //最小比例
            gMap.MaxZoom = 23;  //最大比例
            gMap.DragButton = System.Windows.Forms.MouseButtons.Left;//左键拖拽地图
            gMap.Position = new PointLatLng(23, 113);
            // Read User Info
            gPara.MysqlIP = textBox_Server.Text;
            gPara.MysqlPort = textBox_Port.Text;
            gPara.MysqlTableLabel = textBox_table.Text;
        }

        private void textBox_User_TextChanged(object sender, EventArgs e)
        {
            gPara.LabelUser = textBox_User.Text;
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
                return comboBox_other.Text;
            else
                return "null";
        }


    }
}
