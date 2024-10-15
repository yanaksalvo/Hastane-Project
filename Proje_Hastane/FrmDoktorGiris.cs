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
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace Proje_Hastane
{
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmDoktorGiris_Load(object sender, EventArgs e)
        {

        }

        private void btngirisyap_Click(object sender, EventArgs e)
        {
            //doktor detaya giriş yapmak için kullanılır kullanıcı adı ve ya şifre yanlışsa girmez
          SqlCommand komut = new SqlCommand("select * from tbl_doktorlar where doktortc=@p1 and doktorsifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktc.Text);
            komut.Parameters.AddWithValue("@p2",txtsifre.Text);
            SqlDataReader dr = komut.ExecuteReader();   
            if (dr.Read())
            {
                FrmDoktorDetay fr = new FrmDoktorDetay();
                fr.tc = msktc.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("TC Veya Şifreyi Yanlış Girdiniz!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        
        }
    }
}
