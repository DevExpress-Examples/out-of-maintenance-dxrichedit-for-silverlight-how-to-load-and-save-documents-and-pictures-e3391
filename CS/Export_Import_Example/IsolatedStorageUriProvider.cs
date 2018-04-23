using System;
using System.IO;
using System.IO.IsolatedStorage;
using DevExpress.XtraRichEdit.Services;
using DevExpress.XtraRichEdit.Utils;

namespace Export_Import_Example
{
    #region #uriprovider
    public class IsolatedStorageUriProvider : IUriProvider
    {
        string rootDirecory;
        IsolatedStorageFile store;

        public IsolatedStorageUriProvider(string rootDirectory)
        {
            if (String.IsNullOrEmpty(rootDirectory))
                Exceptions.ThrowArgumentException("rootDirectory", rootDirectory);
            this.rootDirecory = rootDirectory;
            this.store = IsolatedStorageFile.GetUserStoreForApplication();
        }

        public string CreateCssUri(string rootUri, string styleText, string relativeUri)
        {
            string cssDir = String.Format("{0}\\{1}", this.rootDirecory, rootUri.Trim('/'));
            if (!store.DirectoryExists(cssDir))
                store.CreateDirectory(cssDir);
            string cssFileName = String.Format("{0}\\style.css", cssDir);
            using (IsolatedStorageFileStream fStream = store.OpenFile(cssFileName, FileMode.Append)) {
                using (StreamWriter streamWriter = new StreamWriter(fStream))
                    streamWriter.Write(styleText);
            }
            return GetRelativePath(cssFileName);
        }
        public string CreateImageUri(string rootUri, DevExpress.XtraRichEdit.Utils.RichEditImage image, string relativeUri)
        {
            string imagesDir = String.Format("{0}\\{1}", this.rootDirecory, rootUri.Trim('/'));
            if (!store.DirectoryExists(imagesDir))
                store.CreateDirectory(imagesDir);
            string imageName = String.Format("{0}\\{1}.png", imagesDir, Guid.NewGuid());
            using (IsolatedStorageFileStream fStream = store.CreateFile(imageName)) {
                using (MemoryStream ms = new MemoryStream(image.GetImageBytesSafe(RichEditImageFormat.Png)))
                    ms.WriteTo(fStream);
            }
            return GetRelativePath(imageName);
        }
        string GetRelativePath(string path)
        {
            string substring = path.Substring(this.rootDirecory.Length);
            return substring.Replace("\\", "/").Trim('/');
        }
    }
    #endregion #uriprovider
}
