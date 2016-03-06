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
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string Name { get; set; }

    public void SetValues(
      int id,
      string name
      )
    {
      Id = id;
      Name = name;
    }

    public void Clear()
    {
      Id = 0;
      Name = "";
    }

    public void SetId(int id)
    {
      Id = id;
    }
  }
}
