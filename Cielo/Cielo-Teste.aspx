﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cielo-Teste.aspx.cs" Inherits="Cielo_Teste" ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
        <asp:Button runat="server" ID="btn" onclick="btn_Click" Text="Teste" /><br/>
        <asp:TextBox runat="server" ID="txt" TextMode="MultiLine" Width="90%" 
            Height="200px" EnableViewState="false" /><br/>
        <asp:Label runat="server" ID="lbl"/>
    </div>
    </form>
</body>
</html>
