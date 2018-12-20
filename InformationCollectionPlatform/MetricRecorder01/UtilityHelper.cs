using System;
using System.Reflection;
using System.Text;

namespace MetricRecorder01
{
    public static class UtilityHelper
    {
        public static string GetLibraryName()
        {
            StringBuilder debugAssembly = new StringBuilder();
            debugAssembly.AppendLine($"Assembly.GetCallingAssembly().FullName: {Assembly.GetCallingAssembly().FullName}");
            debugAssembly.AppendLine($"Assembly.GetEntryAssembly().FullName: {Assembly.GetEntryAssembly().FullName}");
            debugAssembly.AppendLine($"Assembly.GetExecutingAssembly().FullName: {Assembly.GetExecutingAssembly().FullName}");
            return debugAssembly.ToString();
        }
    }
}
