<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DirectPrinter.aspx.cs" Inherits="DirectPrinter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button runat="server" ID="btnPrint" Text="Imprimir na impressora padrão" 
            onclick="btnPrint_Click" />
    </form>
</body>
</html>
