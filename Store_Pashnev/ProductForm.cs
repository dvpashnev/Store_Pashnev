using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store_Pashnev
{
  public partial class ProductForm : Form
  {
    private int _productId;
    private DataTable _deps;
    private DataTable _produser;

    public ProductForm(int PId, DataTable deps, DataTable produser)
    {
      InitializeComponent();
      _productId = PId;
      _deps = deps;
      _produser = produser;
    }

    private void ProductForm_Load(object sender, EventArgs e)
    {
      textBoxID.Text = _productId.ToString();
      textBoxID.ReadOnly = true;

      comboBoxDep.DataSource = _deps;
      comboBoxDep.DisplayMember = "Name";
      comboBoxDep.ValueMember = "Id";

      comboBoxProducer.DataSource = _produser;
      comboBoxProducer.DisplayMember = "Name";
      comboBoxProducer.ValueMember = "Id";

      textBoxTitle.Text = Product.Title;
      textBoxPrice.Text = Product.Price.ToString();
      comboBoxDep.SelectedIndex = Product.DepartmentId - 1;
      comboBoxProducer.SelectedIndex = Product.ProduserId - 1;
      textBoxQuantity.Text = Product.Quantity.ToString();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Product.SetValues(
        Convert.ToInt32(textBoxID.Text),
        textBoxTitle.Text,
        Convert.ToDouble(textBoxPrice.Text),
        Convert.ToInt32((comboBoxDep.SelectedItem as DataRowView).Row[0]),
        Convert.ToInt32((comboBoxProducer.SelectedItem as DataRowView).Row[0]),
        Convert.ToInt32(textBoxQuantity.Text),
        (Convert.ToInt32(textBoxQuantity.Text) < 5)
        );
      Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      Product.Clear();
      Close();
    }
  }
}
