using System.Runtime.Serialization;

namespace Store_WSL
{
  [DataContract]
  public class User
  {
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public int PositionId { get; set; }

    [DataMember]
    public string Nick { get; set; }

    [DataMember]
    public string Password { get; set; }

    public void SetValues(
      int id,
      int posId,
      string nick,
      string password
      )
    {
      Id = id;
      PositionId = posId;
      Nick = nick;
      Password = password;
    }

    public void Clear()
    {
      Id = 0;
      PositionId = 0;
      Nick = "";
      Password = "";
    }

    public void SetId(int id)
    {
      Id = id;
    }
  }
}
