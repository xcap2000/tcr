# TCR

Bash template:

```bash
$ dotnet test && git commit -am tcr || git reset --hard
```

Commands:

```bash
$ sudo apt install inotify-tools # To be able to run the watch command.
$ git flow feature finish -S -r tcr # To squash during merge and rebase before merging.
```

## Project Infrastructure

### Setting Up Git

Initialize git repository:

```bash
$ git clone git@github.com:xcap2000/tcr.git
$ mkdir tcr
$ cd tcr
$ git config user.name "Carlos Adriano Portes"
$ git config user.email "xcap2000@outlook.com"
```

### Creating The Global Configuration File

```bash
$ dotnet new globaljson
```

\* Be sure to have this framework version or newer installed for development.

### Creating The Ignore File

Create a file named .gitignore with the following contents:

```
.vs
bin
obj
*csproj.user
lcov.info
```

### Creating The Folder Infrastructure

```bash
$ mkdir .vscode
$ mkdir src
$ mkdir test
```

### Creating The Project

```bash
$ dotnet new console -n Tcr -o src/Tcr
$ dotnet add src/Tcr/Tcr.csproj package Roslynator.Analyzers --version 4.5.0
$ dotnet add src/Tcr/Tcr.csproj package Roslynator.Formatting.Analyzers --version 4.5.0
$ dotnet add src/Tcr/Tcr.csproj package Roslynator.CodeAnalysis.Analyzers --version 4.5.0
$ dotnet add src/Tcr/Tcr.csproj package Microsoft.CodeAnalysis.CSharp.Workspaces --version 4.7.0
```

### Creating The Test Project

```bash
$ dotnet new xunit -n Tcr.Tests -o test/Tcr.Tests
$ dotnet add test/Tcr.Tests/Tcr.Tests.csproj reference src/Tcr/Tcr.csproj
$ dotnet add src/Tcr/Tcr.csproj package Roslynator.Analyzers --version 4.5.0
$ dotnet add src/Tcr/Tcr.csproj package Roslynator.Formatting.Analyzers --version 4.5.0
$ dotnet add src/Tcr/Tcr.csproj package Roslynator.CodeAnalysis.Analyzers --version 4.5.0
$ dotnet add src/Tcr/Tcr.csproj package Microsoft.CodeAnalysis.CSharp.Workspaces --version 4.7.0
$ dotnet add test/Tcr.Tests/Tcr.Tests.csproj package coverlet.msbuild --version 3.2.0
$ dotnet add test/Tcr.Tests/Tcr.Tests.csproj package NSubstitute --version 5.0.0
$ dotnet add test/Tcr.Tests/Tcr.Tests.csproj package NSubstitute.Analyzers.CSharp --version 1.0.16
```

### Creating The Solution

```bash
$ dotnet new sln -n Tcr
$ dotnet sln Tcr.sln add src/Tcr/Tcr.csproj
$ dotnet sln Tcr.sln add test/Tcr.Tests/Tcr.Tests.csproj
```

### Creating The Ruleset Configuration File

Create the following files:
* src/Tcr/Tcr.ruleset
* test/Tcr.Tests/Tcr.Tests.ruleset

With the following contents:

```xml
<RuleSet Name="Rules" Description="Rules for this project" ToolsVersion="15.0">
   <Rules AnalyzerId="Microsoft.Analyzers.ManagedCodeAnalysis" RuleNamespace="Microsoft.Rules.Managed">
     <!--<Rule Id="RULE_ID" Action="ACTION" />-->
   </Rules>
</RuleSet>
```

Supported Ruleset Actions Are:
* Warning
* Error
* Info
* Hidden
* None

#### Creating Extensions File

Create a file named extensions.json in the .vscode folder with the following contents:

```json
{
    "recommendations": [
        "ms-dotnettools.csdevkit",
        "ms-dotnettools.vscodeintellicode-csharp",
        "ryanluker.vscode-coverage-gutters",
        "eamodio.gitlens",
        "ow.vscode-subword-navigation",
        "amodio.toggle-excluded-files",
        "pflannery.vscode-versionlens",
        "VisualStudioExptTeam.vscodeintellicode",
        "redhat.vscode-xml",
        "oderwat.indent-rainbow"
    ]
}
```

### Creating Launch File

Create a file named launch.json in the .vscode folder with the following contents:

```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": ".NET Core Launch (console)",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "all: build",
            "program": "${workspaceFolder}/src/Tcr/bin/Debug/net7.0/Tcr.dll",
            "args": [],
            "cwd": "${workspaceFolder}",
            "console": "internalConsole",
            "stopAtEntry": false
        },
        {
            "name": ".NET Core Attach",
            "type": "coreclr",
            "request": "attach",
            "processId": "${command:pickProcess}"
        }
    ]
}
```

