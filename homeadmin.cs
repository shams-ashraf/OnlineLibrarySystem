using DBproject.DatabaseProject;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace DBproject
{
    public partial class homeadmin : Form
    {
        DBAccess DBacc = new DBAccess();
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
         
        public homeadmin()
        {
            InitializeComponent();
            loaddata();
        }
        private void loaddata()
        {            
            string q = "select * from Books join Publishers on Books.PublisherID = Publishers.PublisherID join BookAuthors on Books.ISBN =BookAuthors.isbn join Authors on Authors.AuthorID = BookAuthors.Authorid";
            DataTable dt3 = new DataTable();
            DBacc.readDatathroughAdapter(q, dt3);
            BookList.DataSource = dt3;
        }
        public static int  pbid,  pbid2;
        private void Add_Click(object sender, EventArgs e)
        {
            // get the values of the filds
            string bkname = BKName.Text;
            string ispn = jspntxt.Text;
            string author = Authorname.Text;
            string Publishername = pubname.Text;
            DateTime authordate = dateTimePicker1.Value;
            DateTime pubdate = dateTimePicker2.Value;
            int yearpub = int.Parse(Year.Text);
            string cat = Catgtxt.Text;

            // insert into publisher table
            SqlCommand putpub = new SqlCommand("insert into Publishers (Name, BookID ,publisherid) values(@Publishername , @ispn ,@publisherid) ; SELECT SCOPE_IDENTITY()");
            putpub.Parameters.AddWithValue("@Publishername", Publishername);
            putpub.Parameters.AddWithValue("@ispn", ispn); 
            putpub.Parameters.AddWithValue("@publisherid", publisherid);
            
            using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=Online Library;Integrated Security=True"))
            {
                connection.Open();               
                putpub.Connection = connection;             
                  pbid = Convert.ToInt32(putpub.ExecuteScalar());
            }
            // end insert into publisher table
            // insert into author 
            SqlCommand puta = new SqlCommand("insert into Authors (Name , Bookid) values(@author , @ispn) ; SELECT SCOPE_IDENTITY()");
            puta.Parameters.AddWithValue("@author", author);
            puta.Parameters.AddWithValue("@ispn", ispn);           
            using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=Online Library;;Integrated Security=True"))
            {
                connection.Open();               
                puta.Connection = connection;             
                pbid2 = Convert.ToInt32(puta.ExecuteScalar());
            }
            // end of inser into author

            // insert into book
            SqlCommand putb = new SqlCommand("insert into book (ISBN , PublicationYear , PublisherID, BookName , Category) values(@ispn, @yearpub,@pbid,@bkname ,@cat)");
            putb.Parameters.AddWithValue("@ispn",ispn);
            putb.Parameters.AddWithValue("@yearpub",yearpub);
            putb.Parameters.AddWithValue("@pbid", pbid);
            putb.Parameters.AddWithValue("@bkname", bkname);
            putb.Parameters.AddWithValue("@cat", cat);
            DBacc.executeQuery(putb);
            // end insert into book 
            //insert into book_autor
            SqlCommand i = new SqlCommand("insert into book_Author (isbn, Authorid) values(@ispn , @pbid2)");
            i.Parameters.AddWithValue("@ispn", ispn);
            i.Parameters.AddWithValue("@pbid2",pbid2);
            DBacc.executeQuery(i);
            //end insert into book_autor
            loaddata();
        }
        public static int authorid, publisherid ;
        public static string book;
        private void BookList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                
                DataGridViewRow row = this.BookList.Rows[e.RowIndex];
                BKName.Text = row.Cells["BookName"].Value.ToString();
                dateTimePicker2.Text = row.Cells["construction_date"].Value.ToString() ;
                Year.Text = row.Cells["PublicationYear"].Value.ToString();
                jspntxt.Text = row.Cells["ISBN"].Value.ToString();
                Authorname.Text = row.Cells["Name1"].Value.ToString();
                pubname.Text = row.Cells["Name"].Value.ToString();
                Catgtxt.Text = row.Cells["Category"].Value.ToString();
                authorid = int.Parse(row.Cells["AuthorID1"].Value.ToString());
                publisherid = int.Parse(row.Cells["PublisherID1"].Value.ToString());
                book = jspntxt.Text;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditAccount editAccount = new EditAccount();
            editAccount.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditAccount editAccount = new EditAccount();
            editAccount.Show();
        }

        private void searchbttn_Click(object sender, EventArgs e)
        {
            
            string target = Searchbox.Text;
            if (target.Equals(""))
            {
                loaddata();
            }
            string q = "select * from books join Publishers on Books.PublisherID = Publishers.PublisherID join BookAuthors on books.ISBN = bookAuthors.isbn join Author on Authors.AuthorID = book_Author.Authorid where book.ISBN like '%" +@target+ "%' or book.BookName like '%" +@target+ "%' or Publisher.Name like '%" +@target+ "%' or book.PublicationYear like '%"+@target+"%' or Author.Name like'%" +@target+"%'";
            DataTable dt3 = new DataTable();
            DBacc.readDatathroughAdapter(q, dt3);
            BookList.DataSource = dt3;
        }

        private void label12_Click(object sender, EventArgs e)
        {
            this.Hide();
            Report report = new Report();
            report.Show();
        }

        private void homeadmin_Load(object sender, EventArgs e)
        {
            if (!Login.flg)
            {
                Add.Visible = false;
                Delete.Visible = false;
                Update.Visible = false;
                pictureBox1.Visible = pictureBox2.Visible = pictureBox3.Visible = false;
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            // update the book
            string s1 = BKName.Text , s2 = Year.Text ,s3 = Catgtxt.Text ,s4 = jspntxt.Text;
            string q = "update book set BookName = '"+@s1 +"', PublicationYear='"+int.Parse(@s2)+"' , Category='"+@s3+"' where ISBN ='" +@s4+ "'" ;
            SqlCommand cmd = new SqlCommand(q);
            cmd.Parameters.AddWithValue("@s1", s1);
            cmd.Parameters.AddWithValue("@s2", s2);
            cmd.Parameters.AddWithValue("@s3", s3);
            DBacc.executeQuery(cmd);

            // update the publisher
            s1 = pubname.Text; s2 = jspntxt.Text;
            DateTime d = dateTimePicker2.Value;
            string q2 = "update Publisher set Name = '"+@s1+"' , construction_date='"+ @d +"' where BookID='"+ int.Parse(s2) +"'" ;
            SqlCommand cmd2 = new SqlCommand(q2);
            cmd2.Parameters.AddWithValue("@s1", s1);
            cmd2.Parameters.AddWithValue("@d", d);
            DBacc.executeQuery(cmd2);

            // update the author
            s1 = Authorname.Text;
            d = dateTimePicker1.Value;
            string q3 = "update Author set Name = '" + @s1 + "' , Birthday='" + @d + "' where Bookid='"+ int.Parse(s2)+"'";
            SqlCommand cmd3 = new SqlCommand(q3);
            cmd3.Parameters.AddWithValue("@s1", s1);
            cmd3.Parameters.AddWithValue("@d", d);
            DBacc.executeQuery(cmd3);
            loaddata();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd;
            string q;
         // delete from table book_author
            q = "delete from book_Author where isbn ='" + book +"'";
            cmd = new SqlCommand(q);
            DBacc.executeQuery(cmd);
            //delete from table author
            q = "delete from Author where Bookid ='" + int.Parse(book) + "'";
            cmd = new SqlCommand(q);
            DBacc.executeQuery(cmd);
            // delete from table book
            q = "delete from book where ISBN='" +  book  + "'";
            cmd = new SqlCommand(q);
            DBacc.executeQuery (cmd);

            // delete from table publish
            q = "delete from Publisher where Bookid='" + int.Parse(book) + "'";
            cmd = new SqlCommand(q);    
            DBacc.executeQuery (cmd);

            loaddata();
        }
    }
}
