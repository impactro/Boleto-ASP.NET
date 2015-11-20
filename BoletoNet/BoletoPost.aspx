<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BoletoPost.aspx.vb" Inherits="BoletoPost" %>
<%@ Register assembly="Impactro.Cobranca" namespace="Impactro.WebControls" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Santander</title>
    <style type="text/css">
		.BolCell { FONT-SIZE: 7pt; FONT-FAMILY: Verdana }
	    .BolField { FONT-WEIGHT: bold; FONT-SIZE: 8pt; FONT-FAMILY: Verdana }
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:BoletoWeb id="bltPag" runat="server" CssCell="BolCell" CssField="BolField" ImageCorte="corte.gif" />
    </form>
</body>
</html>
