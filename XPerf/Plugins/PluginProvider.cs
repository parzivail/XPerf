using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XPerf.Plugins
{
    public static class PluginProvider
    {
        /// <summary>
        /// Load plugin types with a given required metadata attribute
        /// </summary>
        /// <typeparam name="TP">The plugin base type</typeparam>
        /// <typeparam name="TM">The metadata decoration type</typeparam>
        /// <param name="pluginDirectory">The directory in which to search for plugins</param>
        /// <returns>A list of instantiated plugin types and metadata tags</returns>
        public static List<PluginInstance<TP, TM>> LoadPlugins<TP, TM>(string pluginDirectory) where TM : Attribute
        {
            var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pluginDirectory);
            if (!Directory.Exists(directory))
                throw new FileNotFoundException(directory);

            var dlls = Directory.GetFiles(directory, "*.dll");
            var types = dlls.SelectMany(file => Assembly.LoadFrom(file).GetTypes(), (file, type) => type);
            var pluginTypes = types.Where(t => typeof(TP).IsAssignableFrom(t) && t != typeof(TP)); // castable to but isn't the base class
            var decoratedTypes = pluginTypes.Select(type => new PluginInstance<TP, TM>((TP)Activator.CreateInstance(type), type.GetCustomAttribute<TM>()));
            return decoratedTypes.Where(instance => instance.Metadata != null).ToList();
        }
    }
}
