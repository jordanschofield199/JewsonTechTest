using System;
using Jewson.Logging.Interfaces;
using log4net.Config;

namespace Jewson.Logging
{
    public static class LogManager
    {
        public static ILogger GetLogger<T>()
        {
            return new Logger(typeof(T));
        }

        public static ILogger GetLogger(Type t)
        {
            return new Logger(t);
        }

        public static void Setup()
        {
            XmlConfigurator.Configure();
        }
    }
}
