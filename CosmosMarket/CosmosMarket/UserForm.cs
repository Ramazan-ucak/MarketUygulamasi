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
    public partial class UserForm : Form
    {
        bool girisVar = false;
        DataModel dm = new DataModel();
        public UserForm()
        {
           
            InitializeComponent();
        }
        private void btn_kapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_giris_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_userName.Text) && !string.IsNullOrEmpty(tb_password.Text))
            {
                User u = dm.UserLogin(tb_userName.Text, tb_password.Text);
                if (u != null)
                {
                    girisVar = true;
                    ActiveUser.user = u;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Bulunamadı", "Sistem Mesajı");
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ve Şifre boş bırakılamaz","Sistem Mesajı");
            }
        }

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (girisVar == false)
            {
                Application.Exit();
            }
        }
    }
}
