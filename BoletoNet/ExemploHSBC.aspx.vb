Imports Impactro.Cobranca

Partial Class ExemploHSBC
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'De acordo com o exemplo da documentação: HSBC-Cobrança-CNR.pdf, pagina 13
        '6.5.1 – Exemplo de Cálculo do Dígito de Autoconferência (DAC)
        'Tomando como base para o exemplo os dados do subitem 5.3:
        'Código do HSBC na Câmara de Compensação ....................... 399
        'Tipo de Moeda (Real) ............................................ 9
        'Fator de Vencimento (Data de Vencimento 04/07/2008) .......... 3923
        'Valor do Documento (R$ 1.200,00) ....................... 0000120000
        'Código do Cedente ......................................... 8351202
        'Código do Documento (sem os 3 dígitos calculados) ... 0000239104761
        'Data de Vencimento no Formato Juliano ........................ 1868
        'Código do Produto CNR ........................................... 2

        'Definição dos dados do cedente
        Dim Cedente As New CedenteInfo
        Cedente.Cedente = "IMPACTRO Informática (teste)"
        Cedente.Banco = "399"
        Cedente.Agencia = "1566"
        Cedente.Conta = "111111"
        Cedente.CodCedente = "4076729" '"09110011663"
        Cedente.Carteira = "01" 'Carteira CNR: Sem Registro
        Cedente.Modalidade = "4" 'Vincula: “vencimento”, “código do cedente” e “código do documento”
        'Cedente.Modalidade = "5" 'Vincula: “código do cedente” e “código do documento”

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

        Boleto.ValorDocumento = 923.81
        Boleto.DataVencimento = CDate("25/07/2010")
        Boleto.NossoNumero = "50311"
        Boleto.NumeroDocumento = Boleto.NossoNumero
        Boleto.DataDocumento = Now
        Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente"

        'Monta o boleto com os dados específicos nas classes
        'AddHandler bltPag.MontaCampoLivre, AddressOf HSBCRegistrado
        bltPag.MakeBoleto(Cedente, Sacado, Boleto)

        'Só pode-se ler a linha digitavel ou o codigo de barras apois executar o 'CalculaBoleto()' ou  'MakeBoleto'
        Me.lblCodBar.Text = bltPag.Boleto.CodigoBarrasFormatado(New Integer() {7, 13, 4, 1})
        Me.lblLinhaDigitavel.Text = bltPag.Boleto.LinhaDigitavel

    End Sub

    Function HSBCRegistrado(ByVal blt As Boleto) As String

        'Posição de Posição até Tamanho Conteúdo

        '03 Código do HSBC na compensação. Igual a “399”
        '01 Tipo de Moeda. Real igual a “9” · Moeda variável igual a “0”
        '01 Dígito de autoconferência do código de barras (DAC). Ver orientação de cálculo a seguir.
        '04 Fator de vencimento (obrigatório a partir de 03/07/2000)

        '10 Valor do documento / título. Para título em moeda variável, ou título com valor zerado ou não definido, gravar “zeros”.
        '11 Número Bancário ( Nosso Número ).

        '11 Código do Cedente composto por:
        '	- 4 posições ( 31 a 34 ) = Código da Agência.
        '	- 7 posições ( 35 a 41 ) = Conta de cobrança.

        '01 Código da carteira = “00”
        '01 Código do aplicativo Cobrança (COB) = “1”

        Dim cLivre As String
        Dim cNossoNumero As String = CobUtil.Right(blt.NossoNumero, 11)
        Dim cAgencia As String = CobUtil.Right(blt.Agencia, 4)
        Dim cConta As String = CobUtil.Right(blt.Conta.Replace("-", ""), 7)

        blt.NossoNumeroExibicao = cNossoNumero

        Dim cSuper As String = CobUtil.Modulo11Especial("1" & cNossoNumero, 9)

        cLivre = cNossoNumero & cAgencia & cConta & "001"

        Return cLivre

    End Function

End Class
