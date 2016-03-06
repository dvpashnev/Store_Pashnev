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
  public partial class DepartmentForm : Form
  {
    public Department dep;

    public DepartmentForm(Department department)
    {
      InitializeComponent();
      dep = department;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      dep.SetValues(
        Convert.ToInt32(textBoxId.Text),
        textBoxName.Text
        );
      Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      dep.Clear();
      Close();
    }

    private void DepartmentForm_Load(object sender, EventArgs e)
    {
      textBoxId.Text = dep.Id.ToString();
      textBoxId.ReadOnly = true;
      textBoxName.Text = dep.Name;
    }
  }
}
