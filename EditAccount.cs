using DBproject.DatabaseProject;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using DBproject;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
namespace DBproject
{
    public partial class EditAccount : Form
    {
        public EditAccount()
        {
            InitializeComponent();
        }
        private void HomePage_Load(object sender, EventArgs e)
        {
            label2.Text = "Wellcome " + Login.name;
            NameTxtBox.Text = Login.name;
            EmailTxtBox.Text = Login.email;
            PasswordTxtBox.Text = Login.password;

        }
        private void btnChange_Click(object sender, EventArgs e)
        {
            string newname = NameTxtBox.Text;
            string newemail = EmailTxtBox.Text;
            string newpassword = PasswordTxtBox.Text;
            DataTable dt = new DataTable();
            DBAccess dbacc = new DBAccess();
            if (newname.Equals(""))
            {
                MessageBox.Show("Enter your new name");
            }
            else if (newemail.Equals(""))
            {
                MessageBox.Show("Enter your new email");
            }
            else if (newpassword.Equals(""))
            {

                MessageBox.Show("Enter your new password");
            }
            else
            {
                string id = Login.id;
                string query2 = "update [Users] set Email = '" + @newemail + "' , Password = '" + @newpassword + "' , fName ='" + @newname + "',lName ='" + @newname + "' where UserID= '" + @id + "'";
                SqlCommand cmd = new SqlCommand(query2);
                cmd.Parameters.AddWithValue("@fName", @newname);
                cmd.Parameters.AddWithValue("@lName", @newname);
                cmd.Parameters.AddWithValue("@email", @newemail);
                cmd.Parameters.AddWithValue("@password", @newpassword);
                int row1 = dbacc.executeQuery(cmd);
                if (row1 == 1)
                {
                    MessageBox.Show("Updates Done !");
                    this.Hide();
                    Login l = new Login();
                    l.Show();
                }
            }
        }
        private void Deletebtn_Click(object sender, EventArgs e)
        {
            DBAccess dbacc = new DBAccess();
            DialogResult dia = MessageBox.Show("Are you sure?", "Delete Account", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dia == DialogResult.Yes)
            {
                string query = "delete from [Users] where Userid ='" + Login.id + "'";
                SqlCommand cmd = new SqlCommand(query);
                int row1 = dbacc.executeQuery(cmd);
                if (row1 == 1)
                {
                    MessageBox.Show("Account Deleted!");
                    this.Hide();
                    Login l = new Login();
                    l.Show();
                }
            }
        }
        private void Skip_Click(object sender, EventArgs e)
        {
            this.Hide();
            homeadmin h = new homeadmin();
            h.Show();
        }
    }
}
