<%@ Page Language="VB" %>

<script runat="server">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tp As Type = Me.GetType()
        lblVer.Text = tp.BaseType.Assembly.FullName
    End Sub
    
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Versão do ASP.NET</title>
</head>
<body>
    <form id="form1" runat="server">
        <strong>Versão do ASP.Net: </strong><asp:Label ID="lblVer" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
