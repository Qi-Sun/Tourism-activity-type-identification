﻿namespace WindowsFormsApp1
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxMap = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.textBox_table = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Server = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_User = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxLabel = new System.Windows.Forms.GroupBox();
            this.groupBox_weibo = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox_weibo = new System.Windows.Forms.RichTextBox();
            this.groupBox_act = new System.Windows.Forms.GroupBox();
            this.comboBox_other = new System.Windows.Forms.ComboBox();
            this.radioButton_other = new System.Windows.Forms.RadioButton();
            this.radioButton_act_entertainment = new System.Windows.Forms.RadioButton();
            this.radioButton_act_buy = new System.Windows.Forms.RadioButton();
            this.radioButton_act_transport = new System.Windows.Forms.RadioButton();
            this.radioButton_act_live = new System.Windows.Forms.RadioButton();
            this.radioButton_act_eat = new System.Windows.Forms.RadioButton();
            this.radioButton_act_sightseeing = new System.Windows.Forms.RadioButton();
            this.radioButton_act_notsure = new System.Windows.Forms.RadioButton();
            this.groupBox_Toursim = new System.Windows.Forms.GroupBox();
            this.radioButton_toursim_false = new System.Windows.Forms.RadioButton();
            this.radioButton_toursim_notsure = new System.Windows.Forms.RadioButton();
            this.radioButton_toursim_true = new System.Windows.Forms.RadioButton();
            this.button_labelit = new System.Windows.Forms.Button();
            this.button_RandomOne = new System.Windows.Forms.Button();
            this.groupBoxInfo.SuspendLayout();
            this.groupBoxLabel.SuspendLayout();
            this.groupBox_weibo.SuspendLayout();
            this.groupBox_act.SuspendLayout();
            this.groupBox_Toursim.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMap
            // 
            this.groupBoxMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMap.Location = new System.Drawing.Point(12, 12);
            this.groupBoxMap.Name = "groupBoxMap";
            this.groupBoxMap.Size = new System.Drawing.Size(812, 646);
            this.groupBoxMap.TabIndex = 0;
            this.groupBoxMap.TabStop = false;
            this.groupBoxMap.Text = "Map";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 661);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1101, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInfo.Controls.Add(this.textBox_table);
            this.groupBoxInfo.Controls.Add(this.label4);
            this.groupBoxInfo.Controls.Add(this.textBox_Port);
            this.groupBoxInfo.Controls.Add(this.label3);
            this.groupBoxInfo.Controls.Add(this.textBox_Server);
            this.groupBoxInfo.Controls.Add(this.label2);
            this.groupBoxInfo.Controls.Add(this.textBox_User);
            this.groupBoxInfo.Controls.Add(this.label1);
            this.groupBoxInfo.Location = new System.Drawing.Point(830, 12);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(259, 155);
            this.groupBoxInfo.TabIndex = 2;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Info";
            // 
            // textBox_table
            // 
            this.textBox_table.Location = new System.Drawing.Point(86, 118);
            this.textBox_table.Name = "textBox_table";
            this.textBox_table.ReadOnly = true;
            this.textBox_table.Size = new System.Drawing.Size(167, 21);
            this.textBox_table.TabIndex = 7;
            this.textBox_table.Text = "suzhou_weibo_labeled";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Table";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(86, 87);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(167, 21);
            this.textBox_Port.TabIndex = 4;
            this.textBox_Port.Text = "6667";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Port";
            // 
            // textBox_Server
            // 
            this.textBox_Server.Location = new System.Drawing.Point(86, 57);
            this.textBox_Server.Name = "textBox_Server";
            this.textBox_Server.Size = new System.Drawing.Size(167, 21);
            this.textBox_Server.TabIndex = 3;
            this.textBox_Server.Text = "222.29.117.240";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Server";
            // 
            // textBox_User
            // 
            this.textBox_User.Location = new System.Drawing.Point(86, 26);
            this.textBox_User.Name = "textBox_User";
            this.textBox_User.Size = new System.Drawing.Size(167, 21);
            this.textBox_User.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "User";
            // 
            // groupBoxLabel
            // 
            this.groupBoxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLabel.Controls.Add(this.groupBox_weibo);
            this.groupBoxLabel.Controls.Add(this.groupBox_act);
            this.groupBoxLabel.Controls.Add(this.groupBox_Toursim);
            this.groupBoxLabel.Controls.Add(this.button_labelit);
            this.groupBoxLabel.Controls.Add(this.button_RandomOne);
            this.groupBoxLabel.Location = new System.Drawing.Point(830, 173);
            this.groupBoxLabel.Name = "groupBoxLabel";
            this.groupBoxLabel.Size = new System.Drawing.Size(259, 485);
            this.groupBoxLabel.TabIndex = 3;
            this.groupBoxLabel.TabStop = false;
            this.groupBoxLabel.Text = "Label";
            // 
            // groupBox_weibo
            // 
            this.groupBox_weibo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_weibo.Controls.Add(this.button1);
            this.groupBox_weibo.Controls.Add(this.richTextBox_weibo);
            this.groupBox_weibo.Location = new System.Drawing.Point(6, 292);
            this.groupBox_weibo.Name = "groupBox_weibo";
            this.groupBox_weibo.Size = new System.Drawing.Size(247, 187);
            this.groupBox_weibo.TabIndex = 4;
            this.groupBox_weibo.TabStop = false;
            this.groupBox_weibo.Text = "Weibo";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(6, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(235, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Show Picture";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // richTextBox_weibo
            // 
            this.richTextBox_weibo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_weibo.Location = new System.Drawing.Point(6, 20);
            this.richTextBox_weibo.Name = "richTextBox_weibo";
            this.richTextBox_weibo.Size = new System.Drawing.Size(235, 132);
            this.richTextBox_weibo.TabIndex = 0;
            this.richTextBox_weibo.Text = "";
            // 
            // groupBox_act
            // 
            this.groupBox_act.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_act.Controls.Add(this.comboBox_other);
            this.groupBox_act.Controls.Add(this.radioButton_other);
            this.groupBox_act.Controls.Add(this.radioButton_act_entertainment);
            this.groupBox_act.Controls.Add(this.radioButton_act_buy);
            this.groupBox_act.Controls.Add(this.radioButton_act_transport);
            this.groupBox_act.Controls.Add(this.radioButton_act_live);
            this.groupBox_act.Controls.Add(this.radioButton_act_eat);
            this.groupBox_act.Controls.Add(this.radioButton_act_sightseeing);
            this.groupBox_act.Controls.Add(this.radioButton_act_notsure);
            this.groupBox_act.Location = new System.Drawing.Point(6, 143);
            this.groupBox_act.Name = "groupBox_act";
            this.groupBox_act.Size = new System.Drawing.Size(247, 143);
            this.groupBox_act.TabIndex = 3;
            this.groupBox_act.TabStop = false;
            this.groupBox_act.Text = "ActivatyType";
            // 
            // comboBox_other
            // 
            this.comboBox_other.FormattingEnabled = true;
            this.comboBox_other.Location = new System.Drawing.Point(78, 112);
            this.comboBox_other.Name = "comboBox_other";
            this.comboBox_other.Size = new System.Drawing.Size(140, 20);
            this.comboBox_other.TabIndex = 8;
            // 
            // radioButton_other
            // 
            this.radioButton_other.AutoSize = true;
            this.radioButton_other.Location = new System.Drawing.Point(25, 113);
            this.radioButton_other.Name = "radioButton_other";
            this.radioButton_other.Size = new System.Drawing.Size(47, 16);
            this.radioButton_other.TabIndex = 7;
            this.radioButton_other.TabStop = true;
            this.radioButton_other.Text = "其他";
            this.radioButton_other.UseVisualStyleBackColor = true;
            // 
            // radioButton_act_entertainment
            // 
            this.radioButton_act_entertainment.AutoSize = true;
            this.radioButton_act_entertainment.Location = new System.Drawing.Point(183, 85);
            this.radioButton_act_entertainment.Name = "radioButton_act_entertainment";
            this.radioButton_act_entertainment.Size = new System.Drawing.Size(35, 16);
            this.radioButton_act_entertainment.TabIndex = 6;
            this.radioButton_act_entertainment.TabStop = true;
            this.radioButton_act_entertainment.Text = "娱";
            this.radioButton_act_entertainment.UseVisualStyleBackColor = true;
            // 
            // radioButton_act_buy
            // 
            this.radioButton_act_buy.AutoSize = true;
            this.radioButton_act_buy.Location = new System.Drawing.Point(109, 85);
            this.radioButton_act_buy.Name = "radioButton_act_buy";
            this.radioButton_act_buy.Size = new System.Drawing.Size(35, 16);
            this.radioButton_act_buy.TabIndex = 5;
            this.radioButton_act_buy.TabStop = true;
            this.radioButton_act_buy.Text = "购";
            this.radioButton_act_buy.UseVisualStyleBackColor = true;
            // 
            // radioButton_act_transport
            // 
            this.radioButton_act_transport.AutoSize = true;
            this.radioButton_act_transport.Location = new System.Drawing.Point(25, 85);
            this.radioButton_act_transport.Name = "radioButton_act_transport";
            this.radioButton_act_transport.Size = new System.Drawing.Size(35, 16);
            this.radioButton_act_transport.TabIndex = 4;
            this.radioButton_act_transport.TabStop = true;
            this.radioButton_act_transport.Text = "行";
            this.radioButton_act_transport.UseVisualStyleBackColor = true;
            // 
            // radioButton_act_live
            // 
            this.radioButton_act_live.AutoSize = true;
            this.radioButton_act_live.Location = new System.Drawing.Point(183, 56);
            this.radioButton_act_live.Name = "radioButton_act_live";
            this.radioButton_act_live.Size = new System.Drawing.Size(35, 16);
            this.radioButton_act_live.TabIndex = 3;
            this.radioButton_act_live.TabStop = true;
            this.radioButton_act_live.Text = "住";
            this.radioButton_act_live.UseVisualStyleBackColor = true;
            // 
            // radioButton_act_eat
            // 
            this.radioButton_act_eat.AutoSize = true;
            this.radioButton_act_eat.Location = new System.Drawing.Point(109, 56);
            this.radioButton_act_eat.Name = "radioButton_act_eat";
            this.radioButton_act_eat.Size = new System.Drawing.Size(35, 16);
            this.radioButton_act_eat.TabIndex = 2;
            this.radioButton_act_eat.TabStop = true;
            this.radioButton_act_eat.Text = "吃";
            this.radioButton_act_eat.UseVisualStyleBackColor = true;
            // 
            // radioButton_act_sightseeing
            // 
            this.radioButton_act_sightseeing.AutoSize = true;
            this.radioButton_act_sightseeing.Location = new System.Drawing.Point(25, 56);
            this.radioButton_act_sightseeing.Name = "radioButton_act_sightseeing";
            this.radioButton_act_sightseeing.Size = new System.Drawing.Size(35, 16);
            this.radioButton_act_sightseeing.TabIndex = 1;
            this.radioButton_act_sightseeing.TabStop = true;
            this.radioButton_act_sightseeing.Text = "游";
            this.radioButton_act_sightseeing.UseVisualStyleBackColor = true;
            // 
            // radioButton_act_notsure
            // 
            this.radioButton_act_notsure.AutoSize = true;
            this.radioButton_act_notsure.Location = new System.Drawing.Point(90, 21);
            this.radioButton_act_notsure.Name = "radioButton_act_notsure";
            this.radioButton_act_notsure.Size = new System.Drawing.Size(59, 16);
            this.radioButton_act_notsure.TabIndex = 0;
            this.radioButton_act_notsure.TabStop = true;
            this.radioButton_act_notsure.Text = "不确定";
            this.radioButton_act_notsure.UseVisualStyleBackColor = true;
            // 
            // groupBox_Toursim
            // 
            this.groupBox_Toursim.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Toursim.Controls.Add(this.radioButton_toursim_false);
            this.groupBox_Toursim.Controls.Add(this.radioButton_toursim_notsure);
            this.groupBox_Toursim.Controls.Add(this.radioButton_toursim_true);
            this.groupBox_Toursim.Location = new System.Drawing.Point(6, 81);
            this.groupBox_Toursim.Name = "groupBox_Toursim";
            this.groupBox_Toursim.Size = new System.Drawing.Size(247, 56);
            this.groupBox_Toursim.TabIndex = 2;
            this.groupBox_Toursim.TabStop = false;
            this.groupBox_Toursim.Text = "Toursim";
            // 
            // radioButton_toursim_false
            // 
            this.radioButton_toursim_false.AutoSize = true;
            this.radioButton_toursim_false.Location = new System.Drawing.Point(164, 20);
            this.radioButton_toursim_false.Name = "radioButton_toursim_false";
            this.radioButton_toursim_false.Size = new System.Drawing.Size(77, 16);
            this.radioButton_toursim_false.TabIndex = 2;
            this.radioButton_toursim_false.TabStop = true;
            this.radioButton_toursim_false.Text = "不是 旅游";
            this.radioButton_toursim_false.UseVisualStyleBackColor = true;
            // 
            // radioButton_toursim_notsure
            // 
            this.radioButton_toursim_notsure.AutoSize = true;
            this.radioButton_toursim_notsure.Location = new System.Drawing.Point(90, 20);
            this.radioButton_toursim_notsure.Name = "radioButton_toursim_notsure";
            this.radioButton_toursim_notsure.Size = new System.Drawing.Size(59, 16);
            this.radioButton_toursim_notsure.TabIndex = 1;
            this.radioButton_toursim_notsure.TabStop = true;
            this.radioButton_toursim_notsure.Text = "不确定";
            this.radioButton_toursim_notsure.UseVisualStyleBackColor = true;
            // 
            // radioButton_toursim_true
            // 
            this.radioButton_toursim_true.AutoSize = true;
            this.radioButton_toursim_true.Location = new System.Drawing.Point(6, 20);
            this.radioButton_toursim_true.Name = "radioButton_toursim_true";
            this.radioButton_toursim_true.Size = new System.Drawing.Size(65, 16);
            this.radioButton_toursim_true.TabIndex = 0;
            this.radioButton_toursim_true.TabStop = true;
            this.radioButton_toursim_true.Text = "是 旅游";
            this.radioButton_toursim_true.UseVisualStyleBackColor = true;
            // 
            // button_labelit
            // 
            this.button_labelit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_labelit.Location = new System.Drawing.Point(154, 20);
            this.button_labelit.Name = "button_labelit";
            this.button_labelit.Size = new System.Drawing.Size(75, 54);
            this.button_labelit.TabIndex = 1;
            this.button_labelit.Text = "标注";
            this.button_labelit.UseVisualStyleBackColor = true;
            this.button_labelit.Click += new System.EventHandler(this.button_labelit_Click);
            // 
            // button_RandomOne
            // 
            this.button_RandomOne.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_RandomOne.Location = new System.Drawing.Point(30, 20);
            this.button_RandomOne.Name = "button_RandomOne";
            this.button_RandomOne.Size = new System.Drawing.Size(71, 55);
            this.button_RandomOne.TabIndex = 0;
            this.button_RandomOne.Text = "随机一个";
            this.button_RandomOne.UseVisualStyleBackColor = true;
            this.button_RandomOne.Click += new System.EventHandler(this.button_RandomOne_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 683);
            this.Controls.Add(this.groupBoxLabel);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxMap);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.groupBoxLabel.ResumeLayout(false);
            this.groupBox_weibo.ResumeLayout(false);
            this.groupBox_act.ResumeLayout(false);
            this.groupBox_act.PerformLayout();
            this.groupBox_Toursim.ResumeLayout(false);
            this.groupBox_Toursim.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMap;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.GroupBox groupBoxLabel;
        private System.Windows.Forms.TextBox textBox_table;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Server;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_User;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_act;
        private System.Windows.Forms.RadioButton radioButton_other;
        private System.Windows.Forms.RadioButton radioButton_act_entertainment;
        private System.Windows.Forms.RadioButton radioButton_act_buy;
        private System.Windows.Forms.RadioButton radioButton_act_transport;
        private System.Windows.Forms.RadioButton radioButton_act_live;
        private System.Windows.Forms.RadioButton radioButton_act_eat;
        private System.Windows.Forms.RadioButton radioButton_act_sightseeing;
        private System.Windows.Forms.RadioButton radioButton_act_notsure;
        private System.Windows.Forms.GroupBox groupBox_Toursim;
        private System.Windows.Forms.RadioButton radioButton_toursim_false;
        private System.Windows.Forms.RadioButton radioButton_toursim_notsure;
        private System.Windows.Forms.RadioButton radioButton_toursim_true;
        private System.Windows.Forms.Button button_labelit;
        private System.Windows.Forms.Button button_RandomOne;
        private System.Windows.Forms.GroupBox groupBox_weibo;
        private System.Windows.Forms.ComboBox comboBox_other;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox_weibo;
    }
}
