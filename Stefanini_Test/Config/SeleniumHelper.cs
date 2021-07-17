using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace Stefanini_Test.Config
{
    public class SeleniumHelper : IDisposable
    {
        public IWebDriver WebDriver;
        public readonly ConfigurationHelper Configuration;
        public WebDriverWait Wait;

        public SeleniumHelper(Browsers browser, ConfigurationHelper configuration, bool headless = true)
        {
            Configuration = configuration;
            WebDriver = WebDriverFactory.CreateWebDriver(browser, headless);
            WebDriver.Manage().Window.Maximize();
            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(Configuration.LoadTimeout));
        }

        public string ObterUrl() => WebDriver.Url;

        public void IrParaUrl() => WebDriver.Navigate().GoToUrl(Configuration.Website);
        public void IrParaUrl(string url) => WebDriver.Navigate().GoToUrl(url);


        public void ClicarLinkPorTexto(string linkText)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(linkText))).Click();
        }
        public void EnviarAnexo(By locator, string path)
        {
            if (ElementoExiste(locator))
            {
                IWebElement campo = WebDriver.FindElement(locator);
                campo.SendKeys(path);
            }
            else
            {
                Assert.True(false, "bug");
            }

        }
        public void PreencherTexto(By locator, string texto)
        {
            var campo = Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            campo.SendKeys(texto);
        }
        public void LimparTexto(By locator)
        {
            var campo = Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            campo.Clear();
            campo.SendKeys(" " + Keys.Backspace + Keys.Tab);

        }
        public void LimparEPreencherTexto(By locator, string texto)
        {
            var campo = Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            campo.Clear();
            campo.SendKeys(texto);
        }
        public void Clicar(By locator)
        {
            var button = Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            button.Click();
        }
        public IWebElement ObterElemento(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public void WaitForElement(By locator)
        {
            WebDriverWait Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
            var FoundElement = Wait.Until<IWebElement>(d => {
                try
                {

                    return d.FindElement(locator);
                }
                catch
                {
                    return null;
                }
            });
        }

        public void ElementoDisplayed(IWebElement element)
        {
            WebDriverWait Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
            var FoundElement = Wait.Until<bool>(d => {
                try
                {
                    return element.Displayed;
                }
                catch
                {
                    return false;
                }
            });

        }
        public void ScrollAteElemento(By locator)
        {
            IJavaScriptExecutor executor = ((IJavaScriptExecutor)WebDriver);
            IWebElement element = ObterElemento(locator);

            executor.ExecuteScript("window.scrollTo(0," + element.Location.Y + ")");
        }
        public void ModalVisivelClicar(By locator, By locatorclick)
        {
            if (ElementoExiste(locator))
            {
                IWebElement element = WebDriver.FindElement(locatorclick);
                element.Click();
            }
            else
            {
                return;
            }

        }

        public bool ElementoPresente(By locator)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return true;
                //return ObjectRepository.Driver.FindElements(locator).Count == 1;
            }
            catch (Exception)
            {
                return false;
            }

        }



        public bool ElementoDisable(By locator)
        {
            IWebElement element = Wait.Until(ExpectedConditions.ElementExists(locator));
            if (!element.Enabled)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void PreencherDropDown(By locator, string valorCampo)
        {
            var campo = Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            var selectElement = new SelectElement(campo);
            selectElement.SelectByText(valorCampo);
        }
        public IWebElement SelectElementOption(By locator)
        {
            IWebElement element = ObterElemento(locator);
            SelectElement select = new SelectElement(element);
            var options = select.SelectedOption;
            return options;
        }
        public void PreencherDropDown(By locator, int index)
        {
            var campo = Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            var selectElement = new SelectElement(campo);
            selectElement.SelectByIndex(index);
        }

        public void ValidarPreenchimentoTexto(By locator, string errado, string correto)
        {
            PreencherTexto(locator, errado);
            Assert.Empty(ObterValorTextBox(locator));
            PreencherTexto(locator, correto);
            Assert.NotEmpty(ObterValorTextBox(locator));
        }
        public void RandomSelect(By locator)
        {
            IWebElement element = ObterElemento(locator);
            Random random = new Random();
            int optionIndex = element.FindElements(By.TagName("option")).Count();
            SelectElement select = new SelectElement(element);
            select.SelectByIndex(random.Next(1, optionIndex));

        }
        public void RandomSelect(By locator, By locator2)
        {
            Random random = new Random();
            IWebElement element = ObterElemento(locator);
            IWebElement element2 = ObterElemento(locator2);
            int optionIndex = element.FindElements(By.TagName("option")).Count();
            SelectElement select = new SelectElement(element2);
            select.SelectByIndex(random.Next(element2.GetAttribute("value").Count(), optionIndex));
            while (false)
            {

            }
        }



        public string ObterValorTextBox(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(locator)).GetAttribute("value");
        }
        public string ObterValorTextoElemento(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(locator)).Text;
        }
        public string ObterValorTextoElementoPorXpath(string locator)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator))).Text;
        }
        public IEnumerable<IWebElement> ObterListaporClasse(By locator)
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        }
        public bool ValidarSeElementoExite(By locator)
        {
            return ElementoExiste(locator);
        }
        private bool ElementoExiste(By locator)
        {
            try
            {
                WebDriver.FindElement(locator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void InicioPagina()
        {
            IJavaScriptExecutor executor = ((IJavaScriptExecutor)WebDriver);
            executor.ExecuteScript("window.scrollTo(0, 0)");
        }
        public void FimPagina()
        {
            IJavaScriptExecutor executor = ((IJavaScriptExecutor)WebDriver);
            executor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        public bool RadioButtonSelecionado(By locator)
        {
            IWebElement elemento = ObterElemento(locator);
            string flag = elemento.GetAttribute("checked");
            if (flag == null)
            {
                return false;
            }
            else
            {
                return flag.Equals("true") || flag.Equals("checked");
            }
        }
        public MediaEntityModelProvider CaptureScreenshoot(string name)
        {
            Thread.Sleep(1000);
            var screenshoot = ((ITakesScreenshot)WebDriver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshoot, name).Build();
        }

        public void Dispose()
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }
    }
}

