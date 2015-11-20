Imports Impactro.Cobranca

Partial Class BoletoSQL
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Conecta-se com o banco de dados
        Dim cConnectionString As String = "" 'Coloque aqui a string de Conexão com seu banco de Dados SQL Server
        If cConnectionString = "" Then
            Throw New Exception("Defina a string de conexão e configure os SELECTs para obtemção dos registros")
        End If

        'Da cobrança sei quem é o cliente
        Dim nCobrancaID As Integer = Request("ID")
        Dim tbCobranca As New Data.DataTable
        Dim adptCobranca As New System.Data.SqlClient.SqlDataAdapter("SELECT * FROM Cobranca WHERE CobrancaID=" & nCobrancaID, cConnectionString)
        adptCobranca.Fill(tbCobranca)

        If tbCobranca.Rows.Count <> 1 Then
            Throw New Exception("Registro não encontrado (boleto)!")
        End If

        'obtem o código do cliente
        Dim nClienteID As Integer = tbCobranca.Rows(0)("ClienteID")
        Dim tbSacado As New Data.DataTable
        Dim adptSacado As New System.Data.SqlClient.SqlDataAdapter("SELECT * FROM Clientes WHERE ClienteID=" & nClienteID, cConnectionString)
        adptSacado.Fill(tbSacado)

        If tbSacado.Rows.Count <> 1 Then
            Throw New Exception("Registro não encontrado (sacado)!")
        End If

        'Dados fixos do emitente do boleto
        Dim Cedente As New CedenteInfo
        Cedente.Cedente = "IMPACTRO Informática"
        Cedente.Banco = 237
        Cedente.Agencia = "1510"
        Cedente.Conta = "1466-4"
        Cedente.Carteira = "06"
        Cedente.Modalidade = "11"

        Dim Sacado As New SacadoInfo
        Sacado.Sacado = tbSacado.Rows(0)("Nome")
        Sacado.Endereco = tbSacado.Rows(0)("Endereco")
        Sacado.Bairro = tbSacado.Rows(0)("Bairro")
        Sacado.Cidade = tbSacado.Rows(0)("Cidade")
        Sacado.UF = tbSacado.Rows(0)("Estado")
        Sacado.Cep = tbSacado.Rows(0)("CEP")

        Dim Boleto As New BoletoInfo
        Boleto.ValorDocumento = tbCobranca.Rows(0)("Valor")
        Boleto.DataVencimento = tbCobranca.Rows(0)("Vencimento")
        Boleto.NossoNumero = tbCobranca.Rows(0)("CobrancaID")

        blt.MakeBoleto(Cedente, Sacado, Boleto)

    End Sub

End Class
