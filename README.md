# shadowroot-digger
*DOM shadow root elements finder using Selenium solution in .NET*. </br></br>
![maintainer](https://img.shields.io/badge/Creator/Maintainer-abhinavminhas-e65c00)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

One of the important aspect of web components is encapsulation and [Shadow DOM API](https://developer.mozilla.org/en-US/docs/Web/Web_Components/Using_shadow_DOM) is a key part of this, allowing hidden DOM trees to be attached to elements in the regular DOM tree. This shadow DOM tree starts with a shadow root, underneath which any elements can be attached, in the same way as the normal DOM. The solution combines the power of [Document Query Selector API](https://developer.mozilla.org/en-US/docs/Web/API/Document/querySelector)  with [Selenium](https://www.selenium.dev/) to grab such shadow root DOM trees and interact with any elements encapsulated within it.

Selenium 4.0.x - Current supported package/solution version.  
Selenium 3 - Check support [here](https://github.com/abhinavminhas/shadowroot-digger-dotnet/tree/Selenium-3-v1.0.x).  

## Download
The package is available and can be downloaded using [nuget.org](https://www.nuget.org/) package manager.  
- Package Name - [ShadowRoot.Digger](https://www.nuget.org/packages/ShadowRoot.Digger/).

## Features
1. Returns shadow root or nested shadow root from DOM.
2. Checks if shadow root or nested shadow root is present or not in the DOM.  
   **NOTE:** *Supports Selenium 3 & 4.0.x versions (Check Selenium Dependency Before Use)*  
   &emsp;&emsp;&nbsp;&nbsp;&nbsp;&nbsp;For Selenium 3 - Use versions [1.x.x](https://www.nuget.org/packages/ShadowRoot.Digger/1.0.5).  

## .NET Supported Versions
The solution is built on .NetStandard 2.0  
<img src="https://user-images.githubusercontent.com/17473202/141665862-0e5e1c0e-e84f-42bf-befb-267e722e9d60.png" />  

## Usage Guidelines
1. Install the nuget package [ShadowRoot.Digger](https://www.nuget.org/packages/ShadowRoot.Digger/).  
2. Use below extension methods to get shadow root or nested shadow root.  
   Requried parameters - webdriver instance & shadow root host selector identifier/s.
    ```
    ShadowRootAssist.GetShadowRootElement()
    ShadowRootAssist.GetNestedShadowRootElement()
    ```
    The returned shadow root element from above extension methods can be used to find element/s encapsulated within it.  
    **NOTE:** *Use **[jQuery Selectors](https://www.w3schools.com/jquery/jquery_ref_selectors.asp)** or **[CSS Selectors](https://www.w3schools.com/cssref/css_selectors.asp)** for shadow root host identifications.*
3. Use below extension methods for checking if shadow root or nested shadow root exists or not.  
   Requried parameters - webdriver instance & shadow root host selector identifier/s.
    ```
    ShadowRootAssist.IsShadowRootElementPresent()
    ShadowRootAssist.IsNestedShadowRootElementPresent()
    ```
    **NOTE:** *Use **[jQuery Selectors](https://www.w3schools.com/jquery/jquery_ref_selectors.asp)** or **[CSS Selectors](https://www.w3schools.com/cssref/css_selectors.asp)** for shadow root host identifications.*

 Check the solution tests for more information.  
**NOTE:** *Google Chrome & shadow DOM in Chrome settings along with [ShadowDOM.html](/ShadowRootDigger.CORE.Tests/TestFiles/ShadowDOM.html) have been used for testing the solution.*

## Verified Versions

   | Google Chrome | Chrome Driver | Microsoft Edge | Edge Driver |
   | ----------- | ----------- | ----------- | ----------- |
   | 98.0.4758.102 | 98.0.4758.102 | 98.0.1108.56 | 98.0.1108.56 |
   | 97.0.4692.71 | 97.0.4692.71 | 97.0.1072.69 | 97.0.1072.69 |
   | 96.0.4664.110 | 96.0.4664.45 | 96.0.1054.62 | 96.0.1054.62 |
   | 95.0.4638.69 | 95.0.4638.54 | 95.0.1020.53 | 95.0.1020.53 |
   | 94.0.4606.81 | 94.0.4606.61 | 94.0.992.23 | 94.0.992.23 |
   | 93.0.4577.82 | 93.0.4577.63 | 93.0.961.27 | 93.0.961.27 |
   | 92.0.4515.131 | 92.0.4515.107 | 92.0.902.45 | 92.0.902.45 |
