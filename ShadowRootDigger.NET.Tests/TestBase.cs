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
        private int _retry = 0;

        [TestInitialize]
        public void GetChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), version: _chromeDriverVersion);
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--disable-notifications");
            chromeOptions.AddArgument("--no-sandbox");
            Retry:
            try
            {
                _retry++;
                if (ConfigurationManager.AppSettings["UseDocker"].ToLower().Equals("true"))
                    WebDriver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub/"), chromeOptions);
                else
                    WebDriver = new ChromeDriver(chromeOptions);
                WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
                WebDriver.Manage().Timeouts().PageLoad.Add(TimeSpan.FromSeconds(40));
                WebDriver.Manage().Window.Maximize();
            }
            catch (WebDriverException ex) { if (_retry <= 1) goto Retry; else throw ex; }
        }

        [TestCleanup]
        public void QuitDriver()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }

        /// <summary>
        /// Gets test files directory path.
        /// </summary>
        /// <returns>Test files directory path.</returns>
        protected string GetTestFilePath()
        {
            var path = @"file:\\\" + Environment.CurrentDirectory + "\\TestFiles\\ShadowDOM.html";
            return path;
        }
    }
}