<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CNAB-Remessa.aspx.vb" Inherits="Registro_CNAB_Remessa" %>
<%@ Register Assembly="Impactro.Cobranca" Namespace="Impactro.WebControls" TagPrefix="cob" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Geração de Arquivos de Remessa para Boletos com Registro e DDA (exemplo funcional)</title>
    <style type="text/css">
		.BolCell { font-size: 7pt; font-family: Verdana; }
	    .BolField { font-weight: bold; font-size: 8pt; font-family: Verdana; }
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Geração de Arquivo de Remessa CNAB240/CNAB400 para Boletos/DDA</h1>
        <p>
            O código fonte deste programa está disponivel no <a href="https://github.com/impactro/Boleto-ASP.NET/tree/master/Registro" target="_blank">GITHUB.com/IMPACTRO</a>.<br/>
            <a href="https://github.com/impactro/Boleto-Test/wiki/Exemplo-de-Remessa-CNAB">Acesse a WIKI para mais informações</a><br/>
            Siga os passos abaixo para gerar arquivos de remessa válidos.<br/>
            Esta página é um exemplo de uso e não um serviço gratuito, por isso se limita em gerar apenas os 10 primeiros registros, você pode baixar o repositório e o fontes deste programa e alterar para usar como quiser, desde que compre os fontes do componente de geração de boletos.<br/>
            A Federação Brasileira do Bancos, em Junho/2015 descontinuou as carteira sem registros para novos contratos, assim em 2016 todas carteiras sem registros deverão ser migradas para carteiras com registros, <a href="http://www.febraban.org.br/Acervo1.asp?id_texto=2660&id_pagina=85" target="_blank">veja aqui a publicação oficial</a>.
            <br/>
            <small>Os dados abaixo não serão gravados, mas ficarão em memoria por cerca de 20 minutos (tempo de sessão)
            <br/>Neste exemplo inicial o importante é a funcionalidade e não o layout, por isso está tudo muito simples, sem nada de CSS</small>
        </p>
        
        <h2>Etapa 1 - Emitente (Antigo Cedente)</h2>
        <div style="float: left">
            Nome do Cedente: <asp:TextBox runat="server" ID="txtCedente" Text="Impactro Informática"/><br/>
            CNPJ: <asp:TextBox runat="server" ID="txtCNPJ" Text="12.345.678/0001-12"/><br/>
            Endereço: <asp:TextBox runat="server" ID="txtEndereco" Text="www.boletoasp.com.br"/><br/>
            Banco: <asp:DropDownList runat="server" ID="ddlBancos">
                <asp:ListItem Value="001">Banco do Brasil</asp:ListItem>
                <asp:ListItem Value="033">Banespa Santander</asp:ListItem>
                <asp:ListItem Value="104">Caixa Econômica Federal</asp:ListItem>
                <asp:ListItem Value="237" Selected="True">Bradesco</asp:ListItem>
                <asp:ListItem Value="341">Itaú Unibanco</asp:ListItem>
                <asp:ListItem Value="353">Santander Banespa</asp:ListItem>
                <asp:ListItem Value="748">Sicredi</asp:ListItem>
                <asp:ListItem Value="756">Sicoob</asp:ListItem>
            </asp:DropDownList> <a href="https://github.com/impactro/Boleto-Test/wiki/Bancos-Suportados" target="_blank">Veja os bancos suportados</a><br/>
            Agencia: <asp:TextBox runat="server" ID="txtAgencia" Text="1234-5" MaxLength="6"/><br/>
            Conta Corrente: <asp:TextBox runat="server" ID="txtConta" Text="67890-1" MaxLength="12"/><br/>
            Carteira: <asp:TextBox runat="server" ID="txtCarteira" Text="9" MaxLength="4"/><br/>
            Modalidade: <asp:TextBox runat="server" ID="txtModalidade" Text="1" MaxLength="4"/><br/>
            Codigo do Cedente: <asp:TextBox runat="server" ID="txtCodCedente" Text="123" MaxLength="30"/><br/>
            Código de Convênio: <asp:TextBox runat="server" ID="txtConvenio" Text="456" MaxLength="30"/><br/>
            Cedente Codigo: <asp:TextBox runat="server" ID="txtCedenteCod" Text="789" MaxLength="30"/><br/>
            <br/>
            <asp:Button runat="server" ID="btnCedenteTeste" Text="Gerar Boleto de teste" OnClick="btnCedenteTeste_Click"/> &nbsp; 
            <asp:Button runat="server" ID="btnOcultar" Text="Ocultar Boleto" Visible="false" EnableViewState="false"/><br/>
        </div>
        <div style="float: left">
            Nosso Numero: <asp:TextBox runat="server" ID="txtNossoNumero" Text="334455" /><br/>
            Valor: <asp:TextBox runat="server" ID="txtValor" Text="123,45" /><br/>
            Vencimento: <asp:TextBox runat="server" ID="txtVencimento" Text="10/06/2016" /><br/>
            Nº Documento: <asp:TextBox runat="server" ID="txtNDocumento" Text="4455" /><br/>
            <br/>

        </div>
        <div style="clear:both"></div>
        <br/>
        <asp:Label runat="server" ID="lblInfoCedente"/>
        <br/>
        <br/>
        <cob:BoletoWeb id="bltPag" runat="server" CssCell="BolCell" CssField="BolField" Visible="false" EnableViewState="false"></cob:BoletoWeb>
        
        <h2>Etapa 2 - Banco de Dados</h2>
        <p>
        Tipo de Banco de dados: <asp:DropDownList runat="server" ID="ddlProvider">
        <%--     
            <asp:ListItem Value="System.Data.OleDb" Selected="True">MDB local</asp:ListItem>
            <asp:ListItem Value="System.Data.SqlClient">MS-SQL</asp:ListItem>
            <asp:ListItem Value="System.Data.MySqlClient">MySQL</asp:ListItem>
        --%>
        </asp:DropDownList><br/>
        String de Conexão: <br/>
            <asp:TextBox runat="server" ID="txtConnectionString" Text="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|eCommerce.mdb;" Width="90%" /> <br/>
            <!-- Outros exemplos de conexão:
