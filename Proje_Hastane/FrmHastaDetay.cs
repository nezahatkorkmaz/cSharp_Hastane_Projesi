using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmHastaDetay : Form
    {
        public string tc { get; set; }
        //Aşağıda "Kişi Bilgisi" kısmında TC verisini gösterebilmek için


        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;

            // Ad Soyad Çekme
            using (SqlCommand komut = new SqlCommand("SELECT HastaAd, HastaSoyad FROM Tbl_Hastalar WHERE HastaTc=@p1", bgl.baglanti()))
            {
                komut.Parameters.AddWithValue("@p1", lblTC.Text);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    lblAdSoyad.Text = dr[0] + " " + dr[1];
                }
            }

           
          
            // Randevu Geçmişi
            DataTable dt = new DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Randevular WHERE HastaTc=@p1", bgl.baglanti()))
            {
                da.SelectCommand.Parameters.AddWithValue("@p1", lblTC.Text);
                
                da.Fill(dt);
            }
            dataGridView1.DataSource = dt;



            // Brans Çekme
            using (SqlCommand komut2 = new SqlCommand("SELECT BransAd FROM Tbl_Branslar", bgl.baglanti()))
            {
                SqlDataReader dr2 = komut2.ExecuteReader();
                while (dr2.Read())
                {
                    cmbBrans.Items.Add(dr2[0]);
                }
            }
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear(); //Önceki verileri siler
            SqlCommand komut3 = new SqlCommand("SELECT DoktorAd, DoktorSoyad FROM Tbl_Doktorlar WHERE DoktorBrans = @p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                string doktorAd = dr3["DoktorAd"].ToString();
                string doktorSoyad = dr3["DoktorSoyad"].ToString();
                cmbDoktor.Items.Add(doktorAd + " " + doktorSoyad);
            }
            bgl.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Randevular WHERE " +
                "RandevuBrans='" + cmbBrans.Text + "'" + " and RandevuDoktor='" + cmbDoktor.Text + 
                "' and RandevuDurum = 0 ", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }


        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle fr = new FrmBilgiDuzenle();
            fr.TCno = lblTC.Text;
            fr.Show();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut4 = new SqlCommand("update Tbl_Randevular set RandevuDurum=1, " +
                "HastaTC=@d1, HastaSikayet=@d2  WHERE RandevuId=@d3", bgl.baglanti());
            komut4.Parameters.AddWithValue("@d1", lblTC.Text);
            komut4.Parameters.AddWithValue("@d2", rchSikayet.Text);
            komut4.Parameters.AddWithValue("@d3", txtRandevuID.Text);
            komut4.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu alındı!", "Bilgi" , MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}


