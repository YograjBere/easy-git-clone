using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace console_git_clone_clipboard.configuration;

public class GitCloneAppSettingsValidator : IValidateOptions<GitCloneAppSettings>
{
    private readonly ILogger<GitCloneAppSettingsValidator> _logger;

    public GitCloneAppSettingsValidator(ILogger<GitCloneAppSettingsValidator> logger)
    {
        this._logger = logger;
    }
    
    public ValidateOptionsResult Validate(string name, GitCloneAppSettings options)
    {
        if (!options.FolderMappings.Any())
            return ValidateOptionsResult.Fail(ErrorContants.FILE_CONTENTS_EMPTY);
       
        _logger.LogInformation("Valid folderMapping.json file.");

        return ValidateOptionsResult.Success;
    }
}
