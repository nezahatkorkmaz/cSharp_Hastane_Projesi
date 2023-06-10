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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        public string TCnumara;

        private void rchRandevuDetay_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = bgl.baglanti(); // Bağlantıyı oluşturun
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@p1)", connection); // SqlCommand nesnesine bağlantıyı atayın
            komut.Parameters.AddWithValue("@p1", rchDuyuruOlustur.Text);
            komut.ExecuteNonQuery();
            connection.Close(); // Bağlantıyı kapatın
            MessageBox.Show("Duyuru oluşturuldu!");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            txtTC.Text = TCnumara;
            //Ad Soyad

            SqlCommand komut1 = new SqlCommand("Select SekreterAdSoyad From Tbl_Sekreterler Where SekreterTC=@p1",
                bgl.baglanti());

            komut1.Parameters.AddWithValue("@p1", txtTC.Text);

            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                TxtAdSoyad.Text = dr1[0].ToString();
            }
            bgl.baglanti().Close();

            //Bransları DataGride aktaralım
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Branslar", bgl.baglanti());

            // Verileri veritabanından almak veya veritabanına kaydetmek için genellikle kullanılan yöntemlerde
            // SqlDataAdapter kullanılır.
            da.Fill(dt1);
            dataGridView2.DataSource = dt1;

            //Doktorları listeye aktaralım
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd +' '+ DoktorSoyad) as 'Mevcut Doktor'," +
                " DoktorBrans as 'Brans' From Tbl_Doktorlar ", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView3.DataSource = dt2;

            //Branşı comboBoxa aktaralım
            SqlCommand komut2 = new SqlCommand("Select BransAd From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbBrans.Items.Add(dr2[0]);

            }
            bgl.baglanti().Close();

        }

        private void txtTC_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand(" insert into Tbl_Randevular (RandevuTarih, RandevuSaat, RandevuBrans, RandevuDoktor)"+
                " values (@r1,@r2, @r3, @r4)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1", mskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2", mskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3", cmbBrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4", cmbDoktor.Text);
            //insert komutu olduğu için ExecuteNonQuery
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu olusturulmustur!");


        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            //Seçileni her seferinde temizlemek içindi bu.


            //Doktor Adlarını comboBoxa aktaralım
            SqlCommand komut3 = new SqlCommand("Select DoktorAd, DoktorSoyad From Tbl_Doktorlar Where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                cmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);

            }
            bgl.baglanti().Close();

        }

        private void btnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();

        }

        private void btnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBrans frb = new FrmBrans();
            frb.Show();

        }

        private void btnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fr1 = new FrmRandevuListesi();
            fr1.Show();

        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frd = new FrmDuyurular();
            frd.Show();
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
