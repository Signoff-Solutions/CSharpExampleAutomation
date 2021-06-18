using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;

namespace CSharpExampleAutomation
{
    public class PlaywrightTests
    {
        private IPlaywright _playwright;
        
        [SetUp]
        public async Task Setup()
        { 
            _playwright = await Playwright.CreateAsync();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public async Task An_authenticated_user_can_add_an_item_to_the_cart()
        {
            await using var browser = await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions 
            { 
                // Headless = false, 
                // SlowMo = 50, 
            });
            
            var page = await browser.NewPageAsync();
            await page.GotoAsync(TestConfig.BaseUrl);

            await page.FillAsync("#user-name", TestConfig.Username);
            await page.FillAsync("#password", TestConfig.Password);
            await page.ClickAsync("#login-button");

            await page.ClickAsync("#add-to-cart-sauce-labs-backpack");

            var basketCount = await page.InnerTextAsync(".shopping_cart_badge");
            
            Assert.That(basketCount, Is.EqualTo("1"));
        }
    }
}