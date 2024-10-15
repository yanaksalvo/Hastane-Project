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
    public partial class Frmduyurular : Form
    {
        public Frmduyurular()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void Frmduyurular_Load(object sender, EventArgs e)
        {
            //veri tabanından duyuruları çeker
            DataTable dt = new DataTable(); 
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_duyurular",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
    }
}
