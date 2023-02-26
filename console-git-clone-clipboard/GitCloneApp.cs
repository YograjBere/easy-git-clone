using console_git_clone_clipboard.configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace console_git_clone_clipboard;

public class GitCloneApp
{
    private readonly GitCloneAppSettings _applicationConfig;

    private readonly IDictionary<int, FolderMapping> _folderMappings;

    private readonly ILogger<GitCloneApp> _logger;

    private readonly IGitWrapper _gitHelper;

    public GitCloneApp(
        IOptions<GitCloneAppSettings> applicationConfig,
        ILogger<GitCloneApp> logger, 
        IGitWrapper gitHelper)
    {
        _applicationConfig = (applicationConfig ?? throw new ArgumentNullException(nameof(applicationConfig))).Value;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _gitHelper = gitHelper;
        _folderMappings = _applicationConfig.FolderMappings.OrderBy(t => t.DisplayOrder).ToDictionary(k => k.Id, v => v);
    }

    public void Process()
    {
        try
        {
            ProcessInternal();
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Error occurred !");
        }
    }

    private void ProcessInternal()
    {
        _logger.LogInformation("Reading folder mappings, from {@_applicationConfig}", _applicationConfig);

        // get the git repo url copied
        var copiedRepoUrl = Clipboard.GetText(TextDataFormat.Text);
        if (string.IsNullOrWhiteSpace(copiedRepoUrl))
        {
            _logger.LogInformation("Whitespace text in clipboard...");
            throw new ArgumentNullException("No valid git URL found in clipboard.");
        }

        _logger.LogInformation($"git repo URL in clipboard is: {copiedRepoUrl}");

        var repoDirName = _gitHelper.GetRepoDirNameFromUrl(copiedRepoUrl);

        // display user menu and accept the input
        var chosenFolder = ReadUserChoice();

        // create parent directory, if not exists
        if (!Directory.Exists(chosenFolder.Path) && _applicationConfig.CreateDirIfNotExists)
            Directory.CreateDirectory(chosenFolder.Path);

        // run git pull, if already cloned
        var repoDirFullPath = Path.Combine(chosenFolder.Path, repoDirName);
        if (Directory.Exists(repoDirFullPath))
        {
            _logger.LogInformation("repository already exists. Executing git pull");
            _gitHelper.RunGetLatest(repoDirFullPath);
            return;
        }

        // invoke git clone command
        _gitHelper.CloneRepo(copiedRepoUrl, chosenFolder.Path, repoDirName);

        // invoke post-clone commands in newly cloned folder
        if (chosenFolder.PostClone?.Any() == true)
            ProcessHandler.InvokePostCloneCommandsSequentially(repoDirFullPath, chosenFolder.PostClone);
    }

    FolderMapping ReadUserChoice()
    {
        UserInput.PrintLine("Enter the folder Id: ");
        UserInput.DisplayMenu(_folderMappings.Select(r => $"{r.Key}. {r.Value.DisplayName}"));
        var userInput = UserInput.Receive();
        if (!int.TryParse(userInput, out int chosenFolderId) ||
            !_folderMappings.TryGetValue(chosenFolderId, out FolderMapping folderMapping))
        {
            _logger.LogInformation($"Invalid choice: {chosenFolderId}");
            throw new ArgumentException($"Invalid choice: {chosenFolderId}");
        }

        _logger.LogInformation($"using folder {folderMapping.Path} to clone repo.");
        return folderMapping;
    }
}
