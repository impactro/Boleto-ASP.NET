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
        'Apenas no primeiro GET da página
        If Not IsPostBack Then

            'Lista os possíveis Factory isntalados na máquina
            Dim factorys As DataTable = DbProviderFactories.GetFactoryClasses()
            ddlProvider.DataValueField = "InvariantName"
            ddlProvider.DataTextField = "Name" 'Description, AssemblyQualifiedName
            ddlProvider.DataSource = factorys
            ddlProvider.DataBind()
            ddlProvider.SelectedIndex = 1

            'Lê os dados padrão de querystring se o banco for definido (por POST ou GET)
            If Not Request("banco") Is Nothing Then

                'Cedente
                txtCedente.Text = Request("Cedente")
                txtCNPJ.Text = Request("CNPJ")
                txtEndereco.Text = Request("Endereco")
                ddlBancos.SelectedValue = Request("Banco")
                txtAgencia.Text = Request("Agencia")
                txtConta.Text = Request("Conta")
                txtCarteira.Text = Request("Carteira")
                txtCarteiraTipo.Text = Request("CarteiraTipo")
                txtModalidade.Text = Request("Modalidade")
                txtCodCedente.Text = Request("CodCedente")
                txtConvenio.Text = Request("Convenio")
                txtCedenteCod.Text = Request("CedenteCOD")

                'Boleto
                txtValor.Text = Request("Valor")
                txtNossoNumero.Text = Request("NossoNumero")
                txtVencimento.Text = Request("Vencimento")
                txtNDocumento.Text = Request("NDocumento")

                Dim db = Request("db")
                If String.IsNullOrEmpty(db) Then
                    Dim item = ddlProvider.Items.FindByValue(db)
                    If Not item IsNot Nothing Then
                        item.Selected = True
                    End If
                    If Not String.IsNullOrEmpty(Request("cn")) Then txtConnectionString.Text = Request("cn")
                    If Not String.IsNullOrEmpty(Request("qry")) Then txtSelect.Text = Request("qry")
                End If
            End If
        End If
    End Sub

    Protected Function GetCedente() As CedenteInfo
        'Obtenho os dados do formulario e defino o cedente
        Dim cedente As New CedenteInfo ' em c# seria: var cedente = new CedenteInfo();
        cedente.Cedente = txtCedente.Text
        cedente.CNPJ = txtCNPJ.Text
        cedente.Endereco = txtEndereco.Text
        cedente.Banco = ddlBancos.SelectedValue
        cedente.Agencia = txtAgencia.Text
        cedente.Conta = txtConta.Text
        cedente.Carteira = txtCarteira.Text
        cedente.CarteiraTipo = txtCarteiraTipo.Text
        cedente.Modalidade = txtModalidade.Text
        cedente.CodCedente = txtCodCedente.Text
        cedente.Convenio = txtConvenio.Text
        cedente.CedenteCOD = txtCedenteCod.Text
        cedente.CarteiraTipo = 1 'Específico para o Santander
        cedente.Layout = LayoutTipo.CNAB240

        'Util apenas para Banespa gerar boletos no formato do Santander
        cedente.useSantander = True
        Return cedente
    End Function

    'Quando o botão de teste for clicado esta rotina será executada
    Protected Sub btnCedenteTeste_Click(sender As Object, e As EventArgs)
        Try
            lblInfoCedente.Text = ""

            'Obtenho os dados do formulario e defino o cedente
            Dim cedente = GetCedente()

            Dim sacado As New SacadoInfo
            sacado.Sacado = "Nome de quem vai pagar"
            sacado.Documento = "123.456.789-12"
            sacado.Endereco = "rua qualquer lugar, 123"
            sacado.Bairro = "Centro da Terra"
            sacado.Cidade = "Universo Virtual"
            sacado.UF = "SP"
            sacado.Cep = "12345-678"

            'Dados do boleto convertidos diretamente, sem testes de conversão!
            'Verifique também os padrões de idioma para conversão correta dos valores
            Dim boleto As New BoletoInfo
            boleto.DataVencimento = DateTime.Parse(txtVencimento.Text)
            boleto.ValorDocumento = Double.Parse(txtValor.Text)
            boleto.NossoNumero = txtNossoNumero.Text
            boleto.NumeroDocumento = txtNDocumento.Text

            'Junta tudo e calcula o boleto
            bltPag.MakeBoleto(cedente, sacado, boleto)
            bltPag.Visible = True 'Algo interessante em .Net é que algo pode estar na página, mas que só será gerado o HTML pelo servidor se estiver de fato visivel
            btnOcultar.Visible = True
            lblInfoCedente.Text = "Número no Código de Barras: <b>" + bltPag.Boleto.CodigoBarras + "</b>"

            'Monta uma querystring 'simples' para ser enviada como exemplo via GET
            lblInfoCedente.Text += " <a href='http://exemplos.boletoasp.com.br/Registro/CNAB-Remessa.aspx?Cedente=" + txtCedente.Text +
                "&CNPJ=" + txtCNPJ.Text +
                "&Endereco=" + txtEndereco.Text +
                "&Banco=" + ddlBancos.SelectedValue +
                "&Agencia=" + txtAgencia.Text +
                "&Conta=" + txtConta.Text +
                "&CarteiraTipo=" + txtCarteiraTipo.Text +
                "&Carteira=" + txtCarteira.Text +
                "&Modalidade=" + txtModalidade.Text +
                "&CodCedente=" + txtCodCedente.Text +
                "&Convenio=" + txtConvenio.Text +
                "&CedenteCOD=" + txtCedenteCod.Text +
                "&Valor=" + txtValor.Text +
                "&NossoNumero=" + txtNossoNumero.Text +
                "&Vencimento=" + txtVencimento.Text +
                "&NDocumento=" + txtNDocumento.Text +
                "&db=" + ddlProvider.SelectedValue +
                "&cn=" + txtConnectionString.Text +
                "&qry=" + txtSelect.Text +
                "' target=_blank>(SALVE OS PARAMETROS PELO LINK GET)</a>"

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
            Dim dbfc As DbProviderFactory
            If ddlProvider.SelectedValue = "System.Data.OleDb" Then 'Para evitar problemas é melhor fazer dessa forma
                dbfc = OleDbFactory.Instance
            Else
                dbfc = DbProviderFactories.GetFactory(ddlProvider.SelectedValue)
            End If

            'Cria a instancia da conexão
            Dim dbcn = dbfc.CreateConnection()
            'Dim dbcn As New OleDbConnection
            dbcn.ConnectionString = txtConnectionString.Text
            dbcn.Open()

            'Se chegou aqui é porque o driver o banco existe e está conectado, então cou criar o comando de execução e ler os dados
            Dim dbcmd = dbfc.CreateCommand()
            'Dim dbcmd As New OleDbCommand
            dbcmd.Connection = dbcn 'conecão relacionada ao comando
            dbcmd.CommandText = txtSelect.Text 'comando a ser executado
            Dim tb As New DataTable
            Dim adpt = dbfc.CreateDataAdapter() 'classe que lê os dados e prenenche a tabela 
            'Dim adpt As New OleDbDataAdapter
            adpt.SelectCommand = dbcmd
            adpt.Fill(tb) ' Os dados lidos estarão em tb!

            'Renderiza tudo em tela
            gvBanco.DataSource = tb
            gvBanco.DataBind()

            'Da mesma forma que no teste, obtenho os dados do formulário
            Dim cedente = GetCedente()

            'Tenta gerar a remessa baseado nos dado do cedente e lidos pelo banco de dados
            Dim arq As New LayoutBancos
            arq.Init(cedente)

            Dim n As Integer = 0
            Dim Baixa As Boolean
            'Adiciona no arquivo os respectivos boletos e sacados
            For Each row As DataRow In tb.Rows

                Dim boleto As New BoletoInfo
                boleto.NossoNumero = row("NossoNumero")
                boleto.DataVencimento = row("Vencimento")
                boleto.ValorDocumento = row("Valor")
                'Campos opcionais do boleto
                If tb.Columns.Contains("NumeroDocumento") Then boleto.NumeroDocumento = row("NumeroDocumento")
                If tb.Columns.Contains("BoletoID") Then boleto.BoletoID = row("BoletoID")
                If tb.Columns.Contains("Emissao") Then boleto.DataDocumento = row("Emissao")

                Dim sacado As New SacadoInfo
                sacado.Sacado = row("Pagador")
                sacado.Endereco = row("Endereco")
                'Campos opcionais do sacado
                If tb.Columns.Contains("Documento") Then sacado.Documento = row("Documento")
                If tb.Columns.Contains("Bairro") Then sacado.Bairro = row("Bairro")
                If tb.Columns.Contains("Cidade") Then sacado.Cidade = row("Cidade")
                If tb.Columns.Contains("UF") Then sacado.Cidade = row("UF")
                If tb.Columns.Contains("CEP") Then sacado.Cep = row("CEP")

                'controle de geração (baixa ou criação) => Padrão criação!
                If tb.Columns.Contains("Baixa") Then Baixa = row("baixa") Else Baixa = False

                If Baixa Then boleto.Instrucao1 = 2 'Nos bancos: BB, Sicredi, Santander o codigo '2' é a instrução de baixa

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

            'Visualiza de forma mais clara o valor dos campos de acordo como tipo de registro
            'gvCampos.DataSource = arq.Table(GetType(CNAB400SantanderRemessa1)) ' compativel com versões anteriores
            gvCampos.DataSource = arq.Table(1) ' em geral é o tipo dos detalhe
            gvCampos.DataBind()

            lblInfoSQL.Text = "OK deu tudo certo!"

        Catch ex As Exception
            lblInfoSQL.Text = "<b>ERRO!</b><br/>" + ex.Message + "<pre>" + ex.StackTrace + "</pre>"
        End Try
    End Sub

End Class
