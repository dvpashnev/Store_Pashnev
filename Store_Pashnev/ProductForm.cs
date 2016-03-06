using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Store_WSL;

namespace Store_Pashnev
{
  public partial class ProductForm : Form
  {
    private DataTable _deps;
    private DataTable _produser;

    public Product product;

    public ProductForm(Product prod, DataTable deps, DataTable produser)
    {
      InitializeComponent();
      product = prod;
      _deps = deps;
      _produser = produser;
    }

    private void ProductForm_Load(object sender, EventArgs e)
    {
      textBoxID.Text = product.Id.ToString();
      textBoxID.ReadOnly = true;

      comboBoxDep.DataSource = _deps;
      comboBoxDep.DisplayMember = "Name";
      comboBoxDep.ValueMember = "Id";

      DataRow[] produsers = _produser.Select("FirmName is not null");

      foreach (DataRow row in produsers)
      {
        comboBoxProducer.Items.Add(row["FirmName"]);
      }

      if (product.Title != null)
      {
        textBoxTitle.Text = product.Title;

        PurchasePriceNumericUpDown.Value = (decimal)product.PurchasePrice;
        MarkupNumericUpDown.Value = (decimal)product.Markup;

        textBoxPrice.Text = product.Price.ToString();
        comboBoxDep.SelectedIndex = product.DepartmentId - 1;

        DataRow produser = _produser.Select("Id = " + product.ProduserId).First();
        comboBoxProducer.SelectedItem = produser["FirmName"];

        textBoxQuantity.Text = product.Quantity.ToString();
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      DataRow produser = _produser.Select("FirmName = '" + comboBoxProducer.SelectedItem + "'").First();
      
      product.SetValues(
        Convert.ToInt32(textBoxID.Text),
        textBoxTitle.Text,
        (double)PurchasePriceNumericUpDown.Value,
        (double)MarkupNumericUpDown.Value,
        Convert.ToDouble(textBoxPrice.Text),
        Convert.ToInt32((comboBoxDep.SelectedItem as DataRowView).Row[0]),
        Convert.ToInt32(produser["Id"]),
        Convert.ToInt32(textBoxQuantity.Text),
        (Convert.ToInt32(textBoxQuantity.Text) < 5)
        );
      Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      product.Clear();
      Close();
    }

    private void PurshasePriceNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (PurchasePriceNumericUpDown.Value <= 0)
      {
        MessageBox.Show(@"Закупочная цена может быть нулевой или отрицательной!", @"Предупреждение!");
        PurchasePriceNumericUpDown.Value = 0.01M;
        return;
      }
      RenewPrice();
    }

    private void MarkupNumericUpDown_ValueChanged(object sender, EventArgs e)
    {
      if (MarkupNumericUpDown.Value < 25)
      {
        MessageBox.Show(@"Наценка должна быть минимум 25 %!", @"Предупреждение!");
        MarkupNumericUpDown.Value = 25;
        return;
      }
      RenewPrice();
    }

    void RenewPrice()
    {
      textBoxPrice.Text = ((PurchasePriceNumericUpDown.Value+((PurchasePriceNumericUpDown.Value/100)*MarkupNumericUpDown.Value)).ToString());
    }
  }
}
