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
    public partial class frmYapilanZamlar : Form
    {
        public frmYapilanZamlar()
        {
            InitializeComponent();
            comboPersoneller.SelectedIndex = 0;
            radioYuzde.Checked = true;
        }

        private void frmYapilanZamlar_Load(object sender, EventArgs e)
        {
            int yil = int.Parse(DateTime.Now.Year.ToString());
            for (int i = yil; i >= (yil-7); i--)
            {
                comboYil.Items.Add(i);
            }
            YapilanZamlar.ComboyaPersonelGetir(comboPersoneller);
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOnay_Click(object sender, EventArgs e)
        {
            Kullanicilar k = new Kullanicilar();    
            YapilanZamlar y = new YapilanZamlar();
            y.Donem = comboAy.Text + "/" + comboYil.Text;
            y.Personel = comboPersoneller.Text;
            y.Aciklama = txtAciklama.Text;
            k.KullaniciID = Kullanicilar.kid;
            y.Tarih = DateTime.Now;

            // Yüzde radio butonu seçiliyse
            if (radioYuzde.Checked)
            {
                y.Yuzde = decimal.Parse(txtYuzde.Text);

                // Tüm personeller seçiliyse
                if (comboPersoneller.SelectedIndex == 0)
                {
                    string sql = "update Personeller set Maasi = Maasi + (Maasi*'" + y.Yuzde + "')/100";
                    SqlCommand komut = new SqlCommand();
                    Veritabani.ESG(komut, sql);
                    MessageBox.Show("Tüm personellerin maaşına yüzde '" + y.Yuzde + "' zam yapıldı.","Maaş Zammı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    string sql = "update Personeller set Maasi = Maasi + (Maasi*'" + y.Yuzde + "')/100 where personelID = '"+lblPersonelID.Text+"'";
                    SqlCommand komut = new SqlCommand();
                    Veritabani.ESG(komut, sql);
                    MessageBox.Show("" + comboPersoneller.Text + " adlı personelin maaşına yüzde " + y.Yuzde + " zam yapıldı.", "Maaş Zammı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Yapılan işlemler YapilanZamlar tablosuna eklendi.
                string sql2 = "insert into YapilanZamlar (KullaniciID,Donem,Personeller,Yuzde,Aciklama,Tarih) values('"+k.KullaniciID+ "','"+y.Donem+"','"+y.Personel+ "',@Yuzde , '"+y.Aciklama+"',@Tarih)";
                SqlCommand komut2 = new SqlCommand();
                komut2.Parameters.Add("@Yuzde", SqlDbType.Decimal).Value = y.Yuzde;
                komut2.Parameters.Add("@Tarih", SqlDbType.Date).Value = y.Tarih;
                Veritabani.ESG(komut2, sql2);
            }

            // Fiyat radio butonu seçiliyse
            if (radioFiyat.Checked)
            {
                y.Fiyat = decimal.Parse(txtFiyat.Text);

                if (comboPersoneller.SelectedIndex == 0)
                {
                    string sql = "update Personeller set Maasi = Maasi + ('" + y.Fiyat + "')";
                    SqlCommand komut = new SqlCommand();
                    Veritabani.ESG(komut, sql);
                    MessageBox.Show("Tüm personellerin maaşına '" + y.Fiyat + "' TL zam yapıldı.", "Maaş Zammı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql = "update Personeller set Maasi = Maasi + ('" + y.Fiyat + "') where personelID = '" + lblPersonelID.Text + "'";
                    SqlCommand komut = new SqlCommand();
                    Veritabani.ESG(komut, sql);
                    MessageBox.Show("" + comboPersoneller.Text + " adlı personelin maaşına " + y.Fiyat + " TL zam yapıldı.", "Maaş Zammı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                // Yapılan işlemler YapilanZamlar tablosuna eklendi.
                string sql2 = "insert into YapilanZamlar (KullaniciID,Donem,Personeller,Fiyat,Aciklama,Tarih) values('" + k.KullaniciID + "','" + y.Donem + "','" + y.Personel + "',@Fiyat , '" + y.Aciklama + "',@Tarih)";
                SqlCommand komut2 = new SqlCommand();
                komut2.Parameters.Add("@Fiyat", SqlDbType.Decimal).Value = y.Fiyat;
                komut2.Parameters.Add("@Tarih", SqlDbType.Date).Value = y.Tarih;
                Veritabani.ESG(komut2, sql2);
            }

        }

        private void radioYuzde_CheckedChanged(object sender, EventArgs e)
        {
            txtFiyat.Enabled = false;
            txtYuzde.Enabled = true;
        }

        private void radioFiyat_CheckedChanged(object sender, EventArgs e)
        {
            txtFiyat.Enabled = true;
            txtYuzde.Enabled = false;
        }

        private void comboPersoneller_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboPersoneller.SelectedIndex == 0)
            {
                lblPersonelID.Text = "0";
                return;
            }
            YapilanZamlar.ComboSecilirsePersonelIDGetir(comboPersoneller, lblPersonelID);
        }
    }
}
