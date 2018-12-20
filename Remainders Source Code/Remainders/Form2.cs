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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Title required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (richTextBox1.Text == "")
            {
                MessageBox.Show("Description required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString);
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand();
                cmd1.Connection = con;
                cmd1.CommandText = "INSERT INTO remind (title,descr,datentime) VALUES (@1,@2,@3)";
                cmd1.Parameters.AddWithValue("@1", textBox1.Text);
                cmd1.Parameters.AddWithValue("@2", richTextBox1.Text);
                cmd1.Parameters.AddWithValue("@3", dateTimePicker1.Text.ToString());
                cmd1.ExecuteNonQuery();
                label4.Text = "Remainder added";
                con.Close();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}
