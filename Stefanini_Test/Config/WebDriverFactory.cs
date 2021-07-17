using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stefanini_Test.Config
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(Browsers browser, bool headless)
        {
            IWebDriver webdriver = null;
            switch (browser)
            {
                case Browsers.Firefox:
                    FirefoxOptions optionsFirefox = new FirefoxOptions();
                    optionsFirefox.AddArguments("start-maximized");
                    if (headless)
                    {
                        optionsFirefox.AddArgument("--headless");
                    }
                    webdriver = new FirefoxDriver(optionsFirefox);
                    break;

                case Browsers.Chrome:
                    ChromeOptions optionsChrome = new ChromeOptions();
                    optionsChrome.AddArguments("start-maximized");
                    if (headless)
                    {
                        optionsChrome.AddArgument("--headless");
                    }
                    webdriver = new ChromeDriver(optionsChrome);
                    break;

            }

            return webdriver;
        }
    }
}
