
# Clone and categorize git repositories easily

This tool allows you to clone and categorize you favorite git repositories easily with just few clicks.
You can also execute command after clone. E.g. for angular/react/nodejs projects, *npm install* or for dotnet solutions, *dotnet restore*

## Pre-requisites
You will need to maintain categorized folders mapped in "folderMapping.json" file

```
{
	"gitCloneAppSettings": {
		"createDirIfNotExists": true,
		"folderMappings": [
			{
				"id": 1,
				"displayName": "C#",
				"path": "D:\\MyGitRepos\\C#",
				"displayOrder": "0",
				"postClone": [
					"start .",
					"dotnet restore",
					"dotnet build"
				]
			},
			{
				"id": 2,
				"displayName": "Java",
				"path": "D:\\MyGitRepos\\Java",
				"displayOrder": "1",
				"postClone": ["start ."]
			},
			{
				"id": 3,
				"displayName": "Nodejs",
				"path": "D:\\MyGitRepos\\Nodejs",
				"displayOrder": "2",
				"postClone": [
					"npm install"
				]
			},
			{
				"id": 4,
				"displayName": "Angular",
				"path": "D:\\MyGitRepos\\Angular",
				"displayOrder": "3",
				"postClone": [
					"npm install"
				]
			},
			{
				"id": 5,
				"displayName": "React",
				"path": "D:\\MyGitRepos\\React",
				"displayOrder": "4",
				"postClone": [
					"npm install",	
					"code ."
				]
			},
			{
				"id": 6,
				"displayName": "Powershell",
				"path": "D:\\MyGitRepos\\Powershell",
				"displayOrder": "5"
			},
			{
				"id": 7,
				"displayName": "javascript",
				"path": "D:\\MyGitRepos\\javascript",
				"displayOrder": "6"
			}
		]
	}
}

```

## Demo

Insert gif or link to demo



## Authors

- [@YograjBere](https://www.github.com/YograjBere)
