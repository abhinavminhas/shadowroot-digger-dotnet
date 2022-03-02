using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;
using System.Runtime.InteropServices;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace ShadowRootDigger.CORE.Tests
{
    [TestClass]
    public class TestBase
    {
        internal IWebDriver WebDriver;
        private readonly string _chromeDriverVersion = "98.0.4758.102";
        protected const string TESTS_DOTNETCORE = "TESTS-DOTNETCORE";
        protected const string DOTNETCORE_CHROME_SETTINGS = "DOTNETCORE-CHROME-SETTINGS";
        protected const string DOTNETCORE_SHADOW_DOM_HTML = "DOTNETCORE-SHADOW-DOM-HTML";
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
            catch (WebDriverException ex){ if (_retry <= 1) goto Retry; else throw ex;  }
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
            var path = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                path = "file:///" + Environment.CurrentDirectory + "/TestFiles/ShadowDOM.html";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                path = "file:///" + Environment.CurrentDirectory + "/TestFiles/ShadowDOM.html";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                path = "file:///" + Environment.CurrentDirectory + "/TestFiles/ShadowDOM.html";
            return path;
        }
    }
}
