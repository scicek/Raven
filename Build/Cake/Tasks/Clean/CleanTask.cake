Task("Clean")
    .Description("Cleans the project.")
    .WithCriteria(!BuildSystem.IsRunningOnAzurePipelinesHosted)
    .Does(() =>
    {
        var stage = new DirectoryInfo(StageDirectory.ToString());

        foreach (var file in stage.GetFiles())
        {
            file.Delete(); 
        }

        foreach (var directory in stage.GetDirectories())
        {
            directory.Delete(true); 
        }

        Verbose($"MSBuild: cleaning: {Solution}, config: {BuildConfiguration}");
        MSBuild(Solution, settings => settings.SetConfiguration(BuildConfiguration.ToString()).WithTarget("Clean").SetMaxCpuCount(0));
    });