using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace takephoto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private FilterInfoCollection CaptureDevice; // list of webcam
        private VideoCaptureDevice FinalFrame;

        private void Form1_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            FinalFrame = new VideoCaptureDevice();
            foreach (FilterInfo Device in CaptureDevice)
            {
                comboBox1.Items.Add(Device.Name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoSourcePlayer1.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap varBmp = videoSourcePlayer1.GetCurrentVideoFrame();
            string date = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
            varBmp.Save("D:\\ФотоЗаемщиков\\" + date + ".jpg", ImageFormat.Jpeg);
            varBmp.Dispose();
            varBmp = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            videoSourcePlayer1.Stop();
            videoSourcePlayer1.VideoSource = new VideoCaptureDevice(CaptureDevice[0].MonikerString);
            videoSourcePlayer1.Start();
        }
    }
}
