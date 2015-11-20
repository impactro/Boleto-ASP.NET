<%@ Page Language="C#"  %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        ltr.Text = "<h1>From</h1>";
        foreach (string cKey in Request.Form.Keys)
            ltr.Text += cKey + ": " + Request.Form[cKey] + "<br/>";

        ltr.Text += "<h1>QueryString</h1>";
        foreach (string cKey in Request.QueryString.Keys)
            ltr.Text += cKey + ": " + Request.QueryString[cKey] + "<br/>";
    }
    
</script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Retorno</title>
</head>
<body>
    <h1>Variáveis retornadas!</h1>
    <asp:Literal runat="server" ID="ltr" EnableViewState="false"/>
    <p>Retorno cielo, tente: <a href='cielo/Cielo-Consulta.aspx'>Consulta</a> | <a href='cielo/Cielo-Captura.aspx'>Capturar</a> | <a href='cielo/Cielo-Cancelar.aspx'>Cancelar</a></p>
</body>
</html>
