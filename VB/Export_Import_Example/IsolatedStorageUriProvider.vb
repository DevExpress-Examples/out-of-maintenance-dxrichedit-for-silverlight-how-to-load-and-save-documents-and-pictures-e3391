Imports System
Imports System.IO
Imports System.IO.IsolatedStorage
Imports DevExpress.Office.Services
Imports DevExpress.XtraRichEdit.Utils
Imports DevExpress.Office.Utils

Namespace Export_Import_Example
    #Region "#uriprovider"
    Public Class IsolatedStorageUriProvider
        Implements IUriProvider

        Private rootDirecory As String
        Private store As IsolatedStorageFile

        Public Sub New(ByVal rootDirectory As String)
            If String.IsNullOrEmpty(rootDirectory) Then
                Throw New ArgumentException("rootDirectory value is invalid", rootDirectory)
            End If
            Me.rootDirecory = rootDirectory
            Me.store = IsolatedStorageFile.GetUserStoreForApplication()
        End Sub

        Public Function CreateCssUri(ByVal rootUri As String, ByVal styleText As String, ByVal relativeUri As String) As String Implements IUriProvider.CreateCssUri
            Dim cssDir As String = String.Format("{0}\{1}", Me.rootDirecory, rootUri.Trim("/"c))
            If Not store.DirectoryExists(cssDir) Then
                store.CreateDirectory(cssDir)
            End If
            Dim cssFileName As String = String.Format("{0}\style.css", cssDir)
            Using fStream As IsolatedStorageFileStream = store.OpenFile(cssFileName, FileMode.Append)
                Using streamWriter As New StreamWriter(fStream)
                    streamWriter.Write(styleText)
                End Using
            End Using
            Return GetRelativePath(cssFileName)
        End Function
        Public Function CreateImageUri(ByVal rootUri As String, ByVal image As DevExpress.Office.Utils.OfficeImage, ByVal relativeUri As String) As String Implements IUriProvider.CreateImageUri
            Dim imagesDir As String = String.Format("{0}\{1}", Me.rootDirecory, rootUri.Trim("/"c))
            If Not store.DirectoryExists(imagesDir) Then
                store.CreateDirectory(imagesDir)
            End If
            Dim imageName As String = String.Format("{0}\{1}.png", imagesDir, Guid.NewGuid())
            Using fStream As IsolatedStorageFileStream = store.CreateFile(imageName)
                Using ms As New MemoryStream(image.GetImageBytesSafe(OfficeImageFormat.Png))
                    ms.WriteTo(fStream)
                End Using
            End Using
            Return GetRelativePath(imageName)
        End Function
        Private Function GetRelativePath(ByVal path As String) As String
            Dim substring As String = path.Substring(Me.rootDirecory.Length)
            Return substring.Replace("\", "/").Trim("/"c)
        End Function
    End Class
    #End Region ' #uriprovider
End Namespace
