﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Store_WSL;

namespace Store_Pashnev
{
  public partial class OrderFindForm : Form
  {
    private DataTable _orders;
    private DataTable _employees;
    private DataTable _productsInOrder;

    private BindingSource _bs;

    public Order order = new Order();

    public OrderFindForm(DataTable orders, DataTable employees, DataTable productsInOrder)
    {
      InitializeComponent();
      _orders = orders;
      _employees = employees;
      _productsInOrder = productsInOrder;
    }

    private void OrderFindForm_Load(object sender, EventArgs e)
    {
      _bs = new BindingSource { DataSource = _orders };
      dataGridViewOrders.DataSource = _bs;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      DataView dvFilter = new DataView(_orders);

      if (textBoxID.Text != String.Empty)
      {
        dvFilter.RowFilter = "Id = " + textBoxID.Text;
      }

      if (textBoxEmployee.Text != String.Empty)
      {
        int emplId = (from row in _employees.AsEnumerable()
                        where (row.Field<string>("LastName") + " " + row.Field<string>("FirstName") + " " + row.Field<string>("MiddleName")).Contains(textBoxEmployee.Text)
                        select row.Field<int>("Id")).First();

        dvFilter.RowFilter = "EmployeeId = " + emplId;
      }

      if (textBoxSumFrom.Text != String.Empty && textBoxSumTo.Text != String.Empty)
      {
        dvFilter.RowFilter = "Sum >= " + textBoxSumFrom.Text + "and Sum <= " + textBoxSumTo.Text;
      }
      else if (textBoxSumTo.Text != String.Empty && textBoxSumTo.Text == String.Empty)
      {
        dvFilter.RowFilter = "Sum <= " + textBoxSumTo.Text;
      }
      else
      {
        dvFilter.RowFilter = "Sum >= " + textBoxSumFrom.Text;
      }

      if (textBoxClient.Text != String.Empty)
      {
        dvFilter.RowFilter = "ClientId = " + textBoxClient.Text;
      }

      if (comboBoxCategory.SelectedIndex != -1)
      {
        string phoneFilter = "Category like '%" + comboBoxCategory.SelectedItem + "%'";
        dvFilter.RowFilter = phoneFilter;
      }

      if (dateTimePickerODFrom.Value.ToShortDateString() != DateTime.Today.ToShortDateString()
       || dateTimePickerODTo.Value.ToShortDateString() != DateTime.Today.ToShortDateString())
      {
        string bd1 = dateTimePickerODFrom.Value.ToShortDateString();
        string bd2 = dateTimePickerODTo.Value.ToShortDateString();

        dvFilter.RowFilter = "OrderDate >= '" + bd1 + "' and OrderDate <= '" + bd2 + "'";
      }

      if (dateTimePickerDDFrom.Value.ToShortDateString() != DateTime.Today.ToShortDateString()
        || dateTimePickerDDTo.Value.ToShortDateString() != DateTime.Today.ToShortDateString())
      {
        string dd1 = dateTimePickerDDFrom.Value.ToShortDateString();
        string dd2 = dateTimePickerDDTo.Value.ToShortDateString();

        dvFilter.RowFilter = "DeliveryDate >= '" + dd1 + "' and DeliveryDate <= '" + dd2 + "'";
      }

      if (textBoxAdress.Visible && textBoxAdress.Text != "")
      {
        dvFilter.RowFilter = "DeliveryAdress like '%" + textBoxAdress.Text + "%'";
      }

      dataGridViewOrders.DataSource = dvFilter;

    }

    private void button2_Click(object sender, EventArgs e)
    {
      textBoxID.Text = String.Empty;
      textBoxEmployee.Text = String.Empty;
      textBoxSumFrom.Text = "";
      textBoxSumTo.Text = "";
      textBoxSumFrom.Text = "";
      textBoxSumTo.Text = "";
      textBoxClient.Text = "";
      comboBoxCategory.SelectedIndex = -1;
      dateTimePickerDDFrom.Value = dateTimePickerODFrom.Value = dateTimePickerDDTo.Value = dateTimePickerODTo.Value = DateTime.Today;
      textBoxAdress.Text = "";

      dataGridViewOrders.DataSource = _orders;
    }

    private void btnTakeOrder_Click(object sender, EventArgs e)
    {
      var pio = (from row in _productsInOrder.AsEnumerable()
        where
          row.Field<int>("OrderId") ==
          Convert.ToInt32(dataGridViewOrders.Rows[dataGridViewOrders.CurrentCell.RowIndex].Cells["Id"].Value)
        select new ProductInOrder
        {
          Id = Convert.ToInt32(row["Id"]),
          OrderId = Convert.ToInt32(row["OrderId"]),
          ProductId = Convert.ToInt32(row["ProductId"]),
          Quantity = Convert.ToInt32(row["Quantity"]),
          FinalPrice = Convert.ToDouble(row["FinalPrice"]),
          Sum = Convert.ToDouble(row["Sum"])
        });

      Dictionary<int, ProductInOrder> products = new Dictionary<int, ProductInOrder>();
      foreach (var row in pio)
      {
        products.Add(row.ProductId, row);
      }

      order.SetValues(
        Convert.ToInt32(dataGridViewOrders.Rows[dataGridViewOrders.CurrentCell.RowIndex].Cells["Id"].Value),
        dataGridViewOrders.Rows[dataGridViewOrders.CurrentCell.RowIndex].Cells["Category"].Value.ToString(),
        Convert.ToInt32(dataGridViewOrders.Rows[dataGridViewOrders.CurrentCell.RowIndex].Cells["EmployeeId"].Value),
        Convert.ToInt32(dataGridViewOrders.Rows[dataGridViewOrders.CurrentCell.RowIndex].Cells["ClientId"].Value),
        Convert.ToDateTime(dataGridViewOrders.Rows[dataGridViewOrders.CurrentCell.RowIndex].Cells["OrderDate"].Value),
        Convert.ToDateTime(dataGridViewOrders.Rows[dataGridViewOrders.CurrentCell.RowIndex].Cells["DeliveryDate"].Value),
        dataGridViewOrders.Rows[dataGridViewOrders.CurrentCell.RowIndex].Cells["DeliveryAdress"].Value.ToString(),
        Convert.ToDouble(dataGridViewOrders.Rows[dataGridViewOrders.CurrentCell.RowIndex].Cells["Sum"].Value),
        products
        );

      Close();
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
      order.Clear();
      Close();
    }
  }
}
