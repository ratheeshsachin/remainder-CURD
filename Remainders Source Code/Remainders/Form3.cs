using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Remainders
{
    public partial class Form3 : Form
    {
        string id;
        int id1;
        int delete_id;

        public Form3()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString);
            con.Open();

            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT ID'ID',title'Title',descr'Description',datentime'DateTime' FROM remind WHERE title like '" + textBox1.Text + "%'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[3].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].Width = 180;
                dataGridView1.Columns[3].Width = 150;
                con.Close();
            
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            id = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();

            if (id == "")
            {
                id1 = 0;
            }
            else
            {
                id1 = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());
            }

            if (id1 != 0)
            {
                MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString);
                con.Open();
                string query = "UPDATE remind SET title=@2, descr=@3, datentime=@4 WHERE ID= @1";
                MySqlCommand cmd = new MySqlCommand(query, con);
                string dt = dataGridView1.Rows[e.RowIndex].Cells["DateTime"].Value.ToString();
                DateTime myDate = Convert.ToDateTime(dt);
                cmd.Parameters.AddWithValue("@1", dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                cmd.Parameters.AddWithValue("@2", dataGridView1.Rows[e.RowIndex].Cells["Title"].Value.ToString());
                cmd.Parameters.AddWithValue("@3", dataGridView1.Rows[e.RowIndex].Cells["Description"].Value.ToString());
                cmd.Parameters.AddWithValue("@4", myDate);                
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Remainders Modified", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           
        }
    }
}
