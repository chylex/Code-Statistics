using System;
using System.ComponentModel;
using System.Net;

namespace CodeStatistics.Input.Methods{
    public class GitHub : IDisposable{
        public enum DownloadStatus{
            NoInternet, NoConnection, Started
        }

        private readonly string target;
        private WebClient downloader;

        public string RepositoryName { get { return target; } }
        public string ZipUrl { get { return "https://github.com/"+target+"/zipball/master"; } }

        public event DownloadProgressChangedEventHandler DownloadProgressChanged;
        public event AsyncCompletedEventHandler DownloadFinished;

        public GitHub(string username, string repository){
            this.target = username+"/"+repository;
        }

        public DownloadStatus DownloadRepositoryZip(string targetFile){
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()){
                return DownloadStatus.NoInternet;
            }

            downloader = new WebClient();

            if (DownloadProgressChanged != null)downloader.DownloadProgressChanged += DownloadProgressChanged;
            if (DownloadFinished != null)downloader.DownloadFileCompleted += DownloadFinished;

            try{
                downloader.DownloadFileAsync(new Uri(ZipUrl),targetFile);
                return DownloadStatus.Started;
            }catch(WebException){
                return DownloadStatus.NoConnection;
            }
        }

        public void Cancel(){
            if (downloader != null)downloader.CancelAsync();
        }

        public void Dispose(){
            if (downloader != null)downloader.Dispose();
        }
    }
}
