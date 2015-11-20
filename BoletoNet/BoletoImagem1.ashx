<%@ WebHandler Language="VB" Class="BoletoImagem1" %>

Imports System
Imports System.Web
Imports Impactro.Cobranca
Imports System.Drawing

Public Class BoletoImagem1 : Implements IHttpHandler
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        'Definição dos dados do cedente
        Dim Cedente As New CedenteInfo
        Cedente.Cedente = "Teste de Cedente para Banco Real"
        Cedente.Banco = "237"
        Cedente.Agencia = "1234"
        Cedente.Conta = "1234567-8"
        Cedente.Carteira = "6"
        ' Novas Exigencias da FREBABAN: Exibir endereço e CNPJ no campo de emitente!
        Cedente.CNPJ = "12.345.678/0001-12"
        Cedente.Endereco = "Rua Sei lá aonde, 123 - Brás, São Paulo/SP"
        
        'Definição dos dados do sacado
        Dim Sacado As New SacadoInfo
        Sacado.Sacado = "Fabio Ferreira (Teste para homologação)"
        Sacado.Documento = "123.456.789-12"
        Sacado.Endereco = "Rua xxx, 1001 ap 24"
        Sacado.Cidade = "São Paulo"
        Sacado.Bairro = "Centro"
        Sacado.Cep = "12345-000"
        Sacado.UF = "SP"

        'Definição dos dados do boleto
        Dim Boleto As New BoletoInfo
        Boleto.NossoNumero = "123"
        Boleto.NumeroDocumento = "123"
        Boleto.ValorDocumento = 123.45
        Boleto.DataDocumento = Now
        Boleto.DataVencimento = Now
        Boleto.Instrucoes = "Boleto de demostração (NÃO PAGUE)"

        Dim bol As New Boleto
        bol.MakeBoleto(Cedente, Sacado, Boleto)
        
        'Os paranetros abaixo são opcional, mas podem ser customizados
        
        'bol.Carne = True 'Exibe em formato de carne
        'bol.ExibeReciboSacado = False 'Exibe o recibo do sacado
        'bol.ExibeReciboLinhaDigitavel = False 'Exibe a linha digitável também no Recibo do sacado
        
        'É possivel personalizar a escala, mas tem que também redefinir o tamanho das fontes
        'bol.Escala = 4.5
        
        ' Tamanho dos titulos dos campos
        'FieldDraw.FontCampoName = "Verdana"
        'FieldDraw.FontCampoSize = 5 * bol.Escala / 3
        'FieldDraw.FontCampoStyle = FontStyle.Regular

        ' Tamanho dos valores dos campos
        'FieldDraw.FontValorName = "Arial"
        'FieldDraw.FontValorSize = 7 * bol.Escala / 3
        'FieldDraw.FontValorStyle = FontStyle.Bold

        ' Tamanho da linha digitável
        'FieldDraw.FontLinhaSize = 9 * bol.Escala / 3
        'FieldDraw.FontLinhaName = "Arial"
        'FieldDraw.FontLinhaStyle = FontStyle.Bold

        ' Recria as instancias dos fontes
        'FieldDraw.Reset()
        
        Dim img As Image = bol.ImageBoleto()
        img.Save(context.Response.OutputStream, Imaging.ImageFormat.Png)
        
        context.Response.ContentType = "image/png"

    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class