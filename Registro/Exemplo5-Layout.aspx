<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Exemplo5-Layout.aspx.cs" Inherits="Registro_Exemplo5_Layout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Varias linhas de vários tipos de registros: 
        <asp:RadioButtonList runat="server" ID="rblTipo" RepeatLayout="Flow" RepeatDirection="Horizontal">
            <asp:ListItem>Remessa</asp:ListItem>
            <asp:ListItem Selected="True">Retorno</asp:ListItem>
         </asp:RadioButtonList>
         </br>
        <asp:TextBox runat="server" ID="txt" Width="90%" Height="200px" TextMode="MultiLine" Wrap="false">02RETORNO01COBRANCA       00000000000004419149RK DISTRIBUIDORA DE ALIMENTOS 237BRADESCO       2603120160000000011                                                                                                                                                                                                                                                                          260312         000001
10203804528000101000000900301020820040000000000000000000000643000000000000000064350000000000000000000000000902260312000000064300000000000000006435120412000000003154523704151  000000000030000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000                          0000000000                                                                  000002
10203804528000101000000900301020820040000000000000000000000666000000000000000066640000000000000000000000000903260312000000066600000000000000006664120423000000039808823700109  000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000                          1800000000                                                                  000028
9201237          000000210000000138906000000011          00021000001389060000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000                                                                                                                                                                              00000000000000000000000         000029</asp:TextBox><br/>
        <asp:Button runat="server" ID="btn" Text="Testar" onclick="btn_Click"/><br/>
        <asp:Label runat="server" ID="lbl"/><br/>
        <asp:DataGrid runat="server" ID="dtg1"/>
        <asp:DataGrid runat="server" ID="dtg2"/>
        <asp:DataGrid runat="server" ID="dtg3"/>
    </div>
    </form>
</body>
</html>
