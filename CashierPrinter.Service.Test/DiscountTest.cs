using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace CashierPrinter.Service.Test
{
  /// <summary>
  /// Summary description for DiscountTest
  /// </summary>
  [TestClass]
  public class DiscountTest
  {
    Discount discount = null;
    public DiscountTest()
    {
      discount = new Discount("code", "name", "pcs", 100M, 20M, 0.95M);
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
    public void Test_Discount()
    {
     
      Type type = discount.GetType();
      FieldInfo productCode = type.GetField("_ProductCode",
                                                 BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue(productCode.GetValue(discount).ToString() == "code");


      FieldInfo productName = type.GetField("_ProductName",
                                                BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue(productName.GetValue(discount).ToString() == "name");

      FieldInfo unit = type.GetField("_Unit",
                                                BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue(unit.GetValue(discount).ToString() == "pcs");

      FieldInfo orderQuantity = type.GetField("_OrderQuantiy",
                                                BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue((decimal)orderQuantity.GetValue(discount) == 100M);

      FieldInfo productPrice = type.GetField("_ProductPrice",
                                                BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue((decimal)productPrice.GetValue(discount) == 20M);

      FieldInfo discountAmount = type.GetField("_Discount",
                                               BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue((decimal)discountAmount.GetValue(discount) == 0.95M);

    }

    [TestMethod]
    public void Test_CalculateAmout()
    {
      //
      // TODO: Add test logic here
      //
      Assert.IsTrue(discount.CalculateAmout() == 100 * 20 * 0.95M);
     
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Discount_GreaterThanOne_Exception()
    {
      try
      {
        Discount discount = new Discount("code", "name", "pcs", 100M, 20M, 1.2M);
      }
      catch (Exception ex)
      {
        Assert.AreEqual("Discount must be between 0 and 1", ex.Message);
        throw;
      }
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Discount_LessThanZero_Exception()
    {
      try
      {
        Discount discount = new Discount("code", "name", "pcs", 100M, 20M, -1.2M);
      }
      catch (Exception ex)
      {
        Assert.AreEqual("Discount must be between 0 and 1", ex.Message);
        throw;
      }
    }
  }
}
