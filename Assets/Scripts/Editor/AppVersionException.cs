using System;

namespace AppVersioning.Editor
{
    public class AppVersionException : InvalidOperationException
    {
        public AppVersionException(string message) : base(message) { }
    }
}