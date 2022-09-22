using System;
using System.Text;

namespace AppVersioning
{
    [Serializable]
    public struct AppVersionData
    {
        public int Major;
        public int Minor;
        public int Patch;
        public string CommitHash;
        public string Branch;

        public AppVersionData(int major, int minor, int patch, string commitHash, string branch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            CommitHash = commitHash;
            Branch = branch;
        }

        public string BuildString(bool appendCommitHash = true, bool appendBranch = true)
        {
            var builder = new StringBuilder($"{Major}.{Minor}.{Patch}");
            
            if (appendCommitHash)
            {
                builder.Append($"-{CommitHash}");
            }

            if (appendBranch)
            {
                builder.Append($"-{Branch}");
            }

            return builder.ToString();
        }

        public override string ToString()
        {
            return BuildString();
        }
    }
}