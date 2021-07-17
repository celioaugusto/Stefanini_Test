using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Stefanini_Test.Config
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _config;

        public ConfigurationHelper()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public Browsers browser => (Browsers)Enum.Parse(typeof(Browsers), $"{_config.GetSection("Browser").Value}");       
        public string Website => $"{_config.GetSection("Website").Value}";       
        public int LoadTimeout => Convert.ToInt32($"{_config.GetSection("PageLoadTimeout").Value}");        
       
      

        public string GetAppPath()
        {
            try
            {
                string getDir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/");
                string projectDir;
                string debugDir;
                string binDir;
                string appDir;

                var targetFrameWork = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Runtime.Versioning.TargetFrameworkAttribute), false);
                var frameWork = ((System.Runtime.Versioning.TargetFrameworkAttribute)(targetFrameWork[0])).FrameworkName;
                string classFrameWork = frameWork.Substring(0, frameWork.IndexOf(","));

                if (classFrameWork == ".NETCoreApp")
                {
                    projectDir = getDir.Substring(0, getDir.LastIndexOf("/"));
                    debugDir = projectDir.Substring(0, projectDir.LastIndexOf("/"));
                    binDir = debugDir.Substring(0, debugDir.LastIndexOf("/"));
                    appDir = binDir.Substring(0, binDir.LastIndexOf("/"));
                }
                else
                {
                    debugDir = getDir.Substring(0, getDir.LastIndexOf("/"));
                    binDir = debugDir.Substring(0, debugDir.LastIndexOf("/"));
                    appDir = binDir.Substring(0, binDir.LastIndexOf("/"));
                }
                return appDir;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception at GetAppPath", ex);
            }
        }
    }



}