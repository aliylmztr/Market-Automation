using MarketAutomation.controller;
using MarketAutomation.enumaration;
using MarketAutomation.model;
using System;
using System.Windows.Forms;

namespace MarketAutomation
{
    public partial class UrunPanel : Form
    {
        Controller controller = new Controller();
        Urun urun = new Urun();
        public UrunPanel()
        {
            InitializeComponent();
        }

        private void UrunPanel_Load(object sender, EventArgs e)
        {
            fillDefaultValues();
            getAllProducts();
        }

        public void getAllProducts()
        {
            dataGridView1.DataSource = controller.getAllProducts();
        }

        public void fillDefaultValues()
        {
            cmb_urunIsim.Items.Add("Brokoli");
            cmb_urunIsim.Items.Add("Çilek");
            cmb_urunIsim.Items.Add("Elma");
            cmb_urunIsim.Items.Add("Lahana");
            cmb_urunIsim.Items.Add("Muz");
            cmb_urunIsim.Items.Add("Portakal");
            cmb_urunIsim.Items.Add("Üzüm");
            cmb_urunIsim.Items.Add("Barbunya");
            cmb_urunIsim.Items.Add("Fasulye");
            cmb_urunIsim.Items.Add("Mısır");
            cmb_urunIsim.Items.Add("Süt");
            cmb_urunIsim.Items.Add("Peynir");
            cmb_urunIsim.Items.Add("Yoğurt");
            cmb_urunIsim.Items.Add("Biftek");
            cmb_urunIsim.Items.Add("Et Şiş");
            cmb_urunIsim.Items.Add("Pastırma");
            cmb_urunIsim.Items.Add("Sosis");
            cmb_urunIsim.Items.Add("Tavuk");            
        }

        private void btn_cikisYap_Click(object sender, EventArgs e)
        {
            AdminPanel admin = new AdminPanel();
            admin.Show();
            this.Hide();
        }

        private void btn_kayitEkle_Click(object sender, EventArgs e)
        {
            urun.id = txt_id.Text;
            urun.qrKod = txt_qrKod.Text;
            urun.barkod = txt_barkod.Text;
            urun.olusturulmaTarih = dtp_olusturulmaTarih.Value;
            urun.guncellenmeTarih = dtp_guncellenmeTarih.Value;
            urun.urunIsim = cmb_urunIsim.SelectedItem.ToString();
            urun.kilo = int.Parse(txt_kilo.Text);
            urun.fiyat = int.Parse(txt_fiyat.Text);
            urun.ciro = int.Parse(txt_ciro.Text);
            LoginStatus result = controller.addProduct(urun);

            if (result == LoginStatus.basarili)
            {
                MessageBox.Show("Kayıt eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = controller.getAllProducts();
            }
            else if (result == LoginStatus.basarisiz)
            {
                MessageBox.Show("Kayıt eklenirken bir hata oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Gerekli alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_qrKod.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_barkod.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dtp_olusturulmaTarih.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            dtp_guncellenmeTarih.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString());
            cmb_urunIsim.SelectedItem = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txt_kilo.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txt_fiyat.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txt_ciro.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        private void btn_kayitGuncelle_Click(object sender, EventArgs e)
        {
            urun.id = txt_id.Text;
            urun.qrKod = txt_qrKod.Text;
            urun.barkod = txt_barkod.Text;
            urun.olusturulmaTarih = dtp_olusturulmaTarih.Value;
            urun.guncellenmeTarih = dtp_guncellenmeTarih.Value;
            urun.urunIsim = cmb_urunIsim.SelectedItem.ToString();
            urun.kilo = int.Parse(txt_kilo.Text);
            urun.fiyat = int.Parse(txt_fiyat.Text);
            urun.ciro = int.Parse(txt_ciro.Text);
            LoginStatus result = controller.updateProduct(urun);

            if (result == LoginStatus.basarili)
            {
                MessageBox.Show("Kayıt güncellendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = controller.getAllProducts();
            }
            else if (result == LoginStatus.basarisiz)
            {
                MessageBox.Show("Kayıt güncellenirken bir hata oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Gerekli alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_kayitSil_Click(object sender, EventArgs e)
        {
            LoginStatus result = controller.deleteProduct(txt_id.Text);

            if (result == LoginStatus.basarili)
            {
                MessageBox.Show("Kayıt silindi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = controller.getAllProducts();
            }
            else if (result == LoginStatus.basarisiz)
            {
                MessageBox.Show("Kayıt silinirken bir hata oluştu!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Silmek istediğiniz kaydın id değerini giriniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