### Creating The VSCode Configuration File

Create a file named settings.json in the .vscode folder with the following contents:

```json
{
    "editor.formatOnSave": true,
    "editor.formatOnPaste": true,
    "files.exclude": {
        "**/.git": false,
        "**/.vscode": false,
        "**/.vs": false,
        "**/.gitignore": false,
        "**/global.json": false,
        "**/bin": false,
        "**/obj": false,
        "**/lcov.info": false,
        "**/*.ruleset": false,
        "**/.config": false
    }
}
```

#### Creating The Project Tasks

Create a file named tasks.json in the .vscode folder with the following contents:

```json
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "src: run",
            "type": "process",
            "group": "build",
            "command": "dotnet",
            "isBackground": true,
            "windows": {
                "args": [
                    "run",
                    "--project",
                    "${workspaceFolder}\\src\\TeamManagement\\TeamManagement.csproj",
                    "/clp:NoSummary"
                ]
            },
            "linux": {
                "args": [
                    "run",
                    "--project",
                    "${workspaceFolder}/src/TeamManagement/TeamManagement.csproj",
                    "/clp:NoSummary"
                ]
            },
            "problemMatcher": [],
            "presentation": {
                "reveal": "always"
            }
        },
        {
            "label": "src: run (watch)",
            "type": "process",
            "group": "build",
            "command": "dotnet",
            "isBackground": true,
            "windows": {
                "args": [
                    "watch",
                    "--project",
                    "${workspaceFolder}\\src\\TeamManagement\\TeamManagement.csproj",
                    "run",
                    "/clp:NoSummary"
                ]
            },
            "linux": {
                "args": [
                    "watch",
                    "--project",
                    "${workspaceFolder}/src/TeamManagement/TeamManagement.csproj",
                    "run",
                    "/clp:NoSummary"
                ]
            },
            "problemMatcher": [],
            "presentation": {
                "reveal": "always"
            }
        },
        {
            "label": "all: clean",
            "type": "shell",
            "group": "build",
            "command": "dotnet clean",
            "problemMatcher": "$msCompile",
            "presentation": {
                "reveal": "silent"
            }
        },
        {
            "label": "all: restore",
            "type": "shell",
            "group": "build",
            "command": "dotnet restore",
            "problemMatcher": "$msCompile",
            "presentation": {
                "reveal": "silent"
            }
        },
        {
            "label": "all: build",
            "type": "shell",
            "group": "build",
            "command": "dotnet build /clp:NoSummary",
            "problemMatcher": "$msCompile",
            "presentation": {
                "reveal": "silent"
            }
        },
        {
            "label": "test: unit",
            "type": "process",
            "group": "test",
            "command": "dotnet",
            "windows": {
                "args": [
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "--filter",
                    "Category=Unit",
                    "${workspaceFolder}\\test\\TeamManagement.Tests\\TeamManagement.Tests.csproj",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "linux": {
                "args": [
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "--filter",
                    "Category=Unit",
                    "${workspaceFolder}/test/TeamManagement.Tests/TeamManagement.Tests.csproj",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "problemMatcher": "$msCompile",
            "presentation": {
                "reveal": "silent"
            }
        },
        {
            "label": "test: unit (watch)",
            "type": "process",
            "group": "test",
            "command": "dotnet",
            "isBackground": true,
            "windows": {
                "args": [
                    "watch",
                    "--project",
                    "${workspaceFolder}\\test\\TeamManagement.Tests\\TeamManagement.Tests.csproj",
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "--filter",
                    "Category=Unit",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "linux": {
                "args": [
                    "watch",
                    "--project",
                    "${workspaceFolder}/test/TeamManagement.Tests/TeamManagement.Tests.csproj",
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "--filter",
                    "Category=Unit",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "problemMatcher": {
                "base": "$msCompile",
                "background": {
                    "beginsPattern": {
                        "regexp": "dotnet watch 🚀"
                    },
                    "endsPattern": {
                        "regexp": "dotnet watch [❌⌚]"
                    }
                }
            },
            "presentation": {
                "reveal": "always"
            }
        },
        {
            "label": "test: integration",
            "type": "process",
            "group": "test",
            "command": "dotnet",
            "windows": {
                "args": [
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "--filter",
                    "Category=Integration",
                    "${workspaceFolder}\\test\\TeamManagement.Tests\\TeamManagement.Tests.csproj",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "linux": {
                "args": [
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "--filter",
                    "Category=Integration",
                    "${workspaceFolder}/test/TeamManagement.Tests/TeamManagement.Tests.csproj",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "problemMatcher": "$msCompile",
            "presentation": {
                "reveal": "silent"
            }
        },
        {
            "label": "test: integration (watch)",
            "type": "process",
            "group": "test",
            "command": "dotnet",
            "isBackground": true,
            "windows": {
                "args": [
                    "watch",
                    "--project",
                    "${workspaceFolder}\\test\\TeamManagement.Tests\\TeamManagement.Tests.csproj",
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "--filter",
                    "Category=Integration",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "linux": {
                "args": [
                    "watch",
                    "--project",
                    "${workspaceFolder}/test/TeamManagement.Tests/TeamManagement.Tests.csproj",
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "--filter",
                    "Category=Integration",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "problemMatcher": {
                "base": "$msCompile",
                "background": {
                    "beginsPattern": {
                        "regexp": "dotnet watch 🚀"
                    },
                    "endsPattern": {
                        "regexp": "dotnet watch [❌⌚]"
                    }
                }
            },
            "presentation": {
                "reveal": "always"
            }
        },
        {
            "label": "test: functional",
            "type": "process",
            "group": "test",
            "command": "dotnet",
            "windows": {
                "args": [
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "--filter",
                    "Category=Functional",
                    "${workspaceFolder}\\test\\TeamManagement.Tests\\TeamManagement.Tests.csproj",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "linux": {
                "args": [
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "--filter",
                    "Category=Functional",
                    "${workspaceFolder}/test/TeamManagement.Tests/TeamManagement.Tests.csproj",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "problemMatcher": "$msCompile",
            "presentation": {
                "reveal": "silent"
            }
        },
        {
            "label": "test: functional (watch)",
            "type": "process",
            "group": "test",
            "command": "dotnet",
            "isBackground": true,
            "windows": {
                "args": [
                    "watch",
                    "--project",
                    "${workspaceFolder}\\test\\TeamManagement.Tests\\TeamManagement.Tests.csproj",
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "--filter",
                    "Category=Functional",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "linux": {
                "args": [
                    "watch",
                    "--project",
                    "${workspaceFolder}/test/TeamManagement.Tests/TeamManagement.Tests.csproj",
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "--filter",
                    "Category=Functional",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "problemMatcher": {
                "base": "$msCompile",
                "background": {
                    "beginsPattern": {
                        "regexp": "dotnet watch 🚀"
                    },
                    "endsPattern": {
                        "regexp": "dotnet watch [❌⌚]"
                    }
                }
            },
            "presentation": {
                "reveal": "always"
            }
        },
        {
            "label": "test: all",
            "type": "process",
            "group": "test",
            "command": "dotnet",
            "windows": {
                "args": [
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "${workspaceFolder}\\test\\TeamManagement.Tests\\TeamManagement.Tests.csproj",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "linux": {
                "args": [
                    "test",
                    "--logger",
                    "console;verbosity=detailed",
                    "${workspaceFolder}/test/TeamManagement.Tests/TeamManagement.Tests.csproj",
                    "/clp:NoSummary",
                    "/p:CollectCoverage=true",
                    "/p:CoverletOutputFormat=lcov",
                    "/p:CoverletOutput=./lcov.info",
                    "/p:Exclude=\"[coverlet.*]*,[*]Coverlet.Core*,[System*]*,[xunit.*]*\""
                ]
            },
            "problemMatcher": "$msCompile",
            "presentation": {
                "reveal": "silent"
            }
        },
        {
            "label": "src: publish x64 (linux)",
            "type": "shell",
            "group": "build",
            "presentation": {
                "reveal": "silent"
            },
            "problemMatcher": "$msCompile",
            "windows": {
                "command": "dotnet publish src\\TeamManagement\\TeamManagement.csproj -c Release -r linux-x64 --self-contained -o src\\TeamManagement\\bin\\Release\\net7.0\\win10-x64\\ /p:PublishSingleFile=true"
            },
            "linux": {
                "command": "dotnet publish src/TeamManagement/TeamManagement.csproj -c Release -r linux-x64 --self-contained -o src/TeamManagement/bin/Release/net7.0/win10-x64/ /p:PublishSingleFile=true"
            }
        }
    ]
}
```

## References

```
https://www.youtube.com/watch?v=tnO2Mos0RjU
https://youtu.be/IIKndRX5qHw
https://git.logikum.hu/flow/feature
```