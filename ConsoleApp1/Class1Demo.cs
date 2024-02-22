using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using NUnit.Framework.Legacy;
using System.Numerics;

namespace ConsoleApp1
{
    [TestFixture]
    internal class Class1Demo
    {
        private IWebDriver _webDriver;

        [SetUp]
        public void SetUp()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
             _webDriver.Quit();
        }

        [Test]
        public void Test()
        {
            // 1. Navigate to login page
            _webDriver.Navigate().GoToUrl("https://www.saucedemo.com/");

            // 2. Enter valid credentials to log in.
            _webDriver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            _webDriver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            _webDriver.FindElement(By.Id("login-button")).Click();

            //3. Verify that the login is successful and the user is redirected to the products page.
            String pageURL = "https://www.saucedemo.com/inventory.html";
             Assert.That(pageURL, Is.EqualTo(_webDriver.Url));

            string productPageUrl = _webDriver.Url;

            if (productPageUrl.Contains("inventory"))
            {
                Console.WriteLine("Login was successful.");
            }
            else
            {
                Console.WriteLine("An error occured while loading the page. Please try again later");
            }


            // 4. Select a T-shirt by clicking on its image or name.
            _webDriver.FindElement(By.PartialLinkText("T-Shirt")).Click();
            
               // 5. Verify that the T-shirt details page is displayed.
               String productTitle = _webDriver.FindElement(By.XPath("//div[@class='inventory_details_name large_size']")).Text;
                Assert.That(productTitle, Does.Contain("T-Shirt"));

            
                // 6. Click the "Add to Cart" button.
                _webDriver.FindElement(By.Id("add-to-cart-sauce-labs-bolt-t-shirt")).Click();

                // 7. Verify that the T-shirt is added to the cart successfully.
                String cartCount = _webDriver.FindElement(By.XPath("//span[@class='shopping_cart_badge']")).Text;
                int n = Int32.Parse(cartCount);
                if (n == 1)
                {
                    Console.WriteLine("T-shirt is added to the cart successfully");
                } else
                {
                    Console.WriteLine("Failed to add product to cart");
                }

                // 8. Navigate to the cart by clicking the cart icon or accessing the cart page directly.
                _webDriver.FindElement(By.XPath("//a[@class='shopping_cart_link']")).Click();



                // 9. Verify that the cart page is displayed.
                String cartPageURL = _webDriver.Url;
                if (cartPageURL.Contains("cart"))
                {
                    Console.WriteLine("Cart loaded successfully");
                }
                else
                {
                    Console.WriteLine("An error occured while loading the cart. Please try again later");
                }


            // 10. Review the items in the cart and ensure that the T-shirt is listed with the correct details (name, price,

            String qty = _webDriver.FindElement(By.XPath("//div[@class='cart_quantity']")).Text;
                int cartQty = Int32.Parse(qty);
                if (cartQty == 1)
                {
                    Console.WriteLine("Quantity of selected T-Shirt is 1");
                }
                else
                {
                    Console.WriteLine("Failed to get Quantity of T-Shirt");
                }

                // 11. Click the "Checkout" button.
                _webDriver.FindElement(By.Id("checkout")).Click();


                // 12. Verify that the checkout information page is displayed
                String pageTitle = _webDriver.FindElement(By.XPath("//span[@class='title']")).Text;
                String checkoutPageTitle = "Checkout: Your Information";
                Assert.That(pageTitle, Is.EqualTo(checkoutPageTitle));
                string checkoutPageURL = _webDriver.Url;
                if (checkoutPageURL.Contains("checkout"))
                {
                    Console.WriteLine("Checkout loaded successfully");
                }
                else
                {
                    Console.WriteLine("An error occured while loading the checkout. Please try again later");
                }


            // 13.Enter the required checkout information (e.g., name, shipping address, payment details).
            _webDriver.FindElement(By.Id("first-name")).SendKeys("John");
                _webDriver.FindElement(By.Id("last-name")).SendKeys("Doe");
                _webDriver.FindElement(By.Id("postal-code")).SendKeys("100023");


                // 14. Click the "Continue" button.
                _webDriver.FindElement(By.Id("continue")).Click();

            // 15.Verify that the order summary page is displayed, showing the T-shirt details and the total amount.
            String summaryPageURL = "https://www.saucedemo.com/checkout-step-two.html";
            if (summaryPageURL == _webDriver.Url)
            {
                Console.WriteLine("order summary page is displayed");
            }
            else
            {
                Console.WriteLine("Failed to load order summary page");
            }
            //  showing the T-shirt details and the total amount.


            // 16. Click the "Finish" button.
            _webDriver.FindElement(By.Id("finish")).Click();


            // 17.Verify that the order confirmation page is displayed, indicating a successful purchase.
            string confirmationPageURL = _webDriver.Url;
            if (confirmationPageURL.Contains("checkout-complete"))
            {
                Console.WriteLine("Thank you for your order!");
            }
            else
            {
                Console.WriteLine("An error occured while placing your order. Please try again later");
            }
            


            // 18. Logout from the application.
            _webDriver.FindElement(By.Id("react-burger-menu-btn")).Click();
            IWebElement logoutLink = _webDriver.FindElement(By.Id("logout_sidebar_link"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_webDriver;
            js.ExecuteScript("arguments[0].click();", logoutLink);


            // 19. Verify that the user is successfully logged out and redirected to the login page.
            string loginPageURL = _webDriver.Url;
            if (loginPageURL == "https://www.saucedemo.com/")
            {
                Console.WriteLine("Logout was successful");
            }
            else
            {
                Console.WriteLine("An error occured while logging out. Please try again later");
            }

        }

    }
}
