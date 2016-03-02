using CashierPrinter.Model;
using CashierPrinter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierPrinter.Strategy
{
  public class StrategyFactory
  {
    private static ICashierPrinter cashierPrinter = null;
    public static ICashierPrinter GetCashierPrinter(PurchasedProduct product, Preferential preferential)
    {

      if (preferential == null || string.IsNullOrEmpty(preferential.PreferentialCode))
      {
        cashierPrinter = new NoPreferential(product.ProductCode, product.ProductName, product.Unit,
                                            product.OrderQty, product.ProductPrice);
      }
      else
      {
        switch (preferential.PreferentialCode)
        {
          case "Discount":
            cashierPrinter = new Discount(product.ProductCode, product.ProductName, product.Unit,
                                          product.OrderQty, product.ProductPrice, Convert.ToDecimal(preferential.Ref1));
            break;
          case "BuyForFree":
            cashierPrinter = new BuyForFree(product.ProductCode, product.ProductName, product.Unit,
                                          product.OrderQty, product.ProductPrice, Convert.ToDecimal(preferential.Ref1),
            Convert.ToDecimal(preferential.Ref2));
            break;
          default:
            break;

        }
      }

      return cashierPrinter;
    }
  }
}
