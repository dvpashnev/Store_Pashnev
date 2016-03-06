using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Store_WSL
{
  [DataContract]
  public class User
  {
    private int _id;
    private int _positionId;
    private string _nick;
    private string _password;

    [DataMember]
    public int Id
    {
      get { return _id; }
    }

    [DataMember]
    public int PositionId
    {
      get { return _positionId; }
    }

    [DataMember]
    public string Nick
    {
      get { return _nick; }
    }

    [DataMember]
    public string Password
    {
      get { return _password; }
    }

    public void SetValues(
      int id,
      int posId,
      string nick,
      string password
      )
    {
      _id = id;
      _positionId = posId;
      _nick = nick;
      _password = password;
    }

    public void Clear()
    {
      _id = 0;
      _positionId = 0;
      _nick = "";
      _password = "";
    }

    public void SetId(int id)
    {
      _id = id;
    }
  }
}
