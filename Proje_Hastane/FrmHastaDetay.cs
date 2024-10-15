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
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lbltc.Text = tc;
            //AD Soyad çekme
            SqlCommand komut = new SqlCommand("select HastaAd,HastaSoyad from Tbl_Hastalar where HastaTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lbltc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[0] + " " + dr[1];
                
            }
            bgl.baglanti().Close();
            
            //Randevu Geçmiş
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Randevular where HastaTc="+tc,bgl.baglanti());
            da.Fill(dt); 
            dataGridView1.DataSource = dt;

            //Branşları Çekme
            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Brans", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbbrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();




        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Branş bilgisini çeker
            cmbdoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cmbbrans.Text);

            SqlDataReader dr3= komut3.ExecuteReader();  
            while(dr3.Read())
            {
                cmbdoktor.Items.Add(dr3[0]+" "+ dr3[1]);
            }
            bgl.baglanti() .Close();
        }

        private void cmbdoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //seçilen branşdaki doktorları getirir
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_randevular where RandevuBrans='" + cmbbrans.Text+"'" +" and RandevuDoktor='" +cmbdoktor.Text+"' and RandevuDurum=0",bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnkbilgidüzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //bilgi düzenleme ekranını açar
            frmbilgiduzenle fr = new frmbilgiduzenle();
            fr.Tcno = lbltc.Text;
            fr.Show();
        }

        private void btnrandevual_Click(object sender, EventArgs e)
        {
            //randevu onaylanır
            SqlCommand komut = new SqlCommand("update tbl_randevular set randevudurum=1 , hastatc=@p1,hastasikayet=@p2 where randevuid=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",lbltc.Text);
            komut.Parameters.AddWithValue("@p2",rchsikayet.Text);
            komut.Parameters.AddWithValue("@p3",txtid.Text);    
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı","Uyarı!",MessageBoxButtons.OK, MessageBoxIcon.Information);
           
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }
    }
}
