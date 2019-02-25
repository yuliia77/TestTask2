using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{

    [TestClass]
    public class UnitTest1
        {
        [TestMethod]
        public void TestMethod1()
        {

            ChromeDriver driver = new ChromeDriver(System.IO.Path.GetFullPath("./resources"));
            //FirefoxDriver driver = new FirefoxDriver(System.IO.Path.GetFullPath("./resources"));
            //InternetExplorerDriver driver = new InternetExplorerDriver(System.IO.Path.GetFullPath("./resources"));
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(120);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("https://onliner.by");

                 Assert.AreEqual("Onliner", driver.Title);
                 driver.FindElement(By.XPath("//div[contains(@class, 'auth-bar__item--text')]")).Click();
                 var inputUsername = driver.FindElement(By.CssSelector("input[class$='auth-form__input_width_full'][type='text']"));
                 var inputPwd = driver.FindElement(By.CssSelector("input[class$='auth-form__input_width_full'][type='password']"));
                 var btnSubmit = driver.FindElement(By.XPath("//div/button[@type='submit']"));
                 Actions actions = new Actions(driver);
                 actions.SendKeys(inputUsername, "TestAccNik").SendKeys(inputPwd, "qwaszx@1").Click(btnSubmit).Build().Perform();
                 //check if user is logged-in
                 WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                 wait.Message = "Log-In Icon is not found";
                 wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".b-top-profile__image")));
                 driver.FindElement(By.LinkText("Каталог")).Click();
                 WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                 {
                     Message = "Requested page is not found"
                 };
                 wait2.Until(ExpectedConditions.TitleContains("Каталог"));

                 //click on the random group link
                 var links = driver.FindElements(By.CssSelector("ul.catalog-bar__list a"));
                 int countDisplayed = 0;
                 foreach (var link in links)
                 {
                     if (link.Displayed)
                     {
                         countDisplayed++;
                         Console.WriteLine(link.Text);
                     }
                 }

                 Random rnd = new Random();
                 int rndNumber = rnd.Next(countDisplayed + 1);
                 var rndLink = driver.FindElements(By.CssSelector("ul.catalog-bar__list a"))[rndNumber];
                 string rndLinkText = driver.FindElements(By.CssSelector("ul.catalog-bar__list a"))[rndNumber].Text;
                 string rndLinkHrefValue = rndLink.GetAttribute("href");
                 rndLink.Click();
                 Assert.AreEqual(rndLinkHrefValue, driver.Url, "Clicked link text and URL don't match");
                 string actualCategoryTitle = driver.FindElement(By.CssSelector(".schema-header__title")).Text;
                 Assert.AreEqual(rndLinkText, actualCategoryTitle, "Clicked link text and actual category title don't match");
                 //logout
                 driver.FindElement(By.XPath("//div[contains(@class, 'item_arrow')]/a")).Click();
                 driver.FindElement(By.CssSelector("div[class$='profile__logout']>a")).Click();
                 WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                 wait3.Message = "User is not logged out";
                 wait3.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[contains(@class, 'auth-bar__item--text')]")));
        }
    }
    }

