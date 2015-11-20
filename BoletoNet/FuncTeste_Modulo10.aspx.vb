
Partial Class FuncTeste_Modulo10
    Inherits System.Web.UI.Page

    Protected Sub btnCalcular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCalcular.Click
        Try
            lblResultado.Text = Funcoes.Modulo10(txtDigitos.Text)
        Catch ex As Exception
            lblResultado.Text = ex.Message
        End Try
    End Sub

End Class
