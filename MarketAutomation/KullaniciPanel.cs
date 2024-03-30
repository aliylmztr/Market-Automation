using MarketAutomation.controller;
using MarketAutomation.enumaration;
using MarketAutomation.model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MarketAutomation
{
    public partial class KullaniciPanel : Form
    {
        Controller controller = new Controller();
        User user = new User();
        public KullaniciPanel()
        {
            InitializeComponent();
        }

        private void KullaniciPanel_Load(object sender, EventArgs e)
        {
            fillDefaultValues();
            fillAllUsers();
        }

        private void fillDefaultValues()
        {
            cmb_yetki.Items.Add("admin");
            cmb_yetki.Items.Add("kasiyer");
            cmb_yetki.SelectedIndex = 0;

            cmb_bolge.Items.Add("Adalar");
            cmb_bolge.Items.Add("Arnavutköy");
            cmb_bolge.Items.Add("Ataşehir");
            cmb_bolge.Items.Add("Avcılar");
            cmb_bolge.Items.Add("Bağcılar");
            cmb_bolge.Items.Add("Bakırköy");
            cmb_bolge.Items.Add("Beyoğlu");
            cmb_bolge.Items.Add("Çatalca");
            cmb_bolge.Items.Add("Çekmeköy");
            cmb_bolge.Items.Add("Sancaktepe");
            cmb_bolge.SelectedIndex = 0;

            cmb_guvenlikSorusu.Items.Add("En Sevdiğiniz Hayvan Nedir?");
            cmb_guvenlikSorusu.Items.Add("En Sevdiğiniz Araba Nedir?");
            cmb_guvenlikSorusu.Items.Add("Birinci Sınıf Öğretmeninizin Adı Nedir?");
            cmb_guvenlikSorusu.Items.Add("En Sevdiğiniz Kedi İsmi Nedir?");
            cmb_guvenlikSorusu.Items.Add("Annenizin Kızlık Soyadı Nedir?");
            cmb_guvenlikSorusu.Items.Add("Hangi Şehirde Yaşıyorsunuz?");
            cmb_guvenlikSorusu.Items.Add("Babanızın Göbek Adı Nedir?");
            cmb_guvenlikSorusu.Items.Add("Çocukluk Lakabınız Nedir?");
            cmb_guvenlikSorusu.Items.Add("İlk Telefonunuzun Markası Nedir?");
            cmb_guvenlikSorusu.SelectedIndex = 0;
        }

        private void fillAllUsers()
        {
            List<User> userList = controller.getAllUsers();
            dataGridView1.DataSource = userList;
        }

        private void btn_kayitEkle_Click(object sender, EventArgs e)
        {
            user.kullaniciAdi = txt_kullaniciAdi.Text;
            user.sifre = txt_sifre.Text;
            user.yetki = cmb_yetki.SelectedItem.ToString();
            user.bolge = cmb_bolge.SelectedItem.ToString();
            user.emailAdres = txt_emailAdres.Text;
            user.guvenlikSorusu = cmb_guvenlikSorusu.SelectedItem.ToString();
            user.guvenlikCevabi = txt_guvenlikCevabi.Text;
            LoginStatus result = controller.addUser(user);

            if(result == LoginStatus.basarili)
            {
                MessageBox.Show("Kayıt eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = controller.getAllUsers();
            }
            else
            {
                MessageBox.Show("Gerekli alanların hepsini doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_kullaniciAdi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_sifre.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cmb_yetki.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cmb_bolge.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txt_emailAdres.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            cmb_guvenlikSorusu.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txt_guvenlikCevabi.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void btn_kayitGuncelle_Click(object sender, EventArgs e)
        {
            user.id = int.Parse(txt_id.Text);
            user.kullaniciAdi = txt_kullaniciAdi.Text;
            user.sifre = txt_sifre.Text;
            user.yetki = cmb_yetki.SelectedItem.ToString();
            user.bolge = cmb_bolge.SelectedItem.ToString();
            user.emailAdres = txt_emailAdres.Text;
            user.guvenlikSorusu = cmb_guvenlikSorusu.SelectedItem.ToString();
            user.guvenlikCevabi = txt_guvenlikCevabi.Text;
            LoginStatus result = controller.updateUser(user);

            if (result == LoginStatus.basarili)
            {
                MessageBox.Show("Kayıt güncellendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = controller.getAllUsers();
            }
            else
            {
                MessageBox.Show("Kayıt güncellenirken bir hata oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_kayitSil_Click(object sender, EventArgs e)
        {
            LoginStatus result = controller.deleteUser(int.Parse(txt_id.Text));
            
            if(result == LoginStatus.basarili)
            {
                MessageBox.Show("Kayıt silindi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = controller.getAllUsers();
            }
            else if(result == LoginStatus.basarisiz)
            {
                MessageBox.Show("Kayıt silinirken bir hata oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Silmek istediğiniz kaydın id değerini giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_cikisYap_Click(object sender, EventArgs e)
        {
            AdminPanel admin = new AdminPanel();
            admin.Show();
            this.Hide();
        }
    } 
}
