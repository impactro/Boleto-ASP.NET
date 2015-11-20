<%@ Page Language="VB" AutoEventWireup="false" CodeFile="eCommerce1.aspx.vb" Inherits="eCommerce1" %>
<%@ Register assembly="Impactro.Cobranca" namespace="Impactro.WebControls" tagprefix="cc1" %>
<%@ Register src="Rodape.ascx" tagname="Rodape" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>e-commerce: Exemplo de aplicação Simples 1</title>
    <style type="text/css">
    body
    {
        font-family: Arial;
        font-size: 10pt;
    }
    table 
    {
        font-size: 9pt;
        font-family: Verdana, Arial;
    }
    .BolCell
    { 
        font-size: 7pt;
        font-family: Verdana;
                 
    }
    .BolField
    {
          font-weight: bold;
          font-size: 8pt;
          font-family: Verdana;
    }
    </style>
</head>
<body>
    <form id="frm" runat="server">
    <asp:AccessDataSource ID="dbProdutos" runat="server" 
        DataFile="~/App_Data/eCommerce.mdb" 
        SelectCommand="SELECT * FROM [Produtos]">
    </asp:AccessDataSource>
    <asp:Panel runat="server" id="pnlSelecao">
        Para realizar sua compra você precisa informar seus dados e o produto que deseja comrpar e confirmar o pedido:<br/>
        Seu nome: <asp:TextBox runat="server" ID="txtNome" Width="300px" />
        <asp:RequiredFieldValidator ID="rfvnome" runat="server" 
            ControlToValidate="txtNome" ErrorMessage="Informe seu nome" 
            SetFocusOnError="True">*</asp:RequiredFieldValidator>
        <br/>
    e-mail: <asp:TextBox runat="server" ID="txtEmail" Width="300px" />
        <asp:RequiredFieldValidator ID="rfvemail" runat="server" 
            ControlToValidate="txtEmail" ErrorMessage="Informe seu e-mail" 
            SetFocusOnError="True">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revemail" runat="server" 
            ControlToValidate="txtEmail" ErrorMessage="Informe um e-mail Valido" 
            SetFocusOnError="True" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
        <br/>
    Produto: <asp:DropDownList runat="server" ID="ddlProduto" AutoPostBack="True" 
            DataSourceID="dbProdutos" DataTextField="Titulo" DataValueField="ProdutoID"/><br/>
    Valor: <asp:Label runat="server" ID="lblValor" Font-Bold="True"/><br/>
    <asp:Button runat="server" ID="btnConfirmar" Text="Confirmar Pedido" />
        <asp:ValidationSummary ID="vs" runat="server" ShowMessageBox="True" 
            ShowSummary="False" />
        <br/>
    </asp:Panel>
    <asp:Panel runat="server" id="pnlBoleto">
        <cc1:BoletoWeb ID="blt" runat="server" CssCell="BolCell" CssField="BolField" />
    </asp:Panel>
    <asp:Label runat="server" ID="lblInfo"/>
    <uc1:Rodape ID="Rodape1" runat="server" />
    </form>
       
</body>
</html>
