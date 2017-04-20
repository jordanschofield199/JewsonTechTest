using System;
using System.Configuration;
using Jewson.RestService.Client.Interfaces;

namespace Jewson.RestService.Client
{
    public class AppSettingsConfiguration : IAppSettings
    {
        public string GetSetting(string name)
        {
            try
            {
                return ConfigurationManager.AppSettings[name];
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
