using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierPrinter.Service
{

  /// <summary>
  /// no discount or any perferential, the product will be sold at the 
  /// normal price.
  /// </summary>
  public class NoPreferential : ICashierPrinter
  {
    protected string _ProductCode;
    protected string _ProductName;
    protected string _Unit;
    protected decimal _OrderQuantiy;
    protected decimal _ProductPrice;

    public NoPreferential(string productCode, string productName, string unit, decimal orderQuantity, decimal productPrice)
    {
      // normally we should not validate the input here, as the order quantity, product price and discount
      // are from either the cashier or product master information. But in case someone has very bad habit to
      // test the system, it would be better to validate the input parameter here although it will decrease the
      // performance slightly.
      if (orderQuantity <= 0)
      {
        throw new Exception("Order quantity must be greater than zero.");
      }
      if (productPrice <= 0)
      {
        throw new Exception("Product price must be no less than zero.");
      }

      _ProductCode = productCode;
      _ProductName = productName;
      _Unit = unit;
      _OrderQuantiy = orderQuantity;
      _ProductPrice = productPrice;
     
    }
    public virtual decimal CalculateAmout()
    {
      return Math.Round(_OrderQuantiy * _ProductPrice, 2);
    }

    public virtual string Print()
    {
      return string.Format("名称：{0}， 数量：{1}{2}，单价：{3}（元）， 小计：{4}（元）", _ProductName,
          _OrderQuantiy, _Unit, _ProductPrice, CalculateAmout());
    }
  }
}
