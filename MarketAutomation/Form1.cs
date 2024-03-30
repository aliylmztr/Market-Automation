using MarketAutomation.controller;
using MarketAutomation.enumaration;
using MarketAutomation.model;
using System;
using System.Windows.Forms;

namespace MarketAutomation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_girisYap_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            User result = controller.login(txt_kullaniciAdi.Text, txt_sifre.Text);
        
            if(result != null && result.status == LoginStatus.basarili && result.yetki == "admin")
            {
                AdminPanel admin = new AdminPanel();
                admin.Show();
                this.Hide();
            }
            else if(result != null && result.status == LoginStatus.basarili && result.yetki == "kasiyer")
            {
                KasiyerPanel kasiyer = new KasiyerPanel();
                kasiyer.Show();
                this.Hide();
            }
            else if(result != null && result.status == LoginStatus.basarisiz)
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Eksik parametre hatası!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            SifreDegistirme sd = new SifreDegistirme();
            sd.Show();
            this.Hide();
        }
    }
}
