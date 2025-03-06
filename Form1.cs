using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entitiy_Projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //------- entitiy kullanmak için entitiy ataması yaptık -----------
        Entitiy_ProjesiEntities db = new Entitiy_ProjesiEntities();
        //-----------------------------------------------------------------

        private void Form1_Load(object sender, EventArgs e)
        {
            // ("var" tüm değerleri alıyor)

            var kategoriler = db.Tbl_Kategoriler.ToList();//---> ToList Listeleme yapar
            // "kategoriler" nesnesi türettik ve Tbl_Kategoriler tablosundaki değerlerin listesini aldık
            
            dataGridView1.DataSource = kategoriler;
            // datagrid in datasource özelliğini "kategori" nesnemize eşitledik

        }

        private void btnListe_Click(object sender, EventArgs e)
        {

            var kategoriler = db.Tbl_Kategoriler.ToList();
            dataGridView1.DataSource = kategoriler;

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtAd.Text == "")
            {
                MessageBox.Show("Lütfen Eklenecek Kategorinin adını girin","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            else
            {
                Tbl_Kategoriler T = new Tbl_Kategoriler();
                // Kategoriler tablosundan "T" nesnesi türettik
                // "T" nesnesi sayesinde istediğimiz hücreye etki edebiliyoruz

                T.Kategori_Ad = txtAd.Text;
                db.Tbl_Kategoriler.Add(T);//---> Add ekleme yapar
                // Tbl_Kategorilere T den gelen değeri atayacak

                db.SaveChanges();
                // Değişiklikleri kaydeder "Sql de execute komutumuz gibi"
                MessageBox.Show("Kayıt yapılmıştır, Yeni kayıt: " + txtAd.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }



        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            if(txtId.Text == "")
            {
                MessageBox.Show("Litfen silmek istediğiniz kategorinin üzerine tıklayın","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            else
            {
                int x = Convert.ToInt32(txtId.Text);// Id değerimiz = x
                var ktgr = db.Tbl_Kategoriler.Find(x);//---> Find bulma işlemi yapar

                db.Tbl_Kategoriler.Remove(ktgr);// remove silme yapar

                db.SaveChanges();
                MessageBox.Show("Kayıt silindi, Silinen kayıt: " + txtAd.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtAd.Text = "";
            txtId.Text = "";
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if(txtId.Text == "")
            {
                MessageBox.Show("Güncellemek istediğiniz kategorinin üstüne tıklayın","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            else
            {

                int t = Convert.ToInt32(txtId.Text);// Id değerimiz = t
                var ktgr = db.Tbl_Kategoriler.Find(t);// t değeri olan stünu bulur

                ktgr.Kategori_Ad = txtAd.Text;// kategori adı textboxdaki değere eşitlicek

                db.SaveChanges();

                MessageBox.Show("Güncelleme işlemi yapıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            
        }
    }
}
