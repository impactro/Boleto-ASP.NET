'Este programa foi feito em VB.Net para facilitar a leitura e compreenção de programadores de ASP Classico
'Caso queira em C#, pode converter usando um conversor online: http://www.carlosag.net/tools/codetranslator/

Imports System.Data 'Referendia do ADO.Net (tabelas em memoria que conterão os resultados)
Imports System.Data.Common 'Referencia da biblioteca padrão de banco de dados generica
Imports System.Data.OleDb
Imports Impactro.Cobranca 'Aqui é uma referencia para o nome dos objetos definidos na biblioteca padrão de boletos
Imports Impactro.Layout 'Os objetos de layout ficam na mesma DLL mas organizados em outro namespace para facilitar

Partial Class Registro_CNAB_Remessa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim factorys As DataTable = DbProviderFactories.GetFactoryClasses()
            'ddlProvider.DataValueField = "InvariantName"
            'ddlProvider.DataTextField = "Name" 'Description, AssemblyQualifiedName
            'ddlProvider.DataSource = factorys
            'ddlProvider.DataBind()
        End If
    End Sub

    'Quando o botão de teste for clicado esta rotina será executada
    Protected Sub btnCedenteTeste_Click(sender As Object, e As EventArgs)
        Try
            lblInfoCedente.Text = ""

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
            lblInfoCedente.Text = "Número no Código de Barras: <b>" + bltPag.Boleto.CodigoBarras + "</b>"

        Catch ex As Exception
            lblInfoCedente.Text = "<b>Erro nos parametros fornecidos para gerar o boleto!</b><br/>" + ex.Message + "<pre>" + ex.StackTrace + "</pre>"
        End Try

    End Sub

    Protected Sub btnSelect_Click(sender As Object, e As EventArgs)
        Try
            'Veja mais sobre conexão com o banco de dados: 
            'https://msdn.microsoft.com/pt-br/library/hktw939c(v=vs.80).aspx
            'http://www.dofactory.com/reference/connection-strings

            Dim sql As String = txtSelect.Text.ToLower
            If (Not sql.StartsWith("select ")) OrElse sql.Contains("drop") OrElse sql.Contains("delete") OrElse sql.Contains("--") OrElse sql.Contains("\") Then
                Throw New Exception("Por favor, sem sacanagem!")
            End If

            lblInfoSQL.Text = ""
            'Cria o driver de coenxão específico de acordo com o tipo de banco de dados
            'Dim dbfc As DbProviderFactory = DbProviderFactories.GetFactory(ddlProvider.SelectedValue)

            'Cria a instancia da conexão
            Dim dbcn As New OleDbConnection() 'DbConnection = dbfc.CreateConnection()
            dbcn.ConnectionString = txtConnectionString.Text
            dbcn.Open()

            'Se chegou aqui é porque o driver o banco existe e está conectado, então cou criar o comando de execução e ler os dados
            Dim dbcmd As New OleDbCommand '= dbfc.CreateCommand()
            dbcmd.Connection = dbcn 'conecão relacionada ao comando
            dbcmd.CommandText = txtSelect.Text 'comando a ser executado
            Dim tb As New DataTable
            Dim adpt As New OleDbDataAdapter  '= dbfc.CreateDataAdapter() 'classe que lê os dados e prenenche a tabela 
            adpt.SelectCommand = dbcmd
            adpt.Fill(tb) ' Os dados lidos estarão em tb!

            'Renderiza tudo em tela
            gvBanco.DataSource = tb
            gvBanco.DataBind()

            'Da mesma forma que no teste, obtenho os dados do formulário
            Dim cedente As New CedenteInfo
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

            'Tenta gerar a remessa baseado nos dado do cedente e lidos pelo banco de dados
            Dim arq As New LayoutBancos
            arq.Init(cedente, LayoutTipo.CNAB400)

            Dim n As Integer = 0
            'Adiciona no arquivo os respectivos boletos e sacados
            For Each row As DataRow In tb.Rows

                Dim boleto As New BoletoInfo
                boleto.NossoNumero = row("NossoNumero")
                boleto.DataVencimento = row("Vencimento")
                boleto.ValorDocumento = row("Valor")

                Dim sacado As New SacadoInfo
                sacado.Sacado = row("Pagador")
                sacado.Documento = row("Documento")
                sacado.Endereco = row("Endereco")

                arq.Add(boleto, sacado)

                'Cria um boleto com os dados para visualização/conferencia
                Dim blt As New Impactro.WebControls.BoletoWeb
                'classe basica de configuração de layout
                blt.CssCell = "BolCell"
                blt.CssField = "BolField"

                'configura o boleto
                blt.MakeBoleto(cedente, sacado, boleto)

                'Adiciona-o em tela
                pnlBoletos.Controls.Add(New LiteralControl("<br/><hr/><br/>")) 'apenas um separador para melhor visualuzar
                pnlBoletos.Controls.Add(blt)

                n += 1
                If n > 10 Then
                    Exit For
                End If
            Next

            'Tenta transformar tudo no arquivo de remessa
            txtRemessa.Text = arq.Remessa

            lblInfoSQL.Text = "OK deu tudo certo!"

        Catch ex As Exception
            lblInfoSQL.Text = "<b>ERRO!</b><br/>" + ex.Message + "<pre>" + ex.StackTrace + "</pre>"
        End Try
    End Sub

End Class
