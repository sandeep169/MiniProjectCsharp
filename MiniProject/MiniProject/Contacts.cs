using MiniProject.ContactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniProject
{
    public partial class Contacts : Form
    {
        public Contacts()
        {
            InitializeComponent();
        }
        //contactClass c = new contactClass();
        contactClass c = new contactClass(); 
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void Contacts_Load(object sender, EventArgs e)
        {
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //get the value from input
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;

            bool success = c.Insert(c);
            if(success==true)
            {
                MessageBox.Show("New Contact is Successfully Inserted");
                //call the clear method
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to add ,Try Again");
            }
            //Local DAta on data gridveiw

            DataTable dt = c.Select(); 
            dgvContactList.DataSource = dt;            
        }

        private void txtBoxAddress_TextChanged(object sender, EventArgs e)
        {

        }
        //clear all feild 
        public void Clear()
        {
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtBoxContactNumber.Text = "";
            txtBoxAddress.Text = "";
            cmbGender.Text = "";
            txtboxContactID.Text = "";

        }

        private void txtBoxContactNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get the data from textboxes
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;
            

            bool success = c.Update(c);
            if (success == true)
            {
                MessageBox.Show("Contact Has been successfully Updated");

                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //call clear method
                Clear();
            }
           
            else
            {
                MessageBox.Show("Failed To Update The Contact");
            }



        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the data from DAta Grid view and load it ti the textboxes respectively
            //identfiy the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnClear_Click_1(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            c.ContactID = Convert.ToInt32(txtboxContactID.Text);
            bool success = c.Delete(c);
            if(success==true)
            {
                MessageBox.Show("Contact successfully Deleted");
                //refresh the data griedview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;

                Clear();
            }
            else
            {
                MessageBox.Show("Failed to Delete the Contact,Try Again");
            }
        }
        static string myconstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        private void txtboxSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtboxSearch.Text;
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%"+keyword+"%' OR LastName LIKE '%"+keyword+"%' OR Address LIKE '%"+keyword+"%'",conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
            
        }
    }
}
