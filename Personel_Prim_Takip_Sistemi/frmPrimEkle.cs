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
    public partial class frmPrimEkle : Form
    {
        public frmPrimEkle()
        {
            InitializeComponent();
        }

        private void frmPrimler_Load(object sender, EventArgs e)
        {
            int yil = int.Parse(DateTime.Now.Year.ToString());
            for (int i = yil; i >= (yil - 7); i--)
            {
                comboYil.Items.Add(i);
            }
            Veritabani.Listele_Ara(dataGridView1, "Select PersonelID,Adi,Soyadi,Maasi,GirisTarihi from Personeller");
        } 


        private void btnPrimEkle_Click(object sender, EventArgs e)
        {
            Primler p = new Primler();
            p.KullaniciID = Kullanicilar.kid;
            p.Donem = comboAy.Text + "/" + comboYil.Text;
            p.PrimTutari = decimal.Parse(txtPrimTutari.Text);
            p.Aciklama = txtAciklama.Text;
            p.Tarih = DateTime.Now;

            if(radioKisiyeOzel.Checked)
            {
                p.PersonelID = int.Parse(txtPersonelID.Text);
                string sql = "insert into Primler (KullaniciID,PersonelID,Donem,PrimTutari,Aciklama,Tarih) values('"+p.KullaniciID+ "','"+p.PersonelID+"','"+p.Donem+"',@PTutari,'"+p.Aciklama+"',@Tarih)";
                SqlCommand komut = new SqlCommand();
                komut.Parameters.Add("@PTutari",SqlDbType.Decimal).Value = p.PrimTutari;
                komut.Parameters.Add("@Tarih",SqlDbType.Date).Value = p.Tarih;
                Veritabani.ESG(komut, sql); 
                MessageBox.Show("İşlem Başarılı.","Prim Ekleme",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(radioTumPersoneller.Checked)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string sql = "insert into Primler (KullaniciID,PersonelID,Donem,PrimTutari,Aciklama,Tarih) values('" + p.KullaniciID + "','"+dataGridView1.Rows[i].Cells[0].Value+"','" + p.Donem + "',@PTutari,'" + p.Aciklama + "',@Tarih)";
                    SqlCommand komut = new SqlCommand();
                    komut.Parameters.Add("@PTutari", SqlDbType.Decimal).Value = p.PrimTutari;
                    komut.Parameters.Add("@Tarih", SqlDbType.Date).Value = p.Tarih;
                    Veritabani.ESG(komut, sql);
                }
                MessageBox.Show("İşlem Başarılı.", "Prim Ekleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            btnTemizle.PerformClick();
        }

        private void btnPrimleriGoster_Click(object sender, EventArgs e)
        {
            frmPrimleriGoster frm = new frmPrimleriGoster();
            frm.ShowDialog();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPersonelID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString() + " " + dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnPersoneleGorePrim_Click(object sender, EventArgs e)
        {
            frmPersoneleGorePrimler frm = new frmPersoneleGorePrimler();
            txtPersonelID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString() + " " + dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Veritabani.Listele_Ara(frm.dataGridView1, "select * from Primler where PersonelID= '" + frm.txtPersonelID + "' ");
            frm.ShowDialog();
        }
    }
}
