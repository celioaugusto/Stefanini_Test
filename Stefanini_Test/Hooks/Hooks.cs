using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Stefanini_Test.Config;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using Stefanini_Test.Config;
using Xunit;

namespace Stefanini_Test.Hooks
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoWebFixtureCollection))]
    public class Hooks
    {
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;
        private readonly AutomacaoWebTestsFixture _testsFixture;
        private readonly LoginUsuario _loginUsuario;


        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static AventStack.ExtentReports.ExtentReports extent;

        public Hooks(FeatureContext featureContext, ScenarioContext scenarioContext, AutomacaoWebTestsFixture testsFixture)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
            _testsFixture = testsFixture;
        }

        [BeforeTestRun]
        public static void TestInitalize()
        {
            //Inicializa o report

            ConfigurationHelper configurationHelper = new ConfigurationHelper();
            string path = configurationHelper.GetAppPath();
            DateTime data = DateTime.Now;
            var htmlReporter = new ExtentHtmlReporter(path + $@"/Relatorio/Relatorio_{data.ToString("dd-MM-yy_HH-mm")}/ExtentReport.html");
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            htmlReporter.Config.ReportName = "Automação Teste Stefanini";
            extent = new AventStack.ExtentReports.ExtentReports();
            extent.AttachReporter(htmlReporter);

        }


        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();


            if (_scenarioContext.TestError == null)
            {
                var screenShot = _testsFixture.BrowserHelper.CaptureScreenshoot(_scenarioContext.StepContext.StepInfo.Text);
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Pass("", screenShot);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Pass("", screenShot);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Pass("", screenShot);
                else if (stepType == "And")
                    scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text).Pass("", screenShot);
            }
            else if (_scenarioContext.TestError != null)
            {
                var screenShot = _testsFixture.BrowserHelper.CaptureScreenshoot(_scenarioContext.ScenarioInfo.Title.Trim());
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, screenShot);

                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, screenShot);

                }
                else if (stepType == "Then")
                {
                   
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, screenShot);

                }
            }



        }


        [BeforeScenario]

        public void BeforeScenario(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _testsFixture.BrowserHelper.IrParaUrl();
            Assert.Contains(_testsFixture.Configuration.Website, _testsFixture.BrowserHelper.ObterUrl());
           
            //Crate nome da feature
            featureName = extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);

            //Get nome cenario
            scenario = featureName.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);



        }


        [AfterTestRun]
        public static void TearDownReport()
        {
            //Write the report to the report directory
            extent.Flush();
        }

        [AfterScenario]
        public void CleanUp()
        {
            _testsFixture.BrowserHelper.Dispose();
        }

    }
}

