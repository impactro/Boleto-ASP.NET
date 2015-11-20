<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BaixaAutomaticaER.aspx.vb" Inherits="BaixaAutomaticaER" ValidateRequest="false" %>
<%@ Register src="Rodape.ascx" tagname="Rodape" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BoletoASP.Net: Baixa Automática de Título de Cobrança por Expressão Regular</title>
    <meta name="Description" content="Este exemplo demostra como executar uma baixa automática por meio de reconhecimento com expressão regulares (Regex.Match) de titulos de cobrança (boletos) inseridas por colagem (ctrl+c/ctrl+v) de um extrato de movimentação bancário" />
    <meta name="keywords" content="BoletoASP.Net, Boleto ASP, Boleto, ASP.Net, Título de Cobrança, ER, Expressão Regular, Regex.Match, Regex, Match, System.Text.RegularExpressions, RegularExpressions"/>
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

    <b>Selecione o Banco ou crie sua expressão regular em &#39;outros&#39;:</b><br />

    <asp:RadioButton ID="rbBradesco" runat="server" Text="Bradesco" GroupName="BANCO" AutoPostBack="True" />
    <asp:RadioButton ID="rbUnibanco" runat="server" Text="Unibanco" GroupName="BANCO" AutoPostBack="True" />
    <asp:RadioButton ID="rbOutros" runat="server" Text="Outros" GroupName="BANCO" AutoPostBack="True" />
    <br />
&nbsp;(entre em cntato que ajudaremos você a criar uma expressão regular que sirva 
    para seu banco)<br />
    <b>Espressão Regular (expressão que retorna os dados desejados,
    <a href="http://pt.wikipedia.org/wiki/Express%C3%A3o_regular" target="_blank">saiba mais</a>):</b><br />
    <asp:TextBox ID="txtER" runat="server" Width="700px"></asp:TextBox>
    <br />
    <b>Texto de entrada (colagem do extrato bancário):</b><br />
    <asp:TextBox ID="txtIN" runat="server" Height="200px" TextMode="MultiLine" 
        Width="700px"></asp:TextBox>
    <br />    <asp:Button ID="btnProcess" runat="server" Text="Processar" />

    <br/> <br/> <b>Resultado da busca:</b><br />
    <asp:Label ID="lblInfo" runat="server"></asp:Label>


    <br /><br />
    
    <b>DataGrid com os Resultados com ?&lt;id&gt;, ?&lt;data&gt;, ?&lt;valor&gt;:</b><br />

    <asp:DataGrid runat="server" ID="dtgResult"></asp:DataGrid>


    <br />
    <br />
    <i>Neste exemplo o grande segredo do código fontes está no uso do metodo &#39;Match&#39; 
    da classe &#39;Regex&#39;</i><br />
    <b>m = Regex.Match(txtIN.Text, txtER.Text)</b>
    <br />Veja mais funcções no namespace: System.Text.RegularExpressions<br/>
    <uc1:Rodape ID="Rodape1" runat="server" />
    </form>
    

</body>
</html>
