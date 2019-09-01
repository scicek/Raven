public class VersionNumber
{
	public VersionNumber(string prefix, uint major, uint minor, uint patch, uint build, string suffix)
	{
		Prefix = prefix;
		Major = major;
		Minor = minor;
		Patch = patch;
		Build = build;
		Suffix = suffix;	
	}
	
	public string Prefix { get; set; }

	public uint Major { get; set; }

	public uint Minor { get; set; }

	public uint Patch { get; set; }

	public uint Build { get; set; }

	public string Suffix { get; set; }

	public string Short 
	{ 
		get => $"{Major}.{Minor}.{Patch}.{Build}";
	}

	public string SemVer 
	{ 
		get => $"{Major}.{Minor}.{Patch}{(!string.IsNullOrWhiteSpace(Suffix) ? $"-{Suffix}" : "")}";
	}

	public string Full 
	{ 
		get => $"{Prefix}{Major}.{Minor}.{Patch}.{Build}{(!string.IsNullOrWhiteSpace(Suffix) ? $"-{Suffix}" : "")}";
	}

	public override string ToString()
	{
		return Full;
	}
}