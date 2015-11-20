Imports Impactro.Cobranca
Imports Impactro.WebControls

Partial Class BoletoCSS
    Inherits System.Web.UI.Page

    'Neste exemplo é mostrado uma forma de layout diferente
    'E como inserir controles de boleto dinamicamente

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim Cedente As New CedenteInfo
        Cedente.Cedente = "IMPACTRO Informática (teste)"
        Cedente.Banco = "104"
        Cedente.Agencia = "1234-8"
        Cedente.Conta = "123456-7"
        Cedente.Carteira = "8"          ' Código da Carteira
        Cedente.Convenio = "12"         ' CNPJ do PV da conta do cliente
        Cedente.CodCedente = "12345"    ' Código do Cliente(cedente)

        'Codigo de cedente especial, com 15 DIgitos (obrigatório Zeros a Frente)
        Cedente.CodCedente = "059100300000608"

        'Definição dos dados do sacado
        Dim Sacado As New SacadoInfo
        Sacado.Sacado = "Fabio Ferreira (Teste)"

        'Definição das Variáveis do boleto
        Dim Boleto As New BoletoInfo
        Boleto.NossoNumero = "8200102757"
        Boleto.NumeroDocumento = "8200102757"
        Boleto.ValorDocumento = 405.8
        Boleto.DataDocumento = Now
        Boleto.DataVencimento = CDate("07/11/2006")

        'Adiciona o WebControl do boleto na tela
        Dim blt As New BoletoWeb
        Me.Form.Controls.Add(blt)

        'monta o boleto com os dados específicos nas classes
        blt.MakeBoleto(Cedente, Sacado, Boleto)

        'configura o layout do boleto
        blt.CssCell = "BolCell"
        blt.CssField = "BolField"
        blt.EspecialColor = Drawing.Color.Silver
        blt.ImageLogo = "SeuLogo.gif"
        blt.ImageType = BoletoImageType.gif
        AddHandler blt.ConfigureTable, AddressOf ConfigureTableBoleto
        blt.MakeBoleto(Cedente, Sacado, Boleto)

    End Sub


    Public Sub ConfigureTableBoleto(ByVal tb As Table)
        tb.BorderStyle = BorderStyle.Solid
        tb.BorderColor = Drawing.Color.Black
        tb.BorderWidth = New Unit("1px")
        tb.CellSpacing = 0
    End Sub

End Class
