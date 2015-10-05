using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Pashnev
{
  static class Department
  {
    private static int _id;
    private static string _name;

    public static int Id
    {
      get { return _id; }
    }

    public static string Name
    {
      get { return _name; }
    }

    public static void SetValues(
      int id,
      string name
      )
    {
      _id = id;
      _name = name;
    }

    public static void Clear()
    {
      _id = 0;
      _name = "";
    }

    public static void SetId(int id)
    {
      _id = id;
    }
  }
}