Data Source=localhost;Initial Catalog=teste;User ID=root;Password=123456
Provider=Microsoft.Jet.OLEDB.4.0; Data Source=W:\Boleto\Boleto.NET\App_Data\eCommerce.mdb 
            -->
        Query(select) de obtenção dos boletos: <br/>
            <asp:TextBox runat="server" ID="txtSelect" TextMode="MultiLine" Width="80%" Height="60" Text="SELECT boletoID as NossoNumero, Data as Vencimento, Valor, Nome as Pagador, '123' as Documento, 'Endereco' as Endereco FROM Boletos"/><br/>
            <!--
SELECT cob.id_Cobranca NossoNumero, cob.Emissao, cob.Documento NumeroDocumento, cob.Valor, cob.vencimento,
cli.Nome Pagador, cli.Endereco, cli.Bairro, cli.Cidade, cli.UF
FROM cobrancas cob
INNER JOIN clientes cli using(id_cliente)
            -->
        O nome dos campos retornados devem ser estes obrigatóriamente, os primeiros campos em <b>negritos</b> abaixo são obrigatórios!<br/>
        </p>
        <ul>
            <li><b>NossoNumero</b>: Identificação única do boleto no banco</li>
            <li><b>Vencimento</b>: Data de vencimento do boleto</li>
            <li><b>Valor</b>: Identificação única do boleto no banco</li>
            <li>NumeroDocumento: Nº do documento</li>
            <li>Emissao: Data de Emissão/Geração do boleto</li>
            <li><b>Pagador</b>: Nome de quem deve pagar o boleto</li>
            <li><b>Endereco</b>: Endereço do pagador</li>
            <li>Documento: CPF ou CNPJ do pagador</li>
            <li>Bairro: Bairro do pagador</li>
            <li>Cidade: Cidade do Pagador</li>
            <li>UF: Estado (UF) do pagador</li>
            <li>Baixa: Indica que é um registro de baixa (cancelamento)</li>
        </ul>
        <p>
        As regras de juros, multa, e desconto variam muito de banco para banco, e na finalidade desse exemplo não serão implementadas, assim como outras informações mais específicas.<br/>
        <asp:Button runat="server" ID="btnSelect" Text="Testar conexão e executar a query" OnClick="btnSelect_Click" />
        </p>
        <br/>
        <asp:Label runat="server" ID="lblInfoSQL"/>
        <br/>
        <br/>
        <asp:GridView runat="server" ID="gvBanco" EnableViewState="false"/>
        <h3>Visualização do Arquivo e Boleto</h3>
        <asp:TextBox runat="server" ID="txtRemessa" TextMode="MultiLine" Width="90%" Height="100" Wrap="false" EnableViewState="false" />
        <br/>
        <asp:Panel runat="server" ID="pnlBoletos" EnableViewState="false"/>
    </div>
    </form>
</body>
</html>
