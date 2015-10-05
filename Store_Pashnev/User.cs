using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Pashnev
{
  static class User
  {
    private static int _id;
    private static int _positionId;
    private static string _nick;
    private static string _password;

    public static int Id
    {
      get { return _id; }
    }

    public static int PositionId
    {
      get { return _positionId; }
    }

    public static string Nick
    {
      get { return _nick; }
    }

    public static string Password
    {
      get { return _password; }
    }

    public static void SetValues(
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

    public static void Clear()
    {
      _id = 0;
      _positionId = 0;
      _nick = "";
      _password = "";
    }

    public static void SetId(int id)
    {
      _id = id;
    }
  }
}
