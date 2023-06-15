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
    public partial class hizmetler : Form
    {
        public hizmetler()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = HUDASAYLAM ;Initial Catalog=rezervasionsistemi;Integrated Security=true");

        public int KoltukProperty { get; set; }
    
        // public string userId { get; set; }

        private void hizmetler_Load(object sender, EventArgs e)
        {
            //hudasaylam@ogr.sakarya.edu.tr
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            con.Open();



            SqlCommand cmd = new SqlCommand();

            string query = "Select * from EK_HIZMETLER ";
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

              string hiz = Convert.ToString(selectedRow.Cells["hizmet_tipi"].Value);
                con.Open();


                string add = "UPDATE  REZERVASYONLAR  SET  hizmet_tipi=@hiz WHERE REZERVASION_ID =(SELECT TOP 1 REZERVASION_ID FROM REZERVASYONLAR  ORDER BY REZERVASION_ID DESC) ";
                SqlCommand command2 = new SqlCommand(add, con);
                command2.Parameters.AddWithValue("@hiz", hiz);

                command2.ExecuteNonQuery();
                string addtofat = "UPDATE  FATURALAR  SET  hizmet_tipi=@hiz WHERE FATURA_ID =(SELECT TOP 1 FATURA_ID FROM FATURALAR  ORDER BY FATURA_ID DESC) ";
                SqlCommand command3 = new SqlCommand(addtofat, con);
                command3.Parameters.AddWithValue("@hiz", hiz);
                command3.ExecuteNonQuery();

                con.Close();

            }

        
            odemetipi v = new odemetipi();
            v.Show();
         
            this.Hide();


        }
    }
}
