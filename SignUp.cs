using DBproject.DatabaseProject;
using System;
using System.Windows.Forms;

using System.Data.SqlClient;
namespace DBproject
{
    public partial class SignUp : Form
    {
        DBAccess DBacc = new DBAccess(); 
        public SignUp()
        {
            InitializeComponent();
        }
        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string fName = NameTxtBox.Text;
            string lName = NameTxtBox.Text;
            string password = PasswordTxtBox.Text;
            string email = EmailTxtBox.Text;
            string id = idbox.Text;
            bool isAdmin = IsAdmin.Checked;
            if (string.IsNullOrEmpty(fName) || string.IsNullOrEmpty(lName) ||
                  string.IsNullOrEmpty(id) || string.IsNullOrEmpty(email) ||
                  string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }
            else
            {
                SqlCommand insert = new SqlCommand("insert into [Users] (UserID ,fName ,lname, Email , Password ,Admin) values(@UserID, @fName,@lname , @email, @password, @Admin)");
                insert.Parameters.AddWithValue("@fName", fName);
                insert.Parameters.AddWithValue("@lName", lName);
                insert.Parameters.AddWithValue("@email", email);
                insert.Parameters.AddWithValue("@password", password);
                insert.Parameters.AddWithValue("@UserID", id);
                insert.Parameters.AddWithValue("@Admin", isAdmin);
                int row1 = DBacc.executeQuery(insert);
                if (row1 == 1)
                {
                    MessageBox.Show("Account Created Successfully!");
                    this.Hide();
                    Login login = new Login();  
                    login.Show();
                }
                else
                {
                    MessageBox.Show("Error Occurred");
                }
            }
             
        }


    }
}




