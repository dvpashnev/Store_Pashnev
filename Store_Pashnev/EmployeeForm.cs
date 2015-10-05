using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store_Pashnev
{
  public partial class EmployeeForm : Form
  {
    private int _emplId;
    private int _department;
    private DataTable _deps;
    private DataTable _roles;
    private DataTable _users;
    private string _connectString;

    public EmployeeForm(int emplId, int department, DataTable deps, DataTable roles, DataTable users, string connectString)
    {
      InitializeComponent();
      _emplId = emplId;
      _department = department;
      _deps = deps;
      _roles = roles;
      _users = users;
      _connectString = connectString;
    }

    private void EmployeeForm_Load(object sender, EventArgs e)
    {
      textBoxId.Text = _emplId.ToString();

      comboBoxDep.DataSource = _deps;
      comboBoxDep.DisplayMember = "Name";
      comboBoxDep.ValueMember = "Id";

      comboBoxDep.SelectedIndex = _department - 1;

      if (Employee.LastName != null)
      {
        textBoxLN.Text = Employee.LastName;
        textBoxFN.Text = Employee.FirstName;
        textBoxMN.Text = Employee.MiddleName;
        textBoxUserID.Text = Employee.UserId.ToString();
        dateTimePickerAccept.Value = Employee.AcceptanceDate;
        textBoxSO.Text = Employee.SumOrders.ToString();
        textBoxBonuses.Text = Employee.Bonuses.ToString();
      }
    }

    private void button5_Click(object sender, EventArgs e)
    {
      using (SqlConnection connect = new SqlConnection(_connectString))
      {
        SqlCommand command = new SqlCommand();
        connect.Open();
        command.Connection = connect;
        command.CommandText = String.Format("select ident_current('User')");
        User.SetId(Convert.ToInt32(command.ExecuteScalar()) + 1);
      }

      UserForm uf = new UserForm(User.Id, _roles, _users);
      uf.ShowDialog();

      if (User.Id != 0)
      {
        textBoxUserID.Text = User.Id.ToString();
      }
    }

    private void buttonSave_Click(object sender, EventArgs e)
    {
      Employee.SetValues(
        Convert.ToInt32(textBoxId.Text),
        textBoxLN.Text,
        textBoxFN.Text,
        textBoxMN.Text,
        Convert.ToInt32((comboBoxDep.SelectedItem as DataRowView).Row[0]),
        Convert.ToInt32(textBoxUserID.Text),
        dateTimePickerAccept.Value,
        Convert.ToDouble(textBoxSO.Text),
        Convert.ToDouble(textBoxBonuses.Text)
        );
      Close();
    }

    private void EmployeeForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (Employee.Id !=0 && textBoxUserID.Text == "")
      {
        MessageBox.Show(@"Вы не создали пользователя для работника");
        textBoxUserID.Focus();
        e.Cancel = true;
      }
    }

    private void buttonCansel_Click(object sender, EventArgs e)
    {
      Employee.Clear();
      User.Clear();
      Close();
    }
  }
}
