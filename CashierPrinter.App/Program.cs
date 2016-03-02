using CashierPrinter.Model;
using CashierPrinter.Service;
using CashierPrinter.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierPrinter.App
{
  class Program
  {

    private static Dictionary<string, List<string>> specialPreferentials = new Dictionary<string, List<string>>();
    static void Main(string[] args)
    {

      // product master
      List<PurchasedProduct> productRepository = new List<PurchasedProduct>(){
        new PurchasedProduct() {ProductCode= "ITEM000001", ProductDesc="超牌羽毛球", ProductName="羽毛球", Unit="个", ProductPrice = 1.00M },
        new PurchasedProduct() {ProductCode= "ITEM000003", ProductDesc="进口苹果", ProductName="苹果", Unit="斤", ProductPrice = 5.50M },
        new PurchasedProduct() {ProductCode= "ITEM000002", ProductDesc="进口图书", ProductName="C#进阶", Unit="本", ProductPrice = 55.50M },
        new PurchasedProduct() {ProductCode= "ITEM000004", ProductDesc="进口CD", ProductName="原版CD", Unit="张", ProductPrice = 33.50M },
        new PurchasedProduct() {ProductCode= "ITEM000005", ProductDesc="可口可乐", ProductName="可口可乐", Unit="瓶", ProductPrice = 3.00M },
        new PurchasedProduct() {ProductCode= "ITEM000006", ProductDesc="陶瓷", ProductName="砂锅", Unit="个", ProductPrice = 203.00M },
        new PurchasedProduct() {ProductCode= "ITEM000007", ProductDesc="国产手机", ProductName="手机", Unit="个", ProductPrice = 1500.00M }
      };

      // preferentials
      List<Preferential> noPreferentialRepository = new List<Preferential>();

      List<Preferential> discountRepository = new List<Preferential>() {
      new Preferential() {  PreferentialCode="Discount", PreferentialDesc="打折", ProductCode = "ITEM000003", Ref1 = "0.95", Ref1Desc ="95折"} };

      List<Preferential> buyForFreeRepository = new List<Preferential>() {
      new Preferential() {  PreferentialCode="BuyForFree", PreferentialDesc="买二赠一", ProductCode = "ITEM000001", Ref1 = "2", Ref1Desc ="买2个", Ref2 = "1", Ref2Desc = "赠送1个",IsPrinted = true},
      new Preferential() {  PreferentialCode="BuyForFree", PreferentialDesc="买二赠一", ProductCode = "ITEM000002", Ref1 = "2", Ref1Desc ="买2个", Ref2 = "1", Ref2Desc = "赠送1个", IsPrinted = true}};

      List<Preferential> preferentialRepository = new List<Preferential>() {
      new Preferential() {  PreferentialCode="BuyForFree", PreferentialDesc="买二赠一", ProductCode = "ITEM000001", Ref1 = "2", Ref1Desc ="买2个", Ref2 = "1", Ref2Desc = "赠送1个"},
      new Preferential() {  PreferentialCode="BuyForFree", PreferentialDesc="买二赠一", ProductCode = "ITEM000002", Ref1 = "2", Ref1Desc ="买2个", Ref2 = "1", Ref2Desc = "赠送1个"}};


      // the input message
      string rawInput = @"[ 'ITEM000001',
                            'ITEM000001',
                            'ITEM000001',
                            'ITEM000001',
                            'ITEM000001',
                            'ITEM000003-2',
                            'ITEM000005',
                            'ITEM000005',
                            'ITEM000005']";

      string[] productList = rawInput.Replace("[", "").Replace("]", "").Replace("'", "").Replace(" ", "")
                                     .Split(new char[] { ',', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);


      Dictionary<string, int> productQty = new Dictionary<string, int>();
      for (int i = 0; i < productList.Length; i++)
      {
        string productCode = productList[i];
        int qty = 1;
        if (productList[i].Contains("-"))
        {
          string[] composit = productList[i].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
          productCode = composit[0];
          qty = Convert.ToInt32(composit[1]);
        }
        if (productQty.ContainsKey(productCode))
        {
          productQty[productCode] = productQty[productCode] + qty;
        }
        else
        {
          productQty.Add(productCode, qty);
        }
      }

      List<PurchasedProduct> purchasedProductList = new List<PurchasedProduct>();
      foreach (KeyValuePair<string, int> item in productQty)
      {
        PurchasedProduct product = productRepository.Where(p => p.ProductCode == item.Key).FirstOrDefault();
        if (product != null)
        {
          PurchasedProduct purchasedProduct = new PurchasedProduct()
          {
            ProductCode = product.ProductCode,
            ProductName = product.ProductName,
            ProductDesc = product.ProductDesc,
            ProductPrice = product.ProductPrice,
            Unit = product.Unit,
            OrderQty = item.Value
          };
          purchasedProductList.Add(purchasedProduct);
        }
      }


      // apply to different preferential 
      StringBuilder sb = new StringBuilder();
      sb.AppendLine(string.Format("***<没钱赚商店>购物清单***"));
      // no preferential
      decimal totalPayAmount = 0;
      decimal totalRawAmount = 0;
      foreach (var item in purchasedProductList)
      {
        // lookup the preferential options
        ICashierPrinter cashierPrinter = StrategyFactory.GetCashierPrinter(item, null);
        CashierPrinterStrategy stategy = new CashierPrinterStrategy(cashierPrinter);
        List<Preferential> preferentials = PreferentialStrategy.GetProductPreferentials(item, buyForFreeRepository);
        foreach (var preferential in preferentials)
        {
          cashierPrinter = StrategyFactory.GetCashierPrinter(item, preferential);
          stategy = new CashierPrinterStrategy(cashierPrinter);
          if (preferential.IsPrinted)
          {
            if (!specialPreferentials.ContainsKey(preferential.PreferentialCode))
            {
              specialPreferentials.Add(preferential.PreferentialCode, new List<string>() { preferential.PreferentialDesc });
            }
            specialPreferentials[preferential.PreferentialCode].Add(string.Format("名称：{0}，数量：{1}{2}", item.ProductName, item.OrderQty, item.Unit));
          }
        }
        totalPayAmount += stategy.CalculatAmount();
        totalRawAmount += item.ProductPrice * item.OrderQty;
        //stategy.SetProductStrategy();
        sb.AppendLine(stategy.Print());
        
      }
      sb.AppendLine("----------------------");

      if (specialPreferentials.Count != 0)
      {
        foreach (KeyValuePair<string,List<string>> item in specialPreferentials)
        {
          foreach (var specialProduct in item.Value)
          {
            sb.AppendLine(specialProduct);
          }
          
        }
        sb.AppendLine("----------------------");
      }

      sb.AppendLine(string.Format("总计：{0}（元）", totalPayAmount));
      if (totalRawAmount != totalPayAmount)
      {
        sb.AppendLine(string.Format("节省：{0}（元）", totalRawAmount - totalPayAmount));
      }
      sb.AppendLine("**********************");
      Console.WriteLine(sb.ToString());
      Console.ReadLine();

    }

  }

}
