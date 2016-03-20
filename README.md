Code Statistics
===============

Utility to analyse and generate statistics for code and asset files in programming projects.

## Supported Input Methods

* Folders
* ZIP Archives (Windows only)
* GitHub Repositories (branches can be specified)

## Supported Languages

* Java 8 and older

View the Planned Features section for upcoming language support.

## Command Line Arguments

You can combine the following arguments to automate generation of the statistics or debug your project, but can only use one from each group (for example, you cannot combine **in:dummy** and **in:folder**).

```
-nogui                               disables GUI (still shows window in the taskbar)

-openbrowser                         immediately opens the browser after the project processing is
                                     finished (only opens the first time when debugging a template)
                                     
-autoclose                           automatically exits the program after the project processing
                                     is finished (closes even when debugging a template)
                                     
-debug                               enables the debugging UI in the project processing form

-template <file>                     sets the custom template file path
-template:debug <file>               sets the custom template file path and rebuilds when the template
                                     file changes

-in:folder <folder>                  sets a folder to be the input
-in:archive <file>                   sets an archive to be the input
-in:github <username/repo[/branch]>  sets a GitHub repository to be the input
-in:dummy                            generates random values for template building

-out <file>                          sets the output file path
```

## Planned Features

* **.codestats** file with special settings to allow custom project names and ignored files/folders
* Support for **C#** and **HTML+CSS+JS**, more coming later
* Support for ZIP archives on Linux and Mac, and more input methods (URLs and other git repositories)
* Got any suggestions? [Create an issue](https://github.com/chylex/Code-Statistics/issues/new)