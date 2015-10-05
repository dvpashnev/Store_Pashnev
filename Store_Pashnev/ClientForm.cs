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
  public partial class ClientForm : Form
  {
    public ClientForm()
    {
      InitializeComponent();
    }

    private void ClientForm_Load(object sender, EventArgs e)
    {
      textBoxID.Text = Client.Id.ToString();
      textBoxID.ReadOnly = true;

      textBoxLN.Text = Client.LastName;
      textBoxFN.Text = Client.FirstName;
      textBoxMN.Text = Client.MiddleName;
      textBoxSum.Text = Client.SumOrders.ToString();
      textBoxDiscount.Text = Client.Discount.ToString();
      textBoxPhone1.Text = Client.Phone1;
      textBoxPhone2.Text = Client.Phone2;
      textBoxPhone3.Text = Client.Phone3;
      textBoxAdress.Text = Client.Adress;
      radioButtonMale.Checked = (Client.Sex == "мужской");
      radioButtonFemale.Checked = (Client.Sex == "женский");
      dateTimePickerBD.Value = Client.BirthDay;

    }

    private void buttonCansel_Click(object sender, EventArgs e)
    {
      Client.Clear();
      Close();
    }

    private void buttonSave_Click(object sender, EventArgs e)
    {
      string sex = radioButtonMale.Checked ? "мужской" : "женский";

      Client.SetValues(
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
        Convert.ToInt32(textBoxDiscount.Text)
      );
      Close();
    }
  }
}
