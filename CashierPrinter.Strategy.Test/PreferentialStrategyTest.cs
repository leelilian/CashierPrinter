using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashierPrinter.Model;
using System.Collections.Generic;

namespace CashierPrinter.Strategy.Test
{
  [TestClass]
  public class PreferentialStrategyTest
  {
    [TestMethod]
    public void Test_GetProductPreferentials_Retrun_Empty()
    {
      PurchasedProduct product = new PurchasedProduct()
      {
        ProductCode = "code",
        ProductName = "name",
        ProductDesc = "desc",
        OrderQty = 500M,
        ProductPrice = 3.87M,
        Unit = "个"
      };

      List<Preferential> preferentials = new List<Preferential>()
      {
        new Preferential() { PreferentialCode = "code1", ProductCode = "c1",IsExclusive = true, IsPrinted =false  },
        new Preferential() { PreferentialCode = "code1", ProductCode = "c2",IsExclusive = true, IsPrinted =false  }
      };

      List<Preferential> productPreferentials = PreferentialStrategy.GetProductPreferentials(product, preferentials);
      Assert.IsNotNull(productPreferentials);
      Assert.IsTrue(productPreferentials.Count == 0);
    }


    [TestMethod]
    public void Test_GetProductPreferentials_Retrun_Exclusive()
    {
      PurchasedProduct product = new PurchasedProduct()
      {
        ProductCode = "code",
        ProductName = "name",
        ProductDesc = "desc",
        OrderQty = 500M,
        ProductPrice = 3.87M,
        Unit = "个"
      };

      List<Preferential> preferentials = new List<Preferential>()
      {
        new Preferential() { PreferentialCode = "code1", ProductCode = "code",IsExclusive = true, IsPrinted =false  },
        new Preferential() { PreferentialCode = "code2", ProductCode = "code",IsExclusive = false, IsPrinted =false  }
      };

      List<Preferential> productPreferentials = PreferentialStrategy.GetProductPreferentials(product, preferentials);
      
      Assert.IsTrue(productPreferentials.Count == 1);

      Preferential perf = productPreferentials[0];
      Assert.IsTrue(perf.PreferentialCode == "code1" && perf.ProductCode == "code");
    }


    [TestMethod]
    public void Test_GetProductPreferentials()
    {
      PurchasedProduct product = new PurchasedProduct()
      {
        ProductCode = "code",
        ProductName = "name",
        ProductDesc = "desc",
        OrderQty = 500M,
        ProductPrice = 3.87M,
        Unit = "个"
      };

      List<Preferential> preferentials = new List<Preferential>()
      {
        new Preferential() { PreferentialCode = "code1", ProductCode = "code",IsExclusive = false, IsPrinted =false  },
        new Preferential() { PreferentialCode = "code2", ProductCode = "code",IsExclusive = false, IsPrinted =false  },
        new Preferential() { PreferentialCode = "code3", ProductCode = "code",IsExclusive = false, IsPrinted =false  }
      };

      List<Preferential> productPreferentials = PreferentialStrategy.GetProductPreferentials(product, preferentials);
      Assert.IsNotNull(productPreferentials);
      Assert.IsTrue(productPreferentials.Count == 3);

      Preferential perf = null;
      perf = productPreferentials.Find(p=>p.PreferentialCode == "code1");
      Assert.IsTrue(perf.PreferentialCode == "code1" && perf.ProductCode == "code" && perf.IsExclusive == false && perf.IsPrinted == false);

      perf = productPreferentials.Find(p => p.PreferentialCode == "code2");
      Assert.IsTrue(perf.PreferentialCode == "code2" && perf.ProductCode == "code" && perf.IsExclusive == false && perf.IsPrinted == false);

      perf = productPreferentials.Find(p => p.PreferentialCode == "code3");
      Assert.IsTrue(perf.PreferentialCode == "code3" && perf.ProductCode == "code" && perf.IsExclusive == false && perf.IsPrinted == false);
    }
  }



}
