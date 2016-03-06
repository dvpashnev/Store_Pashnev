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
using Store_WSL;
using Store_Pashnev.ServiceReference1;

namespace Store_Pashnev
{
  public partial class EmployeeForm : Form
  {
    private int _department;
    private DataTable _deps;
    private DataTable _roles;
    private DataTable _users;

    private StoreServiceClient _client;

    public User user = new User();

    public Employee employee = new Employee();

    public EmployeeForm(Employee empl, int department, DataTable deps, DataTable roles, DataTable users, StoreServiceClient client)
    {
      InitializeComponent();
      employee = empl;
      _department = department;
      _deps = deps;
      _roles = roles;
      _users = users;
      _client = client;
    }

    private void EmployeeForm_Load(object sender, EventArgs e)
    {
      textBoxId.Text = employee.Id.ToString();
      textBoxId.ReadOnly = true;

      comboBoxDep.DataSource = _deps;
      comboBoxDep.DisplayMember = "Name";
      comboBoxDep.ValueMember = "Id";

      comboBoxDep.SelectedIndex = _department - 1;

      if (employee.LastName != null)
      {
        textBoxLN.Text = employee.LastName;
        textBoxFN.Text = employee.FirstName;
        textBoxMN.Text = employee.MiddleName;
        textBoxUserID.Text = employee.UserId.ToString();
        dateTimePickerAccept.Value = employee.AcceptanceDate;
        textBoxSO.Text = employee.SumOrders.ToString();
        textBoxBonuses.Text = employee.Bonuses.ToString();
      }
    }

    private void button5_Click(object sender, EventArgs e)
    {
      user.SetId(_client.GetCurIdentity("User") + 1);

      UserForm uf = new UserForm(user, _roles, _users);
      uf.ShowDialog();

      user = uf.user;

      if (user.Id != 0)
      {
        textBoxUserID.Text = user.Id.ToString();
      }
    }

    private void buttonSave_Click(object sender, EventArgs e)
    {
      employee.SetValues(
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
      if (employee.Id !=0 && textBoxUserID.Text == "")
      {
        MessageBox.Show(@"Вы не создали пользователя для работника");
        textBoxUserID.Focus();
        e.Cancel = true;
      }
    }

    private void buttonCansel_Click(object sender, EventArgs e)
    {
      employee.Clear();
      user.Clear();
      Close();
    }
  }
}
