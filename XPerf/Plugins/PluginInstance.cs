namespace XPerf.Plugins
{
    public class PluginInstance<TP, TA>
    {
        public readonly TP Plugin;
        public readonly TA Metadata;

        public PluginInstance(TP plugin, TA metadata)
        {
            Plugin = plugin;
            Metadata = metadata;
        }
    }
}