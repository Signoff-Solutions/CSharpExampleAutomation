using Coypu;
using Coypu.Drivers;
using NUnit.Framework;

namespace CSharpExampleAutomation
{
    public class CoypuTests
    {
        private BrowserSession _browser;
        
        [SetUp]
        public void Setup()
        {
            _browser = new BrowserSession(new SessionConfiguration
            {
                Browser = Browser.Chrome
            });
        }


        [TearDown]
        public void Teardown()
        {
            _browser.Dispose();
        }

        [Test]
        public void An_authenticated_user_can_add_an_item_to_the_cart()
        {
            _browser.Visit(TestConfig.BaseUrl);
            
            _browser.FillIn("user-name").With(TestConfig.Username);
            _browser.FillIn("password").With(TestConfig.Password);
            _browser.ClickButton("login-button");

            _browser.ClickButton("add-to-cart-sauce-labs-backpack");

            var basketCount = _browser.FindCss(".shopping_cart_badge").Text;
            
            Assert.That(basketCount, Is.EqualTo("1"));
        }
    }
}