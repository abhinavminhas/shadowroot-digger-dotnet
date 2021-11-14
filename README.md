# shadowroot-digger
*DOM shadow root elements finder using Selenium solution in .NET*. </br></br>
![shadowroot-digger (Build)](https://github.com/abhinavminhas/shadowroot-digger/actions/workflows/build.yml/badge.svg)
[![codecov](https://codecov.io/gh/abhinavminhas/shadowroot-digger/branch/main/graph/badge.svg?token=8LXZL9ZLZR)](https://codecov.io/gh/abhinavminhas/shadowroot-digger)
![maintainer](https://img.shields.io/badge/Creator/Maintainer-abhinavminhas-e65c00)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

One of the important aspect of web components is encapsulation and [Shadow DOM API](https://developer.mozilla.org/en-US/docs/Web/Web_Components/Using_shadow_DOM) is a key part of this, allowing hidden DOM trees to be attached to elements in the regular DOM tree. This shadow DOM tree starts with a shadow root, underneath which any elements can be attached, in the same way as the normal DOM. The solution combines the power of [Document Query Selector API](https://developer.mozilla.org/en-US/docs/Web/API/Document/querySelector)  with [Selenium](https://www.selenium.dev/) to grab such shadow root DOM trees and interact with any elements encapsulated within it.

## Download
The package is available and can be downloaded using [nuget.org](https://www.nuget.org/) package manager.  
- Package Name - [ShadowRoot.Digger](https://www.nuget.org/packages/ShadowRoot.Digger/).

## Features
1. Returns shadow root or nested shadow root from DOM.
2. Checks if shadow root or nested shadow root is present or not in the DOM.

## .NET Supported Versions
The solution is built on .NetStandard 2.0  
<img src="https://user-images.githubusercontent.com/17473202/141665862-0e5e1c0e-e84f-42bf-befb-267e722e9d60.png" />  

## Usage Guidelines
1. Install the nuget package [ShadowRoot.Digger](https://www.nuget.org/packages/ShadowRoot.Digger/).
2. Use below extension methods to get shadow root or nested shadow root  
    ```
    ShadowRootAssist.GetShadowRootElement()
    ShadowRootAssist.GetNestedShadowRootElement()
    ```
    The returned shadow root element can be used to find element/s encapsulated within it. Check the solution tests for more information.  
    **NOTE:** *Use **[jQuery Selectors](https://www.w3schools.com/jquery/jquery_ref_selectors.asp)** or **[CSS Selectors](https://www.w3schools.com/cssref/css_selectors.asp)** for shadow root identifications.*
3. Use below extension methods for checking if shadow root or nested shadow root exists or not  
    ```
    ShadowRootAssist.IsShadowRootElementPresent()
    ShadowRootAssist.IsNestedShadowRootElementPresent()
    ```
    **NOTE:** *Use **[jQuery Selectors](https://www.w3schools.com/jquery/jquery_ref_selectors.asp)** or **[CSS Selectors](https://www.w3schools.com/cssref/css_selectors.asp)** for shadow root identifications.*

**NOTE:** *Google Chrome & shadow DOM in Chrome settings have been used for testing the solution.*