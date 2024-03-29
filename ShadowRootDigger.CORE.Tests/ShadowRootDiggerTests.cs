﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ShadowRoot.Digger;
using System;
using System.Linq;

namespace ShadowRootDigger.CORE.Tests
{
    [TestClass]
    public class ShadowRootDiggerTests : TestBase
    {

        #region 'Chrome Settings' Web Controls

        private readonly string _tabRootElement = "body settings-ui > div#container settings-main#main > settings-basic-page.cr-centered-card-container > settings-privacy-page > settings-clear-browsing-data-dialog > cr-tabs[role=\"tablist\"]";
        private readonly string _clearBrowsingDataDialogRootElement = "settings-ui > settings-main#main > settings-basic-page.cr-centered-card-container > settings-privacy-page > settings-clear-browsing-data-dialog";
        private readonly string _settingsDropdownMenuRootElement = "settings-ui > settings-main#main > settings-basic-page.cr-centered-card-container > settings-privacy-page > settings-clear-browsing-data-dialog > settings-dropdown-menu#clearFromBasic";
        private readonly string _divTabIdentifier = "div.tab";
        private readonly string _selectTimeRangeIdentifier = "select#dropdownMenu";
        private readonly string _basicTabCheckboxesIdentifier = "settings-checkbox[id*=\"CheckboxBasic\"]";
        private readonly string _buttonClearDataIdentifier = "#clearBrowsingDataConfirm";
        private readonly string _notExistsNestedShadowRootElement = "settings-ui > settings-main.main";
        private readonly string _existsShadowRootElement = "settings-ui";
        private readonly string _notExistsShadowRootElement = "not-exists";

        #endregion

        #region 'Shadow DOM HTML' Web Controls

        private readonly string _shadowHostElement = "#shadow_host";
        private readonly string _shadowRootEnclosedInput = "input[type='text']";
        private readonly string _shadowRootEnclosedCheckbox = "input[type='checkbox']";
        private readonly string _shadowRootEnclosedInputChooseFile = "input[type='file']";
        private readonly string _notExistsShadowHostElement = "#non_host";
        private readonly string _nestedShadowHostElement = "#shadow_host > #nested_shadow_host";
        private readonly string _nestedShadowHostShadowContent = "div[id='nested_shadow_content']";
        private readonly string _notExistsNestedShadowHostElement = "#shadow_host > #shadow_content";

        #endregion

