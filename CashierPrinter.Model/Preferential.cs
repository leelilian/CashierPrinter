using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierPrinter.Model
{
  public class Preferential
  {
    public string ProductCode { get; set; }

    public string PreferentialCode { get; set; }

    public string PreferentialDesc { get; set; }

    public string Ref1 { get; set; }

    public string Ref1Desc { get; set; }

    public string Ref2 { get; set; }

    public string Ref2Desc { get; set; }

    public string Ref3 { get; set; }

    public string Ref3Desc { get; set; }

    public string Ref4 { get; set; }

    public string Ref4Desc { get; set; }

    public int Priority { get; set; }

    public bool IsExclusive { get; set; }

    public bool IsPrinted { get; set; }
  }
}
