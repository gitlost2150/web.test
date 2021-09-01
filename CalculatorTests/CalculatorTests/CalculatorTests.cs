using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CalculatorTests
{
    public class CalculatorTests
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
            loginfld.SendKeys("test");
            passwordfld.SendKeys("newyork1");
            loginBtn.Click();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void Test1()
        {
            // arrange
            IWebElement amount = driver.FindElement(By.Id("amount"));
            IWebElement percent = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement d365 = driver.FindElement(By.Id("d365"));

            // act
            amount.SendKeys("1000");
            percent.SendKeys("10");
            term.SendKeys("365");
            d365.Click();

            // assert
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");
            string expectedIncome = "1100.00";
            string expectedInterest = "100.00";
            Assert.AreEqual(expectedIncome, actualIncome);
            Assert.AreEqual(expectedInterest, actualInterest);

        }
    }
}
