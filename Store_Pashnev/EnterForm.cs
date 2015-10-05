using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Store_Pashnev
{
  public partial class EnterForm : Form
  {
    public EnterForm()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      using (SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["Store"].ConnectionString)) // создание объекта подключения
      {
        connect.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = connect;
        SqlParameter nick = new SqlParameter();
        nick.ParameterName = "@Nick";
        nick.Direction = ParameterDirection.Input;
        nick.Value = tbNick.Text;
        command.Parameters.Add(nick);

        SqlParameter password = new SqlParameter();
        password.ParameterName = "@Password";
        password.Direction = ParameterDirection.Input;
        password.Value = tbPassword.Text;
        command.Parameters.Add(password);

        command.CommandText = "select Id from [User] where Nick = @Nick and Password = @Password";

        EnterData.UserID = Convert.ToInt32(command.ExecuteScalar());
      }

      if (EnterData.UserID == 0)
      {
        MessageBox.Show(@"Такого пользователя нет. Попробуйте ещё раз");
        tbNick.Text = "";
        tbPassword.Text = "";
        tbNick.Focus();
      }
      else
      {
        Close();
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
