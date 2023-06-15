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
    public partial class rezervasion : Form
    {
        public rezervasion()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = HUDASAYLAM ;Initial Catalog=rezervasionsistemi;Integrated Security=true");
        


        private void rezervasion_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            con.Open();

            SqlCommand cmd = new SqlCommand();
            
            string query = "Select * from OTOBUSLER ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable datatable = new DataTable();
            dataGridView1.DataSource = datatable;

            dataGridView1.ReadOnly = true;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            sda.Fill(datatable);
            dataGridView1.DataSource = datatable;
          

            
            

            con.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

            con.Open();
            string rez = "SELECT TOP 1 rezervasion_id FROM REZERVASYONLAR ORDER BY rezervasion_id DESC";
            SqlCommand command1 = new SqlCommand(rez, con);//select son satır
            int number = (int)command1.ExecuteScalar();

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];



                int otobusId = Convert.ToInt32(selectedRow.Cells["OTOBUS_ID"].Value);
                hizmetler t = new hizmetler();
               // t.userId = UserId; 
                KOLTUKLAR N = new KOLTUKLAR();
                N.MyIntProperty = otobusId;
                N.Show();
         

                string add = "UPDATE REZERVASYONLAR SET  otobus_id = @otobusId WHERE REZERVASION_ID = (SELECT TOP 1 REZERVASION_ID FROM REZERVASYONLAR  ORDER BY REZERVASION_ID DESC)";
                SqlCommand command= new SqlCommand(add, con);
                command.Parameters.AddWithValue("@otobusId", otobusId);

                command.ExecuteNonQuery();
         
               

                this.Close();

                con.Close();
            }










        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 again = new Form1 ();
            again.Show();
            this.Hide();
        }
    }
}
