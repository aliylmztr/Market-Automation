using System;
using System.Windows.Forms;

namespace MarketAutomation
{
    public partial class KasiyerPanel : Form
    {
        public KasiyerPanel()
        {
            InitializeComponent();
        }

        private void KasiyerPanel_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btn_cikisYap_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_saat.Text = DateTime.Now.Hour.ToString() + ":";
            lbl_dakika.Text = DateTime.Now.Minute.ToString() + ":";
            lbl_saniye.Text = DateTime.Now.Second.ToString();
        }

        private void btn_meyveSebze_Click(object sender, EventArgs e)
        {
            MeyveSebzePanel meyveSebze = new MeyveSebzePanel();
            meyveSebze.Show();
            this.Hide();
        }

        private void btn_baklagil_Click(object sender, EventArgs e)
        {
            BaklagilPanel baklagil = new BaklagilPanel();
            baklagil.Show();
            this.Hide();
        }

        private void btn_sut_Click(object sender, EventArgs e)
        {
            SutUrunleriPanel sut = new SutUrunleriPanel();
            sut.Show();
            this.Hide();
        }

        private void btn_et_Click(object sender, EventArgs e)
        {
            EtUrunleriPanel et = new EtUrunleriPanel();
            et.Show();
            this.Hide();
        }
    }
}
