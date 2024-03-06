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

namespace Urun_Takip_Kayit
{
    public partial class CategoryForm : Form
    {
        //Data Source=MSI\SQLEXPRESS;Initial Catalog=DbUrun;Integrated Security=True;Encrypt=False
        SqlConnection connect = new SqlConnection("Data Source = MSI\\SQLEXPRESS; Initial Catalog = DbUrun; Integrated Security = True; Encrypt = False");
        public CategoryForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnList_Click(object sender, EventArgs e)
        {
           
            SqlCommand sqlCommandList = new SqlCommand("Select * From tblKategori",connect);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommandList);
            DataTable dtTable = new DataTable();

            da.Fill(dtTable);
            dataGridView1.DataSource = dtTable;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand sqlCommandSave = new SqlCommand("Insert Into tblKategori (Ad) values(@p1)",connect);
            sqlCommandSave.Parameters.AddWithValue("@p1",txtKategori.Text);
            sqlCommandSave.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Kaydedildi");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand sqlCommandDelete = new SqlCommand("Delete From tblKategori where Id = @p1",connect);
            sqlCommandDelete.Parameters.AddWithValue("@p1",txtId.Text);
            sqlCommandDelete.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Silindi");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand sqlCommandUpdate = new SqlCommand("Update tblKategori Set Ad=@p1 where ID=@p2",connect);
            sqlCommandUpdate.Parameters.AddWithValue("@p1", txtKategori.Text);
            sqlCommandUpdate.Parameters.AddWithValue("@p2", txtId.Text);
            sqlCommandUpdate.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Güncellendi");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommandSearch = new SqlCommand("Select * From tblKategori where ad = @p1",connect);
            sqlCommandSearch.Parameters.AddWithValue("@p1",txtKategori.Text);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommandSearch);
            DataTable dtTable = new DataTable();

            da.Fill(dtTable);
            dataGridView1.DataSource = dtTable;
        }
    }
}
