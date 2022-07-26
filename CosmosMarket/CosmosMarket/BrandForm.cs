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
    public partial class BrandForm : Form
    {
        DataModel dm = new DataModel();
        int selectedid = 0;
        public BrandForm()
        {
            InitializeComponent();
        }
        private void BrandForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dm.GetBrands();
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            Brand b = new Brand();
            b.Name = tb_Name.Text;
            b.Status = cb_durum.Checked;

            int id = dm.AddBrand(b);

            if (id != -1)
            {
                MessageBox.Show($" Marka {id} id ile başaryla eklenmiştir", "Başarılı");
                tb_Name.Text = "";
                cb_durum.Checked = false;
            }
            else
            {
                MessageBox.Show("Marka eklenirken bir hata oluştu", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            dataGridView1.DataSource = dm.GetBrands();
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
                    contextMenuStrip1.Show(this.dataGridView1, new Point(e.X, e.Y));
                    selectedid = Convert.ToInt32(dataGridView1.Rows[hit.RowIndex].Cells[0].Value);
                }
            }
        }

        private void btn_guncelle_Click(object sender, EventArgs e)
        {
            Brand b = dm.GetBrand(selectedid);
            b.Name = tb_Name.Text;
            b.Status = cb_durum.Checked;
            if (dm.UpdateBrand(b))
            {
                MessageBox.Show("Marka başarıyla güncellendi", "Başarılı");
            }
            else
            {
                MessageBox.Show("Marka güncellenirken bir hata oluştu", "Hata");
            }
            dataGridView1.DataSource = dm.GetBrands();
            tb_ID.Text = tb_Name.Text = "";
            cb_durum.Checked = false;
            btn_guncelle.Visible = false;
          
            

        }

        private void TSMI_guncelle_Click(object sender, EventArgs e)
        {
            if (selectedid != -1)
            {
                Brand b = dm.GetBrand(selectedid);
                tb_ID.Text = b.ID.ToString();
                tb_Name.Text = b.Name;
                cb_durum.Checked = b.Status;
                btn_guncelle.Visible = true;
            }
        }

        private void TSMI_sil_Click(object sender, EventArgs e)
        {
            if (selectedid != 0)
            {
                if (dm.DeleteBrand(selectedid))
                {
                    MessageBox.Show("Silme işlemi başarılı", "Başarılı");
                }
                else
                {
                    MessageBox.Show("Hata oluştu", "Hata");
                }
                dataGridView1.DataSource = dm.GetBrands();
            }
        }
    }
}
