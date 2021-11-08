using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ShadowRootDigger.CORE.Tests
{
    [TestClass]
    public class TestBase
    {
        internal IWebDriver WebDriver;
        private readonly string _chromeDriverVersion = "Latest";

        [TestInitialize]
        public void GetChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), version: _chromeDriverVersion);
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("disable-infobars");
            chromeOptions.AddArguments("--disable-notifications");
            chromeOptions.AddArgument("no-sandbox");
            WebDriver = new ChromeDriver(chromeOptions);
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
            WebDriver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void QuitDriver()
        {
            WebDriver.Quit();
        }
    }
}
