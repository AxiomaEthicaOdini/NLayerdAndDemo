using Business.Abstract;
using Business.Concrete;
using Business.DependencyRevolvers.Ninject;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.NHiberrnate;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebFormsUI
{
    public partial class Form1 : Form
    {
        new int Move;
        int Mouse_X;
        int Mouse_Y;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;
        }


        public Form1()
        {
            InitializeComponent();
            _productService = InstanceFactory.GetInstance<IProductService>();
            _categoryService = InstanceFactory.GetInstance<ICategoryService>();
        }
        private IProductService _productService;
        private ICategoryService _categoryService;
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();
        }

        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";

            cbxCategoryId.DataSource = _categoryService.GetAll();
            cbxCategoryId.DisplayMember = "CategoryName";
            cbxCategoryId.ValueMember = "CategoryId";

            cbxCategoryIdUpdate.DataSource = _categoryService.GetAll();
            cbxCategoryIdUpdate.DisplayMember = "CategoryName";
            cbxCategoryIdUpdate.ValueMember = "CategoryId";
        }

        private void LoadProducts()
        {
            dgwProduct.DataSource = _productService.GetAll();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgwProduct.DataSource = _productService.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));
            }
            catch
            {

            }
        }
        private void TbxSearch_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbxSearch.Text))
            {
                LoadProducts();
            }
            else
            {
                dgwProduct.DataSource = _productService.GetProductsByProductName(tbxSearch.Text);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {            
            if (String.IsNullOrEmpty(tbxProductName2.Text) || String.IsNullOrEmpty(tbxQantity.Text) || String.IsNullOrEmpty(tbxUnitPrice.Text) || String.IsNullOrEmpty(tbxStock.Text))
            {
                MessageBox.Show("Tüm Alanları Doldurunuz.","Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
            else
            {                
                _productService.Add(new Product
                {
                    CategoryId = Convert.ToInt32(cbxCategoryId.SelectedValue),
                    ProductName = tbxProductName2.Text,
                    QuantityPerUnit = tbxQantity.Text,
                    UnitPrice = Convert.ToDecimal(tbxUnitPrice.Text),
                    UnitsInStock = Convert.ToInt16(tbxStock.Text)
                });
                MessageBox.Show("Ürününüz Kaydedildi!","Kayıt Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                LoadProducts();
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (tbxUpdateProductName.Text == "" && tbxUnitPriceUpdate.Text == "" && tbxUnitsInStockUpdate.Text == "" && tbxQuantityUpdate.Text == "")
            {
                MessageBox.Show("Tüm Alanları Doldurunuz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
               _productService.Update(new Product
            {
                ProductId=Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value),
                ProductName=tbxUpdateProductName.Text,
                CategoryId=Convert.ToInt32(cbxCategoryIdUpdate.SelectedValue),
                UnitsInStock=Convert.ToInt16(tbxUnitsInStockUpdate.Text),
                QuantityPerUnit=tbxQuantityUpdate.Text,
                UnitPrice=Convert.ToDecimal(tbxUnitPriceUpdate.Text)
            });
            MessageBox.Show("Ürününüz Güncellendi", "Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            LoadProducts();
            }            
        }

        private void DgwProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbxUpdateProductName.Text = dgwProduct.CurrentRow.Cells[1].Value.ToString();
            cbxCategoryIdUpdate.SelectedValue = dgwProduct.CurrentRow.Cells[2].Value;
            tbxUnitsInStockUpdate.Text = dgwProduct.CurrentRow.Cells[5].Value.ToString();
            tbxQuantityUpdate.Text = dgwProduct.CurrentRow.Cells[4].Value.ToString();
            tbxUnitPriceUpdate.Text = dgwProduct.CurrentRow.Cells[3].Value.ToString();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgwProduct.CurrentRow!=null)
            {
                try
                {
                    _productService.Delete(new Product
                    {
                        ProductId = Convert.ToInt32(dgwProduct.CurrentRow.Cells[0].Value)
                    });
                    MessageBox.Show("Ürününüz Silindi", "Silme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    LoadProducts();
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }           
            }
            else
            {

            }
        }
    }    
}

