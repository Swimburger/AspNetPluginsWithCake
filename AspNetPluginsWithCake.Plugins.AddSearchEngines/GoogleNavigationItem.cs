using AspNetPluginsWithCake.Plugins;
using System;

namespace AspNetPluginsWithCake.Plugins.AddSearchEngines
{
    public class GoogleNavigationItem : INavigationItem
    {
        public int Order => 100;

        public string Url => "https://www.google.com";

        public string Title => "Google";
    }
}
