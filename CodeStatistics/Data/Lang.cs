using System.Collections;
using System.Collections.Generic;

namespace CodeStatistics.Data{
    class Lang : IEnumerable<KeyValuePair<string,string>>{
        public static readonly Lang Get;

        static Lang(){ // figure out languages later
            Get = new Lang{
                { "Title", "Code Statistics" },
                { "TitleProject", "Code Statistics - Project" },
                { "TitleDebug", "Code Statistics - Project Debug" },

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
                { "MenuViewSourceCode", "Source Code" },
                { "MenuViewAbout", "About" },

                { "LoadProjectSearchIO", "Searching Files and Folders..." },
                { "LoadProjectProcess", "Processing the Project..." },
                { "LoadProjectProcessingDone", "Project processing finished." },
                { "LoadProjectProcessingFiles", "$1 / $2" },
                { "LoadProjectCancel", "Cancel" },
                { "LoadProjectClose", "Close" },
                { "LoadProjectOpenOutput", "Open HTML" },

                { "LoadProjectDebug", "Debug" },
                { "LoadProjectBreakpoint", "Break" },

                { "DebugProjectReprocess", "Reprocess" },
                { "DebugProjectLoadOriginal", "Original" },
                { "DebugProjectDebug", "Debug" }
            };
        }

        private readonly Dictionary<string,string> strings = new Dictionary<string,string>();

        public string this[string key]{
            get { string value; return strings.TryGetValue(key,out value) ? value : "<UNKNOWN>"; }
        }

        public string this[string key, params object[] data]{
            get {
                string value = this[key];

                for(int index = data.Length; index > 0; index--){
                    value = value.Replace("$"+index,data[index-1].ToString());
                }

                return value;
            }
        }

        private Lang(){}

        private void Add(string key, string value){
            strings.Add(key,value);
        }

        public IEnumerator<KeyValuePair<string,string>> GetEnumerator(){
            return strings.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator(){
            return strings.GetEnumerator();
        }
    }
}
