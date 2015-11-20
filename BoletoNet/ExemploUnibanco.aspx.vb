Imports Impactro.Cobranca

Partial Class ExemploUnibanco
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Definição dos dados do cedente
        Dim Cedente As New CedenteInfo
        Cedente.Cedente = "IMPACTRO Informática (teste)"
        Cedente.Banco = "409"
        Cedente.Agencia = "0352-2"
        Cedente.Conta = "123789-0"
        Cedente.Carteira = "COB"
        Cedente.CodCedente = "1111111"
        Cedente.Modalidade = "14" '14 digitos de nosso numero
        'Qualquer outro valor na modalidade, ou não especificada, implica em nosso numerocom 7 digitos, e prefixo com o codCedente

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

        Boleto.ValorDocumento = 2335.67
        Boleto.DataVencimento = CDate("12/07/2008")
        Boleto.NumeroDocumento = "6119648609"
        Boleto.NossoNumero = "61196486094"
        Boleto.DataDocumento = Now
        Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente"

        'Monta o boleto com os dados específicos nas classes
        AddHandler bltPag.MontaCampoLivre, AddressOf UnibancoRegistrado
        bltPag.MakeBoleto(Cedente, Sacado, Boleto)

        'Só pode-se ler a linha digitavel ou o codigo de barras apois executar o 'CalculaBoleto()' ou  'MakeBoleto'
        Me.lblCodBar.Text = bltPag.Boleto.CodigoBarras
        Me.lblLinhaDigitavel.Text = bltPag.Boleto.LinhaDigitavel

    End Sub

    Function UnibancoRegistrado(ByVal blt As Boleto) As String
        'Posição	Tamanho	Descrição
        '1 a 3	3	Número de identificação do Unibanco: 409 (número FIXO)
        '4	1	Código da moeda. Real (R$)=9 (número FIXO)
        '5	1	dígito verificador do CÓDIGO DE BARRAS
        'Calculado pelo módulo 11 (página 11), onde deverá ser utilizado as 43 dígitos desta seqüência numérica que dará origem ao CÓDIGO DE BARRAS
        '6 a 9	4	fator de vencimento em 4 algarismos, conforme tabela da página 14 
        '10 a 19	10	valor do título com zeros à esquerda
        '--- CAMPO LIVRE ---
        '20 a 21	2	Código para transação CVT =  04 (número FIXO) '(04=5539-5)
        '22 a 27	6	data de vencimento (AAMMDD)
        '28 a 32	5	Código da agência + dígito verificador
        '33 a 43	11	“Nosso Número” (NNNNNNNNNND) onde D é o dígito a ser calculado pelo Módulo 11 (página 11)
        '44	1	Super dígito do “Nosso Número” onde S é o Super Dígito calculado pelo módulo 11 (página 11) – utilizando os algarismos do “Nosso Número” acrescido do número 1 à esquerda = 1NNNNNNNNNNDS

        Dim cLivre As String
        Dim cAgenciaDIG As String = CobUtil.Right(blt.Agencia.Replace("-", ""), 5)
        Dim cNossoNumero As String = CobUtil.Right(blt.NossoNumero, 10)
        Dim cDAC As String = CobUtil.Modulo11Especial(cNossoNumero, 9)

        blt.NossoNumeroExibicao = cNossoNumero & "-" & cDAC

        cNossoNumero &= cDAC

        Dim cSuper As String = CobUtil.Modulo11Especial("1" & cNossoNumero, 9)

        cLivre = "04" & _
            String.Format("{0:yyMMdd}", blt.DataVencimento) & _
            cAgenciaDIG & _
            cNossoNumero & _
            cSuper

        Return cLivre

    End Function

End Class
