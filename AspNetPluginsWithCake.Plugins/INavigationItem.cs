using System;

namespace AspNetPluginsWithCake.Plugins
{
    public interface INavigationItem
    {
        int Order { get; }
        string Url { get; }
        string Title { get; }
    }
}
