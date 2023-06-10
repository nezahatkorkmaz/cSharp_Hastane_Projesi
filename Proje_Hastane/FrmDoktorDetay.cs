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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        public string TC;

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            txtTC.Text = TC;

            //Doktor Ad Soyad
            SqlCommand komut = new SqlCommand(" select DoktorAd, DoktorSoyad From Tbl_Doktorlar " +
                " WHERE DoktorTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read()) // if sadece girişlerde kullanılıyor, eşitlik kontrollerinde
            {
                TxtAdSoyad.Text = dr[0] + " " + dr[1];

            }
            bgl.baglanti().Close();

            //Randevuları görüntüleyelim


            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Randevular WHERE" +
                " RandevuDoktor='" + TxtAdSoyad.Text + "'", bgl.baglanti());


            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rchRandevuDetay.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
