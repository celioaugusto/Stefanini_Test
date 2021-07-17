using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stefanini_Test.Config
{
    public abstract class PageObjectModel
    {
        protected readonly SeleniumHelper Helper;

        public PageObjectModel(SeleniumHelper helper)
        {
            Helper = helper;
        }

        public string ObterUrl() { return Helper.ObterUrl(); }

        public void Clicar(By locator) { Helper.Clicar(locator); }

        public void LimparTexto(By locator) { Helper.LimparTexto(locator); }




    }
}
