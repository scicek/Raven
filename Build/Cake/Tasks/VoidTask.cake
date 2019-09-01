Task("Void")
    .Description("Let's you know you didn't specify a build target.")
    .Does(() =>
    {
        Error($"You have to specify a build target!");
    });