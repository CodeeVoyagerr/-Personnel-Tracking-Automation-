using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personel_Prim_Takip_ve_İtiraz_Sistemi
{
    public class Departmanlar
    {
        private int _DepartmanID;
        private string _Departman;
        private string _Aciklama;

        public int DepartmanID { get => _DepartmanID; set => _DepartmanID = value; }
        public string Departman { get => _Departman; set => _Departman = value; }
        public string Aciklama { get => _Aciklama; set => _Aciklama = value; }

        public static SqlDataReader DepartmanGetir(ListView lst)
        {
            Veritabani.baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From Departmanlar", Veritabani.baglanti); // Tüm kayıtlar ve veritabanı bağlantısı çağrıldı.
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read()) // İşlem gerçekleştiği sürece
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr[0].ToString(); // DepartmanID
                ekle.SubItems.Add(dr[1].ToString()); // Departman
                ekle.SubItems.Add(dr[2].ToString()); // Açıklama
                lst.Items.Add(ekle);
            }
            Veritabani.baglanti.Close();
            return dr;
        }
    }
}