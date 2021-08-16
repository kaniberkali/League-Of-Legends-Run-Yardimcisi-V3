using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace League_Of_Legends_Rün_Yardımcısı
{
    public partial class League_Of_Legends_Hikaye : Form
    {
        public League_Of_Legends_Hikaye()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Thread th = new Thread(islemler);th.Start();
        }
        public string karakter { get; set; }
        string hatırlatıcı = "";
        public void islemler()
        {
            if (hatırlatıcı != karakter)
            {
                hatırlatıcı = karakter;
                this.Text = "League Of Legends Hikaye - " + karakter;
                label1.Text = "League Of Legends Hikaye - " + karakter;
                try
                {
                    string dosya_yolu = "Hikayeler/"+ karakter + ".txt";
                    FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                    StreamReader sw = new StreamReader(fs);
                    string yazi = sw.ReadToEnd();
                    richTextBox1.Text = yazi;
                    sw.Close();
                    fs.Close();
                }
                catch
                {
                    this.Hide();
                    MessageBox.Show("Hikaye çekilirken bir hata oluştu. Lütfen programı sıfırlamayı deneyin.", "@kodzamani.tk");
                }
            }
        }

        private void League_Of_Legends_Hikaye_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
        }
    }
}
