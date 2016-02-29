using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CodeStatistics.Input.Methods{
    class ZipArchive : IDisposable{
        private readonly string file;
        private readonly string extractPath;
        private readonly object inner;

        private CancellationTokenSource cancelToken;

        public ZipArchive(string file){
            var type = typeof(System.IO.Packaging.Package).Assembly.GetType("MS.Internal.IO.Zip.ZipArchive");
            var openFile = type.GetMethod("OpenOnFile",BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            this.file = file;
            this.extractPath = Path.GetFullPath(file+".out/");
            this.inner = openFile.Invoke(null,new object[]{ file, FileMode.Open, FileAccess.Read, FileShare.Read, false });
        }

        public IEnumerable<ArchiveEntry> GetEntries(){
            var getFiles = inner.GetType().GetMethod("GetFiles",BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach(var obj in (IEnumerable)getFiles.Invoke(inner,null)){
                yield return new ArchiveEntry(obj);
            }
        }

        public string Extract(){
            Directory.CreateDirectory(extractPath);

            foreach(ArchiveEntry entry in GetEntries()){
                ExtractFile(entry);
            }

            return extractPath;
        }

        public CancellationTokenSource ExtractAsync(Action<string> onFinished){
            if (cancelToken != null)cancelToken.Cancel();

            cancelToken = new CancellationTokenSource();

            new Task(() => {
                foreach(ArchiveEntry entry in GetEntries()){
                    if (cancelToken.IsCancellationRequested){
                        Dispose();
                        return;
                    }

                    ExtractFile(entry);
                }
                    
                onFinished(extractPath);
                Dispose();
            }).Start();

            return cancelToken;
        }

        private void ExtractFile(ArchiveEntry entry){
            if (entry.IsFolder)return;

            string dirName = Path.GetDirectoryName(entry.Path);
            if (dirName == null)return;

            Directory.CreateDirectory(Path.Combine(extractPath,dirName));

            using(Stream stream = entry.CreateStream()){
                using(var fileStream = System.IO.File.Create(Path.Combine(extractPath,entry.Path))){
                    stream.Seek(0,SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
            }
        }

        public void Dispose(){
            ((IDisposable)inner).Dispose();
        }

        public class ArchiveEntry{
            private readonly object inner;

            public string Path { get { return (string)GetProperty("Name"); } }
            public bool IsFolder { get { return (bool)GetProperty("FolderFlag"); } }

            public ArchiveEntry(object inner){
                this.inner = inner;
            }

            public Stream CreateStream(){
                var getStream = inner.GetType().GetMethod("GetStream",BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                return (Stream)getStream.Invoke(inner,new object[]{ FileMode.Open, FileAccess.Read });
            }

            private object GetProperty(string name){
                return inner.GetType().GetProperty(name,BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(inner,null);
            }
        }
    }
}
