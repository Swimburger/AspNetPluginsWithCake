var target = Argument("target", "Default");

Task("RunWithPlugins")
	.Does(() =>
{

	var pluginFiles = GetFiles("**/AspNetPluginsWithCake.Plugins.*.csproj");
	foreach(var pluginFile in pluginFiles)
	{
		var settings = new DotNetCorePublishSettings
		{
			OutputDirectory = "./AspNetPluginsWithCake/Plugins/",
			Configuration = "Debug"
		};

		 DotNetCorePublish(pluginFile.FullPath, settings);
	}

	DotNetCoreRun("./AspNetPluginsWithCake/AspNetPluginsWithCake.csproj");
});

Task("Default")
	.Does(() =>
{
	DotNetCorePublish("./AspNetPluginsWithCake/AspNetPluginsWithCake.csproj", new DotNetCorePublishSettings
	{
		OutputDirectory = "./Publish/",
        Configuration = "Release"
	});

	var pluginFiles = GetFiles("**/AspNetPluginsWithCake.Plugins.*.csproj");
	foreach(var pluginFile in pluginFiles)
	{
		var settings = new DotNetCorePublishSettings
		{
			OutputDirectory = "./Publish/Plugins/",
			Configuration = "Release"
		};

		 DotNetCorePublish(pluginFile.FullPath, settings);
	}

});

RunTarget(target);