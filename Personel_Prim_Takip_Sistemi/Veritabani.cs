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
    public class Veritabani
    {
        public static SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KM71DF3;Initial Catalog=Personel_Takip;Integrated Security=True");

        public static void ESG(SqlCommand cmd, string sql)
        {
            
                baglanti.Open(); // Bağlantıyı aç.
                cmd.Connection = baglanti;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                baglanti.Close(); // Bağlantıyı kapat.
            
            
        }

  

        // Listeleme için metotumuz
        public static DataTable Listele_Ara(DataGridView gridView , string sql)
        {
            DataTable tbl = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter(sql, baglanti);
            adtr.Fill(tbl);
            gridView.DataSource = tbl; // Tabloyu gridview e aktarıyoruz.
            baglanti.Close();

            return tbl;
        }

    }
}
