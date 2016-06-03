<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Suporte1.aspx.vb" Inherits="BoletoNet_Suporte1" %>
<%@ Register Assembly="Impactro.Cobranca" Namespace="Impactro.WebControls" TagPrefix="cob" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Boleto</title>
    <style type="text/css">
		.BolCell { font-size: 7pt; font-family: Verdana; }
	    .BolField { font-weight: bold; font-size: 12px; font-family: arial; }
        /* alguns banos estão exigindo o boleto em tamanhos mais específicos, por hora a unica solução é aplicar um zoom proporcional */
        /* .BoletoWeb { zoom: 0.9; margin-bottom: 15px; margin-top: 15px; } */
	</style>
</head>
<body>
    <form id="form2" runat="server">
         <cob:BoletoWeb id="bltPag" runat="server" CssCell="BolCell" CssField="BolField" ></cob:BoletoWeb>
     </form>
</body>
</html>