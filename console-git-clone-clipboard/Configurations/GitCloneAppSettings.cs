namespace console_git_clone_clipboard.configuration;

public class GitCloneAppSettings
{
    private string _folderMappingFilePath;

    private bool _createDirIfNotExists;

    private IList<FolderMapping> _folderMappings;

    public string FolderMappingFilePath
    {
        get => _folderMappingFilePath;
        set => _folderMappingFilePath = value;
    }

    public bool CreateDirIfNotExists
    {
        get => _createDirIfNotExists;
        set => _createDirIfNotExists = value;
    }
    
    public IList<FolderMapping> FolderMappings
    {
        get => _folderMappings;
        set => _folderMappings = value;
    }
}
