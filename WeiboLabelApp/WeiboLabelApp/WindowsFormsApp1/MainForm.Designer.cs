namespace Weibo_Label_App
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gMap = new GMap.NET.WindowsForms.GMapControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.checkBox_only_this = new System.Windows.Forms.CheckBox();
            this.checkBox_only_waidi = new System.Windows.Forms.CheckBox();
            this.textBox_User = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxLabel = new System.Windows.Forms.GroupBox();
            this.groupBox_weibo = new System.Windows.Forms.GroupBox();
            this.button_showpic = new System.Windows.Forms.Button();
            this.richTextBox_weibo = new System.Windows.Forms.RichTextBox();
            this.groupBox_act = new System.Windows.Forms.GroupBox();
            this.textbox_type = new System.Windows.Forms.TextBox();
            this.label_type_3 = new System.Windows.Forms.Label();
            this.label_type_2 = new System.Windows.Forms.Label();
            this.label_type_1 = new System.Windows.Forms.Label();
            this.comboBox_type_3 = new System.Windows.Forms.ComboBox();
            this.comboBox_type_2 = new System.Windows.Forms.ComboBox();
            this.comboBox_type_1 = new System.Windows.Forms.ComboBox();
            this.groupBox_Toursim = new System.Windows.Forms.GroupBox();
            this.radioButton_toursim_false = new System.Windows.Forms.RadioButton();
            this.radioButton_toursim_notsure = new System.Windows.Forms.RadioButton();
            this.radioButton_toursim_true = new System.Windows.Forms.RadioButton();
            this.button_labelit = new System.Windows.Forms.Button();
            this.button_RandomOne = new System.Windows.Forms.Button();
            this.groupBox_purpose = new System.Windows.Forms.GroupBox();
            this.textBox_purpose = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_purpose_2 = new System.Windows.Forms.ComboBox();
            this.comboBox_purpose_1 = new System.Windows.Forms.ComboBox();
            this.groupBoxMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBoxInfo.SuspendLayout();
            this.groupBoxLabel.SuspendLayout();
            this.groupBox_weibo.SuspendLayout();
            this.groupBox_act.SuspendLayout();
            this.groupBox_Toursim.SuspendLayout();
            this.groupBox_purpose.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMap
            // 
            this.groupBoxMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMap.Controls.Add(this.dataGridView1);
            this.groupBoxMap.Controls.Add(this.gMap);
            this.groupBoxMap.Location = new System.Drawing.Point(12, 12);
            this.groupBoxMap.Name = "groupBoxMap";
            this.groupBoxMap.Size = new System.Drawing.Size(812, 730);
            this.groupBoxMap.TabIndex = 0;
            this.groupBoxMap.TabStop = false;
            this.groupBoxMap.Text = "Map";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 596);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(800, 128);
            this.dataGridView1.TabIndex = 1;
            // 
            // gMap
            // 
            this.gMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMap.Bearing = 0F;
            this.gMap.CanDragMap = true;
            this.gMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMap.GrayScaleMode = false;
            this.gMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMap.LevelsKeepInMemmory = 5;
            this.gMap.Location = new System.Drawing.Point(6, 20);
            this.gMap.MarkersEnabled = true;
            this.gMap.MaxZoom = 2;
            this.gMap.MinZoom = 2;
            this.gMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMap.Name = "gMap";
            this.gMap.NegativeMode = false;
            this.gMap.PolygonsEnabled = true;
            this.gMap.RetryLoadTile = 0;
            this.gMap.RoutesEnabled = true;
            this.gMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMap.ShowTileGridLines = false;
            this.gMap.Size = new System.Drawing.Size(800, 570);
            this.gMap.TabIndex = 0;
            this.gMap.Zoom = 0D;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 745);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1101, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInfo.Controls.Add(this.checkBox_only_this);
            this.groupBoxInfo.Controls.Add(this.checkBox_only_waidi);
            this.groupBoxInfo.Controls.Add(this.textBox_User);
            this.groupBoxInfo.Controls.Add(this.label1);
            this.groupBoxInfo.Location = new System.Drawing.Point(830, 12);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(259, 84);
            this.groupBoxInfo.TabIndex = 2;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Info";
            // 
            // checkBox_only_this
            // 
            this.checkBox_only_this.AutoSize = true;
            this.checkBox_only_this.Checked = true;
            this.checkBox_only_this.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_only_this.Location = new System.Drawing.Point(142, 59);
            this.checkBox_only_this.Name = "checkBox_only_this";
            this.checkBox_only_this.Size = new System.Drawing.Size(96, 16);
            this.checkBox_only_this.TabIndex = 3;
            this.checkBox_only_this.Text = "只看当前微博";
            this.checkBox_only_this.UseVisualStyleBackColor = true;
            // 
            // checkBox_only_waidi
            // 
            this.checkBox_only_waidi.AutoSize = true;
            this.checkBox_only_waidi.Location = new System.Drawing.Point(31, 59);
            this.checkBox_only_waidi.Name = "checkBox_only_waidi";
            this.checkBox_only_waidi.Size = new System.Drawing.Size(84, 16);
            this.checkBox_only_waidi.TabIndex = 2;
            this.checkBox_only_waidi.Text = "只看外地人";
            this.checkBox_only_waidi.UseVisualStyleBackColor = true;
            // 
            // textBox_User
            // 
            this.textBox_User.Location = new System.Drawing.Point(86, 26);
            this.textBox_User.Name = "textBox_User";
            this.textBox_User.Size = new System.Drawing.Size(167, 21);
            this.textBox_User.TabIndex = 1;
            this.textBox_User.TextChanged += new System.EventHandler(this.textBox_User_TextChanged);
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
            this.groupBoxLabel.Controls.Add(this.groupBox_purpose);
            this.groupBoxLabel.Location = new System.Drawing.Point(830, 102);
            this.groupBoxLabel.Name = "groupBoxLabel";
            this.groupBoxLabel.Size = new System.Drawing.Size(259, 640);
            this.groupBoxLabel.TabIndex = 3;
            this.groupBoxLabel.TabStop = false;
            this.groupBoxLabel.Text = "Label";
            // 
            // groupBox_weibo
            // 
            this.groupBox_weibo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_weibo.Controls.Add(this.button_showpic);
            this.groupBox_weibo.Controls.Add(this.richTextBox_weibo);
            this.groupBox_weibo.Location = new System.Drawing.Point(6, 367);
            this.groupBox_weibo.Name = "groupBox_weibo";
            this.groupBox_weibo.Size = new System.Drawing.Size(247, 267);
            this.groupBox_weibo.TabIndex = 4;
            this.groupBox_weibo.TabStop = false;
            this.groupBox_weibo.Text = "Weibo";
            // 
            // button_showpic
            // 
            this.button_showpic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_showpic.Enabled = false;
            this.button_showpic.Location = new System.Drawing.Point(6, 238);
            this.button_showpic.Name = "button_showpic";
            this.button_showpic.Size = new System.Drawing.Size(235, 23);
            this.button_showpic.TabIndex = 1;
            this.button_showpic.Text = "Show Picture";
            this.button_showpic.UseVisualStyleBackColor = true;
            this.button_showpic.Click += new System.EventHandler(this.button_showpic_Click);
            // 
            // richTextBox_weibo
            // 
            this.richTextBox_weibo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_weibo.Location = new System.Drawing.Point(6, 20);
            this.richTextBox_weibo.Name = "richTextBox_weibo";
            this.richTextBox_weibo.Size = new System.Drawing.Size(235, 212);
            this.richTextBox_weibo.TabIndex = 0;
            this.richTextBox_weibo.Text = "";
            // 
            // groupBox_act
            // 
            this.groupBox_act.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_act.Controls.Add(this.textbox_type);
            this.groupBox_act.Controls.Add(this.label_type_3);
            this.groupBox_act.Controls.Add(this.label_type_2);
            this.groupBox_act.Controls.Add(this.label_type_1);
            this.groupBox_act.Controls.Add(this.comboBox_type_3);
            this.groupBox_act.Controls.Add(this.comboBox_type_2);
            this.groupBox_act.Controls.Add(this.comboBox_type_1);
            this.groupBox_act.Location = new System.Drawing.Point(6, 126);
            this.groupBox_act.Name = "groupBox_act";
            this.groupBox_act.Size = new System.Drawing.Size(247, 129);
            this.groupBox_act.TabIndex = 3;
            this.groupBox_act.TabStop = false;
            this.groupBox_act.Text = "ActivatyType";
            // 
            // textbox_type
            // 
            this.textbox_type.Location = new System.Drawing.Point(25, 98);
            this.textbox_type.Name = "textbox_type";
            this.textbox_type.ReadOnly = true;
            this.textbox_type.Size = new System.Drawing.Size(198, 21);
            this.textbox_type.TabIndex = 6;
            // 
            // label_type_3
            // 
            this.label_type_3.AutoSize = true;
            this.label_type_3.Location = new System.Drawing.Point(23, 75);
            this.label_type_3.Name = "label_type_3";
            this.label_type_3.Size = new System.Drawing.Size(53, 12);
            this.label_type_3.TabIndex = 5;
            this.label_type_3.Text = "三级活动";
            // 
            // label_type_2
            // 
            this.label_type_2.AutoSize = true;
            this.label_type_2.Location = new System.Drawing.Point(23, 49);
            this.label_type_2.Name = "label_type_2";
            this.label_type_2.Size = new System.Drawing.Size(53, 12);
            this.label_type_2.TabIndex = 4;
            this.label_type_2.Text = "二级活动";
            // 
            // label_type_1
            // 
            this.label_type_1.AutoSize = true;
            this.label_type_1.Location = new System.Drawing.Point(23, 23);
            this.label_type_1.Name = "label_type_1";
            this.label_type_1.Size = new System.Drawing.Size(53, 12);
            this.label_type_1.TabIndex = 3;
            this.label_type_1.Text = "一级活动";
            // 
            // comboBox_type_3
            // 
            this.comboBox_type_3.FormattingEnabled = true;
            this.comboBox_type_3.Location = new System.Drawing.Point(102, 72);
            this.comboBox_type_3.Name = "comboBox_type_3";
            this.comboBox_type_3.Size = new System.Drawing.Size(121, 20);
            this.comboBox_type_3.TabIndex = 2;
            this.comboBox_type_3.SelectedIndexChanged += new System.EventHandler(this.comboBox_type_3_SelectedIndexChanged);
            this.comboBox_type_3.TextChanged += new System.EventHandler(this.comboBox_type_3_TextChanged);
            // 
            // comboBox_type_2
            // 
            this.comboBox_type_2.FormattingEnabled = true;
            this.comboBox_type_2.Location = new System.Drawing.Point(102, 46);
            this.comboBox_type_2.Name = "comboBox_type_2";
            this.comboBox_type_2.Size = new System.Drawing.Size(121, 20);
            this.comboBox_type_2.TabIndex = 1;
            this.comboBox_type_2.SelectedIndexChanged += new System.EventHandler(this.comboBox_type_2_SelectedIndexChanged);
            this.comboBox_type_2.TextChanged += new System.EventHandler(this.comboBox_type_2_TextChanged);
            // 
            // comboBox_type_1
            // 
            this.comboBox_type_1.FormattingEnabled = true;
            this.comboBox_type_1.Location = new System.Drawing.Point(102, 20);
            this.comboBox_type_1.Name = "comboBox_type_1";
            this.comboBox_type_1.Size = new System.Drawing.Size(121, 20);
            this.comboBox_type_1.TabIndex = 0;
            this.comboBox_type_1.SelectedIndexChanged += new System.EventHandler(this.comboBox_type_1_SelectedIndexChanged);
            this.comboBox_type_1.TextChanged += new System.EventHandler(this.comboBox_type_1_TextChanged);
            // 
            // groupBox_Toursim
            // 
            this.groupBox_Toursim.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_Toursim.Controls.Add(this.radioButton_toursim_false);
            this.groupBox_Toursim.Controls.Add(this.radioButton_toursim_notsure);
            this.groupBox_Toursim.Controls.Add(this.radioButton_toursim_true);
            this.groupBox_Toursim.Location = new System.Drawing.Point(6, 64);
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
            this.button_labelit.Enabled = false;
            this.button_labelit.Location = new System.Drawing.Point(154, 20);
            this.button_labelit.Name = "button_labelit";
            this.button_labelit.Size = new System.Drawing.Size(75, 38);
            this.button_labelit.TabIndex = 1;
            this.button_labelit.Text = "标注";
            this.button_labelit.UseVisualStyleBackColor = true;
            this.button_labelit.Click += new System.EventHandler(this.button_labelit_Click);
            // 
            // button_RandomOne
            // 
            this.button_RandomOne.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_RandomOne.Location = new System.Drawing.Point(31, 20);
            this.button_RandomOne.Name = "button_RandomOne";
            this.button_RandomOne.Size = new System.Drawing.Size(71, 38);
            this.button_RandomOne.TabIndex = 0;
            this.button_RandomOne.Text = "随机一个";
            this.button_RandomOne.UseVisualStyleBackColor = true;
            this.button_RandomOne.Click += new System.EventHandler(this.button_RandomOne_Click);
            // 
            // groupBox_purpose
            // 
            this.groupBox_purpose.Controls.Add(this.textBox_purpose);
            this.groupBox_purpose.Controls.Add(this.label2);
            this.groupBox_purpose.Controls.Add(this.label3);
            this.groupBox_purpose.Controls.Add(this.comboBox_purpose_2);
            this.groupBox_purpose.Controls.Add(this.comboBox_purpose_1);
            this.groupBox_purpose.Location = new System.Drawing.Point(6, 261);
            this.groupBox_purpose.Name = "groupBox_purpose";
            this.groupBox_purpose.Size = new System.Drawing.Size(247, 100);
            this.groupBox_purpose.TabIndex = 5;
            this.groupBox_purpose.TabStop = false;
            this.groupBox_purpose.Text = "PurposeType";
            // 
            // textBox_purpose
            // 
            this.textBox_purpose.Location = new System.Drawing.Point(25, 72);
            this.textBox_purpose.Name = "textBox_purpose";
            this.textBox_purpose.ReadOnly = true;
            this.textBox_purpose.Size = new System.Drawing.Size(198, 21);
            this.textBox_purpose.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "二级目的";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "一级目的";
            // 
            // comboBox_purpose_2
            // 
            this.comboBox_purpose_2.FormattingEnabled = true;
            this.comboBox_purpose_2.Location = new System.Drawing.Point(102, 46);
            this.comboBox_purpose_2.Name = "comboBox_purpose_2";
            this.comboBox_purpose_2.Size = new System.Drawing.Size(121, 20);
            this.comboBox_purpose_2.TabIndex = 6;
            this.comboBox_purpose_2.SelectedIndexChanged += new System.EventHandler(this.comboBox_purpose_2_SelectedIndexChanged);
            this.comboBox_purpose_2.TextChanged += new System.EventHandler(this.comboBox_purpose_2_TextChanged);
            // 
            // comboBox_purpose_1
            // 
            this.comboBox_purpose_1.FormattingEnabled = true;
            this.comboBox_purpose_1.Location = new System.Drawing.Point(102, 20);
            this.comboBox_purpose_1.Name = "comboBox_purpose_1";
            this.comboBox_purpose_1.Size = new System.Drawing.Size(121, 20);
            this.comboBox_purpose_1.TabIndex = 5;
            this.comboBox_purpose_1.SelectedIndexChanged += new System.EventHandler(this.comboBox_purpose_1_SelectedIndexChanged);
            this.comboBox_purpose_1.TextChanged += new System.EventHandler(this.comboBox_purpose_1_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 767);
            this.Controls.Add(this.groupBoxLabel);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxMap);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBoxMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.groupBoxLabel.ResumeLayout(false);
            this.groupBox_weibo.ResumeLayout(false);
            this.groupBox_act.ResumeLayout(false);
            this.groupBox_act.PerformLayout();
            this.groupBox_Toursim.ResumeLayout(false);
            this.groupBox_Toursim.PerformLayout();
            this.groupBox_purpose.ResumeLayout(false);
            this.groupBox_purpose.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMap;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.GroupBox groupBoxLabel;
        private System.Windows.Forms.TextBox textBox_User;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox_act;
        private System.Windows.Forms.GroupBox groupBox_Toursim;
        private System.Windows.Forms.RadioButton radioButton_toursim_false;
        private System.Windows.Forms.RadioButton radioButton_toursim_notsure;
        private System.Windows.Forms.RadioButton radioButton_toursim_true;
        private System.Windows.Forms.Button button_labelit;
        private System.Windows.Forms.Button button_RandomOne;
        private System.Windows.Forms.GroupBox groupBox_weibo;
        private System.Windows.Forms.Button button_showpic;
        private System.Windows.Forms.RichTextBox richTextBox_weibo;
        private GMap.NET.WindowsForms.GMapControl gMap;
        private System.Windows.Forms.CheckBox checkBox_only_this;
        private System.Windows.Forms.CheckBox checkBox_only_waidi;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textbox_type;
        private System.Windows.Forms.Label label_type_3;
        private System.Windows.Forms.Label label_type_2;
        private System.Windows.Forms.Label label_type_1;
        private System.Windows.Forms.ComboBox comboBox_type_3;
        private System.Windows.Forms.ComboBox comboBox_type_2;
        private System.Windows.Forms.ComboBox comboBox_type_1;
        private System.Windows.Forms.GroupBox groupBox_purpose;
        private System.Windows.Forms.TextBox textBox_purpose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_purpose_2;
        private System.Windows.Forms.ComboBox comboBox_purpose_1;
    }
}

