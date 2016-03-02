using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierPrinter.Service
{
  public interface ICashierPrinter
  {
    decimal CalculateAmout();
    string Print();
  }
}
