using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;

namespace CosmosMarket
{
    
    public partial class ProductForm : Form
    {
        DataModel dm = new DataModel();
        int selectedid = 0;
        public ProductForm()
        {
            InitializeComponent();
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            cb_marka.DisplayMember = "Name";
            cb_marka.ValueMember = "ID";
            cb_marka.DataSource = dm.GetBrands();
            cb_kategori.DisplayMember = "Name";
            cb_kategori.ValueMember = "ID";
            cb_kategori.DataSource = dm.GetCategories();
            dataGridView1.DataSource = dm.GetProducts();
            dataGridView1.Columns[0].HeaderText = "No";
            dataGridView1.Columns[1].HeaderText = "Barkod Numarası";
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].HeaderText = "İsim";
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].HeaderText = "Kategori";
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].HeaderText = "Marka";
            dataGridView1.Columns[7].HeaderText = "Stok";
            dataGridView1.Columns[8].HeaderText = "Fiyat";


        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            Product p = new Product()
            {
                BarcodeNo = tb_barcode.Text,
                ProductName = tb_isim.Text,
                Category_ID = Convert.ToInt32(cb_kategori.SelectedValue),
                Brand_ID = Convert.ToInt32(cb_marka.SelectedValue),
                Stock = Convert.ToInt32(num_stock.Value),
                Price = Convert.ToDecimal(tb_fiyat.Text)
            };
            int id = dm.AddProduct(p);
            if (id != -1)
            {
                MessageBox.Show($"Ürün {id} id ile başarıyla eklenmiştir", "Başarılı");
            }
            else
            {
                MessageBox.Show("Ürün eklenirken bir hata meydana geldi", "Hata");
            }
            dataGridView1.DataSource = dm.GetProducts();
            tb_ID.Text = "";
            tb_barcode.Text = "";
            tb_isim.Text = "";
            num_stock.Value = 0;
            tb_fiyat.Text = "";

        }
        private void btn_addCategry_Click(object sender, EventArgs e)
        {
            AddCategory frm = new AddCategory();
            frm.ShowDialog();
            cb_kategori.DataSource = dm.GetCategories();
        }

        private void addBtn_Brand_Click(object sender, EventArgs e)
        {
            AddBrand frm = new AddBrand();
            frm.ShowDialog();
            cb_marka.DataSource = dm.GetBrands();
        }

        private void TSMI_guncelle_Click(object sender, EventArgs e)
        {
            Product p = dm.GetProduct(selectedid);
            tb_ID.Text = p.ID.ToString();
            tb_barcode.Text = p.BarcodeNo;
            tb_isim.Text = p.ProductName;
            cb_kategori.SelectedValue = p.Category_ID;
            cb_marka.SelectedValue = p.Brand_ID;
            tb_fiyat.Text = p.Price.ToString();
            num_stock.Value = p.Stock;
            btn_guncelle.Visible = true;



        }
        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            Product p = dm.GetProduct(selectedid);
            p.BarcodeNo = tb_barcode.Text;
            p.Category_ID = Convert.ToInt32(cb_kategori.SelectedValue);
            p.Brand_ID = Convert.ToInt32(cb_marka.SelectedValue);
            p.ProductName = tb_isim.Text;
            p.Stock = Convert.ToInt32(num_stock.Value);
            p.Price = Convert.ToDecimal(tb_fiyat.Text);
            if (dm.UpdateProduct(p))
            {
                dataGridView1.DataSource = dm.GetProducts();
                MessageBox.Show("Güncelleme Başarılı", "Başarılı");
                btn_guncelle.Visible = false;
            }
            else
            {
                MessageBox.Show("Hata oluştu", "Hata"); 
            }
        }
        private void TSMI_sil_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show(selectedid.ToString() + "id li ürünü silmek istediğinize emin misiniz?","Ürün sil", MessageBoxButtons.YesNo);
            if (sonuc == DialogResult.Yes)
            {
                if (dm.DeleteProduct(selectedid))
                {
                    dataGridView1.DataSource = dm.GetProducts();
                    MessageBox.Show("Silme  işlemi başarılı", "Başarılı");
                }
                else
                {
                    MessageBox.Show("Hata oluştu", "Hata");
                }
                
            }
            else
            {
                MessageBox.Show("Kullancı silme işlemini iptal etti");
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = dataGridView1.HitTest(e.X, e.Y);
                dataGridView1.ClearSelection();
                if (hit.RowIndex != -1)
                {
                    dataGridView1.Rows[hit.RowIndex].Selected = true;
                    this.contextMenuStrip1.Show(this.dataGridView1, new Point(e.X,e.Y));
                    selectedid = Convert.ToInt32(dataGridView1.Rows[hit.RowIndex].Cells[0].Value);
                }
            }
        }

       
    }
}
