Imports Impactro.Cobranca

Partial Class BoletoPost
    Inherits System.Web.UI.Page

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Definição dos dados do cedente
        Dim Cedente As New CedenteInfo
        Cedente.Cedente = Request("CNOME")
        Cedente.Banco = Request("BANCO")
        Cedente.Agencia = Request("AC")
        Cedente.Conta = Request("CC")
        Cedente.CodCedente = Request("CODIGO_CEDENTE")
        Cedente.Carteira = Request("CARTEIRA")
        Cedente.Modalidade = 0 'Request("MODALIDADE")

        'Definição dos dados do sacado
        Dim Sacado As New SacadoInfo
        Sacado.Sacado = Request("SACADO")
        Sacado.Documento = "" 'Request("BANCO")
        Sacado.Endereco = Request("END")
        Sacado.Cidade = "" '"São Paulo"
        Sacado.Bairro = "" '"Centro"
        Sacado.Cep = "" '"12345-123"
        Sacado.UF = "" '"SP"

        'Definição das Variáveis do boleto
        Dim Boleto As New BoletoInfo

        Boleto.NossoNumero = Request("NN")
        Boleto.NumeroDocumento = Request("IDENTIFICACAO")
        Boleto.ValorDocumento = Request("VALOR")
        Boleto.DataDocumento = Now
        Boleto.DataVencimento = Request("VENCIMENTO")
        Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente"

        'monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto)
    End Sub

End Class
