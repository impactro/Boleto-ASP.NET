
Imports System.Collections.Generic
Imports Impactro.Cobranca
Imports Impactro.WebControls

Partial Class BoletoNet_HomologaCaixaVB
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs)
        ' Definição dos dados do cedente
        Dim Cedente As New CedenteInfo()
        Cedente.Cedente = "RHS CONSULT LTDA - EPP"
        Cedente.Endereco = "Estrada Elias Alves da Costa, nº 957 – São João – Itapevi - SP"
        Cedente.CNPJ = "23.047.156/0001-23"
        Cedente.Banco = "104"
        Cedente.Agencia = "1234"
        Cedente.Conta = "12345678-9"
        Cedente.Carteira = "2" ' 1-Registrada ou 2-Sem registro
        Cedente.CodCedente = "123456"
        Cedente.Convenio = "4353" 'CNPJ do PV da conta do cliente = 00.360.305/4353-48 (usado em alguns casos)
        Cedente.Informacoes = _
            "SAC CAIXA: 0800 726 0101 (informações, reclamações, sugestões e elogios)<br/>" & _
            "Para pessoas com deficiência auditiva ou de fala: 0800 726 2492<br/>" & _
            "Ouvidoria: 0800 725 7474 (reclamações não solucionadas e denúncias)<br/>" & _
            "<a href='http://caixa.gov.br' target='_blank'>caixa.gov.br</a>"

        BoletoTextos.LocalPagamento = "PREFERENCIALMENTE NAS CASAS LOTÉRICAS ATÉ O VALOR LIMITE"

        ' Definição dos dados do sacado
        Dim Sacado As New SacadoInfo()
        Sacado.Sacado = "Fabio Ferreira (Teste para homologação)"
        Sacado.Documento = "123.456.789-99"
        Sacado.Endereco = "Av. Paulista, 1234"
        Sacado.Cidade = "São Paulo"
        Sacado.Bairro = "Centro"
        Sacado.Cep = "12345-123"
        Sacado.UF = "SP"
        Sacado.Avalista = "CNPJ: 123.456.789/00001-23"

        ' Para aprovar a homologação junto a caixa é necessário apresentar 10 boletos com os 10 digitos de controle da linha digitável diferentes
        ' E mais outros 10 com o digito de controle do código de barras
        ' Assim a ideia é criar 2 listas para ir memorizando os boletos já validos e deixa-los entrar em tela

        Dim DAC1 As New List(Of Integer)()
        Dim DAC2 As New List(Of Integer)()
        Dim nBoleto As Integer

        For nBoleto = 1001 To 1100

            ' Definição dos dados do boleto de forma sequencial
            Dim Boleto As New BoletoInfo()
            With Boleto
                .NumeroDocumento = nBoleto.ToString()
                .NossoNumero = nBoleto.ToString()
                .ValorDocumento = 123.45
                .DataVencimento = DateTime.Now
                .DataDocumento = DateTime.Now
            End With

            ' Componente HTML do boleto que poderá ser ou não colocado em tela
            Dim blt As New BoletoWeb()

            ' Junta as informações para fazer o calculo
            blt.MakeBoleto(Cedente, Sacado, Boleto)

            ' A instancia 'blt' é apenas un Webcontrol que renderiza o boleto HTML, tudo fica dentro da propriedade 'Boleto'
            blt.Boleto.CalculaBoleto()
            ' 10491.23456 60000.200042 00000.000844 4 67410000012345
            ' 012345678901234567890123456789012345678901234567890123
            ' 000000000111111111122222222223333333333444444444455555
            Dim D1 As Integer = Integer.Parse(blt.Boleto.LinhaDigitavel.Substring(38, 1))
            Dim D2 As Integer = Integer.Parse(blt.Boleto.LinhaDigitavel.Substring(35, 1))
            ' De acordo com o banco:
            ' Todos os Dígitos Verificadores Geral do Código de Barras possíveis(de 1 a 9) ou seja, campo 4 da Representação Numérica
            ' Todas os Dígitos Verificadores do Campo Livre possíveis(de 0 a 9), 10ª posição   do campo 3 da Representação Numérica

            Dim lUsar As Boolean = False

            If Not DAC1.Contains(D1) Then
                lUsar = True
                DAC1.Add(D1)
            End If

            If Not DAC2.Contains(D2) Then
                lUsar = True
                DAC2.Add(D2)
            End If

            If lUsar Then
                ' Adiciona a instancia na tela do boleto valido para uso
                blt.CssCell = "BolCell"
                blt.CssField = "BolField"
                form1.Controls.Add(blt)
            End If

            ' Quando todas as possibilidades concluidas em até 100 boletos, já pode terminar...
            If DAC1.Count = 9 AndAlso DAC2.Count = 10 Then
                Exit For ' o Modulo 11 padrão não tem o digito Zero, mas o especial para calculo Do nosso numero tem
            End If

            ' Se o boleto foi usado e não acabou, então gera uma quebra de linha
            If lUsar Then
                form1.Controls.Add(New LiteralControl("<div style='page-break-after: always'><br/></div>"))
            End If

        Next
        ' Em geral esse teste gera 11 ou mais boletos contemplando todos os casos
        ' Salve como PDF e envie para homologação
    End Sub

End Class
