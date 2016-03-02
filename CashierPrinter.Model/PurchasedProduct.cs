using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierPrinter.Model
{
  public class PurchasedProduct : Product
  {
    public decimal OrderQty { get; set; }
  }
}
