Imports Impactro.Layout

Partial Class RPS_NFe1
    Inherits System.Web.UI.Page

    'Exemplo de geração registro a registro, usando as classes de RPS
    'é possivel gerar arquivos de retorno e remessa usando a mesma logica

    Protected Sub btnTest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTest.Click

        Dim RPSCabecalho As New Reg(Of RPS1Cabecalho)

        RPSCabecalho(RPS1Cabecalho.Inscricao) = 12345678
        RPSCabecalho(RPS1Cabecalho.DataInicio) = New Date(2011, 2, 1)
        RPSCabecalho(RPS1Cabecalho.DataFim) = New Date(2011, 2, 28)
        txtOut.Text = RPSCabecalho.Line & vbCrLf

        Dim RPSDetalhe As New Reg(Of RPS2Detalhe)

        RPSDetalhe(RPS2Detalhe.Indicador) = 1
        RPSDetalhe(RPS2Detalhe.Tomador) = 19221149870
        RPSDetalhe(RPS2Detalhe.RazaoSocial) = "Fábio Ferreira de Souza"

        RPSDetalhe(RPS2Detalhe.Valor) = 12345.67
        RPSDetalhe(RPS2Detalhe.Deducoes) = 987.64999999999998

        RPSDetalhe(RPS2Detalhe.EnderecoTipo) = "R"
        RPSDetalhe(RPS2Detalhe.EnderecoNome) = "Nome da rua"
        RPSDetalhe(RPS2Detalhe.EnderecoNumero) = "6789"
        RPSDetalhe(RPS2Detalhe.EnderecoComplemento) = "ap 123"
        RPSDetalhe(RPS2Detalhe.Bairro) = "Bairro"
        RPSDetalhe(RPS2Detalhe.Cidade) = "São Paulo"
        RPSDetalhe(RPS2Detalhe.UF) = "SP"
        RPSDetalhe(RPS2Detalhe.CEP) = "12345987"

        RPSDetalhe(RPS2Detalhe.eMail) = "fabio@impactro.com.br"

        RPSDetalhe(RPS2Detalhe.Discriminacao) = "teste de RPS|Linha 2|    Linha 3|FIM"

        txtOut.Text &= RPSDetalhe.Line & vbCrLf

        Dim RPSRodape As New Reg(Of RPS9Rodape)

        RPSRodape(RPS9Rodape.Linhas) = 23
        RPSRodape(RPS9Rodape.Total) = 567.88999999999999
        RPSRodape(RPS9Rodape.Deducoes) = 14.42

        txtOut.Text &= RPSRodape.Line & vbCrLf

    End Sub

End Class
