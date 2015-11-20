<%@ WebHandler Language="VB" Class="SendForm" %>

Imports System
Imports System.Web
Imports System.Net.Mail

Public Class SendForm : Implements IHttpHandler
    
    'Exemplo de uma rotina util para envio de dados de um formulári generico
    'informe apenas: <form action=sendform.ashx method=post>
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim cBody As String
        
        'Obtem a lista de campos e seus respectivos valores para formar a mensagem
        cBody = String.Format("Dados Enviados em {0:dd}/{0:MM}/{0:yy} as {0:HH}:{0:mm}" & vbCrLf & vbCrLf, Now)
        For Each key As String In context.Request.Form
            cBody &= key & ": " & context.Request.Form(key) & vbCrLf
        Next
        
        'Configurações do Cliente SMTP basico
        Dim smtp As New SmtpClient("localhost")
        
        'Para configurar um Cliente SMTP com autenticação: exemplo Google Application
        'Dim smtp As New SmtpClient("smtp.gmail.com", 587)
        'smtp.UseDefaultCredentials = False
        'smtp.EnableSsl = True
        'smtp.Credentials = New Net.NetworkCredential("email@dominio.com.br", "senha")

        'Configurações do e-mail que será enviado
        Dim email As New MailMessage()
        email.From = New MailAddress("emailorigem@dominio.com.br")
        email.To.Add("emaildestino@dominio.com.br")
        email.Subject = "Dados do Formulário"
        email.Body = cBody
        
        'envia o e-mail
        smtp.Send(email)
        
        'Apos enviar o e-mail redireciona para alguma outra página
        context.Response.Redirect("http://www.impactro.com")
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class
