using AForge.Video.DirectShow;
using MarketAutomation.controller;
using MarketAutomation.model;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using ZXing;

namespace MarketAutomation
{
    public partial class BaklagilPanel : Form
    {
        int sayi1;
        int sayi2;
        int islemTip;
        FilterInfoCollection fic;
        VideoCaptureDevice vcd;
        public BaklagilPanel()
        {
            InitializeComponent();
            txt_islem.Text = "";
        }

        private void BaklagilPanel_Load(object sender, EventArgs e)
        {
            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach (FilterInfo camera in fic)
            {
                cmb_kameraAc.Items.Add(camera.Name);
            }
        }

        private void secilenTus(object sender, EventArgs e)
        {
            if (txt_islem.Text == "0")
            {
                txt_islem.Text = "";
            }
            txt_islem.Text += ((Button)sender).Text;
        }

        private void btn_temizle_Click(object sender, EventArgs e)
        {
            txt_islem.Text = "0";
        }

        private void btn_toplama_Click(object sender, EventArgs e)
        {
            islemTip = 1;
            sayi1 = int.Parse(txt_islem.Text);
            txt_islem.Text = "0";
        }

        private void btn_cikartma_Click(object sender, EventArgs e)
        {
            islemTip = 2;
            sayi1 = int.Parse(txt_islem.Text);
            txt_islem.Text = "0";
        }

        private void btn_carpma_Click(object sender, EventArgs e)
        {
            islemTip = 3;
            sayi1 = int.Parse(txt_islem.Text);
            txt_islem.Text = "0";
        }

        private void btn_bolme_Click(object sender, EventArgs e)
        {
            islemTip = 4;
            sayi1 = int.Parse(txt_islem.Text);
            txt_islem.Text = "0";
        }

        private void btn_esittir_Click(object sender, EventArgs e)
        {
            if (islemTip == 1)
            {
                sayi2 = int.Parse(txt_islem.Text);
                txt_islem.Text = (sayi1 + sayi2).ToString();
            }
            else if (islemTip == 2)
            {
                sayi2 = int.Parse(txt_islem.Text);
                txt_islem.Text = (sayi1 - sayi2).ToString();
            }
            else if (islemTip == 3)
            {
                sayi2 = int.Parse(txt_islem.Text);
                txt_islem.Text = (sayi1 * sayi2).ToString();
            }
            else if (islemTip == 4)
            {
                sayi2 = int.Parse(txt_islem.Text);
                if (sayi2 != 0)
                {
                    txt_islem.Text = (sayi1 / sayi2).ToString();
                }
                else
                {
                    txt_islem.Text = "0";
                }
            }
        }

        private void btn_geriGel_Click(object sender, EventArgs e)
        {
            if (txt_islem.Text.Length != 0)
            {
                txt_islem.Text = txt_islem.Text.Substring(0, txt_islem.Text.Length - 1);
            }
            else
            {
                txt_islem.Text = "0";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pctbox_kamera.Image != null)
            {
                BarcodeReader reader = new BarcodeReader();
                Result result = reader.Decode((Bitmap)pctbox_kamera.Image);

                if (result != null)
                {
                    txt_barkod.Text = result.ToString();
                    timer1.Stop();
                }
            }
        }

        private void btn_kameraAc_Click(object sender, EventArgs e)
        {
            vcd = new VideoCaptureDevice(fic[cmb_kameraAc.SelectedIndex].MonikerString);
            vcd.NewFrame += Vcd_NewFrame;
            vcd.Start();
            timer1.Start();
        }

        private void Vcd_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pctbox_kamera.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void btn_kameraKapat_Click(object sender, EventArgs e)
        {
            vcd.Stop();
            pctbox_kamera.Image = Image.FromFile(@"D:\market\resimler\camera.ico");
        }

        private void txt_barkod_TextChanged(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            Urun targetUrun = controller.getProduct(txt_barkod.Text);
            lbl_urunIsim.Text = targetUrun.urunIsim.ToString();
            txt_islem.Text = targetUrun.fiyat.ToString();
            SoundPlayer ses = new SoundPlayer();
            ses.SoundLocation = "barkod.wav";
            ses.Play();
        }

        private void btn_cikisYap_Click(object sender, EventArgs e)
        {
            KasiyerPanel kasiyer = new KasiyerPanel();
            kasiyer.Show();
            this.Hide();
        }

        private void btn_etUrunleriPanel_Click(object sender, EventArgs e)
        {
            EtUrunleriPanel et = new EtUrunleriPanel();
            et.Show();
            this.Hide();
        }

        private void btn_sutUrunleriPanel_Click(object sender, EventArgs e)
        {
            SutUrunleriPanel sut = new SutUrunleriPanel();
            sut.Show();
            this.Hide();
        }

        private void btn_meyveSebzePanel_Click(object sender, EventArgs e)
        {
            MeyveSebzePanel meyveSebze = new MeyveSebzePanel();
            meyveSebze.Show();
            this.Hide();
        }
    }
}
