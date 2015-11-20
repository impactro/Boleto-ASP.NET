<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BoletoVB.aspx.vb" Inherits="BoletoVB" %>
<%@ Register Assembly="Impactro.Cobranca" Namespace="Impactro.WebControls" TagPrefix="cob" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Boleto</title>
    <style type="text/css">
		.BolCell { FONT-SIZE: 7pt; FONT-FAMILY: Verdana }
	    .BolField { FONT-WEIGHT: bold; FONT-SIZE: 8pt; FONT-FAMILY: Verdana }
	</style>
</head>
<body>
    <form id="form1" runat="server">
        Final Nosso Numero:
        <asp:TextBox ID="txtNossoNumero" runat="server" MaxLength="5" Width="50px">7570</asp:TextBox>
        Data: <asp:TextBox ID="txtVencimento" runat="server" MaxLength="10" Width="80px">10/08/06</asp:TextBox>
        Valor: <asp:TextBox ID="txtValor" runat="server" MaxLength="10" Width="50px">483</asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Gerar" Width="80px" />
        <br>
        <br>
        <hr size=1 color=black>
        <br>
         <cob:BoletoWeb id="bltPag" runat="server" CssCell="BolCell" CssField="BolField" ImageCorte="corte.gif"></cob:BoletoWeb>
     </form>
</body>
</html>
