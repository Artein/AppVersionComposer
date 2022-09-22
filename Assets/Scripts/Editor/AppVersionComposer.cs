// Mostly inspired on the blog post "Version Numbering for Games in Unity and Git" by Edward Rowe 
// https://blog.redbluegames.com/version-numbering-for-games-in-unity-and-git-1d05fca83022

using Debug = UnityEngine.Debug;

namespace AppVersioning.Editor
{
    public static class AppVersionComposer
    {
        /// <summary>
        /// Retrieves the build version from git based on the most recent matching tag and commit history.
        /// This returns the version as: {major.minor.build-commitHash} where 'build' represents the n-th commit after the tagged commit.
        /// </summary>
        public static string BuildVersion
        {
            get
            {
                string version = null;
                
                try
                {
                    version = GitCommandExecutor.Execute(@"describe --always --tags --long --match ""v[0-9]*""");
                    if (string.IsNullOrEmpty(version) || !version.Contains('-'))
                    {
                        throw new AppVersionException("Your repository doesn't have tags or doesn't fit into 'v[0-9]*' regex");
                    }
                    
                    version = version.Replace('-', '.');
                    var revision = version.Substring(1, version.LastIndexOf('.') - 1);
                    var commitHash = version[(version.LastIndexOf('.') + 2)..]; // startIndex+2 - to remove 'g' (git) from returned commitHash
                    version = revision + '-' + commitHash;

                    if (Debug.isDebugBuild)
                    {
                        version += '-' + Branch;
                    }
                }
                catch (GitException ex)
                {
                    if (ex.Message == "fatal: No names found, cannot describe anything.\n")
                    {
                        throw new AppVersionException("Your repository doesn't have tags or doesn't fit into 'v[0-9]*' regex");
                    }
                    
                    throw;
                }
                
                return version;
            }
        }

        /// <summary>
        /// The currently active branch.
        /// </summary>
        public static string Branch => GitCommandExecutor.Execute(@"rev-parse --abbrev-ref HEAD");

        /// <summary>
        /// Returns a listing of all uncommitted or untracked (added) files.
        /// </summary>
        public static string Status => GitCommandExecutor.Execute(@"status --porcelain");
    }
}