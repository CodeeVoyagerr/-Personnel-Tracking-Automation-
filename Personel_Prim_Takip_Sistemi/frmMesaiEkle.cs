using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personel_Prim_Takip_ve_İtiraz_Sistemi
{
    public partial class frmMesaiEkle : Form
    {
        public frmMesaiEkle()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void frmMesaiEkle_Load(object sender, EventArgs e)
        {
            int yil = DateTime.Now.Year;
            for (int i = yil; i >= yil-7; i--)
            {
                comboYil.Items.Add(i);
            }
            YapilanZamlar.ComboyaPersonelGetir(comboPersonelAdSoyad);
        }

        Label lbl;
        private void comboPersonelAdSoyad_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl = new Label();
            YapilanZamlar.ComboSecilirsePersonelIDGetir(comboPersonelAdSoyad, lbl);
            MessageBox.Show(lbl.Text);
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnMesaiEkle_Click(object sender, EventArgs e)
        {
            Personeller p = new Personeller();
            Kullanicilar k = new Kullanicilar();
            Mesailer m = new Mesailer();
            k.KullaniciID = Kullanicilar.kid;
            p.PersonelID = int.Parse(lbl.Text);
            m.Baslangic_Saati = dateTimeBaslangic.Text +" "+maskedtxtBaslangic.Text;
            m.Bitis_Saati = dateTimeBitis.Text +" "+maskedtxtBitis.Text;
            m.MesaiSaatUcreti = decimal.Parse(txtMesaiSaatUcreti.Text);
            m.Tutar = decimal.Parse(txtTutar.Text);
            m.Donem = comboAy.Text + "/" + comboYil.Text;
            m.Aciklama = txtAciklama.Text;
            m.Tarih = DateTime.Now;
            string sql = "insert into Mesailer(KullaniciID,PersonelID,BaslangicSaati,BitisSaati,MesaiSaatUcreti,Tutar,Donem,Aciklama,Tarih)  values('"+k.KullaniciID+"','"+p.PersonelID+"','"+m.Baslangic_Saati+"','"+m.Bitis_Saati+"',@MSaatUcreti,@Tutar,'"+m.Donem+"','"+m.Aciklama+"',@Tarih)";
            SqlCommand komut = new SqlCommand();
            komut.Parameters.Add("@MsaatUcreti",SqlDbType.Decimal).Value = m.MesaiSaatUcreti;
            komut.Parameters.Add("@Tutar", SqlDbType.Decimal).Value = m.Tutar;
            komut.Parameters.Add("@Tarih", SqlDbType.Date).Value = m.Tarih;

            Veritabani.ESG(komut,sql);
            MessageBox.Show("Mesai bilgileri eklendi.","Mesailer",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void txtTutar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtMesaiSaatUcreti_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string baslangic = dateTimeBaslangic.Text + " " + maskedtxtBaslangic.Text;
                string bitis = dateTimeBitis.Text + " " + maskedtxtBitis.Text;
                TimeSpan saatfarki = DateTime.Parse(bitis) - DateTime.Parse(baslangic);
                double MSaatUcreti = double.Parse(txtMesaiSaatUcreti.Text);
                double tutar = saatfarki.TotalHours*MSaatUcreti;
                txtTutar.Text = tutar.ToString("0.00");
            }
            catch 
            {

            }
        }
    }
}
