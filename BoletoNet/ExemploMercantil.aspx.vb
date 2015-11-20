Imports Impactro.Cobranca

Partial Class ExemploMercantil
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Definição dos dados do cedente
        Dim Cedente As New CedenteInfo
        Cedente.Cedente = "IMPACTRO Informática (teste)"
        Cedente.Banco = "389"
        Cedente.Agencia = "1234-5"
        Cedente.Conta = "123456-7"
        Cedente.CodCedente = "999888777" 'Contrato
        Cedente.Modalidade = "2" 'Sem desconto

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
        Boleto.NossoNumero = "112233"
        Boleto.NumeroDocumento = "112233"
        Boleto.ValorDocumento = 75
        Boleto.DataDocumento = Now
        Boleto.DataVencimento = CDate("30/07/2000")
        Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente"

        'monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto)

    End Sub
End Class
