using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhoneBookCSharpProject
{
    public partial class Form1 : Form
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e)
        {

        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;//Clear database
            dataGridView1.Rows.Clear();
            dataGridView1.Columns["dbName"].Visible = false;//for view
            dataGridView1.Columns["dbNumber"].Visible = false;//For view
            dataGridView1.Columns["Name1"].Visible = true;//For Insert
            dataGridView1.Columns["TelephoneNumber"].Visible = true;//For Insert
            saveButton.Enabled = true;
            dataGridView1.Refresh();//Refresh the grid view after update


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Table' table. You can move, or remove it, as needed.
            this.tableTableAdapter.Fill(this.database1DataSet.Table);

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Phone[] newNumber = new Phone[dataGridView1.Rows.Count - 1];
            for (int i = 0; i < newNumber.Length; i++)
            {
                newNumber[i] = new Phone();
                newNumber[i].ContactName = dataGridView1[3, i].Value.ToString();
                newNumber[i].ContactNumber = dataGridView1[4, i].Value.ToString();
                db.Phones.InsertOnSubmit(newNumber[i]);
            }
            db.SubmitChanges();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Phones;//load database into grid view
            dataGridView1.Columns["dbName"].Visible = true;//for view
            dataGridView1.Columns["dbNumber"].Visible = true;//For view
            dataGridView1.Columns["Name1"].Visible = false;//For Insert
            dataGridView1.Columns["TelephoneNumber"].Visible = false;//For Insert
            saveButton.Enabled = false;

        }
    }
}
