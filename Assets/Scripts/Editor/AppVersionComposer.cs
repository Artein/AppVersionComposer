// Mostly inspired on the blog post "Version Numbering for Games in Unity and Git" by Edward Rowe 
// https://blog.redbluegames.com/version-numbering-for-games-in-unity-and-git-1d05fca83022

namespace AppVersioning.Editor
{
    public static class AppVersionComposer
    {
        /// <summary>
        /// Retrieves the build version from git based on the most recent matching tag and commit history.
        /// This returns the version as: {major.minor.patch-commitHash} where 'patch' represents the n-th commit after the tagged commit.
        /// </summary>
        public static AppVersionData Version
        {
            get
            {
                int major, minor, patch;
                string commitHash;

                try
                {
                    var gitOutput = GitCommandExecutor.Execute(@"describe --always --tags --long --match ""v[0-9]*""");
                    if (string.IsNullOrEmpty(gitOutput) || !gitOutput.Contains('-'))
                    {
                        throw new AppVersionException("Your repository doesn't have tags or doesn't fit into 'v[0-9]*' regex");
                    }

                    gitOutput = gitOutput[1..]; // skip leading 'v'
                    
                    var tokens = gitOutput.Split('.', '-');
                    major = int.Parse(tokens[0]);
                    minor = int.Parse(tokens[1]);
                    patch = int.Parse(tokens[2]);
                    
                    commitHash = tokens[3][1..]; // skip leading 'g' (it is often gCOMMIT_HASH, where 'g' is for 'git')
                }
                catch (GitException ex)
                {
                    if (ex.Message == "fatal: No names found, cannot describe anything.\n")
                    {
                        throw new AppVersionException("Your repository doesn't have tags or doesn't fit into 'v[0-9]*' regex");
                    }
                    
                    throw;
                }
                
                return new AppVersionData(major, minor, patch, commitHash, Branch);
            }
        }

        /// <summary>
        /// The currently active branch.
        /// </summary>
        public static string Branch => GitCommandExecutor.Execute(@"rev-parse --abbrev-ref HEAD");

        // TODO: Do we need this?
        /// <summary>
        /// Returns a listing of all uncommitted or untracked (added) files.
        /// </summary>
        // public static string Status => GitCommandExecutor.Execute(@"status --porcelain");
    }
}