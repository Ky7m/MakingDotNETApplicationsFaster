var target = Argument("target", "Default");

Task("Restore")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

 Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        var projects = GetFiles("./**/*.xproj");
        foreach(var project in projects)
        {
            DotNetCoreBuild(project.GetDirectory().FullPath);
        }
    });

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);