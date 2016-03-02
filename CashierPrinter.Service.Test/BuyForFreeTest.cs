using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace CashierPrinter.Service.Test
{
  /// <summary>
  /// Summary description for BuyForFreeTest
  /// </summary>
  [TestClass]
  public class BuyForFreeTest
  {
    BuyForFree buyForFree = null;
    public BuyForFreeTest()
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
    public void Test_BuyForFree()
    {
      buyForFree = new BuyForFree("code", "name", "PCS", 30, 8.5M, 4, 1);
      Type type = buyForFree.GetType();
      FieldInfo productCode = type.GetField("_ProductCode",
                                                 BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue(productCode.GetValue(buyForFree).ToString() == "code");


      FieldInfo productName = type.GetField("_ProductName",
                                                BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue(productName.GetValue(buyForFree).ToString() == "name");

      FieldInfo unit = type.GetField("_Unit",
                                                BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue(unit.GetValue(buyForFree).ToString() == "PCS");

      FieldInfo orderQuantity = type.GetField("_OrderQuantiy",
                                                BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue((decimal)orderQuantity.GetValue(buyForFree) == 30);

      FieldInfo productPrice = type.GetField("_ProductPrice",
                                                BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue((decimal)productPrice.GetValue(buyForFree) == 8.5M);

      FieldInfo buyCount = type.GetField("_BuyCount",
                                               BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue((decimal)buyCount.GetValue(buyForFree) == 4);

      FieldInfo freeCount = type.GetField("_FreeCount",
                                               BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue((decimal)freeCount.GetValue(buyForFree) == 1);

    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void BuyCount_LessThanZero_Exception()
    {
      try
      {
        buyForFree = new BuyForFree("code", "name", "PCS", 30, 8.5M, -4, 1);
      }
      catch (Exception ex)
      {
        Assert.AreEqual("Buy count must be greater than zero.", ex.Message);
        throw;
      }
    }


    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void FreeCount_LessThanZero_Exception()
    {
      try
      {
        buyForFree = new BuyForFree("code", "name", "PCS", 30, 8.5M, 4, 0);
      }
      catch (Exception ex)
      {
        Assert.AreEqual("Free count must be greater than zero.", ex.Message);
        throw;
      }
    }
    [TestMethod]
    public void LessThan_BuyCount_CalculateAmout()
    {
      buyForFree = new BuyForFree("code", "name", "PCS", 3, 8.5M, 4, 1);
      decimal amount = buyForFree.CalculateAmout();
      Assert.IsTrue(amount == 3 * 8.5M);

    }

    [TestMethod]
    public void EqualTo_BuyCount_CalculateAmout()
    {
      buyForFree = new BuyForFree("code", "name", "PCS", 4, 8.5M, 4, 1);
      decimal amount = buyForFree.CalculateAmout();
      Assert.IsTrue(amount == 4 * 8.5M);

    }

    [TestMethod]
    public void GreaterThan_BuyCount_CalculateAmout()
    {
      buyForFree = new BuyForFree("code", "name", "PCS", 6, 8.5M, 4, 1);
      decimal amount = buyForFree.CalculateAmout();
      Assert.IsTrue(amount == 5 * 8.5M);
    }
  }
}
