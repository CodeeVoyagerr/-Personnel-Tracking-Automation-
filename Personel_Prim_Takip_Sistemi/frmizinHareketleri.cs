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
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Personel_Prim_Takip_ve_İtiraz_Sistemi
{
    public partial class frmizinHareketleri : Form
    {
        public frmizinHareketleri()
        {
            InitializeComponent();
        }

        izin Izin = new izin();

        private void btnizinTurleri_Click(object sender, EventArgs e)
        {
            frmizinTurleri frm = new frmizinTurleri();
            frm.ShowDialog();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            izin i = new izin();
            i.IzinHareketID = int.Parse(txtizinHareketID.Text);
            i.PersonelID = int.Parse(txtPersonelID.Text);
            i.KullaniciID = Kullanicilar.kid;
            i.IzinTurID = (int)comboizinTuru.SelectedValue;
            i.IzinBaslangic = dateTimeBaslangic.Value;
            i.IzinBitis = dateTimeBitis.Value;
            i.Aciklama = txtAciklama.Text;
            i.Tarih = DateTime.Now;
            i.Saat = DateTime.Now;
            i.Islem = i.IzinHareketID + " nolu izin bilgileri değiştirildi.";
            string sql = "update izinHareketleri set PersonelID = '"+i.PersonelID+"', izinTurID = '"+i.IzinTurID+"',izinBaslangic = @izinBaslangic ,izinBitis = @izinBitis,islem ='"+i.Islem+"',Aciklama = '"+i.Aciklama+"',Tarih = @Tarih, Saat= @saat where izinHareketID='"+i.IzinHareketID+"' ";
            SqlCommand komut = new SqlCommand();
            komut.Parameters.Add("@izinBaslangic", SqlDbType.Date).Value = i.IzinBaslangic;
            komut.Parameters.Add("@izinBitis", SqlDbType.Date).Value = i.IzinBitis;
            komut.Parameters.Add("@Tarih", SqlDbType.Date).Value = i.Tarih;
            komut.Parameters.Add("@Saat", SqlDbType.DateTime).Value = i.Saat;

            try
            {
                Veritabani.ESG(komut, sql);
                Temizle();
                MessageBox.Show("İzin bilgileri güncellendi.", "İzin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Veritabani.Listele_Ara(dataGridView1, "select izinHareketID, PersonelID, KullaniciID, tur.izinTuru, izinBaslangic,izinBitis,islem,Aciklama,Tarih,Saat from izinHareketleri i, izinTurleri tur where i.izinTurID=tur.izinTurID");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Uyarı");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            izin i = new izin();
            i.IzinHareketID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            string sql = "delete from izinHareketleri where izinHareketID = '" + i.IzinHareketID + "'";
            SqlCommand komut = new SqlCommand();
            try
            {
                Veritabani.ESG(komut, sql);
                Temizle();
                MessageBox.Show("İzin bilgileri silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Veritabani.Listele_Ara(dataGridView1, "select izinHareketID, PersonelID, KullaniciID, tur.izinTuru, izinBaslangic,izinBitis,islem,Aciklama,Tarih,Saat from izinHareketleri i, izinTurleri tur where i.izinTurID=tur.izinTurID");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Uyarı");
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmizinHareketleri_Load(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridView1,"select izinHareketID, PersonelID, KullaniciID, tur.izinTuru, izinBaslangic,izinBitis,islem,Aciklama,Tarih,Saat from izinHareketleri i, izinTurleri tur where i.izinTurID=tur.izinTurID");
            Personeller.ComboyaKayitGetir(comboizinTuru);
        }

        private void txtPersonelID_TextChanged(object sender, EventArgs e)
        {
            Primler.PersonelAdSoyadGetir(txtPersonelID,txtAdiSoyadi);
            if (txtPersonelID.Text == "")
            {
                txtAdiSoyadi.Text = "";
            }
        }
        private void Temizle()
        {
            dateTimeBaslangic.Value = DateTime.Now;
            dateTimeBitis.Value = DateTime.Now;
            Personeller.ComboyaKayitGetir(comboizinTuru);
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            izin i = new izin();
            i.PersonelID = int.Parse(txtPersonelID.Text);
            i.KullaniciID = Kullanicilar.kid;
            i.IzinTurID = (int)comboizinTuru.SelectedValue;
            i.IzinBaslangic = dateTimeBaslangic.Value;  
            i.IzinBitis = dateTimeBitis.Value;
            i.Islem = i.PersonelID + "-" + txtAdiSoyadi.Text + " için " + comboizinTuru.Text + " oluşturuldu.";
            i.Aciklama = txtAciklama.Text;
            i.Tarih = DateTime.Now;
            i.Saat = DateTime.Now;
            string sql = "insert into izinHareketleri values('"+i.PersonelID+"','"+i.KullaniciID+"','"+i.IzinTurID+"',@izinBaslangic,@izinBitis,'"+i.Islem+"','"+i.Aciklama+"',@Tarih,@Saat)";
            SqlCommand komut = new SqlCommand();
            komut.Parameters.Add("@izinBaslangic", SqlDbType.Date).Value = i.IzinBaslangic;
            komut.Parameters.Add("@izinBitis",SqlDbType.Date).Value = i.IzinBitis;
            komut.Parameters.Add("@Tarih", SqlDbType.Date).Value = i.Tarih;
            komut.Parameters.Add("@Saat", SqlDbType.DateTime).Value = i.Saat;

            try
            {
                Veritabani.ESG(komut,sql);
                Temizle();
                MessageBox.Show("İzin kaydı oluşturuldu.","İzin",MessageBoxButtons.OK,MessageBoxIcon.Information);
                Veritabani.Listele_Ara(dataGridView1, "select izinHareketID, PersonelID, KullaniciID, tur.izinTuru, izinBaslangic,izinBitis,islem,Aciklama,Tarih,Saat from izinHareketleri i, izinTurleri tur where i.izinTurID=tur.izinTurID");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Uyarı");
            }
        }

        private void txtAciklama_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtizinHareketID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtPersonelID.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboizinTuru.Text = dataGridView1.CurrentRow.Cells["izinTuru"].Value.ToString();
            dateTimeBaslangic.Text = dataGridView1.CurrentRow.Cells["izinBaslangic"].Value.ToString();
            dateTimeBitis.Text =  dataGridView1.CurrentRow.Cells["izinBitis"].Value.ToString();
            txtAciklama.Text = dataGridView1.CurrentRow.Cells["Aciklama"].Value.ToString();
        }

        private void btnExceleAktar_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application uyg = new Microsoft.Office.Interop.Excel.Application();
            uyg.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook kitap = uyg.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sayfa = (Microsoft.Office.Interop.Excel.Worksheet)kitap.Sheets[1];
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sayfa.Cells[1,i+1];
                range.Value2 = dataGridView1.Columns[i].HeaderText;
            }
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sayfa.Cells[j + 2, i + 1];
                    range.Value2= dataGridView1[i,j].Value;
                    sayfa.Columns["B:B"].NumberFormat = "0";
                    sayfa.Columns["E:E"].NumberFormat = "gg.aa.yyyy";
                    sayfa.Columns["F:F"].NumberFormat = "gg.aa.yyyy";
                    sayfa.Columns["I:I"].NumberFormat = "gg.aa.yyyy";
                    sayfa.Columns["J:J"].NumberFormat = "gg.aa.yyyy ss:dd:nn";
                }
            }
        }

        private void comboizinTuru_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
