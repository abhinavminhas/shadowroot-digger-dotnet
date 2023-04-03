# Changelog
All notable changes to this project documented here.

## [Released]

## [3.1.8](https://www.nuget.org/packages/ShadowRoot.Digger/3.1.8) - 2023-04-04
### Changed
- Selenium dependency update from 4.6.0 to 4.7.0.

## [3.1.7](https://www.nuget.org/packages/ShadowRoot.Digger/3.1.7) - 2022-11-18
### Changed
- Selenium dependency update from 4.5.1 to 4.6.0.

## [3.1.6](https://www.nuget.org/packages/ShadowRoot.Digger/3.1.6) - 2022-11-14
### Changed
- Selenium dependency update from 4.5.0 to 4.5.1.

## [3.1.5](https://www.nuget.org/packages/ShadowRoot.Digger/3.1.5) - 2022-11-13
### Changed
- Selenium dependency update from 4.4.0 to 4.5.0.

## [3.1.4](https://www.nuget.org/packages/ShadowRoot.Digger/3.1.4) - 2022-08-31
### Changed
- Selenium dependency update from 4.3.0 to 4.4.0.

## [3.1.3](https://www.nuget.org/packages/ShadowRoot.Digger/3.1.3) - 2022-07-09
### Changed
- Selenium dependency update from 4.2.0 to 4.3.0.

## [3.1.2](https://www.nuget.org/packages/ShadowRoot.Digger/3.1.2) - 2022-07-06
### Changed
- Selenium dependency update from 4.1.1 to 4.2.0.

## [3.1.1](https://www.nuget.org/packages/ShadowRoot.Digger/3.1.1) - 2022-07-05
### Changed
- Selenium dependency update from 4.1.0 to 4.1.1.

## [3.1.0](https://www.nuget.org/packages/ShadowRoot.Digger/3.1.0) - 2022-06-28
### Added
- Addition of 'StaleElementReferenceException' handler.

## [3.0.0](https://www.nuget.org/packages/ShadowRoot.Digger/3.0.0) - 2022-02-02
### Changed
- Selenium dependency update from 4.0.1 to 4.1.0.
- Documentation updates.

### Notes
- This version supports Selenium 4.1.x versions due to the deprecation of Selenium method RemoteWebElement() used in Selenium 4 and change in the way shadow roots are searched in Selenium 4.
- This version supports change in shadow root return values for Selenium in Chromium browsers (Google Chrome and Microsoft Edge) v96 and is backward compatible.

## [2.0.1](https://www.nuget.org/packages/ShadowRoot.Digger/2.0.1) - 2022-01-30
### Changed
- Selenium dependency update from 4.0.0 to 4.0.1.

### Notes
- This version supports Selenium 4.0.x versions due to the deprecation/change of Selenium method RemoteWebElement() used in Selenium 4 and change in the way shadow roots are searched in Selenium 4.
- This version supports change in shadow root return values for Selenium in Chromium browsers (Google Chrome and Microsoft Edge) v96 and is backward compatible.

## [2.0.0](https://www.nuget.org/packages/ShadowRoot.Digger/2.0.0) - 2022-01-30
### Changed
- Selenium dependency update from 3.141.0 to 4.0.0.

### Notes
- This version supports Selenium 4.0.x versions due to the deprecation/change of Selenium method RemoteWebElement() used in Selenium 4 and change in the way shadow roots are searched in Selenium 4.
- This version supports change in shadow root return values for Selenium in Chromium browsers (Google Chrome and Microsoft Edge) v96 and is backward compatible.

## [1.0.5](https://www.nuget.org/packages/ShadowRoot.Digger/1.0.5) - 2022-01-28
### Changed
- Improvement - Removed implicit wait manipulation.
- Is (shadow root/nested shadow root) present, check logic update.

### Notes
- This version supports Selenium 3 due to the deprecation/change of Selenium method RemoteWebElement() used in Selenium 4 and change in the way shadow roots are searched in Selenium 4.
- This version supports change in shadow root return values for Selenium in Chromium browsers (Google Chrome and Microsoft Edge) v96 and is backward compatible.

## [1.0.4](https://www.nuget.org/packages/ShadowRoot.Digger/1.0.4) - 2022-01-25
### Changed
- Nuget package documentation update.

### Notes
- This version supports Selenium 3 due to the deprecation/change of Selenium method RemoteWebElement() used in Selenium 4 and change in the way shadow roots are searched in Selenium 4.
- This version supports change in shadow root return values for Selenium in Chromium browsers (Google Chrome and Microsoft Edge) v96 and is backward compatible.

## [1.0.3](https://www.nuget.org/packages/ShadowRoot.Digger/1.0.3) - 2022-01-18
### Changed
- Nuget package documentation and repo link update.

## [1.0.2](https://www.nuget.org/packages/ShadowRoot.Digger/1.0.2) - 2022-01-02
### Changed
- Improvement - Wait and check returned object from Selenium javascript executor function is of type web element for IsShadowRootElementPresent() & IsNestedShadowRootElementPresent() methods where older chromedriver versions return web element instead of dictionary object.

## [1.0.1](https://www.nuget.org/packages/ShadowRoot.Digger/1.0.1) - 2021-12-29
### Changed
- Fix for Selenium javascript executor function returning dictionary object instead of web element while keeping backward compatibility where older chromedriver versions still return web element. The issue has surfaced due to the fix of below issue in chromedriver by chromium.org.  

    ChromeDriver Release Notes:  
    https://chromedriver.storage.googleapis.com/96.0.4664.18/notes.txt  
    https://chromedriver.storage.googleapis.com/96.0.4664.35/notes.txt  
    https://chromedriver.storage.googleapis.com/96.0.4664.45/notes.txt  

    ChromeDriver Resolved Issue Id:  
    3445  

    ChromeDriver Resolved Issue Details:  
    Resolved issue 3445: Impossible to access elements in iframe inside a shadow root [Pri-3]

    https://bugs.chromium.org/p/chromedriver/issues/detail?id=3445&q=3445&can=2

## [1.0.0](https://www.nuget.org/packages/ShadowRoot.Digger/1.0.0) - 2021-11-14
### Added
- Document Query Selector API with Selenium to grab shadow root DOM trees and interact with any elements encapsulated within it.