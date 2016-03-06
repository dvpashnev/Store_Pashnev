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
  public partial class ClientForm : Form
  {
    public Client client = new Client();

    public ClientForm()
    {
      InitializeComponent();
    }

    public ClientForm(Client clnt)
    {
      InitializeComponent();
      client = clnt;
    }

    private void ClientForm_Load(object sender, EventArgs e)
    {
      textBoxID.Text = client.Id.ToString();
      textBoxID.ReadOnly = true;
      if (client.LastName != null)
      {
        textBoxLN.Text = client.LastName;
        textBoxFN.Text = client.FirstName;
        textBoxMN.Text = client.MiddleName;
        textBoxSum.Text = client.SumOrders.ToString();
        textBoxDiscount.Text = client.Discount.ToString();
        textBoxPhone1.Text = client.Phone1;
        textBoxPhone2.Text = client.Phone2;
        textBoxPhone3.Text = client.Phone3;
        textBoxAdress.Text = client.Adress;
        radioButtonMale.Checked = (client.Sex == "мужской");
        radioButtonFemale.Checked = (client.Sex == "женский");
        dateTimePickerBD.Value = client.BirthDay;
        textBoxFirmName.Text = client.FirmName;
      }
    }

    private void buttonCansel_Click(object sender, EventArgs e)
    {
      client.Clear();
      Close();
    }

    private void buttonSave_Click(object sender, EventArgs e)
    {
      string sex = radioButtonMale.Checked ? "мужской" : "женский";

      client.SetValues(
        Convert.ToInt32(textBoxID.Text),
        textBoxLN.Text,
        textBoxFN.Text,
        textBoxMN.Text,
        sex,
        dateTimePickerBD.Value,       
        textBoxPhone1.Text,
        textBoxPhone2.Text,
        textBoxPhone3.Text,
        textBoxAdress.Text,
        Convert.ToDouble(textBoxSum.Text),
        Convert.ToInt32(textBoxDiscount.Text),
        textBoxFirmName.Text
      );
      Close();
    }
  }
}
