using AspNetPluginsWithCake.Plugins;
using System;

namespace AspNetPluginsCake.Plugins.AddSearchEngines
{
    public class RickRollNavigationItem : INavigationItem
    {
        public int Order =>300;

        public string Url => "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

        public string Title => "Don't press this";
    }
}
