'Este programa foi feito em VB.Net para facilitar a leitura e compreenção de programadores de ASP Classico ou VB6
'Caso queira em C#, pode converter usando um conversor online: http://www.carlosag.net/tools/codetranslator/

Imports System.Data 'Referendia do ADO.Net (tabelas em memoria que conterão os resultados)
Imports System.Data.Common 'Referencia da biblioteca padrão de banco de dados generica
Imports Impactro.Cobranca 'Aqui é uma referencia para o nome dos objetos definidos na biblioteca padrão de boletos
Imports Impactro.Layout 'Os objetos de layout ficam na mesma DLL mas organizados em outro namespace para facilitar

Partial Class Registro_CNAB_Remessa3
    Inherits System.Web.UI.Page

    'Apenas uma simples forma de bloquear e desbloquear todo conteudo a fim de proteger os dados
    Protected Sub btnEntrar_Click(sender As Object, e As EventArgs)
        If txtSenha.Text = "123" Then 'Altera a senha para bloquear o sistema, ou crie outra forma de login
            pnlLogin.Visible = False
            pnlFormulario.Visible = True
            txtInicio.Text = New Date(2001, 1, 1) 'Valor fixo inicial, para funcionar sempre o exemplo
            txtFim.Text = Now.ToShortDateString()
        Else
            lblInfo.Text = "Senha inválida"
        End If
    End Sub

    Protected Sub btnGerar_Click(sender As Object, e As EventArgs)
        Try
            'Renderiza tudo em tela de acordo com o que é habilitado, logico que em modo de download, toda a visualização é cancelado, e apenas o arquivo de remessa gerado é disponibilizado (essa é a forma mais simples e direta de se fazer)
            pnlDados.Visible = chkDados.Checked
            pnlRemessa.Visible = chkRemessa.Checked
            pnlCampos.Visible = chkCampos.Checked
            pnlBoletos.Visible = chkBoletos.Checked

            If Not (pnlDados.Visible Or pnlRemessa.Visible Or pnlCampos.Visible Or pnlBoletos.Visible Or chkDownload.Checked) Then
                lblErro.Text = "<b>Selecione alguma opção</b>"
                Exit Sub
            End If

            'Veja mais sobre conexão com o banco de dados: 
            'https://msdn.microsoft.com/pt-br/library/hktw939c(v=vs.80).aspx
            'http://www.dofactory.com/reference/connection-strings
            'Estou usando a forma mais generica, que não necessáriamente é a mais simples de usar, mas que mudando-se apenas o 'Provider' muda-se tudo
            'Dim dbfc = DbProviderFactories.GetFactory("MySql.Data.MySqlClient") 'MySQL
            'Dim dbfc = DbProviderFactories.GetFactory("System.Data.SqlClient") 'MS-SQL
            Dim dbfc = DbProviderFactories.GetFactory("System.Data.OleDb") 'MDB

            'Este exemplo usa as tabelas descritas no exemplo: Cliente_Cobranca.sql

            'Cria a instancia da conexão, e define a respectiva string de conexão com o banco de acordo com o tipo de banco de dados (provider)
            Dim dbcn = dbfc.CreateConnection()
            'dbcn.ConnectionString = "Data Source=localhost;Initial Catalog=boletoteste;User ID=root;Password=123456"
            'dbcn.ConnectionString = "Data Source=localhost;Initial Catalog=teste;User ID=root;Password=123456"
            dbcn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|eCommerce.mdb"
            dbcn.Open()

            'Se chegou aqui é porque o driver o banco existe e deu certo a conexão com o banco de dados

            'Agora vamos ler dos dado efetivamente com um comando de SELECT sobre as tabelas 
            'A sintaxe do SELECT pode mudar um pouco de acordo com o tipo de banco
            Dim dbcmd = dbfc.CreateCommand()
            dbcmd.Connection = dbcn 'conecão relacionada ao comando
            dbcmd.CommandText = "SELECT boletoID as NossoNumero, Data as Vencimento, Valor, Nome as Pagador, '123' as Documento, 'Endereco' as Endereco FROM Boletos WHERE Data>=@inicio and Data<=@fim "
            'dbcmd.CommandText = "select cob.id_Cobranca NossoNumero, cob.Emissao, cob.Documento NumeroDocumento, cob.Valor, cob.vencimento, cli.Nome Pagador, cli.Endereco, cli.Bairro, cli.Cidade, cli.UF from cobrancas cob inner join clientes cli using(id_cliente) WHERE cob.id_Cobranca>0 AND cob.vencimento BETWEEN @inicio AND @fim"

            'Configuro os parametros prevendo SQLInject, e já fazendo os 'cast' corretos
            Dim prm1 As DbParameter = dbcmd.CreateParameter()
            prm1.ParameterName = "@inicio"
            prm1.Value = DateTime.Parse(txtInicio.Text) 'Não estou tratando erros de conversão, pois o objetivo não é esse, mas tudo está dentro de um grande block de 'try...catch' para tratamento de qualquer erro
            dbcmd.Parameters.Add(prm1)

            Dim prm2 As DbParameter = dbcmd.CreateParameter()
            prm2.ParameterName = "@fim"
            prm2.Value = DateTime.Parse(txtFim.Text)
            dbcmd.Parameters.Add(prm2)

            'No ADO.Net os dados são desconexo, ou seja, todos vem do banco, e em seguida, a conexão já pode ser fechado pois está tudo em memória dentro de um DataTable
            Dim tb As New DataTable
            Dim adpt = dbfc.CreateDataAdapter() 'classe que lê os dados e prenenche a tabela 

            adpt.SelectCommand = dbcmd ' O Adaptador de leitura, contem o comando, que aponta para o banco
            adpt.Fill(tb) 'Os dados lidos estarão em nesta DataTable!

            'Pronto não é mais necessário manter o banco aberto, tudo que preciso está em 'tb'
            dbcn.Close()

            If tb.Rows.Count = 0 Then
                lblErro.Text = "<b>Não há dados para o período selecionado</b>"
                txtRemessa.Text = ""
                Exit Sub
            End If

            '======================================================================
            'A partir daqui que comeca a parte de boleto, antes foi tudo .Net e SQL
            Dim cedente As New CedenteInfo ' em c# seria: var cedente = new CedenteInfo();
            cedente.Cedente = "teste é voce"
            cedente.CNPJ = "123132.123.132"
            cedente.Endereco = "seu endereço"
            cedente.Banco = "033"
            cedente.Agencia = "1234"
            cedente.Conta = "000067890-1"
            cedente.Carteira = "101"
            cedente.CarteiraTipo = "5"
            cedente.Modalidade = "0"
            cedente.CodCedente = "7882866"
            cedente.Convenio = "0000000000000000002222220"
            cedente.CedenteCOD = "33333334892001304444"
            cedente.useSantander = True 'Origatorio apenas para Banespa gerar boletos no formato do Santander
            cedente.CarteiraTipo = 1 'Específico para o Santander
            cedente.Layout = LayoutTipo.Auto

            'Primeiro os dados antes de computar qualquer informação de boleto, se os dados já não aparecerem, verifique o comando executado
            'Teste antes o SELECT em algum software do seu dominio, como o phpmysqladmin, sql management studio, mysql workbench sqlite browse, ms access, etc...

            If pnlDados.Visible AndAlso Not chkDownload.Checked Then
                gvDados.DataSource = tb
                gvDados.DataBind()
            End If

            'Gera a remessa baseado nos dado do cedente e lidos pelo banco de dados
            Dim arq As New LayoutBancos
            arq.Init(cedente)

            Dim Baixa As Boolean
            'Adiciona no arquivo os respectivos boletos e sacados
            For Each row As DataRow In tb.Rows 'faz um loop em todas as linhas (rows) disponiveis na tabela (resultado do select em memória)

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

                'Já otimizando, só crio os componentes de visualização quando é necessário
                If pnlBoletos.Visible AndAlso Not chkDownload.Checked Then
                    'Cria um boleto com os dados para visualização/conferencia
                    Dim blt As New Impactro.WebControls.BoletoWeb
                    'classe basica de configuração de layout
                    blt.CssCell = "BolCell"
                    blt.CssField = "BolField"

                    'Configura o boleto com todos os dados necessários
                    blt.MakeBoleto(cedente, sacado, boleto)

                    'Adiciona-o em tela
                    pnlBoletos.Controls.Add(blt)
                    pnlBoletos.Controls.Add(New LiteralControl("<br/><hr/><br/>")) 'apenas um separador para melhor visualuzar (não to prevendo quebra de página, etc...)

                End If

            Next

            'Tem que chamar esse metodo de qualquer forma, independente se o resultado será usado ou não
            Dim cRemessa As String = arq.Remessa()
            If chkDownload.Checked Then
                Response.ContentType = "text/plain"
                Response.AddHeader("Content-Disposition", String.Format("attachment; filename=""remessa-{0:yyyyMMdd}.txt""", Now))
                Response.Write(cRemessa)
                Response.End() 'finaliza tudo
            End If

            txtRemessa.Text = cRemessa

            If pnlCampos.Visible Then
                'Visualiza de forma mais clara o valor dos campos de acordo como tipo de registro
                gvCampos.DataSource = arq.Table(1) ' em geral o elemento 1 é o tipo dos detalhe, mas há layouts com mais de um tipo de detalhe
                gvCampos.DataBind()
            End If

        Catch ex As Threading.ThreadAbortException ' nessessário no download
        Catch ex As Exception
            lblErro.Text = "<b>ERRO: " + ex.Message + "</b><br/><pre>" + ex.StackTrace + "</pre>"
        End Try
    End Sub

End Class
