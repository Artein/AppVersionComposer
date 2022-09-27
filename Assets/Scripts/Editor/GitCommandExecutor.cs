using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace AppVersioning.Editor
{
    /// <summary>
    /// GitException includes the error output from a Git.Run() command as well as the ExitCode it returned.
    /// </summary>
    public class GitException : InvalidOperationException
    {
        public GitException(int exitCode, string errors) : base(errors) => ExitCode = exitCode;

        /// <summary>
        /// The exit code returned when running the Git command.
        /// </summary>
        public readonly int ExitCode;
    }
    
    public static class GitCommandExecutor
    {
        /// <summary>
        /// Runs 'git' with the specified arguments and returns the output.
        /// </summary>
        public static string Execute(string arguments)
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

            var outputStr = output.ToString().TrimEnd(); // trim '\n' in the end
            return outputStr;
        }
    }
}