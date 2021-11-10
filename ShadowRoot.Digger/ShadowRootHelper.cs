using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Reflection;

namespace ShadowRoot.Digger
{
    public static class ShadowRootHelper
    {

        /// <summary>
        /// Returns nested shadow root element from a nested list of shadow root element selectors separated by '>'.
        /// </summary>
        /// <param name="webDriver">Selenium webdriver instance.</param>
        /// <param name="shadowRootSelector">List of shadow root element selectors (probably jQuery or CssSelectors) separated by '>'.</param>
        /// <param name="timeInSeconds">Wait time in seconds.</param>
        /// <param name="pollingIntervalInMilliseconds">Polling interval time in milliseconds.</param>
        /// <returns>Nested shadow root web element.</returns>
        public static IWebElement GetNestedShadowRootElement(this IWebDriver webDriver, string shadowRootSelector, int timeInSeconds = 20, int pollingIntervalInMilliseconds = 2000)
        {
            var GlobalDriverImplicitWait = webDriver.Manage().Timeouts().ImplicitWait.Ticks;
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(pollingIntervalInMilliseconds);
            var listShadowRootSelectors = shadowRootSelector.Split('>')
                .Select(p => p.Trim()).Where(p => !string.IsNullOrWhiteSpace(p));
            var shadowRootQuerySelector = ".querySelector('{0}').shadowRoot";
            var shadowRootQueryString = "";
            var shadowRootElement = "";
            foreach (var shadowRoot in listShadowRootSelectors)
            {
                var documentReturn = "return document{0};";
                var tempQueryString = string.Format(shadowRootQuerySelector, shadowRoot);
                shadowRootQueryString += tempQueryString;
                shadowRootElement = string.Format(documentReturn, shadowRootQueryString);
                try
                {
                    var webDriverWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeInSeconds))
                    {
                        PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalInMilliseconds)
                    };
                    webDriverWait.Until(item => (IWebElement)((IJavaScriptExecutor)webDriver).ExecuteScript(shadowRootElement) != null);
                }
                catch (WebDriverException) { throw new WebDriverException(string.Format("{0}: Shadow root element for selector '{1}' in DOM hierarchy '{2}' Not Found.", MethodBase.GetCurrentMethod().Name, shadowRoot, shadowRootElement)); }
            }
            var requiredShadowRoot = (IWebElement)((IJavaScriptExecutor)webDriver).ExecuteScript(shadowRootElement);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromTicks(GlobalDriverImplicitWait);
            return requiredShadowRoot;
        }
    }
}