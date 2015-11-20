Partial Class BaixaAutomaticaER
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            rbBradesco_CheckedChanged(Nothing, Nothing)
        End If
    End Sub

    Protected Sub rbBradesco_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbBradesco.CheckedChanged
        'txtER.Enabled = False
        rbBradesco.Checked = True
        txtER.Text = "2\s+11(?<id>\d+)\s+(\d+/\d+/\d+)\s+(?<data>\d+/\d+/\d+)\s+(\d[,]\d+)\s+(?<valor>\d*\.*\d+,\d+)"
        'txtER.Text = "\d+\s10(?<id>\d+)\s+\d+/\d+/\d+\s(?<data>\d+/\d+/\d+)\s+[0-9]*\.*\d+,\d+\s+(?<valor>\d*\.*\d+,\d+)"
        lblInfo.Text = "Se você é cliente do Bradesco<br>Copie e cole o extrato dos titulos pagos no campo de 'Texto de entrada', e clique em processar<br>Você verá que o sistema irá extrari automaticamente os numeros, datas e valores dos pagamentos efetuados"
    End Sub

    Protected Sub rbUnibanco_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbUnibanco.CheckedChanged
        rbUnibanco.Checked = True
        txtER.Enabled = False
        txtER.Text = "(\d{7})(?<id>\d{7})\d\s+(\d{2}/\d{2}/\d{4})\s+(?<data>\d{2}/\d{2}/\d{4})\s+((\d+[.])*\d+,\d+)\s+(?<valor>[0-9]*\.*\d+,\d+)"
        lblInfo.Text = "Se você é cliente do Unibanco<br>Copie e cole o extrato dos titulos pagos no campo de 'Texto de entrada', e clique em processar<br>Você verá que o sistema irá extrari automaticamente os numeros, datas e valores dos pagamentos efetuados"
    End Sub

    Protected Sub rbOutros_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbOutros.CheckedChanged
        txtER.Enabled = True
        txtER.Text = "(?<id>\d+),(?<data>\d+/\d+),(?<valor>\d+.\d+,\d+)"
        'Exemplo de dado qualquer
        txtIN.Text = "123,10/12,1.234,56" & vbCrLf & "234,15/12,345,67"
        lblInfo.Text = "<i>Para que o sistema reconheça a ER será necessário identificar os grupos para extrair os campos id, data do pagamento e valor pago.</i><br>Exemplo: <b>(?&lt;id&gt;\d+),(?&lt;data&gt;\d+/\d+),(?&lt;valor&gt;\d+.\d+,\d+)</b>"
        rbOutros.Checked = True
    End Sub

    Protected Sub btnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        Try
            Dim m As Match
            Dim n As Integer
            m = Regex.Match(txtIN.Text, txtER.Text)
            dtgResult.Visible = False
            If m.Success Then

                'Executa a busca processando os grupos encontrados
                lblInfo.Text = ""
                Do While m.Success
                    For n = 1 To m.Groups.Count - 1
                        lblInfo.Text &= "<b>G" & n & ":</b> " & m.Groups(n).Value & " "
                    Next
                    lblInfo.Text &= "<br/>" & vbCrLf
                    m = m.NextMatch()
                Loop

                'Re-executa a busca para montar uma tabela
                m = Regex.Match(txtIN.Text, txtER.Text)
                If IsNumeric(m.Groups("id").Value) AndAlso IsDate(m.Groups("data").Value) AndAlso IsNumeric(m.Groups("valor").Value) Then
                    lblInfo.Text &= "<br/>Elementos Principais encontrados!<br/>"
                    Dim tb As New System.Data.DataTable("dados")
                    tb.Columns.Add("id", GetType(Integer))
                    tb.Columns.Add("data", GetType(Date))
                    tb.Columns.Add("valor", GetType(Double))
                    Dim cID, cData, cValor As String
                    Do While m.Success
                        cID = m.Groups("id").Value
                        cData = m.Groups("data").Value
                        cValor = m.Groups("valor").Value
                        lblInfo.Text &= String.Format("<b>id:</b> {0} <b>data:</b> {1} <b>valor:</b> {2}<br/>" & vbCrLf, cID, cData, cValor)
                        tb.Rows.Add(CInt(cID), CDate(cData), CDbl(cValor))
                        m = m.NextMatch()
                    Loop
                    dtgResult.Visible = tb.Rows.Count > 0
                    dtgResult.DataSource = tb
                    dtgResult.DataBind()
                    For n = 0 To tb.Rows.Count - 1
                        Dim nCobranca As Integer = tb.Rows(n)("id") 'Nosso Numero
                        Dim dDataPagamento As DateTime = tb.Rows(n)("data")
                        Dim nValorPago As Double = tb.Rows(n)("valor")
                        'Obtem os principais dados do registro identificado
                        'com estes dados você poderá dar a respectiva baixa em seu sistema
                    Next
                End If
            Else
                lblInfo.Text = "(nenhuma ocorrencia encontrada)"
            End If
        Catch ex As Exception
            lblInfo.Text &= "<br/><font color=red><b>" & ex.Message & "</b></font><br><font color=blue>" & vbCrLf & ex.StackTrace.Replace(vbCrLf, "<br>") & "</font>"
        End Try
    End Sub

End Class
