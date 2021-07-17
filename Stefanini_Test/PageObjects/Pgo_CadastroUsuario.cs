using OpenQA.Selenium;
using Stefanini_Test.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stefanini_Test.PageObjects
{
    public class Pgo_CadastroUsuario : PageObjectModel
    {
        public Pgo_CadastroUsuario(SeleniumHelper helper) : base(helper) { }

        //Campos

        /// <summary>
        /// Campo Nome
        /// </summary>
        private By cpNome = By.CssSelector("#name");

        /// <summary>
        /// Campo Email
        /// </summary>
        private By cpEmail = By.CssSelector("#email");

        /// <summary>
        /// Campo Senha
        /// </summary>
        private By cpSenha = By.CssSelector("#password");

        //Botoes

        /// <summary>
        /// Botão Cadastrar
        /// </summary>
        private By btnCadastrar = By.CssSelector("#register");

        //alertas

        /// <summary>
        /// notiicação nome
        /// </summary>
        private By alNome = By.XPath("//p[normalize-space()='O campo Nome é obrigatório.']");

        /// <summary>
        /// notiicação nome
        /// </summary>
        private By alEmail = By.XPath("//p[normalize-space()='O campo E-mail é obrigatório.']");

        /// <summary>
        /// notiicação nome
        /// </summary>
        private By alSenha = By.XPath("//p[normalize-space()='O campo Senha é obrigatório.']");


        public void PreencherCadastro(CadastroUsuario dadosUsuario)
        {
            Helper.PreencherTexto(cpNome, dadosUsuario.Nome);
            Helper.PreencherTexto(cpEmail, dadosUsuario.Email);
            Helper.PreencherTexto(cpSenha, dadosUsuario.Senha);
        }

        public void PreencherNome(string nome)
        {
            Helper.LimparTexto(cpNome);
            Helper.PreencherTexto(cpNome, nome);

        }

        public void PreencherEmail(string email)
        {
            Helper.LimparTexto(cpEmail);
            Helper.PreencherTexto(cpEmail, email);

        }

        public void PreencherPassword(string pwd)
        {
            Helper.LimparTexto(cpSenha);
            Helper.PreencherTexto(cpSenha, pwd);

        }
        public void ClicarCadastrar()
        {
            Helper.Clicar(btnCadastrar);
            Helper.FimPagina();
        }

        public IWebElement AlertaNome()
        {
            IWebElement element = Helper.ObterElemento(alNome);
            return element;
        }
        public IWebElement AlertaEmail()
        {
            IWebElement element = Helper.ObterElemento(alEmail);
            return element;
        }

        public IWebElement AlertaSenha()
        {
            IWebElement element = Helper.ObterElemento(alSenha);
            return element;
        }

       

        public void ExcluirUsuario(string nome)
        {
            IWebElement element = Helper.ObterElemento(By.CssSelector("div[class='register-form expanded'] table"));
            IList<IWebElement> results = element.FindElements(By.TagName("tr"));
            foreach (var item in results)
            {
                var rs = item.FindElements(By.TagName("td"));
                if (nome == rs[1].Text)
                {
                    rs[3].Click();

                }                
            }
        }

      

    }

}