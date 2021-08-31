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
            driver.Url = "http://127.0.0.1:8080/Login";
            IWebElement loginfld = driver.FindElement(By.Id("login"));
            IWebElement passwordfld = driver.FindElement(By.Id("password"));
            IWebElement loginBtn = driver.FindElements(By.Id("login"))[1];

            // act
            loginfld.SendKeys("test");
            passwordfld.SendKeys("newyork1");
            loginBtn.Click();

            // assert
            string expectedUrl = "http://127.0.0.1:8080/Deposit";
            string actualUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl);
            driver.Quit();
        }

        [Test]
        public void IncorrectLoginTest()
        {

            // arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://127.0.0.1:8080/Login";
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

        [Test]
        public void IncorrectPasswordTest()
        {

            // arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://127.0.0.1:8080/Login";
            IWebElement loginfld = driver.FindElement(By.Id("login"));
            IWebElement passwordfld = driver.FindElement(By.Id("password"));
            IWebElement loginBtn = driver.FindElements(By.Id("login"))[1];

            // act
            loginfld.SendKeys("test");
            passwordfld.SendKeys("newyork");
            loginBtn.Click();

            // assert
            string errorLoginMsg = driver.FindElement(By.Id("errorMessage")).Text;
            string expectedErrorMsg = "Incorrect password!";
            Assert.AreEqual(expectedErrorMsg, errorLoginMsg);

            driver.Quit();
        }

        [Test]
        public void EmptyFields()
        {

            // arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://127.0.0.1:8080/Login";
            IWebElement loginBtn = driver.FindElements(By.Id("login"))[1];

            // act
            loginBtn.Click();

            // assert
            string errorLoginMsg = driver.FindElement(By.Id("errorMessage")).Text;
            string expectedErrorMsg = "User name and password cannot be empty!";
            Assert.AreEqual(expectedErrorMsg, errorLoginMsg);

            driver.Quit();
        }


        // Verify button "Remind Password"
        [Test]
        public void VerifyRemindButton()
        {

            //arrange
            IWebDriver driver = new ChromeDriver();
            driver.Url = "http://127.0.0.1:8080/Login";

            // assert
            string remindBtn = driver.FindElement(By.Id("remind")).Text;
            string remindReference = "Remind pasword";
            Assert.AreEqual(remindReference, remindBtn);

            driver.Quit();
        }
    }
}