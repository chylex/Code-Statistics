using System.Diagnostics;

namespace CodeStatistics.Input.Methods{
    class OpenWebsite : IProjectInputMethod{
        private readonly string url;

        public OpenWebsite(string url){
            this.url = url;
        }

        public string[] Run(string[] args){
            Process.Start(url);
            return null;
        }
    }
}
