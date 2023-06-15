using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace testto
{
    public partial class odemetipi : Form
    {
        public odemetipi()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = HUDASAYLAM ;Initial Catalog=rezervasionsistemi;Integrated Security=true");
        private void odemetipi_Load(object sender, EventArgs e)
        {

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            con.Open();

            SqlCommand cmd = new SqlCommand();

            string query = "Select * from ODEME_TİPİ  ";
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
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                string odemetipi = Convert.ToString(selectedRow.Cells["odeme_tipi"].Value);

              
                    con.Open();




                string add = "UPDATE  REZERVASYONLAR  SET  odeme_tipi=@odemetipi WHERE REZERVASION_ID =(SELECT TOP 1 REZERVASION_ID FROM REZERVASYONLAR  ORDER BY REZERVASION_ID DESC) ";
                    SqlCommand command2 = new SqlCommand(add, con);
                command2.Parameters.AddWithValue("@odemetipi", odemetipi);

                command2.ExecuteNonQuery();
             
                con.Close();


            }

         

            fatura v = new fatura();
            v.Show();
            this.Hide();
        }

       
    }
}
