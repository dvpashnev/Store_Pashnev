
using System.Runtime.Serialization;

namespace Store_WSL
{
  [DataContract]
  public class Product
  {
    [DataMember]
    public int Id { get; set; }

    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public double PurchasePrice { get; set; }

    [DataMember]
    public double Markup { get; set; }

    [DataMember]
    public double Price { get; set; }

    [DataMember]
    public int DepartmentId { get; set; }

    [DataMember]
    public int ProduserId { get; set; }

    [DataMember]
    public int Quantity { get; set; }

    [DataMember]
    public bool CriticalQ { get; set; }

    public void SetValues(
      int id, 
      string title,
      double purchasePrice,
      double markup,
      double price,
      int departmentId,
      int produserId,
      int quantity,
      bool criticalQ)
    {
      Id = id;
      Title = title;
      PurchasePrice = purchasePrice;
      Markup = markup;
      Price = price;
      DepartmentId = departmentId;
      ProduserId = produserId;
      Quantity = quantity;
      CriticalQ = criticalQ;
    }

    public void Clear()
    {
      Id = 0;
      Title = "";
      PurchasePrice = 0.0;
      Markup = 0.0;
      Price = 0.0;
      DepartmentId = 0;
      ProduserId = 0;
      Quantity = 0;
      CriticalQ = false;
    }

    public void SetId(int id)
    {
      Id = id;
    }
  }
}
