using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Store_WSL
{
  [DataContract]
  public class ProductInOrder
  {
    [DataMember]
    public int Id { get; set; }
    [DataMember]
    public int OrderId { get; set; }
    [DataMember]
    public int ProductId { get; set; }
    [DataMember]
    public int Quantity { get; set; }
    [DataMember]
    public double FinalPrice { get; set; }
    [DataMember]
    public double Sum { get; set; }

    public ProductInOrder()
    {
      
    }

    public ProductInOrder(int id,
      int orderId,
      int productId,
      int quantity,
      double FinalPrice,
      double sum)
    {
      Id = id;
      OrderId = orderId;
      ProductId = productId;
      Quantity = quantity;
      FinalPrice = FinalPrice;
      Sum = sum;
    }
  }
}
