namespace Application.ApiVersioning;

public class ApiVersioningOptions
{
    public ApiVersioningOptions()
    {
        DefaultVersion = new ApiVersioningDefaultVersion(1, 1);
    }

    public bool AssumeDefaultVersionWhenUnspecified { get; set; } = true;
    public ApiVersioningDefaultVersion DefaultVersion { get; set; }
    public bool ReportApiVersions { get; set; } = true;
    public string[] VersionHeaderReader { get; set; } = { };
}

public class ApiVersioningDefaultVersion
{
    public ApiVersioningDefaultVersion()
    {
    }

    public ApiVersioningDefaultVersion(int majorVersion, int minorVersion)
    {
        MajorVersion = majorVersion;
        MinorVersion = minorVersion;
    }
    public int MajorVersion { get; set; }
    public int MinorVersion { get; set; }
}