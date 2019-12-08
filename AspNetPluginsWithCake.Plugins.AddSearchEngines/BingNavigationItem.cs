using AspNetPluginsWithCake.Plugins;
using System;

namespace AspNetPluginsWithCake.Plugins.AddSearchEngines
{
    public class BingNavigationItem : INavigationItem
    {
        public int Order => 101;

        public string Url => "https://www.bing.com";

        public string Title => "Bing";
    }
}
