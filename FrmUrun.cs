using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Entitiy_Projesi
{
    public partial class FrmUrun : Form
    {
        public FrmUrun()
        {
            InitializeComponent();
        }

        Entitiy_ProjesiEntities ds = new Entitiy_ProjesiEntities();

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            cmbKategori.Text = "";
            txtAd.Text = "";
            txtDurum.Text = "";
            txtFiyat.Text = "";
            txtId.Text = "";
            txtMarka.Text = "";
            txtStok.Text = "";           

        }

        private void FrmUrun_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = (from x in ds.Tbl_Urunler
                                        select new
                                        {
                                            x.Urun_Id,
                                            x.Urun_Ad,
                                            x.Urun_Marka,
                                            x.Urun_Fiyat,
                                            x.Urun_Durum,
                                            x.Tbl_Kategoriler.Kategori_Ad,
                                            x.Urun_Stok

                                        }).ToList();



            var kategoriler = (from x in ds.Tbl_Kategoriler 
                               select new 
                               { 
                                   x.Kategori_Ad, 
                                   x.Kategori_Id 
                               }
                               ).ToList();

            cmbKategori.ValueMember = "Kategori_Id";
            cmbKategori.DisplayMember = "Kategori_Ad";
            cmbKategori.DataSource = kategoriler;

            cmbKategori.Text = "";


        }

        private void btnListele_Click(object sender, EventArgs e)
        {


            dataGridView1.DataSource = (from x in ds.Tbl_Urunler
                                        select new
                                        {
                                            x.Urun_Id,
                                            x.Urun_Ad,
                                            x.Urun_Marka,
                                            x.Urun_Fiyat,
                                            x.Urun_Durum,
                                            x.Tbl_Kategoriler.Kategori_Ad,
                                            x.Urun_Stok

                                        }).ToList();

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            Tbl_Urunler t = new Tbl_Urunler();

            t.Urun_Ad = txtAd.Text;
            t.Urun_Durum = bool.Parse(txtDurum.Text);
            t.Urun_Marka = txtMarka.Text;
            t.Urun_Fiyat = decimal.Parse(txtFiyat.Text);
            t.Urun_Kategori = int.Parse(cmbKategori.SelectedValue.ToString());
            t.Urun_Stok = short.Parse(txtStok.Text);

            ds.Tbl_Urunler.Add(t);

            ds.SaveChanges();

            MessageBox.Show("Yeni ürün eklendi, Ürün adı: "+ txtAd.Text,"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Silmek istediğiniz ürünün üzerine tıklayınız","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                var sil = ds.Tbl_Urunler.Find(Convert.ToInt32(txtId.Text));

                ds.Tbl_Urunler.Remove(sil);

                ds.SaveChanges();

                MessageBox.Show("Ürün silindi, Silinen ürün: " + txtAd.Text, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtMarka.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtFiyat.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtStok.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            txtDurum.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            cmbKategori.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

            

        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Güncellemek istediğiniz ürünün üstüne tıklayınız","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

            else
            {
                var guncel = ds.Tbl_Urunler.Find(Convert.ToInt32(txtId.Text));
                guncel.Urun_Ad = txtAd.Text;
                guncel.Urun_Marka = txtMarka.Text;
                guncel.Urun_Stok = short.Parse(txtStok.Text);
                guncel.Urun_Fiyat = decimal.Parse(txtFiyat.Text);
                guncel.Urun_Durum = bool.Parse(txtDurum.Text);

                ds.SaveChanges();

                MessageBox.Show("Ürün Güncellendi");


            }


        }
    }
}
