using DBproject.DatabaseProject;
using System;
using System.Data;
using System.Windows.Forms;
namespace DBproject
{
    public partial class Login : Form
    {
        public static string id, name, password, email;
        public static bool flg;

        private void signUpLaple_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            SignUp s = new SignUp();
            s.Show();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            idbox.Text = "";
            PasswordTxtBox.Text = "";
        }
        DBAccess dbacc = new DBAccess();
        DataTable dt = new DataTable();
        public Login()
        {
            InitializeComponent();
        }
       private void btnLogin_Click(object sender, EventArgs e)
        {
            string lid = idbox.Text;
            string Password = PasswordTxtBox.Text;
            if (string.IsNullOrEmpty(lid))
            {
                MessageBox.Show("enter your id");
            }
            else if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("enter Password");
            }
            else
            {
                string query = "Select * from [Users] where UserID = '" + lid + "' and Password = '" + Password + "'";
                dbacc.readDatathroughAdapter(query, dt);
                if (dt.Rows.Count > 0)
                {
                    id = dt.Rows[0]["UserID"].ToString();
                    name = dt.Rows[0]["fName"].ToString();
                    name = dt.Rows[0]["lName"].ToString();
                    email = dt.Rows[0]["Email"].ToString();
                    password = dt.Rows[0]["Password"].ToString();
                    flg = dt.Rows[0]["Admin"].Equals(1);
                    MessageBox.Show("Your account exists!");
                    dbacc.closeConn();
                    this.Hide();
                    EditAccount h = new EditAccount();
                    h.Show();
                }
            }
        }
    }
}
