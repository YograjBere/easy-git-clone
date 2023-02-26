namespace console_git_clone_clipboard.configuration;

public class FolderMapping
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Path { get; set; }

    public string DisplayName { get; set; }

    public int DisplayOrder { get; set; }

    public string[] PostClone { get; set; }
}
