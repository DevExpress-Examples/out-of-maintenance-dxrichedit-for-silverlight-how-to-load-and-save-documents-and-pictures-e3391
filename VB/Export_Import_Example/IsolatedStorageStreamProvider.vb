Imports Microsoft.VisualBasic
Imports System
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports System.IO
Imports DevExpress.Office.Services
Imports System.IO.IsolatedStorage

Namespace Export_Import_Example
	#Region "#streamprovider"
	Public Class IsolatedStorageStreamProvider
		Implements IUriStreamProvider
		Private sourceUri As String

		Public Sub New(ByVal sourceUri As String)
			Me.sourceUri = sourceUri
		End Sub

		#Region "IUriStreamProvider Members"
		Private Function GetStream(ByVal uri As String) As Stream Implements IUriStreamProvider.GetStream 
			If uri.ToLower().StartsWith("http") Then
				Return Nothing
			End If
			Dim path As String = sourceUri & uri.Replace("/", "\")
			Using store As IsolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication()
				Return store.OpenFile(path, FileMode.Open)
			End Using
		End Function
		#End Region
	End Class
	#End Region ' #streamprovider
End Namespace

