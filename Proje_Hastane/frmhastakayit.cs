﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;



namespace Proje_Hastane
{
    public partial class FrmUyeKayit : Form
    {
        public FrmUyeKayit()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl= new sqlbaglantisi();

        private void btnkayityap_Click(object sender, EventArgs e)
        {
            //Hasta kayıt eder
            SqlCommand komut = new SqlCommand("insert into Tbl_Hastalar(HastaAd,HastaSoyad,HastaTc,HastaTelefon,HastaSifre,HastaCinsiyet) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3", msktc.Text);
            komut.Parameters.AddWithValue("p4", msktelefon.Text);
            komut.Parameters.AddWithValue("p5", txtsifre.Text);
            komut.Parameters.AddWithValue("p6", cmbcinsiyet.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Gerçekleşmiştir Şifreniz: "+txtsifre.Text,"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);


        }

        private void FrmUyeKayit_Load(object sender, EventArgs e)
        {

        }
    }
}
