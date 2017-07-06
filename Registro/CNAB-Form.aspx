<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CNAB-Form.aspx.cs" Inherits="CNAB_Form" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Boleto</title>
    <style type="text/css">
		.BolCell { font-size: 7pt; FONT-FAMILY: Verdana }
	    .BolField { FONT-WEIGHT: bold; FONT-SIZE: 8pt; FONT-FAMILY: Verdana }
	</style>
</head>
<body>
    <form id="form1" runat="server">
        Parametros (QueryString): <asp:Label runat="server" ID="lblParametros"/><br/>
        <table width="100%">
        <tr>
        <td align="center" width="50%" valign="top">Gerar Remessa<br/>
        <asp:TextBox runat="server" ID="txtRemessa" TextMode="MultiLine" Width="95%" Height="200" Wrap="false"/><br/>
        Nosso Número: <asp:TextBox runat="server" ID="txtNossoNumero" Text="1" Width="50px"/>
        Vencimento: <asp:TextBox runat="server" ID="txtVencimento" Width="110px"/><br />
        Valor: <asp:TextBox runat="server" ID="txtValor" Text="5,67" Width="70px"/>
        Quantidade: <asp:TextBox runat="server" ID="txtQTD" Text="1" Width="25px" /><br />
        <asp:Button runat="server" ID="btnRemessa" Text="Gerar Remessa e Boletos" onclick="btnRemessa_Click" />
        </td>
        <td align="center" valign="top">Ler Retorno<br/>
        <asp:TextBox runat="server" ID="txtRetorno" TextMode="MultiLine" Width="95%" Height="200" Wrap="false" /><br/>
        <br/>
        <asp:Button runat="server" ID="btnRetorno" Text="Processar Retorno" onclick="btnRetorno_Click" />
        </td>
        </tr></table>
        <asp:Label runat="server" ID="ltrInfo" EnableViewState="false"/>
        <hr color="Red"/>
        <asp:DataGrid runat="server" id="dtg" />
        <asp:Panel runat="server" ID="dvBoletos" EnableViewState="false"/>
    </form>
</body>
</html>

