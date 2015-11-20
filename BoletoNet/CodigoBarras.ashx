<%@ WebHandler Language="VB" Class="CodigoBarras" %>

Imports System
Imports System.Web
Imports System.Drawing
Imports Impactro.Cobranca

Public Class CodigoBarras : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        'Esta sendo parrado o código '1234567890' como string a fim de exemplo
        'Mas esse código pode ver de um banco de dados, request ou session de acordo com a necessidade
        Dim cCodBar As String = context.Request("c")
        If cCodBar Is Nothing Then
            cCodBar = "1234567890"
        End If
        Dim img As Bitmap = CobUtil.BarCodeImage(cCodBar)
        img.Save(context.Response.OutputStream, Imaging.ImageFormat.Gif)
        context.Response.ContentType = "image/gif"
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class