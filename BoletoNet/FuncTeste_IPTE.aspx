<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FuncTeste_IPTE.aspx.cs" Inherits="FuncTeste_IPTE" %>

<%@ Register src="Rodape.ascx" tagname="Rodape" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BoletoASP.Net: Conversão do Código de Barras em IPTE (Linha digitável)</title>
    <meta name="Description" content="Veja como converter o código de barras de um boleto em linha digitável (IPTE), este exemplo acompanha os fontes em C#" />
    <meta name="keywords" content="BoletoASP.Net, Boleto ASP, Boleto, ASP.Net, Título de Cobrança, IPTE, linha digitável, Código de Barras, C#, Algoritimo, Conversão, Código Fonte"/>
    <style type="text/css">
        body
        {
        	font-family: Verdana;
        	font-size: 10pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <p>Veja como converter o código de barras de um boleto em linha digitável (IPTE), este exemplo acompanha os fontes em C#<br>
    alguns geradores de boleto se baseiam no IPTE para gerar o boleto e em seguida o código de barras<br>
    mas a maioria gera o código de barras e depois o IPTE<br>
    Digite e código de barras e veja o IPTE gerado, com sua respectiva imagem de código de barras</p>
        Código de Barras:
        <asp:TextBox ID="txtCodBar" runat="server" Width="400px">409  9  2  1546  0000100000  5  1234561  00  112233445566777</asp:TextBox>
        <asp:Button ID="btnExecute" runat="server" OnClick="btnExecute_Click" Text="Gerar" /><br />
        IPTE:
        <b><asp:Label ID="lblIPTE" runat="server" Text="?"></asp:Label></b>
        <br><br>
        Código de Barras:<br>
        <asp:Label ID="lblCodBar" runat="server" Text="?"></asp:Label>
    <uc1:Rodape ID="Rodape1" runat="server" />
    </form>
</body>
</html>
