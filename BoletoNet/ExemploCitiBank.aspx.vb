Imports Impactro.Cobranca

Partial Class ExemploCitiBank
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Definição dos dados do cedente 
        Dim Cedente As New CedenteInfo
        Cedente.Cedente = "Teste Citi!!!"
        Cedente.Banco = "745-5"
        Cedente.Agencia = "1234"
        Cedente.Conta = "1234567"
        Cedente.CodCedente = "123456789" 'Conta COSMOS (somente numeros, sem o indice - 1 digito) 0/123456/789
        Cedente.Modalidade = "650" '3 últimos dígitos do campo de identificação da empresa no CITIBANK

        'Definição dos dados do sacado
        Dim Sacado As New Impactro.Cobranca.SacadoInfo
        Sacado.Sacado = "Fabio Ferreira (Teste)"
        Sacado.Documento = "123.456.789-99"
        Sacado.Endereco = "Av. Paulista, 1234"
        Sacado.Cidade = "São Paulo"
        Sacado.Bairro = "Centro"
        Sacado.Cep = "12345-123"
        Sacado.UF = "SP"

        'Definição dos dados do boleto
        Dim Boleto As New BoletoInfo
        Boleto.NossoNumero = "66660000003"
        Boleto.NumeroDocumento = "000000008"
        Boleto.ValorDocumento = 350
        Boleto.DataDocumento = Now
        Boleto.DataVencimento = New Date(2002, 5, 5)

        'Monta o boleto
        AddHandler bltPag.Boleto.onMontaCampoLivre, AddressOf MontaCampoLivre
        bltPag.MakeBoleto(Cedente, Sacado, Boleto)

    End Sub

    Public Function MontaCampoLivre(ByVal blt As Boleto) As String
        Dim cLivre As String
        Dim cCodIOF As String = CobUtil.Right(blt.Modalidade, 2)
        Dim cContaCOSMOS As String = CobUtil.Right(blt.CodCedente, 10)
        Dim cNossoNumero As String = CobUtil.Right(blt.NossoNumero, 12)
        '1 + 2 + 10 + 12 => 25 OK
        cLivre = "4" & cCodIOF & cContaCosmos & cNossoNumero
        Return cLivre
    End Function

End Class
