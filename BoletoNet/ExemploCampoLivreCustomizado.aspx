<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExemploCampoLivreCustomizado.aspx.vb" Inherits="ExemploCampoLivreCustomizado" %>
<%@ Register Assembly="Impactro.Cobranca" Namespace="Impactro.WebControls" TagPrefix="cob" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Boleto</title>
    <style type="text/css">
		.BolCell { font-size: 7pt; FONT-FAMILY: Verdana }
	    .BolField { FONT-WEIGHT: bold; FONT-SIZE: 8pt; FONT-FAMILY: Verdana }
	</style>
</head>
<body>
    <form id="form1" runat="server">
         <cob:BoletoWeb id="bltPag" runat="server" CssCell="BolCell" CssField="BolField" ImageCorte="corte.gif"></cob:BoletoWeb>
     </form>
</body>
</html>
