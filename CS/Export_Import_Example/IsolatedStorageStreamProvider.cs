using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using DevExpress.XtraRichEdit.Services;
using System.IO.IsolatedStorage;

namespace Export_Import_Example
{
    #region #streamprovider
    public class IsolatedStorageStreamProvider : IUriStreamProvider
    {
        string sourceUri;

        public IsolatedStorageStreamProvider(string sourceUri)
        {
            this.sourceUri = sourceUri;
        }

        #region IUriStreamProvider Members
        Stream IUriStreamProvider.GetStream(string uri)
        {
            if (uri.ToLower().StartsWith("http"))
                return null;
            string path = sourceUri + uri.Replace("/", "\\");
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForApplication()) {
                return store.OpenFile(path, FileMode.Open);
            }
        }
        #endregion
    }
    #endregion #streamprovider
}

