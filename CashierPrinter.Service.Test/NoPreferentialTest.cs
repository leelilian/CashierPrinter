using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace CashierPrinter.Service.Test
{
  /// <summary>
  /// Summary description for NoPreferentialTest
  /// </summary>
  [TestClass]
  public class NoPreferentialTest
  {
    NoPreferential noPreferential = null;
    public NoPreferentialTest()
    {
      //
      // TODO: Add constructor logic here
      //
      noPreferential = new NoPreferential("productcode", "name", "PCS", 20.00M, 87.50M);
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
    public void Test_NoPreferential()
    {
      //
      // TODO: Add test logic here
      //
      Type type = noPreferential.GetType();
      FieldInfo productCode = type.GetField("_ProductCode", 
                                                 BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue(productCode.GetValue(noPreferential).ToString() == "productcode");


      FieldInfo productName = type.GetField("_ProductName",
                                                BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue(productName.GetValue(noPreferential).ToString() == "name");

      FieldInfo unit = type.GetField("_Unit",
                                                BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue(unit.GetValue(noPreferential).ToString() == "PCS");

      FieldInfo orderQuantity = type.GetField("_OrderQuantiy",
                                                BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue((decimal)orderQuantity.GetValue(noPreferential) == 20M);

      FieldInfo productPrice = type.GetField("_ProductPrice",
                                                BindingFlags.NonPublic | BindingFlags.Instance);
      Assert.IsTrue((decimal)productPrice.GetValue(noPreferential) == 87.5M);

    }

    [TestMethod]
    public void Test_CalculateAmout()
    {
      //
      // TODO: Add test logic here
      //
      decimal amount = noPreferential.CalculateAmout();
      Assert.IsTrue(amount == (decimal)(20 * 87.5));
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Price_LessThanZero_Exception()
    {
      try
      {
        NoPreferential noPref = new NoPreferential("code", "name", "PCS", 25, -0.9M);
      }
      catch (Exception ex)
      {
        Assert.AreEqual("Product price must be no less than zero.", ex.Message);
        throw;
      }
     
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void Quantity_LessThanZero_Exception()
    {
      try
      {
        NoPreferential noPref = new NoPreferential("code", "name", "PCS", -25, 0.9M);
      }
      catch (Exception ex)
      {
        Assert.AreEqual("Order quantity must be greater than zero.", ex.Message);
        throw;
      }

    }
  }
}
