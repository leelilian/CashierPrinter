using CashierPrinter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierPrinter.Strategy
{
  public class PreferentialStrategy
  {
    public static List<Preferential> GetProductPreferentials(PurchasedProduct product, List<Preferential> preferentials)
    {
      var preferentialList = preferentials.Where(p => p.ProductCode == product.ProductCode).ToList();
      if (preferentialList == null || preferentialList.Count == 0)
      {
        return new List<Preferential>();
      }
      var exclusivePreferential = preferentialList.Where(p => p.IsExclusive == true).ToList();
      if (exclusivePreferential != null && exclusivePreferential.Count > 0)
      {
        return exclusivePreferential;
      }
      return preferentialList;

    }

  }
}
