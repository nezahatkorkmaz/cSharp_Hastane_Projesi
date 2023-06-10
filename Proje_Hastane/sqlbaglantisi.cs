using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje_Hastane
{
    internal class sqlbaglantisi
    {
        public SqlConnection baglanti() // metot oluşturalım
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-3OHF6EG\\SQLEXPRESS;" +
                "Initial Catalog=HastaneProje;Integrated Security=True");
            //Tek slash idi, çift yaptık ki adres olduğunu programa bildirelim
            baglan.Open();
            return baglan;
            
        }
    }
}
