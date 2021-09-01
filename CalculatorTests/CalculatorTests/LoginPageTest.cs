using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CalculatorTests
{
    public class LoginPageTest
    {
        private IWebDriver driver;
        private IWebElement loginfld;
        private IWebElement passwordfld;
        private IWebElement loginBtn;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "http://127.0.0.1:8080/Login";
            loginfld = driver.FindElement(By.Id("login"));
            passwordfld = driver.FindElement(By.Id("password"));
            loginBtn = driver.FindElement(By.Id("loginBtn"));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void PositiveLoginTest()
        {
            // arrange

            // act
            loginfld.SendKeys("test");
            passwordfld.SendKeys("newyork1");
            loginBtn.Click();

            // assert
            string expectedUrl = "http://127.0.0.1:8080/Deposit";
            string actualUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
        }


        [TestCase("test", "newyork", "Incorrect username or password!")]
        [TestCase("", "", "User name and password cannot be empty!")]
        [TestCase("test1", "newyork1", "Incorrect username or password!")]
        public void NegativeLoginTest(string login, string password, string expectedErrorMsg)
        {
            // arrange

            // act
            loginfld.SendKeys(login);
            passwordfld.SendKeys(password);
            loginBtn.Click();

            // assert
            string errorLoginMsg = driver.FindElement(By.Id("errorMessage")).Text;
            Assert.AreEqual(expectedErrorMsg, errorLoginMsg);
        }

        // Verify button "Remind Password"
        [Test]
        public void VerifyRemindButton()
        {

            //arrange
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("remindBtn")));
            // assert
            string remindBtn = driver.FindElement(By.Id("remindBtn")).Text;
            string remindReference = "Remind password";
            Assert.AreEqual(remindReference, remindBtn);

        }
    }
}