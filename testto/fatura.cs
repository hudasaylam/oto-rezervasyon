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
    public partial class fatura : Form
    {
        
        public fatura()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = HUDASAYLAM ;Initial Catalog=rezervasionsistemi;Integrated Security=true");
       

        private void fatura_Load(object sender, EventArgs e)
        {



            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            con.Open();

            string query = "SELECT TOP 1 * FROM FATURALAR ORDER BY fatura_id DESC   ";//select faturalardaki son satır 
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable datatable = new DataTable();
            dataGridView1.DataSource = datatable;

            dataGridView1.ReadOnly = true;


            sda.Fill(datatable);


        
        
            //SqlCommand cmd = new SqlCommand();
            //string son = "SELECT TOP 1 rezervasion_id FROM REZERVASYONLAR ORDER BY rezervasion_id DESC";
            //SqlCommand command0 = new SqlCommand(son, con);//  select son satırdaki rezervasıon id
            //int number2 = (int)command0.ExecuteScalar();

          
      






            con.Close();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("koltuk aindi");
        }
    }
}
