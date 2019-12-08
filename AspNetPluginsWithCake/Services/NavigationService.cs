using AspNetPluginsWithCake.Plugins;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;

namespace AspNetPluginsWithCake.Services
{
    public class NavigationService
    {
        private const string NavigationCacheKey = "navigation";
        private readonly IMemoryCache cache;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IServiceProvider serviceProvider;

        public NavigationService(IMemoryCache cache, IWebHostEnvironment webHostEnvironment, IServiceProvider serviceProvider)
        {
            this.cache = cache;
            this.webHostEnvironment = webHostEnvironment;
            this.serviceProvider = serviceProvider;
        }

        public List<INavigationItem> GetNavigationItems()
        {
            List<INavigationItem> navigationItems;
            if (!cache.TryGetValue(NavigationCacheKey, out navigationItems))
            {
                navigationItems = GetNavigationItemsFromPlugins();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(10));

                // Save data in cache.
                cache.Set(NavigationCacheKey, navigationItems, cacheEntryOptions);
            }

            return navigationItems;
        }

        public List<INavigationItem> GetNavigationItemsFromPlugins()
        {
            var navigationItems = new List<INavigationItem>();
            var pluginsPath = Path.Combine(webHostEnvironment.ContentRootPath, "Plugins");
            if (!Directory.Exists(pluginsPath))
            {
                //return empty list
                return navigationItems;
            }

            var dllFiles = Directory.GetFiles(pluginsPath, "*.dll");
            var assemblyLoadContext = AssemblyLoadContext.Default;
            var navigationInterfaceType = typeof(INavigationItem);
            foreach (var dllFile in dllFiles)
            {
                var pluginAssembly = assemblyLoadContext.LoadFromAssemblyPath(dllFile);
                var navigationItemTypes = pluginAssembly.GetTypes()
                    .Where(t => t != navigationInterfaceType && navigationInterfaceType.IsAssignableFrom(t))
                    .ToList();

                foreach (var navigationItemType in navigationItemTypes)
                {
                    var navigationItem = (INavigationItem)ActivatorUtilities.CreateInstance(serviceProvider, navigationItemType);
                    navigationItems.Add(navigationItem);
                }
            }

            navigationItems = navigationItems
                .OrderBy(n => n.Order)
                .ToList();

            return navigationItems;
        }
    }
}
