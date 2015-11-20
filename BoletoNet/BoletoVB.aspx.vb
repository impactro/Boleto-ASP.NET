Imports Impactro.Cobranca

Partial Class BoletoVB
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Definição dos dados do cedente - QUEM RECEBE / EMITE
        Dim Cedente As New CedenteInfo
        Cedente.Cedente = "Teste do Cedente"
        Cedente.Banco = "409-0"
        Cedente.Agencia = "1234"
        Cedente.Conta = "123456-0"
        Cedente.Carteira = "Especial"
        Cedente.Modalidade = ""
        Cedente.Convenio = "1878794"   ' ATENÇÃO: Alguns Bancos usam um código de convenio para remapear a conta do clientes
        Cedente.CodCedente = "1878794" ' outros bancos chama isto de Codigo do Cedente ou Código do Cliente

        ' Novas Exigencias da FREBABAN: Exibir endereço e CNPJ no campo de emitente!
        Cedente.CNPJ = "12.345.678/0001-12"
        Cedente.Endereco = "Rua Sei lá aonde, 123 - Brás, São Paulo/SP"

        ' outros usam os 2 campos para controles distintos!
        ' Veja com atenção qual é o seu caso e qual destas variáveis deve ser usadas!
        ' Olhe sempre os exemplos em ASP.Net se tiver dúvidas, pois lá há um exemplo para cada banco
        Cedente.UsoBanco = "CVT 7744-5" 'Obrigatório para unibanco

        'Definição dos dados do sacado - QUEM PAGA
        Dim Sacado As New SacadoInfo
        'Sacado.SacadoCOD = "teste" 'código opcional interno do sacado (pode ser um /ID numeroco ou um udentificado alfacumerico)
        Sacado.Sacado = "Fabio Ferreira (Teste para homologação)"
        Sacado.Documento = "123.456.789-12"
        Sacado.Endereco = "Rua xxx, 1001 ap 24"
        Sacado.Cidade = "São Paulo"
        Sacado.Bairro = "Centro"
        Sacado.Cep = "12345-000"
        Sacado.UF = "SP"

        Dim nCont As Integer = 0
        'If Session("Cont") Is Nothing Then
        '    Session("Cont") = 7566
        'Else
        '    nCont = Session("Cont")
        '    nCont += 1
        '    Session("Cont") = nCont
        'End If

        nCont = txtNossoNumero.Text

        'Definição dos dados do boleto
        Dim Boleto As New BoletoInfo
        Boleto.NossoNumero = nCont
        Boleto.NumeroDocumento = nCont
        Boleto.ValorDocumento = txtValor.Text
        Boleto.DataDocumento = Now
        'Boleto.DataVencimento = CDate(txtVencimento.Text) 'Campo obrigatório

        'Campos especiais para sistemas de pagamento online e emissão com
        'valores de multa e desocntos (acrescimos/abatimentos), quantidades, e valor cobrado
        'ATENÇÃO: Estes valores são apenas informativos em tela, o valor de fato que será cobrado
        'será o "ValorDocumento", mas na maioria dos casos estes campos abaixo não precisam ser configurados
        'pois são preenchidos manualmente pelo caixa na hora do pagamento
        Boleto.Quantidade = 3
        Boleto.ValorUnitario = 10
        Boleto.ValorCobrado = 20
        Boleto.ValorAcrescimo = 30
        Boleto.ValorDesconto = 40
        Boleto.ValorMora = 50
        Boleto.ValorOutras = 60

        'Obrigatório para o UNIBANCO
        Boleto.LocalPagamento = "Até o vencimento, pagável em qualquer banco. Após o vencimento, em qualquer agência do Unibanco"
        Boleto.Aceite = "N"
        Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente"
        Boleto.Demonstrativo = "Exemplo da descição dos serviços..."

        'monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto)

        'é posivel imprimir (exibir) em tela um valor diferente do que está no calculo do código de barras
        'para isso primeiro efetue a configuração normalmente do valor, e depois reconfigure como no exemplo abaixo
        bltPag.Boleto.ValorDocumento = Boleto.ValorDocumento * 10

    End Sub

End Class
