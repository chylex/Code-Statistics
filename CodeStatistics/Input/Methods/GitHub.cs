using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Web.Script.Serialization;
using CodeStatistics.Forms;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using CodeStatistics.Input.Helpers;

namespace CodeStatistics.Input.Methods{
    public class GitHub : IInputMethod, IDisposable{
        public const string DefaultBranch = "master";

        private static readonly Regex RepositoryRegexCustom = new Regex(@"^([a-zA-Z0-9\-]+)/([\w\-\. ]+)(?:/([^\\[?^:~ ]+))?$",RegexOptions.CultureInvariant);

        /// <summary>
        /// Checks whether a repository in the format &lt;username&gt;/&lt;repository&gt;[/&lt;branch&gt;] is valid. Allows spaces in &lt;repository&gt; that are
        /// automatically converted into dashes in <see cref="GitHub"/> constructor for convenience. Repository names are case-insensitive.
        /// </summary>
        public static bool IsRepositoryValid(string repo){
            return RepositoryRegexCustom.Match(repo).Groups.Count >= 2;
        }

        public enum DownloadStatus{
            NoInternet, NoConnection, Started
        }

        public delegate void BranchListRetrieved(IEnumerable<string> branches);

        private readonly string target;
        private WebClient dlBranches, dlRepo;
        private CancellationTokenSource dlRepoCancel;

        public string Branch = DefaultBranch;

        public string RepositoryName { get { return target; } }
        public Uri BranchesUrl { get { return new Uri("https://api.github.com/repos/"+target+"/branches"); } }
        public Uri ZipUrl { get { return new Uri("https://github.com/"+target+"/zipball/"+Branch); } }

        public event DownloadProgressChangedEventHandler DownloadProgressChanged;
        public event AsyncCompletedEventHandler DownloadFinished;

        public GitHub(string repository){ // TODO validate
            Match match = RepositoryRegexCustom.Match(repository.Replace(' ','-'));

            target = match.Groups[1].Value+'/'+match.Groups[2].Value;
            if (match.Groups.Count == 4)Branch = match.Groups[3].Value;
        }

        public DownloadStatus RetrieveBranchList(BranchListRetrieved onRetrieved){
            if (dlBranches != null)Reset();

            if (!NetworkInterface.GetIsNetworkAvailable()){
                return DownloadStatus.NoInternet;
            }

            dlBranches = CreateWebClient();

            dlBranches.DownloadStringCompleted += (sender, args) => {
                if (args.Cancelled || args.Error != null){
                    onRetrieved(null);
                    return;
                }
                
                List<string> branches = new List<string>(2);

                foreach(object entry in (object[])new JavaScriptSerializer().DeserializeObject(args.Result)){
                    branches.Add((string)((Dictionary<string,object>)entry)["name"]);
                }

                if (branches.Remove("master"))onRetrieved(Enumerable.Repeat("master",1).Concat(branches));
                else onRetrieved(branches);
            };

            try{
                dlBranches.DownloadStringAsync(BranchesUrl);
                return DownloadStatus.Started;
            }catch(WebException){
                return DownloadStatus.NoConnection;
            }
        }

        public DownloadStatus DownloadRepositoryZip(string targetFile){
            if (dlRepo != null)Reset();

            if (!NetworkInterface.GetIsNetworkAvailable()){
                return DownloadStatus.NoInternet;
            }

            dlRepo = CreateWebClient();

            if (DownloadProgressChanged != null)dlRepo.DownloadProgressChanged += DownloadProgressChanged;
            if (DownloadFinished != null)dlRepo.DownloadFileCompleted += DownloadFinished;

            try{
                dlRepo.DownloadFileAsync(ZipUrl,targetFile);
                return DownloadStatus.Started;
            }catch(WebException){
                return DownloadStatus.NoConnection;
            }
        }

        public void Cancel(){
            if (dlBranches != null)dlBranches.CancelAsync();
            if (dlRepo != null)dlRepo.CancelAsync();
            if (dlRepoCancel != null)dlRepoCancel.Cancel();
        }

        public void Dispose(){
            if (dlBranches != null)dlBranches.Dispose();
            if (dlRepo != null)dlRepo.Dispose();
        }

        private void Reset(){
            Cancel();
            Dispose();
        }

        public void BeginProcess(ProjectLoadForm.UpdateCallbacks callbacks){
            string tmpDir = IOUtils.CreateTemporaryDirectory();
            string tmpFile = tmpDir == null ? "github.zip" : Path.Combine(tmpDir,"github.zip");

            DownloadProgressChanged += (sender, args) => {
                callbacks.UpdateProgress(args.TotalBytesToReceive == -1 ? -1 : args.ProgressPercentage);
                callbacks.UpdateDataLabel(args.TotalBytesToReceive == -1 ? (args.BytesReceived/1024)+" kB" : (args.BytesReceived/1024)+" / "+(args.TotalBytesToReceive/1024)+" kB");
            };

            DownloadFinished += (sender, args) => {
                callbacks.UpdateInfoLabel("Extracting repository...");

                ZipArchive archive = new ZipArchive(tmpFile);
                
                dlRepoCancel = archive.ExtractAsync(path => {
                    callbacks.OnReady(new FileSearch(new string[]{ path }));
                    archive.DeleteAndDispose();
                });
            };

            switch(DownloadRepositoryZip(tmpFile)){
                case DownloadStatus.Started:
                    callbacks.UpdateInfoLabel("Downloading repository...");
                    break;

                case DownloadStatus.NoInternet:
                    callbacks.UpdateInfoLabel("No internet connection.");
                    break;

                case DownloadStatus.NoConnection:
                    callbacks.UpdateInfoLabel("Could not establish connection with GitHub.");
                    break;
            }
        }

        public void CancelProcess(Action onCancelFinish){
            Reset();
            onCancelFinish();
        }

        private static WebClient CreateWebClient(){
            WebClient client = new WebClient();
            client.Proxy = null;
            client.Headers.Add("User-Agent","CodeStatistics");
            return client;
        }
    }
}
