using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierPrinter.Service
{

  /// <summary>
  /// only discount
  /// </summary>
  public class Discount : NoPreferential
  {
    private decimal _Discount;

    public Discount(string productCode, string productName, string unit, decimal orderQuantity, decimal productPrice, decimal discount)
        : base(productCode, productName, unit, orderQuantity, productPrice)
    {
      if (discount < 0 || discount > 1)
      {
        throw new Exception("Discount must be between 0 and 1");
      }
      _Discount = discount;
    }
    public override decimal CalculateAmout()
    {
      return Math.Round(_OrderQuantiy * _ProductPrice * _Discount, 2);
    }

    public override string Print()
    {
      decimal savedMoney = Math.Round((_OrderQuantiy * _ProductPrice - CalculateAmout()), 2);
      return string.Format("名称：{0}， 数量：{1}（{2}），单价：{3}（元）， 小计：{4}（元）， 节省：{5}（元）", _ProductName,
          _OrderQuantiy, _Unit, _ProductPrice, CalculateAmout(), savedMoney);
    }
  }
}
