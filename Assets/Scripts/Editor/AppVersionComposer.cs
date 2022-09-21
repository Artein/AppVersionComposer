using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace AppVersioning.Editor
{
    /// <summary>
    /// GitException includes the error output from a Git.Run() command as well as the
    /// ExitCode it returned.
    /// </summary>
    public class GitException : InvalidOperationException
    {
        public GitException(int exitCode, string errors) : base(errors) => ExitCode = exitCode;

        /// <summary>
        /// The exit code returned when running the Git command.
        /// </summary>
        public readonly int ExitCode;
    }
    
    public static class AppVersionComposer
    {
        /// <summary>
        /// Retrieves the build version from git based on the most recent matching tag and
        /// commit history. This returns the version as: {major.minor.build-commitHash} where 'build'
        /// represents the nth commit after the tagged commit.
        /// Note: The initial 'v' and the commit hash code are removed.
        /// </summary>
        public static string BuildVersion
        {
            get
            {
                var version = RunGitCommand(@"describe --tags --long --match ""v[0-9]*""");
                // TODO: What will be if there no tags in repository
                version = version.Replace('-', '.');
                var revision = version.Substring(1, version.LastIndexOf('.') - 1);
                var commitHash = version[(version.LastIndexOf('.') + 2)..]; // startIndex+2 - to remove 'g' (git) from returned commitHash
                version = revision + '-' + commitHash;

                if (Debug.isDebugBuild)
                {
                    version += '-' + Branch;
                }
                
                return version;
            }
        }

        /// <summary>
        /// The currently active branch.
        /// </summary>
        public static string Branch => RunGitCommand(@"rev-parse --abbrev-ref HEAD");

        /// <summary>
        /// Returns a listing of all uncommitted or untracked (added) files.
        /// </summary>
        public static string Status => RunGitCommand(@"status --porcelain");


        /* Methods ================================================================================================================ */

        /// <summary>
        /// Runs git.exe with the specified arguments and returns the output.
        /// </summary>
        public static string RunGitCommand(string arguments)
        {
            using var process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = @"git", 
                Arguments = arguments, 
                CreateNoWindow = true, 
                WorkingDirectory = Application.dataPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false
            };
            
            var output = new StringBuilder();
            process.OutputDataReceived += (_, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    output.AppendLine(e.Data);
                }
            };

            var error = new StringBuilder();
            process.ErrorDataReceived += (_, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    error.AppendLine(e.Data);
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            
            if (process.ExitCode != 0)
            {
                throw new GitException(process.ExitCode, error.ToString());
            }

            var outputStr = output.ToString()[..^1];  // ^1 to remove \n in the end
            return outputStr;
        }
    }
}