using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Stefanini_Test.Config
{
    [CollectionDefinition(nameof(AutomacaoWebFixtureCollection))]

    public class AutomacaoWebFixtureCollection : ICollectionFixture<AutomacaoWebFixtureCollection> { }
    public class AutomacaoWebTestsFixture
    {
        public SeleniumHelper BrowserHelper;
        public readonly ConfigurationHelper Configuration;


        public AutomacaoWebTestsFixture()
        {
            Configuration = new ConfigurationHelper();
            BrowserHelper = new SeleniumHelper(Configuration.browser, Configuration, false);
        }

    }
}
