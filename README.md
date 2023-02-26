
# Clone git repositories very easily from command line

This tool allows you to clone git repositories with just few clicks.

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
				"path": "D:\\MyCode\\C#",
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
				"path": "D:\\MyCode\\Java",
				"displayOrder": "1",
				"postClone": ["start ."]
			},
			{
				"id": 3,
				"displayName": "Nodejs",
				"path": "D:\\MyCode\\Nodejs",
				"displayOrder": "2",
				"postClone": [
					"npm install"
				]
			},
			{
				"id": 4,
				"displayName": "Angular",
				"path": "D:\\MyCode\\Angular",
				"displayOrder": "3",
				"postClone": [
					"npm install"
				]
			},
			{
				"id": 5,
				"displayName": "React",
				"path": "D:\\MyCode\\React",
				"displayOrder": "4",
				"postClone": [
					"npm install",
					"code ."
				]
			},
			{
				"id": 6,
				"displayName": "Powershell",
				"path": "D:\\MyCode\\Powershell",
				"displayOrder": "5"
			}
		]
	}
}

```
## Demo

Insert gif or link to demo


## Authors

- [@YograjBere](https://www.github.com/YograjBere)

