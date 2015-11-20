<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Exemplo4-Registro.aspx.cs" Inherits="Registro_Exemplo4_Registro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Tipo de Registro:
        <asp:RadioButtonList runat="server" ID="rblTipo" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="0">Header</asp:ListItem>
            <asp:ListItem Value="1">Registro</asp:ListItem>
            <asp:ListItem Value="2" Selected="True">Retorno</asp:ListItem>
            <asp:ListItem Value="9">Trailer</asp:ListItem>
        </asp:RadioButtonList>
        </br>
        <asp:TextBox runat="server" ID="txt" Width="90%" Font-Names="Courrier New">10203804528000101000000900301020820040000000000000000000000666000000000000000066640000000000000000000000000903260312000000066600000000000000006664120423000000039808823700109  000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000                          1800000000                                                                  000028</asp:TextBox><br/>
        <asp:Button runat="server" ID="btn" Text="Testar" onclick="btn_Click"/><br/>
        <asp:Label runat="server" ID="lbl"/><br/>
        <asp:DataGrid runat="server" ID="dtg"/>
    </div>
    </form>
</body>
</html>
