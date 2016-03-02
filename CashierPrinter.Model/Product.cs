using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierPrinter.Model
{
  public class Product
  {
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public string ProductDesc { get; set; }
    public decimal ProductPrice { get; set; }
    public string Unit { get; set; }

  }
}
