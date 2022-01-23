using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ShadowRootDigger.NET.Tests
{
    [TestClass]
    public class TestBase
    {
        internal IWebDriver WebDriver;
        private readonly string _chromeDriverVersion = "97.0.4692.71";
        protected const string TESTS_DOTNETFRAMEWORK = "TESTS-DOTNETFRAMEWORK";
        protected const string DOTNETFRAMEWORK_CHROME_SETTINGS = "DOTNETFRAMEWORK-CHROME-SETTINGS";
        protected const string DOTNETFRAMEWORK_SHADOW_DOM_HTML = "DOTNETFRAMEWORK-SHADOW-DOM-HTML";

        [TestInitialize]
        public void GetChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), version: _chromeDriverVersion);
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--disable-notifications");
            chromeOptions.AddArgument("--no-sandbox");
            if (ConfigurationManager.AppSettings["UseDocker"].ToLower().Equals("true"))
                WebDriver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub/"), chromeOptions);
            else
                WebDriver = new ChromeDriver(chromeOptions);
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
            WebDriver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(40));
            WebDriver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void QuitDriver()
        {
            WebDriver.Quit();
        }

        /// <summary>
        /// Gets test files directory path.
        /// </summary>
        /// <returns>Test files directory path.</returns>
        protected string GetTestFilePath()
        {
            var path = Environment.CurrentDirectory + "\\TestFiles\\ShadowDOM.html";
            return path;
        }
    }
}