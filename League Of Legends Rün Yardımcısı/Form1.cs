using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http;

namespace League_Of_Legends_Rün_Yardımcısı
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        OpenQA.Selenium.Chrome.ChromeDriver drv; Thread th;
        List<string> Rünler = new List<string>();
        private void Form1_Load(object sender, EventArgs e)
        {
            timer2.Enabled = true;
            timer2.Interval = 1000;
            timer1.Interval = 100;
            timer1.Enabled = true;
            th = new Thread(loading); th.Start();
        }

        private void defaultcep()
        {
            try
            {
                foreach (var control in this.Controls)
                {
                    if (control.GetType() == typeof(PictureBox))
                    {
                        ((PictureBox)control).Image = null;
                    }
                }
            }
            catch {  }
        }
        int count = 0;
        string karakter = "Yasuo";
        string Rün = "";
        string Adet = "155";
        private void runkaydet(string img,string text,int count)
        {
            string veri = drv.FindElements(By.XPath(text))[count].Text.Replace(":", "");
            string kontrol = Application.StartupPath + "\\Rünler\\" + veri + ".png";
            Console.Items.Add(veri+" Seçildi.");
            Rün += veri+",";
            if (File.Exists(kontrol) != true)
            {
                string resim = drv.FindElements(By.XPath(img))[count].GetAttribute("src");
                using (WebClient client = new WebClient())
                    client.DownloadFile(new Uri(resim), kontrol);
                Console.Items.Add(veri +" : Rün Resmi güncellendi.");
            }
            else
            {
                Console.Items.Add(veri + " : Rün Resmi güncel.");
            }
        }
        private void runkaydetalt(string img, string text, int count)
        {
            string veri = drv.FindElement(By.XPath(text)).Text.Split('\n')[count].Replace(":","").Replace(" ","");
            if (veri.Contains("Attack")==true)
                veri = "Saldırı Hızı";
            if (veri.Contains("Adaptive") == true)
                veri = "Değişken Kuvvet";
            if (veri.Contains("Armor") == true)
                veri = "Zırh";
            if (veri.Contains("CDRScaling") == true)
                veri = "Bekleme Süresinde Azalma";
            if (veri.Contains("Healt") == true)
                veri = "Can";
            if (veri.Contains("Magic") == true)
                veri = "Büyü Direnci";
            
            string kontrol = Application.StartupPath + "\\Rünler\\" + veri + ".png";
            Console.Items.Add(veri + " Seçildi.");
            Rün += veri + ",";
            if (File.Exists(kontrol) != true)
            {
                string resim = drv.FindElement(By.XPath(img)).GetAttribute("src");
                using (WebClient client = new WebClient())
                    client.DownloadFile(new Uri(resim), kontrol);
                Console.Items.Add(veri + " : Rün Resmi güncellendi.");
            }
            else
            {
                Console.Items.Add(veri + " : Rün Resmi güncel.");
            }
        }
        private void loading()
        {
            progmin.Maximum = 14;
            progall.Maximum = 14;
            progall.Minimum = 0;
            progmin.Minimum = 0;
            progall.Value = 1; progmin.Value = 1;
            progall.Value = 2; progmin.Value = 2;
            Console.Items.Add("Programın Yazarı : @kaniberkali");
            progall.Value = 3; progmin.Value = 3;
            try
            {
                if (Directory.Exists("Hikayeler") == false)
                    Directory.CreateDirectory("Hikayeler");
            }
            catch {  }
            try
                {
                    if (Directory.Exists("Karakterler") == false)
                    Directory.CreateDirectory("Karakterler");
            }
            catch {  }
            progall.Value = 4; progmin.Value = 4;
            try
                {
                    if (Directory.Exists("Rünler") == false)
                    Directory.CreateDirectory("Rünler");
            }
            catch {  }
            progall.Value = 5; progmin.Value = 5;
           
            progall.Value = 6; progmin.Value = 6;

            progall.Value =7; progmin.Value = 7;
            try
            {
                progall.Value = 8; progmin.Value = 8;
                Console.Items.Add("Ayarlarınız Çekiliyor...");
                progall.Value = 9; progmin.Value = 9;
                string dosya_yolu = @"Settings.txt";
                FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
                StreamReader sw = new StreamReader(fs);
                string yazi = sw.ReadLine();
                while (yazi != null)
                {
                    if (yazi.Split('=')[0] == "Rün")
                    {
                        string çar = yazi.Split('=')[1];
                        string newçar = çar.Split('{')[0];
                        string rüncük = çar.Split('{')[1].Replace("}", "");
                        comboBox1.Items.Add(newçar);
                        Rünler.Add(rüncük);
                        label10.Text = comboBox1.Items.Count.ToString();
                    }
                    if (yazi.Split(':')[0] == "Karakter")
                        karakter = yazi.Split(':')[1];
                    if (yazi.Split(':')[0] == "Adet")
                    {
                        Adet = yazi.Split(':')[1];
                        label9.Text = Adet + " /";
                    }
                    yazi = sw.ReadLine();
                }
                sw.Close();
                fs.Close();
                progall.Value = 10; progmin.Value = 10;
                Console.Items.Add("Ayarlarınız Başarıyla Çekildi.");
            }
            catch
            {
                Console.Items.Add("Ayarlarınız Çekilirken Bir Hata Oluştu.");
            }
            progall.Value = 11; progmin.Value = 11;
            comboBox1.SelectedItem = karakter;
            progall.Value = 12; progmin.Value = 12;
            label1.Text = comboBox1.Items.Count.ToString();
            button8.Visible = false;
            button7.Visible = false;
            progall.Value = 13; progmin.Value = 13;
            button2.Enabled = true;
            progall.Value = 14; progmin.Value = 14;
            progall.Value = 0; progmin.Value = 0;
            progmin.Maximum = 12;
            progall.Maximum = 12;
            ChromeOptions option = new ChromeOptions();
            progall.Value = 1; progmin.Value = 1;
            option.AddExcludedArgument("enable-automation");
            option.AddAdditionalCapability("useAutomationExtension", false);
            //option.AddArgument("--headless");
            progall.Value = 2; progmin.Value = 2;
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            progall.Value = 3; progmin.Value = 3;
            service.HideCommandPromptWindow = true;
            progall.Value = 4; progmin.Value = 4;
            drv = new ChromeDriver(service, option);
            drv.Manage().Window.Position = new Point(-32000, -32000);
            progall.Value = 5; progmin.Value = 5;
            Console.Items.Add("Chrome ayarları yapıldı.");
            progall.Value = 6; progmin.Value = 6;
            drv.Navigate().GoToUrl("https://tr.leagueoflegends.com/tr-tr/champions/");

            progall.Value = 7; progmin.Value = 7;
            Console.Items.Add("Karakterlerin Çekileceği Siteye Girildi.");
            progall.Value = 8; progmin.Value = 8;
        basdon:
            Thread.Sleep(2000);
            try
            {
                count = drv.FindElements(By.XPath("//span[@class='style__Text-sc-12h96bu-3 gPUACV']")).Count;
                Console.Items.Add("Karakter Sayıları Çekildi.");
                progall.Value = 9; progmin.Value = 9;
            }
            catch
            {
                if (drv.Url != null)
                goto basdon;
            }
            progall.Value = 10; progmin.Value = 10;
            Console.Items.Add("Karakter Sayıları Karşılaştırılıyor.");
            progall.Value =11; progmin.Value = 11;
            List<string> güncellemeler = new List<string>();
            progall.Value = 12; progmin.Value = 12;
            progall.Value = 0; progmin.Value = 0;
            progmin.Maximum = 6;
            progall.Maximum = count;
            try
            {
                if (count != comboBox1.Items.Count)
                {
                    Console.Items.Add("Eşit Değil Güncelleme Denetleniyor");
                    for (int i = 0; i < count; i++)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        progall.Value = i;
                        progmin.Value = 1;
                        string karakterismi = drv.FindElements(By.XPath("//span[@class='style__Text-sc-12h96bu-3 gPUACV']"))[i].Text;
                        progmin.Value = 2;
                            if (comboBox1.Items.Contains(karakterismi) == false)
                            {
                                progmin.Value = 3;
                                Console.Items.Add(karakterismi + " : Güncelleneceklere Eklendi.");
                                progmin.Value = 4;
                                güncellemeler.Add(karakterismi);
                                progmin.Value = 5;
                            }
                            else
                            {
                                Console.Items.Add(karakterismi + " : Zaten Güncel.");
                            }
                        progmin.Value = 6;
                        stopwatch.Stop();
                        string zamansay = Convert.ToString(stopwatch.Elapsed.TotalSeconds);
                        double sonuc = Convert.ToDouble(zamansay);
                        double hesapla = (Convert.ToInt32(label9.Text.Split('/')[0].Replace(" ",""))- Convert.ToInt32(label10.Text)) * sonuc;
                        int bitiş = Convert.ToInt32(hesapla.ToString().Split(',')[0]);
                        if (bitiş >= 60)
                            label12.Text = bitiş / 60 + " Dakika";
                        else
                            label12.Text = bitiş + " Saniye";
                    }
                    Console.Items.Add("Denetleme başarıyla bitirildi.");
                    drv.Navigate().GoToUrl("https://omylegend.com/sampiyon/Aatrox/build");
                    Thread.Sleep(2000);
                    Console.Items.Add("Rün çekme sayfasına giriş yapıldı.");
                    progall.Value = 0; progmin.Value = 0;
                    progmin.Maximum = 26;
                    progall.Maximum = güncellemeler.Count;
                    label9.Text = count + " /";
                    label10.Text = Convert.ToString(count - güncellemeler.Count);
                    for (int i = 0; i < güncellemeler.Count; i++)
                    {
                        try
                        {
                            Stopwatch stopwatch = new Stopwatch();
                            stopwatch.Start();
                            basadon:
                            label10.Text = Convert.ToString(count-((güncellemeler.Count-1)-i));
                            progall.Value = Convert.ToInt32(i);
                            progmin.Value = 1;
                            Console.Items.Add(güncellemeler[i] + " : Bilgiler çekiliyor.");
                            progmin.Value = 2;
                            drv.FindElement(By.XPath("//html/body/nav/div/div/form/span[1]/span[1]/span/span[1]")).Click();
                            progmin.Value = 3;
                            Thread.Sleep(300);
                            progmin.Value = 4;
                            drv.FindElement(By.XPath("//html/body/span/span/span[1]/input")).SendKeys(güncellemeler[i]);
                            progmin.Value = 5;
                            Thread.Sleep(300);
                            progmin.Value = 6;
                            drv.FindElement(By.XPath("//html/body/span/span/span[2]/ul/li")).Click();
                            progmin.Value = 7;
                            Thread.Sleep(1000);
                            progmin.Value = 8;
                            string kontrol = Application.StartupPath + "\\Karakterler\\" + güncellemeler[i] + ".png";
                            progmin.Value = 9;
                            Console.Items.Add("Profil fotoğrafı kontrol ediliyor.");
                            progmin.Value = 10;
                            string isim = drv.FindElement(By.XPath("//html/body/main/div/div[2]/div[1]/div[1]/div/div[2]/h1")).Text;
                            if (güncellemeler[i] == "Wukong")
                            {
                                if (isim.Replace("'", "").Replace(".", "").ToLower().Contains("monkeyking") == false)
                                    goto basadon;
                            }
                            else
                            {
                                if (isim.Replace("'", "").Replace(".", "").ToLower().Contains(güncellemeler[i].Replace("'", "").Replace(".", "").Split(' ')[0].ToLower()) == false)
                                    goto basadon;
                            }
                                if (File.Exists(kontrol) != true)
                            {
                                string resim = drv.FindElement(By.XPath("//html/body/main/div/div[2]/div[1]/div[1]/div/div[1]/img")).GetAttribute("src");
                                using (WebClient client = new WebClient())
                                    client.DownloadFile(new Uri(resim), kontrol);
                                Console.Items.Add(güncellemeler[i] + " : Profil Güncellendi.");
                            }
                            else
                            {
                                Console.Items.Add(güncellemeler[i] + " : Profil Güncel.");
                            }
                            progmin.Value = 11;
                            runkaydet("//div[@class='w-100 align-items-center d-flex']/img", "//div[@class='w-100 align-items-center d-flex']/h3/span",0);
                            progmin.Value = 12;
                            runkaydet("//div[@class='w-100 align-items-center d-flex']/img", "//div[@class='w-100 align-items-center d-flex']/h3/span", 1);
                            progmin.Value = 13;
                            runkaydet("//td[@class='text-center p-1 w-25']/div[1]/img", "//td[@class='text-center p-1 w-25']/div[2]",0);
                            progmin.Value = 14;
                            runkaydet("//td[@class='text-center p-1 w-25']/div[1]/img", "//td[@class='text-center p-1 w-25']/div[2]", 1);
                            progmin.Value = 15;
                            runkaydet("//td[@class='text-center p-1 w-25']/div[1]/img", "//td[@class='text-center p-1 w-25']/div[2]", 2);
                            progmin.Value = 16;
                            runkaydet("//td[@class='text-center p-1 w-25']/div[1]/img", "//td[@class='text-center p-1 w-25']/div[2]", 3);
                            progmin.Value = 17;
                            runkaydet("//td[@class='text-center p-1 w-25']/div[1]/img", "//td[@class='text-center p-1 w-25']/div[2]", 4);
                            progmin.Value = 18;
                            runkaydet("//td[@class='text-center p-1 w-25']/div[1]/img", "//td[@class='text-center p-1 w-25']/div[2]", 5);
                            progmin.Value = 19;
                            runkaydetalt("//div[@class='col-12 col-lg-6 border-start pt-2 pt-lg-0 border border-bottom-0 border-top-0 border-end-0 lolvvv-table']/div[2]/img[1]", "//div[@class='col-12 col-lg-6 border-start pt-2 pt-lg-0 border border-bottom-0 border-top-0 border-end-0 lolvvv-table']/div[2]",0);
                            progmin.Value = 20;
                            runkaydetalt("//div[@class='col-12 col-lg-6 border-start pt-2 pt-lg-0 border border-bottom-0 border-top-0 border-end-0 lolvvv-table']/div[2]/img[2]", "//div[@class='col-12 col-lg-6 border-start pt-2 pt-lg-0 border border-bottom-0 border-top-0 border-end-0 lolvvv-table']/div[2]", 1);
                            progmin.Value = 21;
                            runkaydetalt("//div[@class='col-12 col-lg-6 border-start pt-2 pt-lg-0 border border-bottom-0 border-top-0 border-end-0 lolvvv-table']/div[2]/img[3]", "//div[@class='col-12 col-lg-6 border-start pt-2 pt-lg-0 border border-bottom-0 border-top-0 border-end-0 lolvvv-table']/div[2]", 2);
                            progmin.Value = 22;
                            Console.Items.Add("Hikaye kontrol ediliyor.");
                            if (File.Exists("Hikayeler/" + güncellemeler[i]+".txt") == false)
                            {
                                Console.Items.Add(güncellemeler[i] + " : Hikaye çekiliyor.");
                                string hikayecik = drv.FindElement(By.XPath("//div[@class='box rounded p-2 mb-4 mb-lg-0']/p")).Text;
                                Console.Items.Add("Hikaye başarıyla çekildi.");
                                Console.Items.Add("Hikaye kaydediliyor.");
                                string dosya_yolum2 = "Hikayeler/" + güncellemeler[i] + ".txt";
                                FileStream fsm2 = new FileStream(dosya_yolum2, FileMode.Create, FileAccess.Write);
                                StreamWriter swm2 = new StreamWriter(fsm2);
                                swm2.Write(hikayecik);
                                swm2.Flush();
                                swm2.Close();
                                swm2.Close();
                                Console.Items.Add("Hikaye kaydedildi.");
                            }
                            else
                                Console.Items.Add("Hikaye zaten var.");

                            progmin.Value = 23;
                            Rünler.Add(Rün);
                            progmin.Value = 24;
                            comboBox1.Items.Add(güncellemeler[i]);
                            progmin.Value = 25;
                            label1.Text = comboBox1.Items.Count.ToString();
                            progmin.Value = 26;
                            Rün = "";
                            stopwatch.Stop();
                            string zamansay = Convert.ToString(stopwatch.Elapsed.TotalSeconds);
                            double sonuc = Convert.ToDouble(zamansay);
                            double hesapla = (Convert.ToInt32(label9.Text.Split('/')[0].Replace(" ", "")) - Convert.ToInt32(label10.Text)) * sonuc;
                            int bitiş = Convert.ToInt32(hesapla.ToString().Split(',')[0]);
                            if (bitiş >= 60)
                                label12.Text = bitiş/60 + " Dakika";
                            else
                                label12.Text = bitiş + " Saniye";
                        }
                        catch { Rün = "";  i--; }
                    }
                    progall.Value = progall.Maximum;
                    progmin.Value = progmin.Maximum;
                }
                else
                {
                    progall.Value = 0; progmin.Value = 0;
                    Console.Items.Add("Karakter Sayıları Eşit");
                }
            }
            catch {  }
            Console.Items.Add("İşlemler Başarıyla Bitirildi.");
            label12.Text = "Güncellendi.";
            string dosya_yolum = @"Settings.txt";
            FileStream fsm = new FileStream(dosya_yolum, FileMode.Create, FileAccess.Write);
            StreamWriter swm = new StreamWriter(fsm);
            swm.Write("Adet:" + comboBox1.Items.Count);
            swm.WriteLine();
            if (comboBox1.SelectedIndex != -1)
                swm.Write("Karakter:" + comboBox1.Text);
            else
                swm.Write("Karakter:" + comboBox1.Items[comboBox1.Items.Count - 1]);
            swm.WriteLine();
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                swm.Write("Rün=" + comboBox1.Items[i] + "{" + Rünler[i] + "}");
                swm.WriteLine();
            }
            swm.Flush();
            swm.Close();
            fsm.Close();
            label1.Text = comboBox1.Items.Count.ToString();
            panel2.Visible = false;
            timer2.Enabled = false;
            if (drv != null)
                drv.Quit();
        }
        public static double DirSize(DirectoryInfo d)
        {
            double size = 0;
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            th = new Thread(rüngetir); th.Start();
        }
        private void rüngetir()
        {
            try
            {
                Thread.Sleep(100);
                string rünccü = Rünler[comboBox1.SelectedIndex];
                string karaktercik = comboBox1.Text;
                try
                {
                    pictureBox10.Load(Application.StartupPath + "\\Karakterler\\" + karaktercik + ".png");
                }
                catch {  }
                string[] rünlercik = rünccü.Split(',');
                try { pictureBox1.Load(Application.StartupPath + "\\Rünler\\" + rünlercik[0] + ".png"); } catch {  }
                try
                { pictureBox8.Load(Application.StartupPath + "\\Rünler\\" + rünlercik[1] + ".png"); }
                catch {  }
                try
                {
                    pictureBox3.Load(Application.StartupPath + "\\Rünler\\" + rünlercik[2] + ".png");
                }
                catch {  }

                try
                {
                    pictureBox2.Load(Application.StartupPath + "\\Rünler\\" + rünlercik[3] + ".png");
                }
                catch {  }

                try
                {
                    pictureBox4.Load(Application.StartupPath + "\\Rünler\\" + rünlercik[4] + ".png");
                }
                catch {  }

                try
                {
                    pictureBox5.Load(Application.StartupPath + "\\Rünler\\" + rünlercik[5] + ".png");
                }
                catch {  }

                try
                {
                    pictureBox9.Load(Application.StartupPath + "\\Rünler\\" + rünlercik[6] + ".png");
                }
                catch {  }
                try
                {
                    pictureBox7.Load(Application.StartupPath + "\\Rünler\\" + rünlercik[7] + ".png");
                }
                catch {  }
                try
                {
                    pictureBox21.Load(Application.StartupPath + "\\Rünler\\" + rünlercik[8] + ".png");
                }
                catch {  }
                try
                {
                    pictureBox26.Load(Application.StartupPath + "\\Rünler\\" + rünlercik[9] + ".png");
                }
                catch {  }
                try
                {
                    pictureBox29.Load(Application.StartupPath + "\\Rünler\\" + rünlercik[10] + ".png");
                }
                catch
                {

                }
            }
            catch {  }
            comboBox1.Enabled = true;
        } 

       
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (comboBox1.Items.Count > 2)
                {
                    if (notsave == false)
                    {
                        string dosya_yolu = @"Settings.txt";
                        FileStream fs = new FileStream(dosya_yolu, FileMode.Create, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write("Adet:" + comboBox1.Items.Count);
                        sw.WriteLine();
                        if (comboBox1.SelectedIndex != -1)
                            sw.Write("Karakter:" + comboBox1.Text);
                        else
                            sw.Write("Karakter:" + comboBox1.Items[comboBox1.Items.Count - 1]);
                        sw.WriteLine();
                        for (int i = 0; i < comboBox1.Items.Count; i++)
                        {
                            sw.Write("Rün=" + comboBox1.Items[i] + "{" + Rünler[i] + "}");
                            sw.WriteLine();
                        }
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                    }
                }
                if (drv != null)
                    drv.Quit();
            }
            catch
            {
                if (drv != null)
                    drv.Quit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }
        double boyutum = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            th = new Thread(offyeterlan);th.Start();
        }
        private void offyeterlan()
        {
            if (this.WindowState != FormWindowState.Minimized && TopMost == false)
            {
                TopMost = true;
            }
            try
            {
                DirectoryInfo info = new DirectoryInfo(Application.StartupPath);
                double boyut = DirSize(info);
                if (boyutum != 0)
                {
                    double deger = boyut - boyutum;
                    if (deger <1024)
                    label4.Text = deger+ " B/S";
                    else if (deger >= 1024 && deger < 1024 * 1024)
                        label4.Text = Math.Round(deger /1024,3) + " KB/S";
                    else if (deger >=1024*1024)
                        label4.Text = Math.Round(deger/1024/1024,3) + " MB/S";
                    Thread.Sleep(Convert.ToInt32((boyut - boyutum) / 10));
                }
                else
                    label4.Text = "0 B/S";
                if (boyut < 1024)
                {
                    label3.Text = boyut + " B";
                }
                else if (boyut >= 1024 && boyut < 1024 * 1024)
                {
                    label3.Text = Math.Round((boyut / 1024), 3) + " KB";
                }
                else if (boyut >= 1024 * 1024)
                {
                    label3.Text = Math.Round(boyut / 1024 / 1024, 3) + " MB";
                }
                if (comboBox1.SelectedIndex == -1)
                    comboBox1.SelectedIndex = 0;
                boyutum = boyut;

            }
            catch {  }
            try
            {
                Console.SelectedIndex = Console.Items.Count - 1;
                label1.Text = Convert.ToString(comboBox1.Items.Count);
            }
            catch { }
        }
        int sayac = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac++;
            if (sayac < 60)
            {
                label13.Text = sayac.ToString() + " Saniye";
            }
            else
            {
                int sonuc = sayac / 60;
                label13.Text = sonuc.ToString() + " Dakika";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://instagram.com/kodzamani.tk");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://kodzamani.weebly.com");
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            th = new Thread(sıfırla);th.Start();
        }
        bool notsave = false;
        private void sıfırla()
        {
            notsave = true;
            defaultcep();
            timer1.Enabled = false;
            timer2.Enabled = false;

            button7.Visible = true;
            panel2.Visible = true;
            button8.Visible = true;
            try
            {
                File.Delete(Application.StartupPath + "/" + "Settings.txt");
                Console.Items.Add("Ayarlar silindi.");
            }
            catch { Console.Items.Add("Ayarlar silinemedi."); }
            Thread.Sleep(200);
           
            Thread.Sleep(200);
            Thread.Sleep(200);
            try
            {
                Directory.Delete("Hikayeler", true);
                Console.Items.Add("Hikayeler silindi.");
            }
            catch {  Console.Items.Add("Hikayeler silinemedi."); }
            Thread.Sleep(200);
            try
            {
                Directory.Delete("Karakterler", true);
                Console.Items.Add("Karakterler silindi.");
            }
            catch  { Console.Items.Add("Karakterler silinemedi."); }
            Thread.Sleep(200);
            try
            {
                Directory.Delete("Rünler", true);
                Console.Items.Add("Rünler silindi.");
            }
            catch {   Console.Items.Add("Rünler silinemedi."); }
            
            Thread.Sleep(1000);
            Application.Restart();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            for (int i=0;i<comboBox1.Items.Count;i++)
            {
                if (comboBox1.Items[i].ToString().ToLower().Contains(textBox1.Text.ToLower())==true)
                {
                    comboBox1.SelectedIndex = i;
                    break;
                }
            }
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

           
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items.Clear();
                string veri = System.IO.Path.GetFileName(pictureBox1.ImageLocation).Replace(".png", "");
                contextMenuStrip1.Items.Add(veri);
                pictureBox1.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items.Clear();
                string veri = System.IO.Path.GetFileName(pictureBox3.ImageLocation).Replace(".png", "");
                contextMenuStrip1.Items.Add(veri);
                pictureBox3.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items.Clear();
                string veri = System.IO.Path.GetFileName(pictureBox2.ImageLocation).Replace(".png", "");
                contextMenuStrip1.Items.Add(veri);
                pictureBox2.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items.Clear();
                string veri = System.IO.Path.GetFileName(pictureBox4.ImageLocation).Replace(".png", "");
                contextMenuStrip1.Items.Add(veri);
                pictureBox4.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void pictureBox26_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items.Clear();
                string veri = System.IO.Path.GetFileName(pictureBox26.ImageLocation).Replace(".png", "");
                contextMenuStrip1.Items.Add(veri);
                pictureBox26.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void pictureBox21_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items.Clear();
                string veri = System.IO.Path.GetFileName(pictureBox21.ImageLocation).Replace(".png", "");
                contextMenuStrip1.Items.Add(veri);
                pictureBox21.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items.Clear();
                string veri = System.IO.Path.GetFileName(pictureBox5.ImageLocation).Replace(".png", "");
                contextMenuStrip1.Items.Add(veri);
                pictureBox5.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void pictureBox29_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items.Clear();
                string veri = System.IO.Path.GetFileName(pictureBox29.ImageLocation).Replace(".png", "");
                contextMenuStrip1.Items.Add(veri);
                pictureBox29.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void pictureBox10_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items.Clear();
                string veri = System.IO.Path.GetFileName(pictureBox10.ImageLocation).Replace(".png", "");
                contextMenuStrip1.Items.Add(veri);
                pictureBox10.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void pictureBox7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items.Clear();
                string veri = System.IO.Path.GetFileName(pictureBox7.ImageLocation).Replace(".png", "");
                contextMenuStrip1.Items.Add(veri);
                pictureBox7.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void pictureBox9_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items.Clear();
                string veri = System.IO.Path.GetFileName(pictureBox9.ImageLocation).Replace(".png", "");
                contextMenuStrip1.Items.Add(veri);
                pictureBox9.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void pictureBox6_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureBox8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Items.Clear();
                string veri = System.IO.Path.GetFileName(pictureBox8.ImageLocation).Replace(".png", "");
                contextMenuStrip1.Items.Add(veri);
                pictureBox8.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
        League_Of_Legends_Hikaye hikaye = new League_Of_Legends_Hikaye();
        private void button6_Click_1(object sender, EventArgs e)
        {
            hikaye.karakter = comboBox1.Text;
            hikaye.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lütfen biraz bekleyin.", "@kodzamani.tk");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lütfen biraz bekleyin.", "@kodzamani.tk");
        }
    }
}
