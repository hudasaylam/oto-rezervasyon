using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testto
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = HUDASAYLAM ;Initial Catalog=rezervasionsistemi;Integrated Security=true");
        //connection
        private void Form1_Load(object sender, EventArgs e)
        {
           

        }
        bool x;
        private void button2_Click(object sender, EventArgs e)
        {
           
            string Email_text=emailtextbox.Text;
            string sifre_text=sifretextbox.Text;

            try //login 
            {

                string query = "Select * from KULLANCILAR WHERE EMAIL='"+emailtextbox.Text+"'AND SİFRE='"+sifretextbox.Text+"'";
                SqlDataAdapter sda = new SqlDataAdapter(query ,con);
          
                DataTable datatable = new DataTable();
                sda.Fill(datatable);
                if (datatable.Rows.Count > 0)
                {

                    Email_text = emailtextbox.Text;
                    sifre_text = sifretextbox.Text;


      x=true ;

                    rezervasion form2 = new rezervasion();
                    form2.Show();
                    this.Hide();

                }
                else
                {

                    MessageBox.Show("hatalı giriş");
                    emailtextbox.Clear();
                    sifretextbox.Clear();
                    x = false;
                }
            }
            catch
            {

                MessageBox.Show("hatalı giriş !!!");
                x = false;
            }
            finally

            {
                con.Open();
                string rez = "SELECT TOP 1 rezervasion_id FROM REZERVASYONLAR ORDER BY rezervasion_id DESC";
                SqlCommand command1 = new SqlCommand(rez, con);
                int rezerid = (int)command1.ExecuteScalar();

                string rezu = "SELECT TOP 1 fatura_id FROM FATURALAR ORDER BY fatura_id DESC";
                SqlCommand comi = new SqlCommand(rezu, con);
                int number = (int)comi.ExecuteScalar();
                if (x == true)
                {
                    rezerid++;

                    string addrow = "INSERT INTO REZERVASYONLAR (REZERVASION_ID) VALUES(@rezerid)";
                    SqlCommand commandd = new SqlCommand(addrow, con);
                    commandd.Parameters.AddWithValue("@rezerid", rezerid);
                    commandd.ExecuteNonQuery();


                    number++;
                    string addnewrow = "INSERT INTO FATURALAR (fatura_id ,ucret) VALUES (@number, 0) ";
                    SqlCommand comma = new SqlCommand(addnewrow, con);
                    comma.Parameters.AddWithValue("@number", number);
                    comma.ExecuteNonQuery();

                    string addreztofat = "UPDATE  FATURALAR  SET  rezervasion_id=@rezerid WHERE fatura_id =(SELECT TOP 1 fatura_id FROM FATURALAR  ORDER BY fatura_id DESC) ";
                    SqlCommand commando = new SqlCommand(addreztofat, con);
                    commando.Parameters.AddWithValue("@rezerid", rezerid);
                    commando.ExecuteNonQuery();








                    string selectSql = "SELECT KULLANCI_ID FROM [KULLANCILAR] WHERE [EMAIL] = @Email_text";
                    SqlCommand command = new SqlCommand(selectSql, con);
                    command.Parameters.AddWithValue("@Email_text", Email_text);

                    int kullanci_id = (int)command.ExecuteScalar();

                    string add = "UPDATE  REZERVASYONLAR  SET  kullanci_id=@kullanci_id WHERE REZERVASION_ID =(SELECT TOP 1 REZERVASION_ID FROM REZERVASYONLAR  ORDER BY REZERVASION_ID DESC) ";
                    SqlCommand command2 = new SqlCommand(add, con);
                    command2.Parameters.AddWithValue("@rezerid", rezerid);
                    command2.Parameters.AddWithValue("@kullanci_id", kullanci_id);

                    command2.ExecuteNonQuery();


                    string addtofat = "UPDATE  FATURALAR  SET  kullanci_id=@kullanci_id WHERE FATURA_ID =(SELECT TOP 1 FATURA_ID FROM FATURALAR  ORDER BY FATURA_ID DESC) ";
                    SqlCommand command3 = new SqlCommand(addtofat, con);
                    command3.Parameters.AddWithValue("@kullanci_id", kullanci_id);
                    command3.ExecuteNonQuery();
                    



                }


                con.Close();

            }
           
        }

        private void registerbutton_Click(object sender, EventArgs e)
        {
            //con.Open();

            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText="insert to GIRIS values('"++"')"
        }
    }
}
