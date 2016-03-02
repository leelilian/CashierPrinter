using CashierPrinter.Model;
using CashierPrinter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierPrinter.Strategy
{
  /// <summary>
  /// work out the product preferential and calculate the amount to be charged
  /// </summary>
  public class ProductStrategy
  {
    private ICashierPrinter _CalculateAmount = null;
    public ProductStrategy(ICashierPrinter calculateAmount)
    {
      _CalculateAmount = calculateAmount;
    }

    public decimal CalculatAmount()
    {
      return _CalculateAmount.CalculateAmout();
    }
    public string Print()
    {
      return _CalculateAmount.Print();
    }
  }
}
