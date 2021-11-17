using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ShadowRoot.Digger;
using System.Linq;

namespace ShadowRootDigger.NET.Tests
{
    [TestClass]
    public class ShadowRootDiggerTests : TestBase
    {
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

        [TestMethod]
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_GetShadowRootElement_ShadowRootElementExists()
        {
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            var clearBrowsingTab = ShadowRootAssist.GetShadowRootElement(WebDriver, _existsShadowRootElement);
            Assert.IsNotNull(clearBrowsingTab);
        }

        [TestMethod]
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_GetShadowRootElement_ShadowRootElementDoesNotExists()
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
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_GetShadowRootElement_ImplicitWaitManipulationCheck_Exists()
        {
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            ShadowRootAssist.GetShadowRootElement(WebDriver, _existsShadowRootElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
        }

        [TestMethod]
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_GetShadowRootElement_ImplicitWaitManipulationCheck_NotExists()
        {
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
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
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_GetNestedShadowRootElement_ClearChromeData()
        {
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            var clearBrowsingTab = ShadowRootAssist.GetNestedShadowRootElement(WebDriver, _tabRootElement);
            clearBrowsingTab.FindElements(By.CssSelector(_divTabIdentifier))
                .FirstOrDefault(item => item.Text == "Basic").Click();
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
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_GetNestedShadowRootElement_RootElementDoesNotExists()
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
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_GetNestedShadowRootElement_ImplicitWaitManipulationCheck_Exists()
        {
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            ShadowRootAssist.GetNestedShadowRootElement(WebDriver, _tabRootElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
        }

        [TestMethod]
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_GetNestedShadowRootElement_ImplicitWaitManipulationCheck_NotExists()
        {
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
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
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_IsShadowRootElementPresent_ShadowRootExists()
        {
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            var exists = ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _existsShadowRootElement);
            Assert.AreEqual(true, exists);
        }

        [TestMethod]
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_IsShadowRootElementPresent_ShadowRootNotExists()
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
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_IsShadowRootElementPresent_ImplicitWaitManipulationCheck_Exists()
        {
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            ShadowRootAssist.IsShadowRootElementPresent(WebDriver, _existsShadowRootElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
        }

        [TestMethod]
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_IsShadowRootElementPresent_ImplicitWaitManipulationCheck_NotExists()
        {
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
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
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_IsNestedShadowRootElementPresent_NestedShadowRootExists()
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
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_IsNestedShadowRootElementPresent_NestedShadowRootNotExists()
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
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_IsNestedShadowRootElementPresent_ImplicitWaitManipulationCheck_Exists()
        {
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            WebDriver.Navigate().GoToUrl("chrome://settings/clearBrowserData");
            ShadowRootAssist.IsNestedShadowRootElementPresent(WebDriver, _tabRootElement);
            var implicitWaitAfter = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
            Assert.AreEqual(implicitWaitBefore, implicitWaitAfter);
        }

        [TestMethod]
        [TestCategory("TESTS-DOTNETFRAMEWORK")]
        public void Test_IsNestedShadowRootElementPresent_ImplicitWaitManipulationCheck_NotExists()
        {
            var implicitWaitBefore = WebDriver.Manage().Timeouts().ImplicitWait.Ticks;
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
    }
}