        #region 'Chrome Settings' Tests

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_GetShadowRootElement_ChromeSettings_ShadowRootElementExists()
        {
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            var clearBrowsingTab = ShadowRootAssist.GetShadowRootElement(WebDriver, _existsShadowRootElement);
            Assert.IsNotNull(clearBrowsingTab);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_GetShadowRootElement_ChromeSettings_ShadowRootElementDoesNotExists()
        {
            var expectedErrorMessage = "GetShadowRootElement: Shadow root element for selector 'not-exists' Not Found.";
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            try
            {
                ShadowRootAssist.GetShadowRootElement(WebDriver, _notExistsShadowRootElement);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException ex) { Assert.AreEqual(expectedErrorMessage, ex.Message); }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_GetShadowRootElement_ChromeSettings_ImplicitWaitManipulationCheck_Exists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            ShadowRootAssist.GetShadowRootElement(WebDriver, _existsShadowRootElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_GetShadowRootElement_ChromeSettings_ImplicitWaitManipulationCheck_NotExists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            try
            {
                ShadowRootAssist.GetShadowRootElement(WebDriver, _notExistsShadowRootElement);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException)
            {
                var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
                Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
            }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_GetNestedShadowRootElement_ChromeSettings_ClearChromeData()
        {
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            var clearBrowsingTab = ShadowRootAssist.GetNestedShadowRootElement(WebDriver, _tabRootElement);
            Retry:
            int count = 0;
            var isReady = clearBrowsingTab.FindElements(By.CssSelector(_divTabIdentifier))
                .FirstOrDefault(item => item.Text == "Basic" && item.Displayed == true && item.Enabled == true);
            if (isReady == null)
            {
                count++;
                if (count > 1)
                    clearBrowsingTab.FindElements(By.CssSelector(_divTabIdentifier)).FirstOrDefault(item => item.Text == "Basic").Click();
                else
                    goto Retry;
            }
            else
                clearBrowsingTab.FindElements(By.CssSelector(_divTabIdentifier)).FirstOrDefault(item => item.Text == "Basic").Click();
            var settingsDropdownMenu = ShadowRootAssist.GetNestedShadowRootElement(WebDriver, _settingsDropdownMenuRootElement);
            var timeRangeSelect = settingsDropdownMenu.FindElement(By.CssSelector(_selectTimeRangeIdentifier));
            new SelectElement(timeRangeSelect).SelectByText("Last hour");
            var clearBrowsingDataDialog = ShadowRootAssist.GetNestedShadowRootElement(WebDriver, _clearBrowsingDataDialogRootElement);
            var basicCheckboxes = clearBrowsingDataDialog.FindElements(By.CssSelector(_basicTabCheckboxesIdentifier));
            foreach (var checkbox in basicCheckboxes)
            {
                var isCheckboxChecked = checkbox.GetAttribute("checked");
                if (isCheckboxChecked == null)
                    checkbox.Click();
            }
            clearBrowsingDataDialog.FindElement(By.CssSelector(_buttonClearDataIdentifier)).Click();
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_GetNestedShadowRootElement_ChromeSettings_RootElementDoesNotExists()
        {
            var expectedErrorMessage = "GetNestedShadowRootElement: Nested shadow root element for selector 'settings-main.main' in DOM hierarchy 'settings-ui > settings-main.main' Not Found.";
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            try
            {
                ShadowRootAssist.GetNestedShadowRootElement(WebDriver, _notExistsNestedShadowRootElement);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException ex) { Assert.AreEqual(expectedErrorMessage, ex.Message); }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_GetNestedShadowRootElement_ChromeSettings_ImplicitWaitManipulationCheck_Exists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            ShadowRootAssist.GetNestedShadowRootElement(WebDriver, _tabRootElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_GetNestedShadowRootElement_ChromeSettings_ImplicitWaitManipulationCheck_NotExists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            try
            {
                ShadowRootAssist.GetNestedShadowRootElement(WebDriver, _notExistsNestedShadowRootElement);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException)
            {
                var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
                Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
            }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_IsShadowRootElementPresent_ChromeSettings_ShadowRootExists()
        {
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            var exists = ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _existsShadowRootElement);
            Assert.AreEqual(true, exists);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_IsShadowRootElementPresent_ChromeSettings_ShadowRootNotExists()
        {
            var expectedErrorMessage = "IsShadowRootElementPresent: Shadow root element for selector 'not-exists' Not Found.";
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            var exists = ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _notExistsShadowRootElement);
            Assert.AreEqual(false, exists);
            try
            {
                ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _notExistsShadowRootElement, throwError: true);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException ex) { Assert.AreEqual(expectedErrorMessage, ex.Message); }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_IsShadowRootElementPresent_ChromeSettings_ImplicitWaitManipulationCheck_Exists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _existsShadowRootElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_IsShadowRootElementPresent_ChromeSettings_ImplicitWaitManipulationCheck_NotExists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _notExistsShadowRootElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
            try
            {
                ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _notExistsShadowRootElement, throwError: true);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException)
            {
                implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
                Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
            }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_IsNestedShadowRootElementPresent_ChromeSettings_NestedShadowRootExists()
        {
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            var exists = ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _tabRootElement);
            Assert.AreEqual(true, exists);
            exists = ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _clearBrowsingDataDialogRootElement);
            Assert.AreEqual(true, exists);
            exists = ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _tabRootElement);
            Assert.AreEqual(true, exists);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_IsNestedShadowRootElementPresent_ChromeSettings_NestedShadowRootNotExists()
        {
            var expectedErrorMessage = "IsNestedShadowRootElementPresent: Nested shadow root element for selector 'settings-main.main' in DOM hierarchy 'settings-ui > settings-main.main' Not Found.";
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            var exists = ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _notExistsNestedShadowRootElement);
            Assert.AreEqual(false, exists);
            try
            {
                ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _notExistsNestedShadowRootElement, throwError: true);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException ex) { Assert.AreEqual(expectedErrorMessage, ex.Message); }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_IsNestedShadowRootElementPresent_ChromeSettings_ImplicitWaitManipulationCheck_Exists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _tabRootElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_CHROME_SETTINGS)]
        public void Test_IsNestedShadowRootElementPresent_ChromeSettings_ImplicitWaitManipulationCheck_NotExists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _notExistsNestedShadowRootElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
            try
            {
                ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _notExistsNestedShadowRootElement, throwError: true);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException)
            {
                implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
                Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
            }
        }

        #endregion

        #region 'Shadow DOM HTML' Tests

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_GetShadowRootElement_ShadowDOMHTML_ShadowRootElementExists()
        {
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            var shadowHost = ShadowRootAssist.GetShadowRootElement(WebDriver, _shadowHostElement);
            Assert.IsNotNull(shadowHost);
            var shadowInput = shadowHost.FindElement(By.CssSelector(_shadowRootEnclosedInput));
            shadowInput.SendKeys("Input inside Shadow DOM");
            var shadowCheckbox = shadowHost.FindElement(By.CssSelector(_shadowRootEnclosedCheckbox));
            shadowCheckbox.Click();
            var shadowInputFile = shadowHost.FindElement(By.CssSelector(_shadowRootEnclosedInputChooseFile));
            Assert.IsNotNull(shadowInputFile);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_GetShadowRootElement_ShadowDOMHTML_ShadowRootElementDoesNotExists()
        {
            var expectedErrorMessage = "GetShadowRootElement: Shadow root element for selector '#non_host' Not Found.";
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            try
            {
                ShadowRootAssist.GetShadowRootElement(WebDriver, _notExistsShadowHostElement);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException ex) { Assert.AreEqual(expectedErrorMessage, ex.Message); }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_GetShadowRootElement_ShadowDOMHTML_ImplicitWaitManipulationCheck_Exists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            ShadowRootAssist.GetShadowRootElement(WebDriver, _shadowHostElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_GetShadowRootElement_ShadowDOMHTML_ImplicitWaitManipulationCheck_NotExists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            try
            {
                ShadowRootAssist.GetShadowRootElement(WebDriver, _notExistsShadowHostElement);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException)
            {
                var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
                Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
            }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_GetNestedShadowRootElement_ShadowDOMHTML_RootElementExists()
        {
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            var nestedShadowHost = ShadowRootAssist.GetNestedShadowRootElement(WebDriver, _nestedShadowHostElement);
            var nestedText = nestedShadowHost.FindElement(By.CssSelector(_nestedShadowHostShadowContent));
            Assert.AreEqual("nested text", nestedText.Text);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_GetNestedShadowRootElement_ShadowDOMHTML_RootElementDoesNotExists()
        {
            var expectedErrorMessage = "GetNestedShadowRootElement: Nested shadow root element for selector '#shadow_content' in DOM hierarchy '#shadow_host > #shadow_content' Not Found.";
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            try
            {
                ShadowRootAssist.GetNestedShadowRootElement(WebDriver, _notExistsNestedShadowHostElement);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException ex) { Assert.AreEqual(expectedErrorMessage, ex.Message); }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_GetNestedShadowRootElement_ShadowDOMHTML_ImplicitWaitManipulationCheck_Exists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            ShadowRootAssist.GetNestedShadowRootElement(WebDriver, _nestedShadowHostElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_GetNestedShadowRootElement_ShadowDOMHTML_ImplicitWaitManipulationCheck_NotExists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            try
            {
                ShadowRootAssist.GetNestedShadowRootElement(WebDriver, _notExistsNestedShadowHostElement);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException)
            {
                var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
                Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
            }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_IsShadowRootElementPresent_ShadowDOMHTML_ShadowRootExists()
        {
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            var exists = ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _shadowHostElement);
            Assert.AreEqual(true, exists);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_IsShadowRootElementPresent_ShadowDOMHTML_ShadowRootNotExists()
        {
            var expectedErrorMessage = "IsShadowRootElementPresent: Shadow root element for selector '#non_host' Not Found.";
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            var exists = ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _notExistsShadowHostElement);
            Assert.AreEqual(false, exists);
            try
            {
                ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _notExistsShadowHostElement, throwError: true);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException ex) { Assert.AreEqual(expectedErrorMessage, ex.Message); }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_IsShadowRootElementPresent_ShadowDOMHTML_ImplicitWaitManipulationCheck_Exists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _shadowHostElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_IsShadowRootElementPresent_ShadowDOMHTML_ImplicitWaitManipulationCheck_NotExists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _notExistsShadowHostElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
            try
            {
                ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _notExistsShadowHostElement, throwError: true);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException)
            {
                implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
                Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
            }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_IsNestedShadowRootElementPresent_ShadowDOMHTML_NestedShadowRootExists()
        {
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            var exists = ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _nestedShadowHostElement);
            Assert.AreEqual(true, exists);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_IsNestedShadowRootElementPresent_ShadowDOMHTML_NestedShadowRootNotExists()
        {
            var expectedErrorMessage = "IsNestedShadowRootElementPresent: Nested shadow root element for selector '#shadow_content' in DOM hierarchy '#shadow_host > #shadow_content' Not Found.";
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            var exists = ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _notExistsNestedShadowHostElement, timeInSeconds: 10);
            Assert.AreEqual(false, exists);
            try
            {
                ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _notExistsNestedShadowHostElement, timeInSeconds: 10, throwError: true);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException ex) { Assert.AreEqual(expectedErrorMessage, ex.Message); }
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_IsNestedShadowRootElementPresent_ShadowDOMHTML_ImplicitWaitManipulationCheck_Exists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _nestedShadowHostElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
        }

        [TestMethod]
        [TestCategory(TESTS_DOTNETCORE), TestCategory(DOTNETCORE_SHADOW_DOM_HTML)]
        public void Test_IsNestedShadowRootElementPresent_ShadowDOMHTML_ImplicitWaitManipulationCheck_NotExists()
        {
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, 0);
            WebDriver.Navigate().GoToUrl(GetTestFilePath());
            ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _notExistsNestedShadowHostElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
            try
            {
                ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _notExistsNestedShadowHostElement, throwError: true);
                Assert.Fail("No Exception Thrown.");
            }
            catch (AssertFailedException ex) { throw ex; }
            catch (WebDriverException)
            {
                implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
                Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
            }
        }

        #endregion

    }
}