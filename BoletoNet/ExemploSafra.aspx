<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExemploSafra.aspx.cs" Inherits="ExemploSafra" %>
<%@ Register Assembly="Impactro.Cobranca" Namespace="Impactro.WebControls" TagPrefix="cob" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Boleto</title>
    <style type="text/css">
		.BolCell { FONT-SIZE: 7pt; FONT-FAMILY: Verdana }
	    .BolField { FONT-WEIGHT: bold; FONT-SIZE: 8pt; FONT-FAMILY: Verdana }
	</style>
</head>
<body>
    <form id="form1" runat="server">
        Cod.Barras: <b><asp:Label runat="server" ID="lblInfo"/></b><br/>
         <cob:BoletoWeb id="bltPag" runat="server" CssCell="BolCell" CssField="BolField" ImageCorte="corte.gif"></cob:BoletoWeb>
     </form>
</body>
</html>
