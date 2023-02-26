using System.Diagnostics;

namespace console_git_clone_clipboard;

public class ProcessHandler
{
    private const string GIT_EXE_NAME = "git.exe";

    public static void InvokeGitClone(string wd, string gitRepoUrl)
    {
        InvokeCommand(wd, new PostCloneCommand()
        {
            Arguments = $" {GIT_EXE_NAME} clone {gitRepoUrl}",
            WorkingDirectory = wd
        }, sequential: true);
    }

    public static void InvokeGitClone(string wd, string gitRepoUrl, string directoryName)
    {
        InvokeCommand(wd, new PostCloneCommand()
        {
            Arguments = $" {GIT_EXE_NAME} clone {gitRepoUrl} {directoryName}",
            WorkingDirectory = wd
        }, sequential: true);
    }

    public static void InvokeGitPull(string repoDirPath)
    {
        InvokeCommand(repoDirPath, new PostCloneCommand()
        {
            Arguments = $" {GIT_EXE_NAME} pull",
            WorkingDirectory = repoDirPath
        }, sequential: true);
    }

    public static void InvokePostCloneCommandsSequentially(string wd, IEnumerable<PostCloneCommand> postCloneCommands)
    {
        foreach (var postCloneCommand in postCloneCommands)
        {
            InvokeCommand(wd, postCloneCommand);
        }
    }

    // public static void InvokePostCloneCommand(string wd, string postcloneCommand)
    // {
    //     InvokeCommand(wd, new PostCloneCommand() { FileName = "cmd", Arguments = $"/c {postcloneCommand}" }, sequential: true);
    // }

    public static void InvokePostCloneCommandsSequentially(string wd, IEnumerable<string> postCloneCommands)
    {
        foreach (var command in postCloneCommands)
        {
            InvokeCommand(wd, new PostCloneCommand() { Arguments = command, WorkingDirectory = wd });
        }
    }

    private static void InvokeCommand(string wd, PostCloneCommand postCloneCommand, bool sequential = true)
    {
        var startInfo = new ProcessStartInfo
        {
            WorkingDirectory = wd,
            WindowStyle = ProcessWindowStyle.Normal,
            FileName = "cmd",
            RedirectStandardInput = true,
            Arguments = $"/c {postCloneCommand.Arguments}",
            UseShellExecute = false
        };

        var proc = Process.Start(startInfo);
        if (sequential)
            proc.WaitForExit();
    }

    public static void InvokePostCloneCommandsParallel(string wd, PostCloneCommand[] postCloneCommands)
    {
        Parallel.ForEach(postCloneCommands, (postCloneCmd) =>
        {
            InvokeCommand(wd, postCloneCmd, sequential: false);
        });
    }
}
