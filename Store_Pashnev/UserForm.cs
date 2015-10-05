using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store_Pashnev
{
  public partial class UserForm : Form
  {
    private int _userId;
    private static DataTable _roles;
    private static DataTable _users;
    private static bool _nickChanged = false;


    private string _oldPassword;

    public UserForm(int uId, DataTable roles, DataTable users)
    {
      InitializeComponent();
      _userId = uId;
      _roles = roles;
      _users = users;
    }

    private void UserForm_Load(object sender, EventArgs e)
    {
      textBoxId.Text = _userId.ToString();
      comboBoxPos.DataSource = _roles;
      comboBoxPos.DisplayMember = "Title";
      comboBoxPos.ValueMember = "Id";

      comboBoxPos.SelectedIndex = -1;

      if (User.PositionId != 0)
      {
        comboBoxPos.SelectedIndex = User.PositionId - 1;
        textBoxNick.Text = User.Nick;

        _oldPassword = User.Password;

        labelOldPassword.Visible = true;
        tbOldPassword.Visible = true;
        tbOldPassword.Enabled = true;
      }

    }
    
    private void button1_Click(object sender, EventArgs e)
    {
      if (User.PositionId != 0)
      {
        if (tbOldPassword.Text == String.Empty)
        {
          MessageBox.Show(@"Введите старый пароль!");
          tbOldPassword.Focus();
          return;
        }
      }

      if (comboBoxPos.SelectedItem != null)
      {
        if (textBoxNick.Text == String.Empty)
        {
          MessageBox.Show(@"Введите ник!");
          textBoxNick.Focus();
          return;
        }

        if (_nickChanged)
        {
          if (isNickExist(textBoxNick.Text))
          {
            MessageBox.Show(@"Такой ник уже существует. Введите другой!");
            textBoxNick.Focus();
            return;
          }
        }

        if (textBoxPassword.Text == String.Empty)
        {
          MessageBox.Show(@"Введите новый пароль!");
          textBoxPassword.Focus();
          return;
        }

      if (textBoxPassword.Text == textBoxConfirm.Text)
      {
          User.SetValues(
            Convert.ToInt32(textBoxId.Text),
            Convert.ToInt32((comboBoxPos.SelectedItem as DataRowView).Row[0]),
            textBoxNick.Text,
            textBoxPassword.Text
            );
          Close();
        }
        else
        {
          MessageBox.Show(@"Выберите должность!");
          comboBoxPos.Focus();
          return;
        }
      }
      else
      {
        MessageBox.Show(@"Пароль и подтверждение пароля не совпадают!");
        textBoxPassword.Focus();
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      _nickChanged = false;
      User.Clear();
      Close();
    }

    private void textBoxNick_TextChanged(object sender, EventArgs e)
    {
      if (_nickChanged)
      {
        isNickExist(textBoxNick.Text);
      }
      if (!_nickChanged)
      {
        _nickChanged = true;
      }
    }

    bool isNickExist(string nick)
    {
      try
      {
        DataRow isExist = (from row in _users.AsEnumerable()
                       where row.Field<string>("Nick").Equals(textBoxNick.Text)
                       select row).First();
        if (isExist != null)
        {
          errorProvider1.SetError(textBoxNick, "Такой ник уже существует");
          return true;
        }
        errorProvider1.SetError(textBoxNick, String.Empty);
        return false;
      }
      catch (InvalidOperationException)
      {
        errorProvider1.SetError(textBoxNick, String.Empty);
        return false;
      }
    }
  }
}
