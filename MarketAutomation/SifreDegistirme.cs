using MarketAutomation.controller;
using MarketAutomation.enumaration;
using MarketAutomation.model;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Windows.Forms;

namespace MarketAutomation
{
    public partial class SifreDegistirme : Form
    {
        int code;
        public SifreDegistirme()
        {
            InitializeComponent();
        }

        private void SifreDegistirme_Load(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            List<LoginTable>  loginTableList = controller.getLoginTable();
            grpbox_mail.Enabled = false;
            grpbox_sifre.Enabled = false;
            
            foreach(LoginTable list in loginTableList)
            {
                cmb_guvenlikSorusu.Items.Add(list.guvenlikSorusu.ToString());
            }
            cmb_guvenlikSorusu.SelectedIndex = 0;
        }

        private void btn_sorgula_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            LoginStatus result = controller.doAuthentication(txt_kullaniciAdi.Text.Trim().ToLower(), cmb_guvenlikSorusu.SelectedItem.ToString(), txt_guvenlikCevabi.Text.ToLower());
            
            if(result == LoginStatus.basarili)
            {
                grpbox_mail.Enabled = true;
            }
            else if(result == LoginStatus.basarisiz)
            {
                MessageBox.Show("Girdiğiniz bilgileri kontrol ediniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Tüm alanları doldurunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_dogrulamaKoduGonder_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            string emailAdres = controller.checkEmailAddress(txt_kullaniciAdi.Text);
            
            if(emailAdres == txt_emailAdres.Text)
            {
                Random rnd = new Random();
                code = rnd.Next(111111, 999999);

                MailAddress mailAlan = new MailAddress(txt_emailAdres.Text, txt_kullaniciAdi.Text);
                MailAddress mailGonderen = new MailAddress("system.management.user@hotmail.com", "System Admin");
                MailMessage mesaj = new MailMessage();

                mesaj.To.Add(mailAlan);
                mesaj.From = mailGonderen;
                mesaj.Subject = "Şifre Değiştirme";
                mesaj.Body = "Şifrenizi değiştirmek için doğrulama kodunuz: " + code;

                SmtpClient smtp = new SmtpClient("smtp.outlook.com", 587);
                smtp.Credentials = new System.Net.NetworkCredential("system.management.user@hotmail.com", "************");
                smtp.EnableSsl = true;
                smtp.Send(mesaj);
                MessageBox.Show("Doğrulama kodu gönderildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Hesabınıza bağlı mail adresini giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_onayla_Click(object sender, EventArgs e)
        {
            if(txt_dogrulamaKodu.Text == code.ToString())
            {
                grpbox_sifre.Enabled = true;
            }
            else
            {
                MessageBox.Show("Doğrulama kodunuz yanlış!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_degistir_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            
            if(txt_yeniSifre.Text == txt_yeniSifreTekrar.Text)
            {
                LoginStatus result = controller.changePassword(txt_kullaniciAdi.Text, txt_yeniSifre.Text);

                if(result == LoginStatus.basarili)
                {
                    MessageBox.Show("Şifreniz değiştirilmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Gerekli alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("İki şifre birbirleriyle aynı değildir!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }

        private void btn_cikisYap_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
