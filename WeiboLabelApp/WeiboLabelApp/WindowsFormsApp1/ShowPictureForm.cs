using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weibo_Label_App
{
    public partial class ShowPictureForm : Form
    {
        public ShowPictureForm(string url)
        {
            url_picture = url;
            InitializeComponent();
        }
        public string url_picture = "";

        private void ShowPictureForm_Load(object sender, EventArgs e)
        {
            Bitmap img_show = (Bitmap)Get_url_Image(url_picture);
            pictureBox1.Image = Resize_Image(img_show,pictureBox1.Size);
            pictureBox1.Refresh();
        }

        public Image Get_url_Image(string url)
        {
            WebRequest imgRequest = WebRequest.Create(url);
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)imgRequest.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
                return null;
            }
            if (res.StatusCode.ToString() == "OK")
            {
                System.Drawing.Image downImage = System.Drawing.Image.FromStream(imgRequest.GetResponse().GetResponseStream());
                return downImage;
            }
            return null;
        }

        private static System.Drawing.Image Resize_Image(System.Drawing.Image imgToResize, Size size)
        {
            //获取图片宽度
            int sourceWidth = imgToResize.Width;
            //获取图片高度
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //计算宽度的缩放比例
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //计算高度的缩放比例
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //期望的宽度
            int destWidth = (int)(sourceWidth * nPercent);
            //期望的高度
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //绘制图像
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
