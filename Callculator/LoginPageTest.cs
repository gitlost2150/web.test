using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
            driver.Url = "http://127.0.0.1:8080/Login";
            loginfld = driver.FindElement(By.Id("login"));
            passwordfld = driver.FindElement(By.Id("password"));
            loginBtn = driver.FindElements(By.Id("login"))[1];
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


        [TestCase("test", "newyork", "Incorrect password!")]
        [TestCase("", "", "User name and password cannot be empty!")]
        [TestCase("test1", "newyork1", "Incorrect user name!")]
        public void IncorrectPasswordTest(string login, string password, string expectedErrorMsg)
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

            // assert
            string remindBtn = driver.FindElement(By.Id("remind")).Text;
            string remindReference = "Remind pasword";
            Assert.AreEqual(remindReference, remindBtn);

        }
    }
}