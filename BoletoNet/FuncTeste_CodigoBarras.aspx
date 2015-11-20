<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FuncTeste_CodigoBarras.aspx.cs" Inherits="FuncTeste_CodigoBarras" %>

<%@ Register src="Rodape.ascx" tagname="Rodape" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        Numero a Gerar: <asp:TextBox runat="server" ID="txtCodBat" Text="1234567890"/>
        <asp:Button runat="server" ID="btnGerar" Text="GERAR" 
            onclick="btnGerar_Click" /><br/>
            <asp:Image runat="server" ID="img" Visible="false" />
        <uc1:Rodape ID="Rodape1" runat="server" />
    </form>
</body>
</html>
