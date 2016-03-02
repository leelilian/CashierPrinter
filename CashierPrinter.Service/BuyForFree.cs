using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierPrinter.Service
{

  /// <summary>
  /// buy X to get y for free. 
  /// X is the purchased quantity, and y is the quatity for free.
  /// </summary>
  public class BuyForFree : NoPreferential
  {
    private decimal _BuyCount;
    private decimal _FreeCount;

    public BuyForFree(string productCode, string productName, string unit, decimal orderQuantity, decimal productPrice, decimal buyCount, decimal freeCount) 
      :base(productCode, productName, unit, orderQuantity, productPrice)
    {
      if (buyCount <= 0)
      {
        throw new Exception("Buy count must be greater than zero.");
      }
      if (freeCount <= 0)
      {
        throw new Exception("Free count must be greater than zero.");
      }
      _BuyCount = buyCount;
      _FreeCount = freeCount;
    }

    public override decimal CalculateAmout()
    {
      int qty = (int)(_OrderQuantiy / (_BuyCount + _FreeCount));
      decimal remainer = _OrderQuantiy % (_BuyCount + _FreeCount);

      // work out the quantity should be charged
      decimal priceQty = qty * _BuyCount + remainer;

      return Math.Round(priceQty * _ProductPrice, 2);
    }


  }
}
