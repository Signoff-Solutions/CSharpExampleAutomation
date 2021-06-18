using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CSharpExampleAutomation
{
    public class WebDriverTests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver {Url = TestConfig.BaseUrl};
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }

        [Test]
        public void An_authenticated_user_can_add_an_item_to_the_cart()
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(3))
                .Until(d => d.FindElement(By.Id("user-name")).Displayed);
            
            _driver.FindElement(By.Id("user-name")).SendKeys(TestConfig.Username);
            _driver.FindElement(By.Id("password")).SendKeys(TestConfig.Password);
            _driver.FindElement(By.Id("login-button")).Click();
            
            new WebDriverWait(_driver, TimeSpan.FromSeconds(3))
                .Until(d => d.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Displayed);

            _driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();

            var basketCount = _driver.FindElement(By.CssSelector(".shopping_cart_badge")).Text;
            
            Assert.That(basketCount, Is.EqualTo("1"));
        }
    }
}