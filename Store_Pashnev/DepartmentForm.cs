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
  public partial class DepartmentForm : Form
  {
    private int _depId;

    public DepartmentForm(int depId)
    {
      InitializeComponent();
      _depId = depId;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Department.SetValues(
        Convert.ToInt32(textBoxId.Text),
        textBoxName.Text
        );
      Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      Department.Clear();
      Close();
    }

    private void DepartmentForm_Load(object sender, EventArgs e)
    {
      textBoxId.Text = _depId.ToString();
      textBoxName.Text = Department.Name;
    }
  }
}
