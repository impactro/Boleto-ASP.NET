<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CNAB-RemessaSimples.aspx.cs" Inherits="CNAB_RemessaSimples" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Remessa</title>
    <style type="text/css">
	    .BolCell { font-size: 7pt; FONT-FAMILY: Verdana }
	    .BolField { FONT-WEIGHT: bold; FONT-SIZE: 8pt; FONT-FAMILY: Verdana }
	    .BoletoWeb { page-break-after: always; margin-bottom: 100px; }
        @media print {
            /*Veja mais: http://www.w3schools.com/css/css_mediatypes.asp */
            .noprint { display: none; }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="noprint">
            <asp:Button runat="server" ID="btnTest" Text="Gerar" onclick="btnTest_Click"/><br/>
            <asp:TextBox runat="server" ID="txtOut" TextMode="MultiLine" Width="90%" Height="200" Wrap="False" /><br/>
            <asp:GridView runat="server" ID="gvHeader1"/>
            <asp:GridView runat="server" ID="gvHeader2"/>
            <asp:GridView runat="server" ID="gvItens1"/>
            <asp:GridView runat="server" ID="gvItens2"/>
            <asp:GridView runat="server" ID="gvFooter1"/>
            <asp:GridView runat="server" ID="gvFooter2"/>
            <br/>
        </div>
    </form>
</body>
</html>
