<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cielo-Proxy.aspx.cs" Inherits="Cielo_Proxy" ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
        Numero do Pedido: <asp:TextBox ID="txtPedido" runat="server" Text="123"/><br/>
        Valor: <asp:TextBox ID="txtValor" runat="server" Text="56,78"/><br/>
        Cartão: <asp:DropDownList runat="server" ID="ddlCartao">
            <asp:ListItem  Text="Visa" />
            <asp:ListItem  Text="VisaElectron" />
            <asp:ListItem  Text="MasterCard" />
            <asp:ListItem  Text="Elo" />
            <asp:ListItem  Text="Diners" />
            <asp:ListItem  Text="Discover" />
        </asp:DropDownList><br/>
        <asp:CheckBox runat="server" ID="chkDebito" Text="Debito a Vista" />
        Parcelas: <asp:TextBox ID="txtParcelas" MaxLength="1" runat="server" Text="3"/><br/>
        <asp:CheckBox runat="server" ID="chkCapturar" Text="Capturar" /><br/>
        <asp:Button runat="server" ID="btn" onclick="btn_Click" Text="Teste" /><br/>
        <asp:TextBox runat="server" ID="txt" TextMode="MultiLine" Width="90%" Height="200px" EnableViewState="false" /><br/>
        <asp:Label runat="server" ID="lbl"/>
    </div>
    </form>
</body>
</html>
