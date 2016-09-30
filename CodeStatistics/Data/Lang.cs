using System.Collections;
using System.Collections.Generic;

namespace CodeStatistics.Data{
    class Lang : IEnumerable<KeyValuePair<string, string>>{
        public static readonly Lang Get;

        static Lang(){ // figure out languages later
            Get = new Lang{
                { "Title", "Code Statistics" },
                { "TitleAbout", "About" },
                { "TitleProject", "Code Statistics - Project" },
                { "TitleDebug", "Code Statistics - Project Debug" },

                { "ErrorLaunchTitle", "Launch Error" },
                { "ErrorLaunchMonoOnWindowsBuild", "You are running Mono with the Windows build of Code Statistics. Please, download the Mono build instead." },

                { "ErrorInvalidArgsTitle", "Invalid Program Arguments" },
                { "ErrorInvalidArgsDuplicateIdentifier", "Duplicate identifier: $1" },
                { "ErrorInvalidArgsUnknown", "Unknown parameter: $1" },
                { "ErrorInvalidArgsShouldBeVariable", "Parameter has no specified value: $1" },
                { "ErrorInvalidArgsFileNotFound", "File not found: $1" },
                { "ErrorInvalidArgsFolderNotFound", "Folder not found: $1" },
                { "ErrorInvalidArgsGitHub", "Invalid GitHub repository, correct format is <username/repo[/branch]>: $1" },

                { "MenuProjectFromFolder", "Project From Folder" },
                { "MenuProjectFromGitHub", "Project From GitHub" },
                { "MenuProjectFromArchive", "Project From Archive" },
                { "MenuViewOptions", "Options" },
                { "MenuViewSourceCode", "Source Code" },
                { "MenuViewAbout", "About" },

                { "LoadProjectSearchIO", "Searching Files and Folders..." },
                { "LoadProjectProcess", "Processing the Project..." },
                { "LoadProjectProcessingDone", "Project processing finished." },
                { "LoadProjectDummyTemplateWait", "Waiting for template to update..." },
                { "LoadProjectDummyTemplateRebuild", "Rebuilding template..." },
                { "LoadProjectDummyTemplateFailed", "Template generation failed." },
                { "LoadProjectProcessingFiles", "$1 / $2" },
                { "LoadProjectCancel", "Cancel" },
                { "LoadProjectClose", "Close" },
                { "LoadProjectOpenOutput", "Open HTML" },
                
                { "LoadProjectError", "Project processing failed" },
                { "LoadProjectErrorFileSearch", "Could not read project files:\n$1" },
                { "LoadProjectErrorProcessing", "Could not process the project:\n$1" },
                { "LoadProjectErrorNoTemplate", "Could not read the template file." },
                { "LoadProjectErrorInvalidTemplate", "The template contains errors:\n$1" },
                { "LoadProjectErrorIO", "Could not create the HTML file:\n$1" },
                { "LoadProjectErrorUnknown", "Unknown error occurred when generating the HTML file." },

                { "LoadProjectDebug", "Debug" },
                { "LoadProjectBreakpoint", "Break" },

                { "LoadGitHubRepositoryName", "Repository Name:" },
                { "LoadGitHubBranch", "Branch:" },
                { "LoadGitHubBranchLoading", "(Loading...)" },
                { "LoadGitHubBranchFailure", "(Failed)" },
                { "LoadGitHubDownload", "Download" },
                { "LoadGitHubCancel", "Cancel" },
                
                { "LoadGitHubError", "GitHub Project Error" },
                { "LoadGitHubDownloadingRepo", "Downloading repository..." },
                { "LoadGitHubExtractingRepo", "Extracting repository..." },
                { "LoadGitHubNoConnection", "No internet connection." },
                { "LoadGitHubNoInternet", "Could not establish connection with GitHub." },
                { "LoadGitHubDownloadError", "Could not download the repository." },
                { "LoadGitHubTrustError", "Your Mono installation is not trusting GitHub certificates. Do you want to download them automatically? You can also run 'mozroots --import --ask-remove' for manual installation." },
                
                { "LoadArchiveExtracting", "Extracting archive..." },

                { "DebugProjectReprocess", "Reprocess" },
                { "DebugProjectLoadOriginal", "Original" },
                { "DebugProjectDebug", "Debug" },

                { "AboutReadme", "View ReadMe" },

                { "DialogFilterFolders", "Folders" },
                { "DialogFilterArchives", "Archives (.zip)" },
                
                { "TemplateErrorNotFound", "Template not found: $1" },
                { "TemplateErrorUnknownToken", "Unknown template token: $1" }
            };
        }

        private readonly Dictionary<string, string> strings = new Dictionary<string, string>();

        public string this[string key]{
            get { string value; return strings.TryGetValue(key, out value) ? value : "<UNKNOWN>"; }
        }

        public string this[string key, params object[] data]{
            get {
                string value = this[key];

                for(int index = data.Length; index > 0; index--){
                    value = value.Replace("$"+index, data[index-1].ToString());
                }

                return value;
            }
        }

        private Lang(){}

        private void Add(string key, string value){
            strings.Add(key, value);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator(){
            return strings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator(){
            return strings.GetEnumerator();
        }
    }
}
