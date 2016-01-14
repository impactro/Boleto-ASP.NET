Imports Impactro.Cobranca

Partial Class ExemploCaixa2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Baseado no IPTE: 10498.20010 72393.059109 03000.006084 1 28390000018750
        'Utilizando o programa "FuncTeste_DecodIPTE.aspx"
        'Obten-se o código de barras: 1049.1.28390000018750.8200172393059100300000608
        'O campo livre são os ultimos 25 caracteres do código de barras: 8200172393059100300000608
        'De acordo com o modelo estes caracteres de acordo com são compostos por:
        '8200172393059100300000608
        '82xxxnnnnn059100300000608
        'onde:
        ' xxx, código de operação
        ' nnnnn, número sequencial do nosso numero
        ' 82 constante
        ' 059100300000608 código da agencia cedente

        'Definição dos dados do cedente
        Dim Cedente As New Impactro.Cobranca.CedenteInfo
        Cedente.Cedente = "Impactro (Teste)"
        Cedente.Banco = "104-0"
        Cedente.Agencia = "9999"
        Cedente.Conta = "608-3"
        Cedente.CodCedente = "059100300000608"
        'Para que o campo livre tenha 25 caracteres
        'o código de cedente deve ser representado completamente (15 digitos)
        'o digito final será calculado pela rotina modulo 11 padrão

        'Definição dos dados do sacado
        Dim Sacado As New Impactro.Cobranca.SacadoInfo
        Sacado.Sacado = "Fabio Ferreira (Teste)"
        Sacado.Documento = "123.456.789-99"
        Sacado.Endereco = "Av. Paulista, 1234"
        Sacado.Cidade = "São Paulo"
        Sacado.Bairro = "Centro"
        Sacado.Cep = "12345-123"
        Sacado.UF = "SP"

        'Definição das Variáveis do boleto
        Dim Boleto As New Impactro.Cobranca.BoletoInfo
        Dim nOperacao As String = 1
        Dim nNossoNumero As String = 72393

        Boleto.DataDocumento = Now 'Data de geração
        Boleto.NossoNumero = "82" & ValZeros(nOperacao, 3) & ValZeros(nNossoNumero, 5)
        'O nosso numero deve ter obrigatóriamente 10 digitos no total
        'com isso o campo livre completa os 25 caracteres
        Boleto.NumeroDocumento = Boleto.NossoNumero
        Boleto.DataVencimento = CDate("07/01/2015")
        Boleto.ValorDocumento = 405.8

        'Boleto.PercentualMora = 0.003
        'Boleto.PercentualMulta = 0.02
        'Boleto.CalculaMultaMora = True
        Boleto.ValorMora = 1.23 ' usado na remessa quando não cor calculado pelo componente
        'Boleto.ValorCobrado = 12345 'quando existe substitui o valor do bolet
        Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente"

        'Remapeia a rotina de calculo por uma personalizada
        'Desta forma é possive gerenciar o boleto por uma rotina externa
        AddHandler bltPag.MontaCampoLivre, AddressOf MontaCampoLiveCaixa
        'monta o boleto com os dados específicos nas classes
        bltPag.ExibeReciboLinhaDigitavel = True
        bltPag.MakeBoleto(Cedente, Sacado, Boleto)

        'Atualiza o valor total
        'bltPag.Boleto.ValorCobrado = bltPag.Boleto.ValorMoraMulta + bltPag.Boleto.ValorDocumento

        'Recalcula o código de barras e linha digitável agora com o valor cobrado
        bltPag.Boleto.CalculaBoleto()


    End Sub

    ''' <summary>
    ''' Rotina externa responsável por montar a logica de geração do campolivre e configurar os demais campos de exibição se nescessário
    ''' </summary>
    ''' <param name="blt">Instancia da classe gerador com os valores padrão</param>
    ''' <returns>Deverá retornar uma string com 25 posições que irá compor o código de barras</returns>
    Public Function MontaCampoLiveCaixa(ByVal blt As Impactro.Cobranca.Boleto) As String
        'Restaura as variáveis informados nas estruturas do boleto
        Dim cNossoNumero As String = blt.NossoNumero ' 10 digitos
        Dim cCodCedente As String = blt.CodCedente '15 digitos
        'Monta o campo livre
        Dim cLivre As String = cNossoNumero + cCodCedente ' totaliza 25 digitos

        'Valor a ser exibido no campo nosso numero
        blt.NossoNumeroExibicao = cNossoNumero & "-" & CobUtil.Modulo11Padrao(cNossoNumero, 9)

        'Adiciona o digito final no código do cedente
        cCodCedente &= CobUtil.Modulo11Padrao(cCodCedente, 9)
        'Valor a ser exibido no campo Agencia/Conta
        blt.AgenciaConta = _
            cCodCedente.Substring(0, 4) & "." & _
            cCodCedente.Substring(4, 3) & "-" & _
            cCodCedente.Substring(7, 8) & "." & _
            cCodCedente.Substring(15, 1)

        'Retorna o Camo Livre
        Return cLivre

    End Function

    ''' <summary>
    ''' Transforma uma string em um numero completado por zeros a esquerda, e os numeros a direita
    ''' </summary>
    ''' <param name="nVal"></param>
    ''' <param name="nSize"></param>
    ''' <returns></returns>
    ''' <remarks>Equivale ao metodo stativo Util.Right, que alinha os numeros a direita completando por zeros a esquerda</remarks>
    Function ValZeros(ByVal nVal As Integer, ByVal nSize As Integer) As String
        Dim cValor As New String("0", nSize)    'Cria uma string somente com Zeros no tamanho desejado
        cValor &= nVal                          'Concatena simplestente
        cValor = cValor.Substring(cValor.Length - nSize)
        'Elimina os caracteres 'zeros' excessivos na string
        Return cValor
    End Function


End Class
