namespace AppVersioning
{
    public interface IAppVersionProvider
    {
        AppVersionData AppVersion { get; }
    }
}