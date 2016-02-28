using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CodeStatistics.Input.Methods{
    class ZipExtraction{
        class Archive : IDisposable{
            private readonly string file;
            private readonly object inner;

            public Archive(string file){
                var type = typeof(System.IO.Packaging.Package).Assembly.GetType("MS.Internal.IO.Zip.ZipArchive");
                var openFile = type.GetMethod("OpenOnFile",BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                this.file = file;
                this.inner = openFile.Invoke(null,new object[]{ file, FileMode.Open, FileAccess.Read, FileShare.Read, false });
            }

            public IEnumerable<ArchiveEntry> GetEntries(){
                var getFiles = inner.GetType().GetMethod("GetFiles",BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                foreach(var obj in (IEnumerable)getFiles.Invoke(inner,null)){
                    yield return new ArchiveEntry(obj);
                }
            }

            public string Extract(){
                string path = Path.GetFullPath(file+".out/");
                Directory.CreateDirectory(path);

                foreach(ArchiveEntry entry in GetEntries()){
                    if (entry.IsFolder)continue;

                    Directory.CreateDirectory(Path.Combine(path,Path.GetDirectoryName(entry.Path)));

                    using(Stream stream = entry.CreateStream()){
                        using(var fileStream = System.IO.File.Create(Path.Combine(path,entry.Path))){
                            stream.Seek(0,SeekOrigin.Begin);
                            stream.CopyTo(fileStream);
                        }
                    }
                }

                return path;
            }

            public void Dispose(){
                ((IDisposable)inner).Dispose();
            }
        }

        class ArchiveEntry{
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
