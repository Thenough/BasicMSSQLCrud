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
    public partial class UrunForm : Form
    {
        SqlConnection connect = new SqlConnection("Data Source = MSI\\SQLEXPRESS; Initial Catalog = DbUrun; Integrated Security = True; Encrypt = False");
        public UrunForm()
        {
            InitializeComponent();
        }

        private void Ürünler_Load(object sender, EventArgs e)
        {
            SqlCommand sqlCommandLoadCategory = new SqlCommand("Select * From tblKategori",connect);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommandLoadCategory);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            cmbCategory.DisplayMember = "Ad";
            cmbCategory.ValueMember = "Id";
            cmbCategory.DataSource = dt; 

        }

        private void btnProductList_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommandList = new SqlCommand("Select * From tblUrunler",connect);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommandList);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnProductSave_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand sqlCommandProductSave = new SqlCommand("Insert into tblUrunler (urunAdi,stokSayisi,AlisFiyati,SatisFiyati,Kategori) values (@p1, @p2, @p3, @p4, @p5)",connect);
            sqlCommandProductSave.Parameters.AddWithValue("@p1",txtProductName.Text);
            sqlCommandProductSave.Parameters.AddWithValue("@p2",numStock.Value);
            sqlCommandProductSave.Parameters.AddWithValue("@p3",txtBuying.Text);
            sqlCommandProductSave.Parameters.AddWithValue("@p4",txtSales.Text);
            sqlCommandProductSave.Parameters.AddWithValue("@p5",cmbCategory.SelectedValue);
            sqlCommandProductSave.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Ürün Kaydedildi");
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand sqlCommandProductDelete = new SqlCommand("Delete From tblUrunler where urunId = @p1", connect);
            sqlCommandProductDelete.Parameters.AddWithValue("@p1",txtProductId.Text);
            sqlCommandProductDelete.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Ürün Silindi");

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtProductName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            numStock.Value =int.Parse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            txtBuying.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtSales.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            cmbCategory.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnProductUpdate_Click(object sender, EventArgs e)
        {
            connect.Open();
            SqlCommand sqlCommandUpdate = new SqlCommand("Update tblUrunler Set urunadi = @p1, stoksayisi = @p2, alisfiyati = @p3, satisfiyati = @p4, kategori = @p5 where urunId = @p6",connect);
            sqlCommandUpdate.Parameters.AddWithValue("@p1",txtProductName.Text);
            sqlCommandUpdate.Parameters.AddWithValue("@p2",numStock.Value);
            sqlCommandUpdate.Parameters.AddWithValue("@p3",decimal.Parse(txtBuying.Text));
            sqlCommandUpdate.Parameters.AddWithValue("@p4",decimal.Parse(txtSales.Text));
            sqlCommandUpdate.Parameters.AddWithValue("@p5",cmbCategory.SelectedValue);
            sqlCommandUpdate.Parameters.AddWithValue("@p6",txtProductId.Text);
            sqlCommandUpdate.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Ürün Güncellendi");
        }
    }
}
