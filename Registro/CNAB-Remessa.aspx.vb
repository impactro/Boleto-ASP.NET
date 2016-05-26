'Este programa foi feito em VB.Net para facilitar a leitura e compreenção de programadores de ASP Classico
'Caso queira em C#, pode converter usando um conversor online: http://www.carlosag.net/tools/codetranslator/

Imports Impactro.Cobranca 'Aqui é uma referencia para o nome dos objetos definidos na biblioteca

Partial Class Registro_CNAB_Remessa
    Inherits System.Web.UI.Page

    'Quando o botão de teste for clicado esta rotina será executada
    Protected Sub btnCedenteTeste_Click(sender As Object, e As EventArgs)
        Try
            lblInfo.Text = ""

            'Obtenho os dados do formulario e defino o cedente
            Dim cedente As New CedenteInfo ' em c# seria: var cedente = new CedenteInfo();
            cedente.Cedente = txtCedente.Text
            cedente.CNPJ = txtCNPJ.Text
            cedente.Endereco = txtEndereco.Text
            cedente.Banco = ddlBancos.SelectedValue
            cedente.Agencia = txtAgencia.Text
            cedente.Conta = txtConta.Text
            cedente.Carteira = txtCarteira.Text
            cedente.Modalidade = txtModalidade.Text
            cedente.CodCedente = txtCodCedente.Text
            cedente.Convenio = txtConvenio.Text
            cedente.CedenteCOD = txtCedenteCod.Text

            Dim sacado As New SacadoInfo
            sacado.Sacado = "Nome de quem vai pagar"
            sacado.Documento = "123.456.789-12"
            sacado.Endereco = "rua qualquer lugar, 123"
            sacado.Bairro = "Centro da Terra"
            sacado.Cidade = "Universo Virtual"
            sacado.UF = "SP"
            sacado.Cep = "12345-678"

            Dim boleto As New BoletoInfo
            boleto.DataVencimento = DateTime.Now
            boleto.ValorDocumento = 123.45
            boleto.NossoNumero = 4567
            boleto.NumeroDocumento = 123

            'Junta tudo e calcula o boleto
            bltPag.MakeBoleto(cedente, sacado, boleto)
            bltPag.Visible = True 'Algo interessante em .Net é que algo pode estar na página, mas que só será gerado o HTML pelo servidor se estiver de fato visivel
            btnOcultar.Visible = True
            lblInfo.Text = "Número no Código de Barras: <b>" + bltPag.Boleto.CodigoBarras + "</b>"

        Catch ex As Exception
            lblInfo.Text = "<b>Erro nos parametros fornecidos para gerar o boleto!</b><br/>" + ex.Message + "<pre>" + ex.StackTrace + "</pre>"
        End Try

    End Sub

End Class
