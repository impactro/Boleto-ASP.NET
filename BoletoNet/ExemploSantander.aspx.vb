Imports Impactro.Cobranca

Partial Class ExemploSantander
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Definição dos dados do cedente
        Dim Cedente As New CedenteInfo
        Cedente.Cedente = "Teste de Cedente para Banco Santander"
        Cedente.Banco = "353"
        Cedente.Agencia = "0138"
        Cedente.Conta = "013001609"
        Cedente.CodCedente = "01381874489"
        Cedente.Carteira = "102"

        'É possivel usar o banco 33 (banespa) com a rotina do santander
        'Cedente.useSantander = True
        'Cedente.Banco = "033"

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

        Boleto.NossoNumero = "9666"
        Boleto.NumeroDocumento = Boleto.NossoNumero
        Boleto.ValorDocumento = 1
        Boleto.DataDocumento = Now
        Boleto.DataVencimento = CDate("18/12/2009")
        Boleto.Demonstrativo = "Texto com o descritivo do que tem que ser pago" ' Só é exibido no recibo
        Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente" ' Só é exibido no boleto



        'monta o boleto com os dados específicos nas classes



        bltPag.MakeBoleto(Cedente, Sacado, Boleto)

        'Detalhes do campo livre: 
        'valor '9' fixo         (01 digito)
        'Código do Cedente      (07 digitos)
        'Nosso Numeroo          (12 digitos)
        'Digito do Nosso Numero (01 digito)
        'valor '9' fixo         (01 digito)
        'Carteira               (03 digitos)
        '====================== TOTAL: 25 digitos no campo livre
        CodBar.Text = bltPag.Boleto.CodigoBarrasFormatado(New Integer() {1, 7, 12, 1, 1, 3})

    End Sub
End Class
