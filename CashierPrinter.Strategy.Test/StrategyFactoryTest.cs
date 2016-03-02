using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CashierPrinter.Model;
using CashierPrinter.Service;

namespace CashierPrinter.Strategy.Test
{
  /// <summary>
  /// Summary description for StrategyFactory
  /// </summary>
  [TestClass]
  public class StrategyFactoryTest
  {
    public StrategyFactoryTest()
    {
      //
      // TODO: Add constructor logic here
      //
    }

    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    #region Additional test attributes
    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion

    [TestMethod]
    public void Test_GetCashierPrinter_Preferential_NULL()
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

      Preferential preferential = null;

      ICashierPrinter cashierPrinter = StrategyFactory.GetCashierPrinter(product, preferential);
      Assert.IsNotNull(cashierPrinter);
      Assert.IsInstanceOfType(cashierPrinter, typeof(NoPreferential));
    }


    [TestMethod]
    public void Test_GetCashierPrinter_Preferential_Empty()
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

      Preferential preferential = new Preferential();

      ICashierPrinter cashierPrinter = StrategyFactory.GetCashierPrinter(product, preferential);
      Assert.IsNotNull(cashierPrinter);
      Assert.IsInstanceOfType(cashierPrinter, typeof(NoPreferential));
    }

    [TestMethod]
    public void Test_GetCashierPrinter_Return_NULL()
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

      Preferential preferential = new Preferential()
      {
        PreferentialCode = "code"
      };

      ICashierPrinter cashierPrinter = StrategyFactory.GetCashierPrinter(product, preferential);
      Assert.IsNull(cashierPrinter);
    }


    [TestMethod]
    public void Test_GetCashierPrinter_Return_Discount()
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

      Preferential preferential = new Preferential()
      {
        PreferentialCode = "Discount",
        Ref1 = "0.9"
      };

      ICashierPrinter cashierPrinter = StrategyFactory.GetCashierPrinter(product, preferential);
      Assert.IsNotNull(cashierPrinter);
      Assert.IsInstanceOfType(cashierPrinter, typeof(Discount));
    }

    [TestMethod]
    public void Test_GetCashierPrinter_Return_BuyForFree()
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

      Preferential preferential = new Preferential()
      {
        PreferentialCode = "BuyForFree",
        Ref1 = "5",
        Ref2 = "1"
      };

      ICashierPrinter cashierPrinter = StrategyFactory.GetCashierPrinter(product, preferential);
      Assert.IsNotNull(cashierPrinter);
      Assert.IsInstanceOfType(cashierPrinter, typeof(BuyForFree));
    }
  }
}
