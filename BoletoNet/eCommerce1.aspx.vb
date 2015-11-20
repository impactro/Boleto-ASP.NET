Imports System.Data
Imports System.Data.OleDb
Imports Impactro.Cobranca
Imports System.Net.Mail

Partial Class eCommerce1
    Inherits System.Web.UI.Page

    '   ATENÇÃO
    '============
    'Este é um exemplo basico de utilização do componente de boleto integrandose um banco de dados MDB
    'Você precisa configurar e customizar este programa para que ele funcione corretamente
    'Leia todos os comentários, estude, analize, para entender o que está sendo feito.
    'Os parametros abaixo devem ser configurados para enviar e exibir os valores corretos
    Private cURL As String = "seusite.com.br"
    Private cEmail As String = "seuemail@seusite.com.br"
    'Alem destes parametros você também deve alterar as configurações do Cendente do Boleto
    'para que o valor do boleto pago entre em sua conta
    'Crie mais campos cadastrais para um melhor controle (endereço, bairro, cidade, telefone, etc)
    'Melhorar o layout também é recomendável pois da uma cara mais profissional e séria para quem estiver comprando.

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim cID As String = Request("BOL")

            If cID Is Nothing Then

                'Exibe a tela de pedido
                pnlSelecao.Visible = True
                pnlBoleto.Visible = False

                'Na primeira carga pre seleciona um produto específico
                If Not IsPostBack Then
                    ddlProduto.DataBind() 'Para preecher o Dropdown
                    ddlProduto.SelectedValue = 1 'Pre-selecione aqui o produto principal que deseja vender (valor inicial)
                    ddlProduto_SelectedIndexChanged(Nothing, Nothing)
                End If

            Else

                'Exibe o boleto
                pnlSelecao.Visible = False
                pnlBoleto.Visible = True

                'Lê as informações do boleto já gravado na tabela
                Dim adpt As New OleDbDataAdapter("SELECT * FROM Boletos WHERE BoletoID=" & CInt(cID), dbProdutos.ConnectionString)
                Dim tb As New DataTable
                adpt.Fill(tb)

                'Define os dados do quem emite o boleto (Cedente=>quem emite)
                Dim ci As New CedenteInfo
                ci.Cedente = "Razão Social (sua empresa / você)"
                ci.Agencia = "1234-5"
                ci.Conta = "123456-7"
                ci.Banco = "237"
                ci.Carteira = "06" 'Veja nos demais exemplos como configurar seu banco

                'Define os dados de quem vai pagar (Sacado=>quem Paga)
                Dim si As New SacadoInfo
                si.Sacado = tb.Rows(0)("nome")
                si.Endereco = tb.Rows(0)("email")

                'O valor vira da tabela de boletos assim vocçe poderá mudar o preço a hora que desejar
                'e manterá um histórico dos preço e valor da compra horiginal
                Dim bi As New BoletoInfo
                bi.DataVencimento = tb.Rows(0)("Data")
                bi.ValorDocumento = tb.Rows(0)("Valor")
                bi.NossoNumero = cID
                bi.NumeroDocumento = cID
                bi.Demonstrativo = "Referente compra no site: http://www." & cURL
                bi.Instrucoes = "NÃO RECEBER ESTE BOLETO<br>Esta é uma versão de teste/demostração de aplicação utilizando boleto bancário"
                'Retire a linha acima para que o cliente de fato possa pagar este voleto no banco

                'Busca a Descrição do produto para adicionar no demostrativo
                'ATENÇÃO: Aqui eu estou usando as mesmas váriáveis do adpt e tb, ou seja, estou apenas reaproveitando para siplificar
                adpt = New OleDbDataAdapter("SELECT Titulo FROM Produtos WHERE ProdutoID=" & tb.Rows(0)("ProdutoID"), dbProdutos.ConnectionString)
                tb = New DataTable
                adpt.Fill(tb)
                bi.Demonstrativo &= "<br/>" & tb.Rows(0)("Titulo")

                'A data de vencimento usada será a data da cmopra +3 dias
                bi.DataVencimento = bi.DataVencimento.AddDays(3)

                'Gera o boleto com os parametros
                blt.MakeBoleto(ci, si, bi)

            End If

        Catch ex As Exception
            'Oculta tudo
            pnlSelecao.Visible = False
            pnlBoleto.Visible = False
            'Exibe uma descrião simples do erro na tela 
            'e envia detalhes do erro por e-mail
            lblInfo.Text = ex.Message ' & vbCrLf & ex.StackTrace 'retire este comentário para exibir a origem do erro completo
            Try
                Dim smtp As New SmtpClient("localhost")
                Dim email As New MailMessage(cEmail, cEmail, "ERRO", ex.Message & vbCrLf & ex.StackTrace)
                smtp.Send(email)
            Catch ex2 As Exception
                'pode ser que ocorra erro ao enviar o e-mail de erro
            End Try
        End Try

    End Sub

    Protected Sub btnConfirmar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmar.Click
        Try

            'Inicializa a conexão com o banco
            Dim cn As New OleDbConnection(dbProdutos.ConnectionString)
            cn.Open()

            'Busca o Valor - igual a mudança do produto
            Dim adpt As New OleDbDataAdapter("SELECT Valor FROM Produtos WHERE ProdutoID=" & ddlProduto.SelectedValue, cn)
            Dim tb As New DataTable
            adpt.Fill(tb)

            'Cria o boleto (insere na tabela de boletos
            Dim cmd As New OleDbCommand("INSERT INTO Boletos(Data,ProdutoID,Nome,Email,Valor) VALUES(?,?,?,?,?)", cn)
            cmd.Parameters.AddWithValue("Data", Now.Date) 'Deve ser passada somente a data simples (sem o horario, a menos que você mude no banco para data+hora)
            cmd.Parameters.AddWithValue("ID", CInt(ddlProduto.SelectedValue)) 'Importante: o código deve ser passado como numero inteiro
            cmd.Parameters.AddWithValue("Nome", txtNome.Text)
            cmd.Parameters.AddWithValue("Email", txtEmail.Text)
            cmd.Parameters.AddWithValue("Valor", tb.Rows(0)("Valor"))
            cmd.ExecuteNonQuery()

            'Obtem o ultimo boleto
            cmd.Parameters.Clear()
            cmd.CommandText = "SELECT MAX(BoletoID) FROM Boletos"
            Dim nBoleto As Integer = cmd.ExecuteScalar()

            'Fecha a conexão e redireciona para o boleto
            cn.Close()

            'Envia um e-mail para a pessoa lembrando o link do boleto
            Try
                Dim cTexto As String = _
                    "Obrigado por seu pedido" & vbCrLf & vbCrLf & _
                    "Produto: " & tb.Rows(0)("Valor") & vbCrLf & vbCrLf & _
                    "Para receber seu produto pague o boleto disponível no link:" & vbCrLf & _
                    "http://www." & cURL & "/eCommerce1.aspx?bol=" & nBoleto & vbCrLf & _
                    "Obrigado" & vbCrLf
                Dim smtp As New SmtpClient("localhost")
                Dim email As New MailMessage(cEmail, txtEmail.Text, "Novo Pedido", cTexto)
                email.CC.Add(cEmail) 'envia uma cópia do pedido para você
                smtp.Send(email)
            Catch ex As Exception
                'Se der algum erro
                lblInfo.Text = ex.Message
            End Try

            Response.Redirect("eCommerce1.aspx?bol=" & nBoleto)

        Catch ex As Exception
            lblInfo.Text = ex.Message & vbCrLf & ex.StackTrace 'retire este comentário para exibir a origem do erro completo
            'aqui você pode também usar o mesmo tipo de tratamento do anterior enviando o erro por e-mail
            'é só copiar/colar a rotina do page_load aqui
        End Try
    End Sub

    Protected Sub ddlProduto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProduto.SelectedIndexChanged
        Dim adpt As New OleDbDataAdapter("SELECT Valor FROM Produtos WHERE ProdutoID=" & ddlProduto.SelectedValue, dbProdutos.ConnectionString)
        Dim tb As New DataTable
        adpt.Fill(tb)
        'Exibe o preço do produto formatado em Moeda
        ' (Verifique sempre as opçõs de globalização do web.config ao tratar com datas, valores e moeda
        ' <globalization fileEncoding="iso-8859-15" requestEncoding="iso-8859-15" responseEncoding="iso-8859-15" culture="pt-BR" uiCulture="pt-BR"/>
        lblValor.Text = String.Format("{0:C}", tb.Rows(0)("Valor"))
    End Sub

End Class
