<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BoletoSQL.aspx.vb" Inherits="BoletoSQL" %>

<%@ Register Assembly="Impactro.Cobranca" Namespace="Impactro.WebControls" TagPrefix="cob" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Exemplo de geração de boletos com consulta a dados em SQL</title>
     <style type="text/css">
		.BolCell { font-size: 7pt; FONT-FAMILY: Verdana }
	    .BolField { FONT-WEIGHT: bold; FONT-SIZE: 8pt; FONT-FAMILY: Verdana }
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <cob:BoletoWeb id="blt" runat="server" CssCell="BolCell" CssField="BolField"></cob:BoletoWeb>
    </form>
</body>
</html>
