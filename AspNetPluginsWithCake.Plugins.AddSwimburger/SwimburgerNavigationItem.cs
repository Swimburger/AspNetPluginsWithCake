using AspNetPluginsWithCake.Plugins;
using System;

namespace AspNetPluginsWithCake.Plugins.AddSearchEngines
{
    public class SwimburgerNavigationItem : INavigationItem
    {
        public int Order => 0;

        public string Url => "https://swimburger.net";

        public string Title => "Swimburger";
    }
}
