<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Consessionaria.aspx.vb" Inherits="Consessionaria" %>
<%@ Register src="Rodape.ascx" tagname="Rodape" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BoletoASP.Net: Título de Concessiona/Arrecadação, Código de Barras</title>
    <meta name="Description" content="Prefeituras, Órgãos Governamentais (Saneamento, Energia Elétrica e Gás), Telecomunicações, Multas de trânsito, podem emitir títulos de arrecadação (Boleto Concessionária) com um código de barras específico" />
    <meta name="keywords" content="BoletoASP.Net, Boleto ASP, Boleto, ASP.Net, Concessionária, arrecadação, Identificação do Segmento, Prefeituras, Saneamento, Energia Elétrica, Gás, Telecomunicações, Órgãos Governamentais, Carnes, Multas de trânsito, Empresa/Órgão, Identificação do Segmento"/>
    <style type="text/css">
        body
        {
        	font-family: Verdana;
        	font-size: 10pt;
        }
    </style></head>
<body>
    <form id="form1" runat="server">
    <table width=640 height=200 bgcolor="#ffffcc"><tr><td align=center>Conteúdo Customizado<br><br>Monte aqui seu conteudo customizado</td></tr></table>
    <font ></font>
        <asp:Label ID="lblDigitos" runat="server" Font-Names="Courier New" Font-Size="9pt"></asp:Label><br />
        <asp:Label ID="lblCodBar" runat="server"></asp:Label>
        <br />
        <br />
        <pre>'Composição do Código de Barras
        'POSIÇÃO    -  TAM  - CONTEÚDO
        '01 – 01    -   1   - Identificação do Produto
        '                       Constante "8" para identificar arrecadação
        '02 – 02	-   1   - Identificação do Segmento 
        '                       1. Prefeituras
        '                       2. Saneamento
        '                       3. Energia Elétrica e Gás
        '                       4. Telecomunicações
        ' 	                    5. Órgãos Governamentais
        '                       6. Carnes e Assemelhados ou demais Empresas / Órgãos que serão identificadas através do CNPJ.
        '                       7. Multas de trânsito
        '                       9. Uso interno do banco
        '03 – 03	-   1   - Identificação do valor real ou referência
        '                       Geralmente "6" valor a ser cobrado efetivamente em reais.
        '04 – 04	-   1   - Dígito verificador geral (módulo 10 )
        '05 – 15    -   11  - Valor
        '== opção 1 ==
        '16 – 19	-   4   - Identificação da Empresa/Órgão
        '20 – 44	-   25  - Campo livre de utilização da Empresa/Órgão
        '== opção 2 ==
        '16 – 23    -   8   - CNPJ / MF
        '24 – 44    -   21  - Campo livre de utilização da Empresa/Órgão
        </pre>
        <uc1:Rodape ID="Rodape1" runat="server" />
        </form>
        </body>
</html>
