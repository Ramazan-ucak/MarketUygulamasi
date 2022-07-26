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
    public partial class AddBrand : Form
    {
        DataModel dm = new DataModel(); 
        public AddBrand()
        {
            InitializeComponent();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Brand b = new Brand();
            b.Name = tb_brandname.Text;
            b.Status = cb_Durum.Checked;

            int id = dm.AddBrand(b);

            if (id != -1)
            {
                MessageBox.Show($"Marka {id} id ile başarıyla eklenmiştir", "Başarılı");
                tb_brandname.Text = "";
                cb_Durum.Checked = false;
            }
            else
            {
                MessageBox.Show("Marka eklenirken bir hata oluştu", "Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
