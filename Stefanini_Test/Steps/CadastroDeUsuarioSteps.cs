using Bogus;
using OpenQA.Selenium;
using Stefanini_Test.Config;
using Stefanini_Test.PageObjects;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace Stefanini_Test.Steps
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoWebFixtureCollection))]
    public class CadastroDeUsuarioSteps
    {

        private readonly AutomacaoWebTestsFixture testsFixture;
        private readonly Pgo_CadastroUsuario cadastroUsuario;
        IList<string> vs = new List<string>();

  


        public CadastroDeUsuarioSteps(AutomacaoWebTestsFixture testsFixture)
        {
            this.testsFixture = testsFixture;
            cadastroUsuario = new Pgo_CadastroUsuario(testsFixture.BrowserHelper);
            

        }

        [Given(@"Que eu estou na tela de cadastro de usuario")]
        public void DadoQueEuEstouNaTelaDeCadastroDeUsuario()
        {
            Assert.Contains(testsFixture.Configuration.Website, testsFixture.BrowserHelper.ObterUrl());
        }
        
        [Then(@"Eu valido todos os componentes na tela\.")]
        public void EntaoEuValidoTodosOsComponentesNaTela_()
        {
            //Arrange
            IWebElement nome = testsFixture.BrowserHelper.ObterElemento(By.CssSelector("#name"));
            IWebElement email = testsFixture.BrowserHelper.ObterElemento(By.CssSelector("#email"));
            IWebElement senha = testsFixture.BrowserHelper.ObterElemento(By.CssSelector("#password"));
            IWebElement cadastrar = testsFixture.BrowserHelper.ObterElemento(By.CssSelector("#register"));
            
            
            //Asserts
            Assert.Equal("", nome.GetAttribute("value"));
            Assert.Equal("", email.GetAttribute("value"));
            Assert.Equal("", senha.GetAttribute("value"));

            
            Assert.True(cadastrar.TagName.Equals("button"));

            Assert.True(!testsFixture.BrowserHelper.ValidarSeElementoExite(By.CssSelector("div[class='register-form expanded'] table")));


        }

        [When(@"Eu clicar no botão Cadastrar sem informar nenhum registro")]
        public void QuandoEuClicarNoBotaoCadastrarSemInformarNenhumRegistro()
        {
            cadastroUsuario.ClicarCadastrar();
        }

        [Then(@"Eu valido os alertas de cada campo")]
        public void EntaoEuValidoOsAlertasDeCadaCampo()
        {
           
            Assert.True(cadastroUsuario.AlertaNome().Text.Equals("O campo Nome é obrigatório.") && cadastroUsuario.AlertaNome().GetCssValue("color").Equals("rgba(255, 0, 0, 1)"));

            Assert.True(cadastroUsuario.AlertaEmail().Text.Equals("O campo E-mail é obrigatório.") && cadastroUsuario.AlertaNome().GetCssValue("color").Equals("rgba(255, 0, 0, 1)"));

            Assert.True(cadastroUsuario.AlertaSenha().Text.Equals("O campo Senha é obrigatório.") && cadastroUsuario.AlertaNome().GetCssValue("color").Equals("rgba(255, 0, 0, 1)"));
        }

        [When(@"Eu informar o nome incompleto '(.*)' sem sobrenome")]
        public void QuandoEuInformarONomeIncompletoSemSobrenome(string nome)
        {
            var dadosusuario = new Faker<CadastroUsuario>("pt_BR")
                            .RuleFor(c => c.Senha, f => f.Person.Random.AlphaNumeric(10))
                            .RuleFor(c => c.Email, f => f.Person.Email)
                            .Generate();

            cadastroUsuario.PreencherNome(nome);
            cadastroUsuario.PreencherEmail(dadosusuario.Email);
            cadastroUsuario.PreencherPassword(dadosusuario.Senha);
            cadastroUsuario.ClicarCadastrar();
        }

        [Then(@"Eu devo receber o alerta com a mensagem '(.*)'")]
        public void EntaoEuDevoReceberOAlertaComAMensagem(string mensagem)
        {
            //Arrange
            IWebElement element = testsFixture.BrowserHelper.ObterElemento(By.XPath($@"//p[normalize-space()='{mensagem}']"));
            var tt = element.GetCssValue("color");
            //Assert
            Assert.True(element.Text.Equals(mensagem) && element.GetCssValue("color").Equals("rgba(255, 0, 0, 1)"));

        }

        [When(@"Eu informar um email inválido '(.*)'")]
        public void QuandoEuInformarUmEmailInvalido(string email)
        {
            var dadosusuario = new Faker<CadastroUsuario>("pt_BR")
                            .RuleFor(c => c.Nome, f => f.Name.FullName())
                            .RuleFor(c => c.Senha, f => f.Person.Random.AlphaNumeric(10))          
                            .Generate();

            cadastroUsuario.PreencherNome(dadosusuario.Nome);
            cadastroUsuario.PreencherEmail(email);
            cadastroUsuario.PreencherPassword(dadosusuario.Senha);
            cadastroUsuario.ClicarCadastrar();

            
        }


        [When(@"Eu informar uma senha com menos caracteres '(.*)'")]
        public void QuandoEuInformarUmaSenhaComMenosCaracteres(string pwd)
        {
            var dadosusuario = new Faker<CadastroUsuario>("pt_BR")
                            .RuleFor(c => c.Nome, f => f.Name.FullName())
                            .RuleFor(c => c.Email, f => f.Person.Email)
                            .Generate();

            cadastroUsuario.PreencherNome(dadosusuario.Nome);
            cadastroUsuario.PreencherEmail(dadosusuario.Email);
            cadastroUsuario.PreencherPassword(pwd);
            cadastroUsuario.ClicarCadastrar();
        }


        [When(@"Eu preencher o campo nome (.*)")]
        public void QuandoEuPreencherOCampoNome(string nome)
        {
            cadastroUsuario.PreencherNome(nome);
        }

        [When(@"Eu preencher o campo email (.*)")]
        public void QuandoEuPreencherOCampoEmail(string email)
        {
            cadastroUsuario.PreencherEmail(email);
        }

        [When(@"Eu preencher a senha (.*)")]
        public void QuandoEuPreencherASenha(string pwd)
        {
            cadastroUsuario.PreencherPassword(pwd);
            
        }

        [Then(@"Eu clico em cadastrar")]
        public void EntaoEuClicoEmCadastrar()
        {
            cadastroUsuario.ClicarCadastrar();

            Assert.True(testsFixture.BrowserHelper.ValidarSeElementoExite(By.CssSelector("div[class='register-form expanded'] table")));
        }

        [When(@"Eu cadastrar os usuarios")]
        [When(@"Eu cadastrar os usuarios:")]
        public void QuandoEuCadastrarOsUsuarios(Table table)
        {

            foreach (var row in table.Rows)
            {
                cadastroUsuario.PreencherNome(row["nome"]);
                cadastroUsuario.PreencherEmail(row["email"]);
                cadastroUsuario.PreencherPassword(row["senha"]);
                cadastroUsuario.ClicarCadastrar();              
                
            }

           
        }

        [Then(@"Eu quero excluir o usuario")]
        public void EntaoEuQueroExcluirOUsuario(Table table)
        {
            foreach (var row in table.Rows)
            {
                cadastroUsuario.ExcluirUsuario(row["nome"]);
            }
        }

    }
}
