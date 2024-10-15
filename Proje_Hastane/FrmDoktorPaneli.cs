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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void txtad_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            //Doktorları veri tabanından çeker 
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from tbl_doktorlar", bgl.baglanti());
            da1.Fill(dt1);
           dataGridView1.DataSource = dt1;


            //Branşları combobox a aktarma
            cmbbrans.Items.Clear();
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad from tbl_doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cmbbrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbbrans.Items.Add(dr[0] + " " + dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {

            //Doktor Ekleme 
            string ad, soyad, tc, brans, sifre;
            ad= txtad.Text;
            soyad= txtsoyad.Text;
            brans=cmbbrans.Text;
            tc=msktc.Text;
            sifre=txtsifre.Text;

            if(!tc.Any(c => char.IsDigit(c)))
            {
                MessageBox.Show("Lütfen TC Alanını doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(brans) || string.IsNullOrWhiteSpace(soyad) ||string.IsNullOrWhiteSpace(ad) || string.IsNullOrWhiteSpace(sifre))
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand komut1 = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTc,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5)", bgl.baglanti());
                komut1.Parameters.AddWithValue("@d1", txtad.Text);
                komut1.Parameters.AddWithValue("@d2", txtsoyad.Text);
                komut1.Parameters.AddWithValue("@d3", cmbbrans.Text);
                komut1.Parameters.AddWithValue("@d4", msktc.Text);
                komut1.Parameters.AddWithValue("@d5", txtsifre.Text);
                komut1.ExecuteNonQuery();
                MessageBox.Show("Doktor Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

          
            bgl.baglanti().Close();
            
        }

       

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //seçilen kısmın bilgilerini boşluklara getirir
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtsoyad.Text= dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            msktc.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            cmbbrans.Text= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtsifre.Text= dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            //kayıt silme işlemi
            SqlCommand komut = new SqlCommand("delete from Tbl_Doktorlar where DoktorTc=@p1",bgl.baglanti());   
            komut.Parameters.AddWithValue("@p1",msktc.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
            
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            //Doktor bilgi güncellemek için kullanılır 
            SqlCommand komut = new SqlCommand("update tbl_doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorSifre=@d5 where DoktorTc=@d4",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", txtad.Text);
            komut.Parameters.AddWithValue("@d2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@d3", cmbbrans.Text);
            komut.Parameters.AddWithValue("@d4", msktc.Text);
            komut.Parameters.AddWithValue("@d5", txtsifre.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Doktor Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
