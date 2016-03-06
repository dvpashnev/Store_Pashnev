using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Store_WSL;

namespace Store_Pashnev
{
  public partial class ClientFindForm : Form
  {
    private DataTable _clients;
    Dictionary<string, bool> _rights;

    public Client clnt = new Client();

    public ClientFindForm(DataTable clients, Dictionary<string, bool> rights)
    {
      InitializeComponent();
      _clients = clients;
      _rights = rights;
    }

    private void ClientFindForm_Load(object sender, EventArgs e)
    {
      dgwClients.DataSource = _clients;

      if (!_rights["Client_LN"])
      {
        labelLN.Hide();
        textBoxLN.Hide();
      }
      if (!_rights["Client_FN"])
      {
        labelFN.Hide();
        textBoxFN.Hide();
      }
      if (!_rights["Client_MN"])
      {
        labelMN.Hide();
        textBoxMN.Hide();
      }
      if (!_rights["Client_Sex"])
      {
        labelSex.Hide();
        radioButtonMale.Hide();
        radioButtonFemale.Hide();
      }
      if (!_rights["Client_BD"])
      {
        labelBD.Hide();
        labelBD1.Hide();
        labelBD2.Hide();

        dateTimePickerBDFrom.Hide();
        dateTimePickerBDTo.Hide();
      }
      if (!_rights["Client_Phone1"])
      {
        labelPhone.Hide();
        textBoxPhone.Hide();
      }
      if (!_rights["Client_Adress"])
      {
        labelAdress.Hide();
        textBoxAdress.Hide();
      }
      if (!_rights["Client_SO"])
      {
        labelSO.Hide();
        labelSO2.Hide();
        textBoxSumFrom.Hide();
        textBoxSumTo.Hide();
      }
      if (!_rights["Client_Discount"])
      {
        labelDiscount.Hide();
        labelDiscount2.Hide();
        labelDiscount3.Hide();
        textBoxDiscountFrom.Hide();
        textBoxDiscountTo.Hide();
      }
    }

    private void btnFind_Click(object sender, EventArgs e)
    {
      DataView dv = new DataView(_clients);

      if (textBoxID.Text != "")
      {
        dv.RowFilter = "Id = " + textBoxID.Text;
      }

      if (textBoxLN.Visible && textBoxLN.Text != "")
      {
        dv.RowFilter = "LastName like '%" + textBoxLN.Text + "%'";
      }

      if (textBoxFN.Visible && textBoxFN.Text != "")
      {
        dv.RowFilter = "FirstName like '%" + textBoxFN.Text + "%'";
      }

      if (textBoxMN.Visible && textBoxMN.Text != "")
      {
        dv.RowFilter = "MiddleName like '%" + textBoxMN.Text + "%'";
      }

      if (textBoxSumFrom.Visible && textBoxSumFrom.Text != "")
      {
        dv.RowFilter = "SumOrders >= " + textBoxSumFrom.Text + "";
      }

      if (textBoxSumTo.Visible && textBoxSumTo.Text != "")
      {
        dv.RowFilter = "SumOrders <= " + textBoxSumTo.Text + "";
      }

      if (textBoxDiscountFrom.Visible && textBoxDiscountFrom.Text != "")
      {
        dv.RowFilter = "Discount >= " + textBoxDiscountFrom.Text + "";
      }

      if (textBoxDiscountTo.Visible && textBoxDiscountTo.Text != "")
      {
        dv.RowFilter = "Discount <= " + textBoxDiscountTo.Text + "";
      }

      if (textBoxPhone.Visible && textBoxPhone.Text != "")
      {
        string phoneFilter = "Phone1 like '%" + textBoxPhone.Text
                             + "%' or Phone2 like '%" + textBoxPhone.Text
                             + "%' or Phone3 like '%" + textBoxPhone.Text + "%'";
        dv.RowFilter = phoneFilter;
      }

      if (textBoxAdress.Visible && textBoxAdress.Text != "")
      {
        dv.RowFilter = "Adress like '%" + textBoxAdress.Text + "%'";
      }

      if (radioButtonMale.Visible && radioButtonMale.Checked)
      {
        dv.RowFilter = "Sex = 'мужской'";
      }

      if (radioButtonFemale.Visible && radioButtonFemale.Checked)
      {
        dv.RowFilter = "Sex = 'женский'";
      }

      if (dateTimePickerBDFrom.Visible && dateTimePickerBDFrom.Value.ToShortDateString() != DateTime.Today.ToShortDateString())
      {
        string bd1 = dateTimePickerBDFrom.Value.ToShortDateString();
        string bd2 = dateTimePickerBDTo.Value.ToShortDateString();

        dv.RowFilter = "BirthDay >= '" + bd1 + "' and BirthDay <= '"+ bd2 + "'";
      }

      dgwClients.DataSource = dv;

    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      textBoxID.Text = "";
      textBoxLN.Text = "";
      textBoxFN.Text = "";
      textBoxMN.Text = "";
      textBoxSumFrom.Text = "";
      textBoxSumTo.Text = "";
      textBoxDiscountFrom.Text = "";
      textBoxDiscountTo.Text = "";
      textBoxPhone.Text = "";
      textBoxAdress.Text = "";
      radioButtonMale.Checked = false;
      radioButtonFemale.Checked = false;
      dateTimePickerBDFrom.Value = DateTime.Today;
      dateTimePickerBDTo.Value = DateTime.Today;

      dgwClients.DataSource = _clients;
    }

    private void buttonCansel_Click(object sender, EventArgs e)
    {
      clnt.Clear();
      Close();
    }

    private void ToOrderBtn_Click(object sender, EventArgs e)
    {
      clnt.SetValues(Convert.ToInt32(dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["Id"].Value),
        dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["LastName"].Value.ToString(),
        dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["FirstName"].Value.ToString(),
        dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["MiddleName"].Value.ToString(),
        dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["Sex"].Value.ToString(),
        Convert.ToDateTime(dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["BirthDay"].Value),
        dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["Phone1"].Value.ToString(),
        dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["Phone2"].Value.ToString(),
        dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["Phone3"].Value.ToString(),
        dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["Adress"].Value.ToString(),
        Convert.ToDouble(dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["SumOrders"].Value),
        Convert.ToInt32(dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["Discount"].Value),
        dgwClients.Rows[dgwClients.CurrentCell.RowIndex].Cells["FirmName"].Value.ToString()
        );
      Close();
    }

  }
}
