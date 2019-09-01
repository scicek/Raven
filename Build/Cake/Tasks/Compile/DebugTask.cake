Task("Debug")
    .Description("Builds the Debug configuration.")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        if (!IsRunningOnWindows())
        {
            throw new Exception("The build can only run on a Windows machine!");
        }

        Verbose($"MSBuild: building: {Solution}, config: {BuildConfiguration}");

        MSBuild(Solution, settings => settings.SetConfiguration(BuildConfiguration.ToString())
                                              .WithRestore()
                                              .WithTarget("Build")
                                              .WithProperty("Platform", Platform)
                                              .AddFileLogger(new MSBuildFileLogger { MSBuildFileLoggerOutput = MSBuildFileLoggerOutput.All, LogFile = LogFile })
                                              .WithProperty("CodeAnalysisGenerateSuccessFile", "false")
                                              .WithProperty("CodeAnalysisRuleSet", CodeAnalysisRuleSetFile.FullPath)
                                              .SetMaxCpuCount(0));

        BuiltConfiguration = Configuration.Debug;
    });