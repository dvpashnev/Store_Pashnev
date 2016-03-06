using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Store_WSL
{
  [DataContract]
  public class Department
  {
    private int _id;
    private string _name;

    [DataMember]
    public int Id
    {
      get { return _id; }
    }

    [DataMember]
    public string Name
    {
      get { return _name; }
    }

    public void SetValues(
      int id,
      string name
      )
    {
      _id = id;
      _name = name;
    }

    public void Clear()
    {
      _id = 0;
      _name = "";
    }

    public void SetId(int id)
    {
      _id = id;
    }
  }
}
