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
        public string tcnumara;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lbltc.Text = tcnumara;

            //Ad Soyad
            SqlCommand komut1 = new SqlCommand("select SekreterAdSoyad from Tbl_Sekreter where SekreterTc=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",lbltc.Text);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                lbladsoyad.Text = dr1[0].ToString();
                bgl.baglanti().Close();

                //Branşları Datagride Aktarma
                DataTable dt1 = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Brans",bgl.baglanti());
                da.Fill(dt1);
                dataGridView1.DataSource = dt1;


                //Doktorları Listeye Aktarma

                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd +' '+DoktorSoyad) as 'Doktor Ad Soyad',DoktorBrans from tbl_doktorlar", bgl.baglanti());
                da2.Fill(dt2);
                dataGridView2.DataSource = dt2;

                //Branşı aktarma
                SqlCommand komut2 = new SqlCommand("Select BransAd from Tbl_Brans", bgl.baglanti());
                SqlDataReader dr2 = komut2.ExecuteReader();
                while (dr2.Read())
                {
                    cmbbrans.Items.Add(dr2[0]);

                }
                bgl.baglanti().Close();

             
            }
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
         //Randevu Oluşturma Alanlarını Dolu olup olmadığını kontrol eder ve Doluysa Randevu Oluşturur.
            string brans,  doktor;
            string tarih = msktarih.Text;
            string saat = msksaat.Text;
            brans = cmbbrans.Text;
            doktor=cmbdoktor.Text;

            if (!tarih.Any(c => char.IsDigit(c)) || !saat.Any(c => char.IsDigit(c)))
            {
                MessageBox.Show("Lütfen tarih ve saat alanlarını doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            

               else if (string.IsNullOrWhiteSpace(brans) || string.IsNullOrWhiteSpace(doktor) ) 
            {
                MessageBox.Show("Lütfen Branş Ve Doktor Alanlarını Doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                SqlCommand komutkaydet = new SqlCommand("insert into tbl_randevular(RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)", bgl.baglanti());
                komutkaydet.Parameters.AddWithValue("@r1", msktarih.Text);
                komutkaydet.Parameters.AddWithValue("@r2", msksaat.Text);
                komutkaydet.Parameters.AddWithValue("@r3", cmbbrans.Text);
                komutkaydet.Parameters.AddWithValue("r4", cmbdoktor.Text);
                komutkaydet.ExecuteNonQuery();
                MessageBox.Show("Randevu Oluşturuldu", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
            bgl.baglanti().Close();

            

        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbdoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad from tbl_doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",cmbbrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
           while (dr.Read()) {
                cmbdoktor.Items.Add(dr[0]+ " "+ dr[1]);
            }
           bgl.baglanti().Close();
        }

        private void btnduyuruolustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1",rchduyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btndoktorpanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli doktorPaneli = new FrmDoktorPaneli();
            doktorPaneli.Show();
        }

        private void btnbranspanel_Click(object sender, EventArgs e)
        {
            FrmBrans frb = new FrmBrans();
            frb.Show();
        }

        private void btnliste_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fr = new FrmRandevuListesi();
            fr.Show();
        }

        private void btnduyuru_Click(object sender, EventArgs e)
        {
            Frmduyurular fr = new Frmduyurular();
            fr.Show();
        }
    }
}
