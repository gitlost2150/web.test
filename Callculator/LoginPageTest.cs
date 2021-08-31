using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CalculatorTests
{
    public class LoginPageTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PositiveLoginTest()
        {
            // arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";
            IWebElement loginfld = driver.FindElement(By.Id("login"));
            IWebElement passwordfld = driver.FindElement(By.Id("password"));
            IWebElement loginBtn = driver.FindElements(By.Id("login"))[1];

            // act
            loginfld.SendKeys("test");
            passwordfld.SendKeys("newyork1");
            loginBtn.Click();

            // assert
            string expectedUrl = "http://localhost:64177/Deposit";
            string actualUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
            driver.Quit();
        }

        [Test]
        public void IncorrectLoginTest()
        {

            // arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:64177/Login";
            IWebElement loginfld = driver.FindElement(By.Id("login"));
            IWebElement passwordfld = driver.FindElement(By.Id("password"));
            IWebElement loginBtn = driver.FindElements(By.Id("login"))[1];

            // act
            loginfld.SendKeys("test1");
            passwordfld.SendKeys("newyork1");
            loginBtn.Click();

            // assert
            string errorLoginMsg = driver.FindElement(By.Id("errorMessage")).Text;
            string expectedErrorMsg = "Incorrect user name!";
            Assert.AreEqual(expectedErrorMsg, errorLoginMsg);

            driver.Quit();
        }

        // Verify button "Remind Password"
    }
}