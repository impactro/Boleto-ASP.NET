Imports Impactro.Cobranca

Partial Class ExemploBradesco
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Definição dos dados do cedente
        Dim Cedente As New Impactro.Cobranca.CedenteInfo
        Cedente.Cedente = "IMPACTRO Informática (teste)"
        Cedente.Banco = "237"
        Cedente.Agencia = "6789-8"
        Cedente.Conta = "9999-5"
        Cedente.Carteira = "06"
        'Cedente.Modalidade = "0490"   'Prefixo do nosso numero a ser utilizado

        'Caso especial - customizado (veja a rotina: bltPag_MontaCampoLivre)
        'Cedente.Modalidade = 901 'Modalidade
        'Cedente.CodCedente = 4 'Mês
        'AddHandler bltPag.MontaCampoLivre, AddressOf bltPag_MontaCampoLivre

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
        Boleto.NossoNumero = "68717" 'Numero que retorna digito 'P'
        Boleto.NumeroDocumento = Cedente.Carteira & "/" & Boleto.NossoNumero
        Boleto.ValorDocumento = 215.88
        Boleto.DataDocumento = Now
        Boleto.DataVencimento = CDate("07/04/2009")
        Boleto.Instrucoes = "coloque aqui as instruções..."

        'monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto)

        lblCodBar.Text = bltPag.Boleto.CodigoBarrasFormatado(New Integer() {4, 2, 4, 7, 7, 1})

    End Sub

    Protected Function bltPag_MontaCampoLivre(ByVal blt As Impactro.Cobranca.Boleto) As String 'Handles bltPag.MontaCampoLivre
        Dim cCampoLivre As String
        Dim cAgenciaNumero As String = CobUtil.Right(blt.Agencia.Split("-")(0), 4) 'agencia sem digito
        Dim cContaNumero As String = CobUtil.Right(blt.Conta.Split("-")(0), 7) 'conta sem digito
        Dim cCarteira As String = CobUtil.Right(blt.Carteira, 2)
        Dim cMes As String = CobUtil.Right(blt.CodCedente, 2)
        Dim cModalidade As String = CobUtil.Right(blt.Modalidade, 3)
        Dim cNossoNumero As String = CobUtil.Right(blt.NossoNumero, 6)

        'ao todo tem que dar 25 posições sendo:
        ' Nosso numero com 11 = 2 + 3 + 6 (Mês + Modalidade + Numero)
        cCampoLivre = cAgenciaNumero & _
               cCarteira & _
               cMes & _
               cModalidade & _
               cNossoNumero & _
               cContaNumero & "0"

        Dim nTotalNumero As Integer = CobUtil.Modulo11Total(cCarteira & cMes & cModalidade & cNossoNumero, 7)
        Dim DAC As String
        nTotalNumero *= 10
        Dim nResto As Integer = nTotalNumero Mod 11
        If nResto = 10 Then
            DAC = "P"
        Else
            DAC = nResto
        End If
        blt.NossoNumeroExibicao = cMes + cModalidade + cNossoNumero + "." + DAC

        Return cCampoLivre

    End Function

End Class
