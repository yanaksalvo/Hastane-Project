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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        
        private void FrmSekreterGiris_Load(object sender, EventArgs e)
        {

        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void btngirisyap_Click(object sender, EventArgs e)
        {
            //sekreterin giriş yapabilmesi için bilgileri kontrol eder
            SqlCommand komut = new SqlCommand("Select * from Tbl_Sekreter where SekreterTc=@p1 and SekreterSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktc.Text);
            komut.Parameters.AddWithValue("@p2", txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
             if (dr.Read()) { 

                FrmSekreterDetay frs = new FrmSekreterDetay();
                frs.tcnumara=msktc.Text;
                frs.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Şifre veya TC yanlış girdiniz", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
             bgl.baglanti().Close();
                    
        }
    }
}
