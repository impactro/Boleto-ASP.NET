Imports Impactro.Cobranca

Partial Class ExemploReal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Definição dos dados do cedente
        Dim Cedente As New CedenteInfo
        Cedente.Cedente = "Teste de Cedente para Banco Real"
        Cedente.Banco = "356"
        Cedente.Agencia = "0064"
        Cedente.Conta = "7724286-3"
        Cedente.Carteira = 57

        'Definição dos dados do sacado
        Dim Sacado As New SacadoInfo
        Sacado.Sacado = "Fabio Ferreira (Teste)"
        Sacado.Documento = "123.456.789-99"
        Sacado.Endereco = "Av. Paulista, 1234"
        Sacado.Cidade = "São Paulo"
        Sacado.Bairro = "Centro"
        Sacado.Cep = "12345-123"
        Sacado.UF = "SP"

        'Definição das Variáveis do boleto
        Dim Boleto As New BoletoInfo

        Boleto.NossoNumero = "5107805"
        Boleto.NumeroDocumento = Boleto.NossoNumero
        Boleto.ValorDocumento = 120
        Boleto.DataDocumento = Now
        Boleto.DataVencimento = CDate("01/05/2009")
        Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente"

        'monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto)

    End Sub

End Class
