using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumTek
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //var options = new ChromeOptions();
            //options.AddArgument("start-maximized"); 
            //IWebDriver driver = new ChromeDriver(options);
            //driver.Navigate().GoToUrl("https://giris.turkiye.gov.tr/Giris/gir");
            //IWebElement tcInput = driver.FindElement(By.Id("tridField"));
            //IWebElement sifreInput = driver.FindElement(By.Id("egpField"));
            //tcInput.SendKeys("?");
            //sifreInput.SendKeys("?");
            //IWebElement girisButton = driver.FindElement(By.CssSelector(".btn-send"));
            //girisButton.Click();
            //Console.ReadLine();
            //driver.Quit();
            // Chrome WebDriver'ı başlat
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            IWebDriver driver = new ChromeDriver(options);

            try
            {
                // N11 sitesine git
                driver.Navigate().GoToUrl("https://www.n11.com/telefon-ve-aksesuarlari");

                // Ürünlerin listesini al
                var productElements = driver.FindElements(By.CssSelector(".productName"));

                // Ürün adı ve fiyatını yazdır
                foreach (var productElement in productElements)
                {
                    string productName = productElement.Text;

                    // Ürün fiyatını alma
                    string productPrice = "";
                    try
                    {
                        productPrice = productElement.FindElement(By.XPath("./ancestor::div[@class='columnContent']//ins")).Text;
                    }
                    catch (NoSuchElementException)
                    {
                        try
                        {
                            productPrice = productElement.FindElement(By.XPath("./ancestor::div[@class='columnContent']//span[@class='newPrice']")).Text;
                        }
                        catch (NoSuchElementException)
                        {
                            productPrice = "Fiyat bilgisi bulunamadı";
                        }
                    }

                    // Ürün adını ve fiyatını renkli olarak yazdır
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Ürün Adı: " + productName);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ürün Fiyatı: " + productPrice);
                    Console.ResetColor();
                    Console.WriteLine("--------------------");

                    // Pop-up penceresinde ürün adı ve fiyatını göster
                    ShowPopup(productName, productPrice);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
            finally
            {
                // Tarayıcıyı kapat
                driver.Quit();
                Console.ReadLine();
            }
        }

        // Pop-up penceresinde ürün adı ve fiyatını gösteren metod
        static void ShowPopup(string productName, string productPrice)
        {
            // Burada pop-up penceresi açılabilir, örneğin Windows Forms veya WPF uygulamasında
            // Aşağıdaki kod örnek amaçlıdır ve gerçek bir pop-up penceresi açmaz
            Console.WriteLine("Pop-up: " + productName + " - " + productPrice);
            
        }
    }
}