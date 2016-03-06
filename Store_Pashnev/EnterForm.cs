using System;
using System.Windows.Forms;
using Store_Pashnev.ServiceReference1;
using Store_WSL;

namespace Store_Pashnev
{
  public partial class EnterForm : Form
  {
    private StoreServiceClient _client;

    public User user = new User();

    public EnterForm(StoreServiceClient client)
    {
      InitializeComponent();
      _client = client;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      int res = _client.GetUserId(tbNick.Text, tbPassword.Text);

      switch (res)
      {
        case 0:
          {
            MessageBox.Show(@"Такого пользователя нет. Попробуйте ещё раз");
            tbNick.Text = "";
            tbPassword.Text = "";
            tbNick.Focus();
          }
          break;
        case -1:
          {
            MessageBox.Show(@"Пароль неверный. Попробуйте ещё раз");
            tbPassword.Text = "";
            tbPassword.Focus();
          }
          break;
        default:
          user.SetId(res);
          Close();
          break;
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
