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
        DotNetCoreBuild("./MakingDotNETApplicationsFaster.sln");
    });

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);