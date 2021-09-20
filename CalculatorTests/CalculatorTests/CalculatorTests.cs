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

        [TestCase("", "", "")]
        [TestCase("0", "0", "0")]
        [TestCase("0", "10", "365")]
        [TestCase("0", "0", "365")]
        public void EmptyOrZeroDepositTest(string depositAmount, string roi, string investmentTerm)
        {
            // arrange
            IWebElement amount = driver.FindElement(By.Id("amount"));
            IWebElement percent = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement d365 = driver.FindElement(By.Id("d365"));

            // act
            amount.SendKeys(depositAmount);
            percent.SendKeys(roi);
            term.SendKeys(investmentTerm);
            d365.Click();

            // assert
            string expectedIncome = "0.00";
            string expectedInterest = "0.00";
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual(expectedIncome, actualIncome);
            Assert.AreEqual(expectedInterest, actualInterest);
        }

        [TestCase("1000", "0", "0")]
        [TestCase("1000", "10", "0")]
        [TestCase("1000", "0", "365")]
        public void NoInterestDepositTest(string depositAmount, string roi, string investmentTerm)
        {
            // arrange
            IWebElement amount = driver.FindElement(By.Id("amount"));
            IWebElement percent = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement d365 = driver.FindElement(By.Id("d365"));

            // act
            amount.SendKeys(depositAmount);
            percent.SendKeys(roi);
            term.SendKeys(investmentTerm);
            d365.Click();

            // assert
            string expectedIncome = "1000.00";
            string expectedInterest = "0.00";
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual(expectedIncome, actualIncome);
            Assert.AreEqual(expectedInterest, actualInterest);
        }

        [TestCase("1000", "10", "365")]
        [TestCase("1000", "10", "360")]
        public void PositiveDepositTest_365(string depositAmount, string roi, string investmentTerm)
        {
            // arrange
            IWebElement amount = driver.FindElement(By.Id("amount"));
            IWebElement percent = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement finYear = driver.FindElement(By.Id("d" + investmentTerm));

            // act
            amount.SendKeys(depositAmount);
            percent.SendKeys(roi);
            term.SendKeys(investmentTerm);
            finYear.Click();

            // assert
            string expectedIncome = "1100.00";
            string expectedInterest = "100.00";
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual(expectedIncome, actualIncome);
            Assert.AreEqual(expectedInterest, actualInterest);
        }


        [TestCase("-", "0")]
        [TestCase("0", "0")]
        [TestCase("1", "1")]
        [TestCase("1000", "1000")]
        [TestCase("100000", "100000")]
        [TestCase("100001", "0")]
        public void DepositAmountRangeTest(string depositAmount, string expectedAmount)
        {
            // arrange
            IWebElement amount = driver.FindElement(By.Id("amount"));

            // act
            amount.SendKeys(depositAmount);
            string amountValue = driver.FindElement(By.Id("amount")).GetAttribute("value");

            // assert
            Assert.AreEqual(expectedAmount, amountValue);
        }

        [TestCase("-", "0")]
        [TestCase("0", "0")]
        [TestCase("1", "1")]
        [TestCase("54", "54")]
        [TestCase("100", "100")]
        [TestCase("101", "0")]
        public void PercentRangeTest(string percentRange, string expectedPercent)
        {
            // arrange
            IWebElement amount = driver.FindElement(By.Id("percent"));

            // act
            amount.SendKeys(percentRange);
            string percentValue = driver.FindElement(By.Id("percent")).GetAttribute("value");

            // assert
            Assert.AreEqual(expectedPercent, percentValue);
        }

        [TestCase("-", "0")]
        [TestCase("0", "0")]
        [TestCase("1", "1")]
        [TestCase("33", "33")]
        [TestCase("365", "365")]
        [TestCase("366", "0")]
        public void TermRangeTest_365(string termRange, string expectedTerm)
        {
            // arrange
            IWebElement amount = driver.FindElement(By.Id("term"));

            // act
            amount.SendKeys(termRange);
            string termValue = driver.FindElement(By.Id("term")).GetAttribute("value");

            // assert
            Assert.AreEqual(expectedTerm, termValue);
        }

        [TestCase("-", "0")]
        [TestCase("0", "0")]
        [TestCase("1", "1")]
        [TestCase("33", "33")]
        [TestCase("360", "360")]
        [TestCase("361", "0")]
        public void TermRangeTest_360(string termRange, string expectedTerm)
        {
            // arrange
            IWebElement amount = driver.FindElement(By.Id("term"));
            IWebElement d360 = driver.FindElement(By.Id("d360"));

            // act
            d360.Click();
            amount.SendKeys(termRange);
            string termValue = driver.FindElement(By.Id("term")).GetAttribute("value");
            
            // assert
            Assert.AreEqual(expectedTerm, termValue);
        }

        [TestCase("Deposit Amount *")]
        public void DepositAmountTitle(string expectedDepositTitle)
        {
            // act
            string actualDepositTitle = driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[1]/td[1]")).GetAttribute("outerText");

            // assert
            Assert.AreEqual(expectedDepositTitle, actualDepositTitle);
        }

        [TestCase("Rate of interest: *")]
        public void InterestFieldTitle(string expectedDepositTitle)
        {
            // act
            string actualDepositTitle = driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[2]/td[1]")).GetAttribute("outerText");

            // assert
            Assert.AreEqual(expectedDepositTitle, actualDepositTitle);
        }

        [TestCase("Investment term: *")]
        public void TermFieldTitle(string expectedDepositTitle)
        {
            // act
            string actualDepositTitle = driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[3]/td[1]")).GetAttribute("outerText");

            // assert
            Assert.AreEqual(expectedDepositTitle, actualDepositTitle);
        }

        [TestCase("Start date:")]
        public void DateFieldTitle(string expectedDepositTitle)
        {
            // act
            string actualDepositTitle = driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[4]/td[1]")).GetAttribute("outerText");

            // assert
            Assert.AreEqual(expectedDepositTitle, actualDepositTitle);
        }

        [TestCase("Financial year: *")]
        public void FinYearFieldTitle(string expectedDepositTitle)
        {
            // act
            string actualDepositTitle = driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[5]/td[1]")).GetAttribute("outerText");

            // assert
            Assert.AreEqual(expectedDepositTitle, actualDepositTitle);
        }

        [TestCase("Income:")]
        public void IncomeFieldTitle(string expectedDepositTitle)
        {
            // act
            string actualDepositTitle = driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[6]/th[1]")).GetAttribute("outerText");

            // assert
            Assert.AreEqual(expectedDepositTitle, actualDepositTitle);
        }

        [TestCase("Interest earned:")]
        public void InterestEarnedFieldTitle(string expectedDepositTitle)
        {
            // act
            string actualDepositTitle = driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[7]/th[1]")).GetAttribute("outerText");

            // assert
            Assert.AreEqual(expectedDepositTitle, actualDepositTitle);
        }

        [TestCase("End date:")]
        public void EndDateFieldTitle(string expectedDepositTitle)
        {
            // act
            string actualDepositTitle = driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[8]/th[1]")).GetAttribute("outerText");

            // assert
            Assert.AreEqual(expectedDepositTitle, actualDepositTitle);
        }
        [TestCase("* - mandatory fields")]
        public void MandatoryFieldTitle(string expectedDepositTitle)
        {
            // act
            string actualDepositTitle = driver.FindElement(By.XPath("/html/body/div/div/table/tbody/tr[9]/td")).GetAttribute("outerText");

            // assert
            Assert.AreEqual(expectedDepositTitle, actualDepositTitle);
        }

        [TestCase("1000", "10", "365")]
        public void NonSelectedCheckBoxFinancialYearTest(string depositAmount, string roi, string investmentTerm)
        {
            // arrange
            IWebElement amount = driver.FindElement(By.Id("amount"));
            IWebElement percent = driver.FindElement(By.Id("percent"));
            IWebElement term = driver.FindElement(By.Id("term"));
            IWebElement d365 = driver.FindElement(By.Id("d365"));

            // act
            amount.SendKeys(depositAmount);
            percent.SendKeys(roi);
            term.SendKeys(investmentTerm);

            // assert
            string expectedIncome = "";
            string expectedInterest = "";
            string actualIncome = driver.FindElement(By.Id("income")).GetAttribute("value");
            string actualInterest = driver.FindElement(By.Id("interest")).GetAttribute("value");

            Assert.AreEqual(expectedIncome, actualIncome);
            Assert.AreEqual(expectedInterest, actualInterest);
        }
    }
}
