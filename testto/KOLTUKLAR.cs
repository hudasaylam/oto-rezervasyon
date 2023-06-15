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
    public partial class KOLTUKLAR : Form
    {
        public int MyIntProperty { get; set; }

        public KOLTUKLAR()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source = HUDASAYLAM ;Initial Catalog=rezervasionsistemi;Integrated Security=true");
        private void KOLTUKLAR_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            con.Open();

            SqlCommand cmd = new SqlCommand();

            string query = "Select * from KOLTUKLAR WHERE OTOBUS_ID=  '" + MyIntProperty + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable datatable = new DataTable();
            dataGridView1.DataSource = datatable;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            sda.Fill(datatable);
            dataGridView1.DataSource = datatable;








            con.Close();




        }

        private void button1_Click(object sender, EventArgs e)
        {
            rezervasion again = new rezervasion();
            again.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                string koltukDurumu = dataGridView1.Rows[e.RowIndex].Cells["KOLTUK_DURUMU"].Value.ToString();
                int kolId = Convert.ToInt32(selectedRow.Cells["KOLTUK_ID"].Value);

                if (koltukDurumu == " musait")
                {

                    con.Open();
                  

                    string add = "UPDATE  REZERVASYONLAR  SET  koltuk_id=@kolId WHERE REZERVASION_ID =(SELECT TOP 1 REZERVASION_ID FROM REZERVASYONLAR  ORDER BY REZERVASION_ID DESC) ";
                    SqlCommand command2 = new SqlCommand(add, con);
                    command2.Parameters.AddWithValue("@kolId", kolId);

                    command2.ExecuteNonQuery();
                    string addtofat = "UPDATE  FATURALAR  SET  koltuk_id=@kolId WHERE FATURA_ID =(SELECT TOP 1 FATURA_ID FROM FATURALAR  ORDER BY FATURA_ID DESC) ";
                    SqlCommand command3 = new SqlCommand(addtofat, con);
                    command3.Parameters.AddWithValue("@kolId",kolId);
                    command3.ExecuteNonQuery();

                    string updateKoltuklar = "UPDATE KOLTUKLAR SET koltuk_durumu = 'dolu' WHERE koltuk_id = @koltukId";
                    using (SqlCommand command = new SqlCommand(updateKoltuklar, con))
                    {
                        command.Parameters.AddWithValue("@koltukId", kolId);
                        command.ExecuteNonQuery();
                    }

                    con.Close();

                    hizmetler page = new hizmetler ();
                  
                    page.Show();
                    this.Hide();
                }
                else if(koltukDurumu == "dolu")
                {
                    MessageBox.Show("Bu koltuk müsait değil seçemesiniz");
                }
            }


        }
    }
}