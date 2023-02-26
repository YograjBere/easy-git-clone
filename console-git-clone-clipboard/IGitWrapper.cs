namespace console_git_clone_clipboard;

public interface IGitWrapper
{
    void CloneRepo(string repoUrl, string parentDirPath, string repoDirName);

    void RunGetLatest(string repoDirFullPath);

    string GetRepoDirNameFromUrl(string repoUrl);
}

public class GitWrapper : IGitWrapper
{
    private const char _forwardSlash = '/';

    public void CloneRepo(string repoUrl, string parentDirPath, string repoDirName)
    {
        ProcessHandler.InvokeGitClone(parentDirPath, repoUrl, repoDirName);
    }

    public string GetRepoDirNameFromUrl(string repoUrl)
    {
        return repoUrl.Split(_forwardSlash).Last().Replace(".git", string.Empty);
    }

    public void RunGetLatest(string repoDirFullPath)
    {
        ProcessHandler.InvokeGitPull(repoDirFullPath);
    }
}
