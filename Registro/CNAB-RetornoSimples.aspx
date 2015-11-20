<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CNAB-RetornoSimples.aspx.cs" Inherits="CNAB_RetornoSimples" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox runat="server" ID="txtIn" TextMode="MultiLine" Width="90%" Height="300" Wrap="false"/><br/>
        <asp:Button runat="server" ID="btnTest" Text="Ler Retorno" onclick="btnTest_Click"/><br/>
        <asp:Label runat="server" ID="lblOut" />
        <asp:GridView runat="server" ID="gv"/>
    </div>
    </form>
</body>
</html>
