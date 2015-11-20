Imports Impactro.Cobranca

Partial Class ExemploCampoLivreCustomizado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Os dados de geração deste exemplo são os mesmos fornecidos pela documentação
        'Configra os resultados na página 16 da documentação

        'Definição dos dados do sacado
        Dim Sacado As New SacadoInfo
        Sacado.Sacado = "Fabio Ferreira (Teste para homologação)"
        Sacado.Documento = "123.456.789-12"
        Sacado.Endereco = "Rua xxx, 1001 ap 24"
        Sacado.Cidade = "São Paulo"
        Sacado.Bairro = "Centro"
        Sacado.Cep = "12345-000"
        Sacado.UF = "SP"

        'Definição dos dados do cedente 
        Dim Cedente As New CedenteInfo
        Cedente.Cedente = "Teste Citi!!!"
        Cedente.Banco = "745-5"
        Cedente.Agencia = "1234"
        Cedente.Conta = "1234567"
        Cedente.CodCedente = "123456789" 'Conta COSMOS (somente numeros, sem o indice - 1 digito) 0/123456/789
        Cedente.Modalidade = "650" '3 últimos dígitos do campo de identificação da empresa no CITIBANK

        'Definição dos dados do boleto
        Dim Boleto As New BoletoInfo
        Boleto.NossoNumero = "66660000003"
        Boleto.NumeroDocumento = "000000008"
        Boleto.ValorDocumento = 350
        Boleto.DataDocumento = Now
        Boleto.DataVencimento = New Date(2002, 5, 5)

        'monta o boleto com os dados específicos nas classes
        AddHandler bltPag.MontaCampoLivre, AddressOf CampoLivre
        'Comente a linha acima (addhandler) para usar a geração padrão pelo componente)
        bltPag.ImageType = Impactro.WebControls.BoletoImageType.gif
        bltPag.ImagePath = "imagens/"
        bltPag.MakeBoleto(Cedente, Sacado, Boleto)

    End Sub

    Public Function CampoLivre(ByVal blt As Impactro.Cobranca.Boleto) As String

        'De acrodo com a documentação (pg 5) segue o calculo do digito do nosso numero
        blt.NossoNumero = CobUtil.Right(blt.NossoNumero, 11) 'Força ter 11 digitos
        Dim cDV As String = CobUtil.Modulo11Padrao(blt.NossoNumero, 9) 'Calcula o digito verificador
        blt.NossoNumeroExibicao = blt.NossoNumero & "." & cDV 'formata o numero com o digito na tela
        blt.NossoNumero &= cDV 'acrescenta o digito no boleto

        'De acordo com a documentação (pg 9) os 25 caracteres do ampo livre são
        'TAM - Descrição
        '  1 - Código do Produto 3 - Cobrança com registro / sem registro
        '  3 - Portfólio, 3 últimos dígitos do campo de identificação da empresa no CITIBANK (Posição 44 a 46 do arquivo retorno)
        '  6 - Base da conta COSMOS (pg 13, veja abaixo)
        '  2 - Seqüência da conta COSMOS (pg 13, veja abaixo)
        '  1 - Dígito Conta COSMOS (pg 13, veja abaixo)
        ' 12 - Nosso Número 
        '----
        ' 25 - Total (campo livre)

        'De acordo com a documentação (pg 13) temos a configuração da CONTA COSMOS
        'Ex.: 0/ 123456/ 789 = Conta Cosmos
        '     0 Índice
        '123456 Base (Posição 24 a 29)
        '    78 Seqüência (Posição 30 a 31)
        '     9 Dígito Verificador (Posição 32)

        'Parametros:
        'O código da conta COSMOS ficará no campo 'CodCedente' somento os numeros 0123456789
        'O código do portfolio ficará no campo 'Modalidade'

        Dim cLivre As String
        cLivre = "3" & _
            blt.Modalidade & _
            blt.CodCedente & _
            blt.NossoNumero

        Return cLivre

    End Function

End Class
