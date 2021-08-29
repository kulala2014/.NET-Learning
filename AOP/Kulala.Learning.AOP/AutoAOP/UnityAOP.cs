using Kulala.Learning.AOP.IContract;
using Kulala.Learning.AOP.Model;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Kulala.Learning.AOP.AutoAOP
{
    public class UnityAOP
    {
        public static void Show()
        {
            User user = new User
            {
                Name = "Kulala",
                Id = 9527,
                Password = "Qq123!@#"
            };

            IUnityContainer container = new UnityContainer();
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CfgFiles\\Unity.Config");
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

            UnityConfigurationSection configSection = (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);
            configSection.Configure(container, "aopContainer");

            IUserProcess processor = container.Resolve<IUserProcess>();
            processor.Register(user);
        }
    }
}
