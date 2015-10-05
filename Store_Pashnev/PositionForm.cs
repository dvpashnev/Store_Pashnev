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
  public partial class PositionForm : Form
  {
    private int PositionId;

    public PositionForm(int PosId)
    {
      InitializeComponent();
      PositionId = PosId;
    }

    private void PositionForm_Load(object sender, EventArgs e)
    {
      textBoxPId.Text = PositionId.ToString();
      textBoxTitle.Text = Position.Title;
      checkBoxProducts.Checked = Position.Products;
      checkBoxOrder.Checked = Position.Order;
      checkBoxClients.Checked = Position.Clients;
      checkBoxClaims.Checked = Position.Claims;
      checkBoxReports.Checked = Position.Reports;
      checkBoxManagement.Checked = Position.Reports;
      checkBoxLN.Checked = Position.Client_LN;
      checkBoxFN.Checked = Position.Client_FN;
      checkBoxMN.Checked = Position.Client_MN;
      checkBoxSex.Checked = Position.Client_Sex;
      checkBoxBD.Checked = Position.Client_BD;
      checkBoxPhone1.Checked = Position.Client_Phone1;
      checkBoxPhone2.Checked = Position.Client_Phone2;
      checkBoxPhone3.Checked = Position.Client_Phone3;
      checkBoxAdress.Checked = Position.Client_Adress;
      checkBoxSO.Checked = Position.Client_SO;
      checkBoxDiscount.Checked = Position.Client_Discount;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Position.SetValues(
        Convert.ToInt32(textBoxPId.Text),
        textBoxTitle.Text,
        checkBoxProducts.Checked,
        checkBoxOrder.Checked,
        checkBoxClients.Checked,
        checkBoxClaims.Checked,
        checkBoxReports.Checked,
        checkBoxManagement.Checked,
        checkBoxLN.Checked,
        checkBoxFN.Checked,
        checkBoxMN.Checked,
        checkBoxSex.Checked,
        checkBoxBD.Checked,
        checkBoxPhone1.Checked,
        checkBoxPhone2.Checked,
        checkBoxPhone3.Checked,
        checkBoxAdress.Checked,
        checkBoxSO.Checked,
        checkBoxDiscount.Checked
        );
      Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      Position.Clear();
      Close();
    }
  }
}
