using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personel_Prim_Takip_ve_İtiraz_Sistemi
{
    public partial class frmAnaSayfa : Form
    {
        public frmAnaSayfa()
        {
            InitializeComponent();
        }

        void FormGetir(Form form,Panel panel)
        {
            panel.Controls.Clear();
            form.TopLevel = false;
            panel.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            form.BringToFront();
            form.Show();
            form.FormBorderStyle = FormBorderStyle.None;
        }

        private void btnDepartmanlar_Click(object sender, EventArgs e)
        {
            // Formlar arası geçiş komutları
            frmDepartmanlar frm = new frmDepartmanlar();
            frm.ShowDialog();
        }

        private void btnPersonelEkle_Click(object sender, EventArgs e)
        {
            // Formlar arası geçiş komutları
            frmPersonelEkle frm = new frmPersonelEkle();
            frm.ShowDialog();
        }

        private void btnPersonelListele_Click(object sender, EventArgs e)
        {
            frmPersonelListele frm = new frmPersonelListele();
            FormGetir(frm, panelFormlar);
        }

        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            frmKullanici k = new frmKullanici();
            k.ShowDialog();
            panelIslemler.Width = 87;
        }

        private void btnMaasZamlari_Click(object sender, EventArgs e)
        {
            frmYapilanZamlar frm = new frmYapilanZamlar();
            frm.ShowDialog();
        } 

        private void btnPrim_Click(object sender, EventArgs e)
        {
            frmPrimEkle frm = new frmPrimEkle();
            FormGetir(frm, panelFormlar);
        }

        private void btnMesaiEkle_Click(object sender, EventArgs e)
        {
            // Formlar arası geçiş komutları
            frmMesaiEkle frm = new frmMesaiEkle();
            frm.ShowDialog();
        }

        private void btnMesailer_Click_1(object sender, EventArgs e)
        {
            frmMesailer frm = new frmMesailer();
            FormGetir(frm, panelFormlar);
        }

        private void btnizinHareketListele_Click(object sender, EventArgs e)
        {
            frmizinHareketleri frm = new frmizinHareketleri();
            FormGetir(frm, panelFormlar);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (panelIslemler.Width == 200)
            {
                panelIslemler.Width = 87;
            }
            else if (panelIslemler.Width == 87)
            {
                panelIslemler.Width = 200;
            }
        }

        private void panelIslemler_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelFormlar_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}