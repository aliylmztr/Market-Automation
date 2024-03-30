using System;
using System.Windows.Forms;

namespace MarketAutomation
{
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_saat.Text = DateTime.Now.Hour.ToString() + ":";
            lbl_dakika.Text = DateTime.Now.Minute.ToString() + ":";
            lbl_saniye.Text = DateTime.Now.Second.ToString();
        }

        private void btn_kullanici_Click(object sender, EventArgs e)
        {
            KullaniciPanel kullaniciPanel = new KullaniciPanel();
            kullaniciPanel.Show();
            this.Hide();
        }

        private void btn_urun_Click(object sender, EventArgs e)
        {
            UrunPanel urun = new UrunPanel();
            urun.Show();
            this.Hide();
        }

        private void btn_cikis_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
