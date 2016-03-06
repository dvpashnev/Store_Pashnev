using System;
using System.Windows.Forms;
using Store_WSL;

namespace Store_Pashnev
{
  public partial class PositionForm : Form
  {
    public Position position = new Position();

    public PositionForm(Position pos)
    {
      InitializeComponent();
      position = pos;
    }

    private void PositionForm_Load(object sender, EventArgs e)
    {
      textBoxPId.Text = position.Id.ToString();
      textBoxPId.ReadOnly = true;
      textBoxTitle.Text = position.Title;
      checkBoxProducts.Checked = position.Products;
      checkBoxOrder.Checked = position.Order;
      checkBoxClients.Checked = position.Clients;
      checkBoxReports.Checked = position.Reports;
      checkBoxManagement.Checked = position.Reports;
      checkBoxLN.Checked = position.Client_LN;
      checkBoxFN.Checked = position.Client_FN;
      checkBoxMN.Checked = position.Client_MN;
      checkBoxSex.Checked = position.Client_Sex;
      checkBoxBD.Checked = position.Client_BD;
      checkBoxPhone1.Checked = position.Client_Phone1;
      checkBoxPhone2.Checked = position.Client_Phone2;
      checkBoxPhone3.Checked = position.Client_Phone3;
      checkBoxAdress.Checked = position.Client_Adress;
      checkBoxSO.Checked = position.Client_SO;
      checkBoxDiscount.Checked = position.Client_Discount;
      checkBoxFirmName.Checked = position.Client_FirmName;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      position.SetValues(
        Convert.ToInt32(textBoxPId.Text),
        textBoxTitle.Text,
        checkBoxProducts.Checked,
        checkBoxOrder.Checked,
        checkBoxClients.Checked,
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
        checkBoxDiscount.Checked,
        checkBoxFirmName.Checked
        );
      Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      position.Clear();
      Close();
    }
  }
}
