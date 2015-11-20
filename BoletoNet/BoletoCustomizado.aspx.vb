
Partial Class BoletoCustomizado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Este exemplo, é um layoout, e calculo livre, 100% customizado, que não utiliza a Impactro.Cobranca.DLL
        'Mas este exemplo utiliza as classes de funções em C# da pasta APP_CODE, 
        'que tem todas as funçõe nescessárias para a geração do boleto

        'Variáveis principais para o calculo
        Dim cAgenciaNumero As String = "501"            'Número da Agencia do Cedente
        Dim cContaNumero As String = "6703255"          'Número da Conta do Cedente
        Dim cNossoNumero As String = "3020"             'Número principal do boleto a ser gerado
        Dim nValor As Double = 35.0                     'Valor do boleto
        Dim dtVenc As DateTime = CDate("2/10/2001")    'Data de vencimento




        '1ª) parte ======================================
        '	04. LEIAUTE DO CÓDIGO DE BARRAS PADRÃO (vale para qualquer banco)
        '...............................................................    
        '   N.    POSIÇÕES     PICTURE     USAGE        CONTEÚO                
        '...............................................................    
        '    01    001 a 003    9/003/      Display      Identificação do banco
        '    02    004 a 004    9/001/      Display      9 /Real/
        '(a) 03    005 a 005    9/001/      Display      DV /*/
        '(b) 04    006 a 009    9/004/      Display      fator de vencimento
        '    05    010 a 019    9/008/v99   Display      Valor
        '    06    020 a 044    9/025/      Display      CAMPO LIVRE
        '...............................................................    
        'OBS: 1 - o digito verificador da 5 (quinta) posição é calculado     
        '         com base no módulo 11 específico previsto no item 12;         
        'OBS: 2 - o fator de vencimento é calculado com base na metodologia 
        '         descrita no item 05.                                          

        Dim cValor As String = CInt(nValor * 100)               'Multiplicando por 100 a centenas somem, e elimina-se o ponto decimal (cuidado com arredondamentos)
        Dim nFatVenc As Integer = Funcoes.CalcFatVenc(dtVenc)   'calcula o fator do vencimento
        'Concatena-se o Numero do banco (3 digitos), o código da moeda ('9' Real), o Fator de vencimento e o Valor
        Dim cCodePadrao As String = _
            "356" & _
            "9" & _
            Right("000" & nFatVenc, 4) & _
            Right("0000000000" & cValor, 10)




        '2ª) parte ======================================
        'Monta o campo livre que varia de acordo com o banco, neste caso o banco Real

        'Fixa os tamanhos das variáveis
        cNossoNumero = Right("000000000000" & cNossoNumero, 13)
        cAgenciaNumero = Right("000" & cAgenciaNumero, 4)
        cContaNumero = Right("000000" & cContaNumero, 7)

        'Calcula o Digito de controle
        Dim cDAC As String = Funcoes.Modulo10(cNossoNumero + cAgenciaNumero + cContaNumero)

        'Finaliza o campo livre
        Dim cLivre As String = cAgenciaNumero & cContaNumero & cDAC & cNossoNumero

        'O campo libre pode ser obtido por meio das rotinas internas do componente como no exemplo abaixo comentado
        'cLivre = Impactro.Cobranca.Banco_Real.CampoLivre(New Impactro.Cobranca.Boleto(), cAgenciaNumero, cContaNumero, cNossoNumero)
        'Veja o exemplo: ExemploRealCustomizado.aspx

        '3ª) parte ======================================
        'Finaliza o código de barras inserindo o digito de controle final
        Dim cDV As String = Funcoes.Modulo11Padrao(cCodePadrao & cLivre, 9)
        Dim cCodBarras As String = cCodePadrao.Substring(0, 4) & cDV & cCodePadrao.Substring(4, 14) & cLivre

        'com o código de barras, monta-se a linha digitável
        Dim cIPTE As String = Funcoes.CalcLinDigitavel(cCodBarras)

        'Transforma-se a sequencia numerica em uma string que representa o código de barras
        Dim cBarras As String = Funcoes.BarCode(cCodBarras)
        'substiue-se as duplas de caracteres que representam as barras por suas respectivas imagens
        cBarras = cBarras.Replace("bf", "<img src='imagens/b.gif' width=1 height=50>")
        cBarras = cBarras.Replace("bl", "<img src='imagens/b.gif' width=3 height=50>")
        cBarras = cBarras.Replace("pf", "<img src='imagens/p.gif' width=1 height=50>")
        cBarras = cBarras.Replace("pl", "<img src='imagens/p.gif' width=3 height=50>")






        '4ª) parte ======================================
        'Daqui para baixo é só atribuir os valores calculados aos controles na página
        lblAgenciaConta.Text = cAgenciaNumero & "/" & cContaNumero & "-" & cDAC
        lblData.Text = Now.ToShortDateString()
        lblDataDoc.Text = Now.ToShortDateString()
        lblValorDocumento.Text = String.Format("{0:C}", nValor)
        lblVencimento.Text = dtVenc.ToShortDateString()
        lblNumDoc.Text = cNossoNumero
        lblNossoNumero.Text = cNossoNumero
        lblSacado.Text = "Fábio F RG: 123.456.789-X<br>Rua xyz, 123<br>Bras - SP - CEP 12345-123"
        lblIPTE.Text = cIPTE
        lblcodBar.Text = cBarras





    End Sub

End Class
