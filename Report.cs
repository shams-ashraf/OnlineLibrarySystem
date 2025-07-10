using DBproject.DatabaseProject;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace DBproject
{
    public partial class Report : Form
    {
        DBAccess DBacc = new DBAccess();
       DataTable dt2 = new DataTable();
        public Report()
        {
            InitializeComponent();
            loaddata();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Hide();
            homeadmin homeadmin = new homeadmin();
            homeadmin.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditAccount editAccount = new EditAccount();
            editAccount.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditAccount editAccount = new EditAccount();
            editAccount.Show();
        }

         private void loaddata()
        {   

            string q = "select [Users].Name as Name , Title , Publication_date as 'Publication day' , Description , [User].Email , Reportid as 'Report ID' from [User] join Report on [User].Userid = Report.Userid and is_Admin ='" +true+"'";
            DataTable dt3 = new DataTable();
            DBacc.readDatathroughAdapter(q, dt3);
            dataGridView1.DataSource = dt3;
        }

        private void Report_Load(object sender, EventArgs e)
        {
            loaddata();
            if (!Login.flg)
            {
                Add.Visible = Update.Visible = Delete.Visible = false;
                pictureBox1.Visible = pictureBox2.Visible = pictureBox3.Visible = false;
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            string s1 = Titletxt.Text , s2 = destxt.Text ;
            DateTime d = dateTimePicker1.Value;
            

            string q = "insert into Report (Title , Publication_date , Description , Userid) values (@s1,  @d  ,@s2  , @value)";
            SqlCommand cmd = new SqlCommand(q);
            
            cmd.Parameters.AddWithValue("@s1", s1);
            cmd.Parameters.AddWithValue("@s2", s2);
            cmd.Parameters.AddWithValue("@d", d);
            cmd.Parameters.AddWithValue("@value", int.Parse(Login.id));
            DBacc.executeQuery(cmd);
            loaddata();
        }
        public static int repid;
       private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            Titletxt.Text = row.Cells["Title"].Value.ToString();
            destxt.Text = row.Cells["Description"].Value.ToString();
            dateTimePicker1.Text = row.Cells["Publication day"].Value.ToString();
            repid = int.Parse(row.Cells["Report ID"].Value.ToString());
        }
        private void Update_Click(object sender, EventArgs e)
        {
            string s1 = Titletxt.Text, s2 = destxt.Text;
            DateTime d = dateTimePicker1.Value;
            string q = "update Report set Title ='" +@s1+"' , Publication_date ='"+@d+"' , Description ='"+@s2+"' where Reportid='"+@repid+"'";
            SqlCommand cmd = new SqlCommand(q);
            DBacc.executeQuery(cmd);
            loaddata();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            string q;
            
            q = "delete from Report where Reportid ='"+@repid+"'";
            SqlCommand cmd = new SqlCommand(q);
            DBacc.executeQuery(cmd);
            loaddata();
        }

        private void searchbttn_Click(object sender, EventArgs e)
        {

            string q = "select [User].Name as Name , Title , Publication_date as 'Publication day' , Description , [User].Email , Reportid as 'Report ID' from [User] join Report on [User].Userid = Report.Userid and is_Admin ='" + true + "' and Title like '%"+@Searchbox.Text.ToString()+ "%'";
            SqlCommand cmd = new SqlCommand(q);
            DataTable dt = new DataTable();
            DBacc.readDatathroughAdapter(q, dt);
            dataGridView1.DataSource = dt;
             
        }
    }
}
