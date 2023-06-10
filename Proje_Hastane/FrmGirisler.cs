using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje_Hastane
{
    public partial class FrmGirisler : Form
    {
        public FrmGirisler()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void btnHastaGirisi_Click_1(object sender, EventArgs e)
        {

            FrmHastaGiris fr = new FrmHastaGiris();
            fr.Show();
            this.Hide(); // yani üzerinde çalıştığımız formu gizle, bu sekmeyi göster
        }

        private void btnDoktorGirisi_Click_1(object sender, EventArgs e)
        {
            FrmDoktorGiris fr = new FrmDoktorGiris();
            fr.Show();
            this.Hide(); // yani üzerinde çalıştığımız formu gizle, bu sekmeyi göster
        }

        private void btnSekreterGirisi_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris fr = new FrmSekreterGiris();
            fr.Show();
            this.Hide(); // yani üzerinde çalıştığımız formu gizle, bu sekmeyi göster
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
