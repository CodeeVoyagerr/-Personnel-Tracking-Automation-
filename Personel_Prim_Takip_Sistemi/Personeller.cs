using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personel_Prim_Takip_ve_İtiraz_Sistemi
{
    class Personeller
    {
        private int _PersonelID;
        private string _Adi;
        private string _Soyadi;
        private string _Telefon;
        private string _Adres;
        private string _Email;
        private int _DepartmanID;
        private decimal _Maasi;
        private DateTime _GirisTarihi;
        private string _Aciklama;
        private DateTime _Tarih;
        private string _islem;
        private DateTime _CikisTarihi;


        public int PersonelID { get => _PersonelID; set => _PersonelID = value; }
        public string Adi { get => _Adi; set => _Adi = value; }
        public string Soyadi { get => _Soyadi; set => _Soyadi = value; }
        public string Telefon { get => _Telefon; set => _Telefon = value; }
        public string Adres { get => _Adres; set => _Adres = value; }
        public string Email { get => _Email; set => _Email = value; }
        public int DepartmanID { get => _DepartmanID; set => _DepartmanID = value; }
        public decimal Maasi { get => _Maasi; set => _Maasi = value; }
        public DateTime GirisTarihi { get => _GirisTarihi; set => _GirisTarihi = value; }
        public string Aciklama { get => _Aciklama; set => _Aciklama = value; }
        public DateTime Tarih { get => _Tarih; set => _Tarih = value; }
        public string Islem { get => _islem; set => _islem = value; }
        public DateTime CikisTarihi { get => _CikisTarihi; set => _CikisTarihi = value; }

        public static int PersonelIDSonKayit(Personeller p)
        {
            Veritabani.baglanti.Open();
            SqlCommand komut = new SqlCommand("select ident_current('Personeller')",Veritabani.baglanti);
            p.PersonelID=int.Parse(komut.ExecuteScalar().ToString());
            Veritabani.baglanti.Close();
            return p.PersonelID;
        }
        public static void PersonelislemEkle(Personeller p, Kullanicilar k)
        {
            k.KullaniciID = Kullanicilar.kid;
            p.Tarih = DateTime.Now;
            string sql = "insert into personelİslemleri values('"+k.KullaniciID+"' , '" + p.PersonelID + "', '" + p.Islem + "', '" + p.Aciklama + "', @Tarih)";
            SqlCommand komut = new SqlCommand();
            komut.Parameters.Add("@Tarih",SqlDbType.Date).Value = p.Tarih;
            Veritabani.ESG(komut, sql);
        }

        public static string sql = "select * from Departmanlar";
        public static string value = "DepartmanID";
        public static string text = "Departman";

        public static DataTable ComboyaKayitGetir(ComboBox combo)
        {
            DataTable tbl = new DataTable();
            Veritabani.baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter(sql, Veritabani.baglanti);
            adtr.Fill(tbl);
            combo.DataSource = tbl;
            combo.ValueMember = value;
            combo.DisplayMember = text;
            Veritabani.baglanti.Close();
            return tbl;
        }

        public static DataTable TariheGoreAra(DateTimePicker dt,DataGridView gridview)
        {
            DataTable tbl = new DataTable();
            Veritabani.baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT p.PersonelID,p.Adi,p.Soyadi,p.Telefon,p.Adres,p.Email,d.Departman, p.Durumu,p.Maasi,p.GirisTarihi, p.Aciklama FROM Personeller p , Departmanlar d WHERE p.DepartmanID = d.DepartmanID and GirisTarihi=@P1 '%", Veritabani.baglanti);
            adtr.SelectCommand.Parameters.Add("@P1",SqlDbType.Date).Value = DateTime.Parse(dt.Value.ToShortDateString());
            adtr.Fill(tbl);
            gridview.DataSource = tbl;
            Veritabani.baglanti.Close();
            return tbl;
        }
    }
}