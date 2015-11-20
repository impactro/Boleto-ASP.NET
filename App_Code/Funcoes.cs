using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class Funcoes
{
    /// <summary>
    /// Calcula apenas a SOMA dos digitos retornados pela multiplicação dos pesos de acordo com a base selecionada
    /// Esta rotina é utilizada pelas rotinas Modulo11Padrao, Modulo11Especial para obter o valor total dos pessos.
    /// </summary>
    /// <param name="Sequencia">Sequencia Numerica a ser calculada</param>
    /// <param name="NumBase">É o Valor do Peso Máximo do multiplicador, 7, se de 7 a 2 (765432), ou 9, se for de 9 a 2 (98765432)</param>
    /// <returns>Valor Total da soma dos pesos</returns>
    public static int Modulo11Total(string Sequencia, int NumBase)
    {
        int Numero;
        int Contador = 0;
        int Multiplicador = 2;
        int TotalNumero = 0;

        // Para ser passado na geração da Exception em caso de conter conteudo não numerico na Sequencia informada
        string Caracter = "NULL";

        try
        {
            for (Contador = Sequencia.Length - 1; Contador >= 0; Contador--)
            {
                Caracter = Sequencia.Substring(Contador, 1);
                Numero = Int32.Parse(Caracter) * Multiplicador;
                TotalNumero += Numero;
                Multiplicador++;
                if (Multiplicador > NumBase)
                    Multiplicador = 2;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(String.Format("ERRO: {0} \r\nSequencia: '{1}' Base: '{2}' Posição: '{3}' Caracter: '{4}'", ex.Message, Sequencia, NumBase, Contador, Caracter), ex);
        }
        return TotalNumero;
    }

    /// <summary>
    /// O Modulo 11 Padrão, é utilizado utilizado para o calculo do digito verificador do código de barras, e tambem por alguns bancos para calculo de digitos no Nosso Numero, Conteoles, e verificações
    /// </summary>
    /// <param name="Sequencia">Sequencia Numerica a ser calculada</param>
    /// <param name="NumBase">É o Valor do Peso Máximo do multiplicador, 7, se de 7 a 2 (765432), ou 9, se for de 9 a 2 (98765432)</param>
    /// <returns>O Digito Verificador</returns>
    /// <remarks></remarks>
    public static int Modulo11Padrao(string Sequencia, int NumBase)
    {
        int TotalNumero = Modulo11Total(Sequencia, NumBase);
        TotalNumero *= 10;
        int Resto = TotalNumero % 11;
        if (Resto == 0 || Resto == 1 || Resto == 10)
            return 1;
        else
            return Resto;

        //(versão antiga, mas gera o mesmo valor)
        //int Resto = Modulo11Total(Sequencia, NumBase) % 11;
        //int resultado = 11 - Resto;
        //if (resultado == 10 || resultado == 11)
        //    return 1;
        //return resultado;
    }

    /// <summary>
    /// O Modulo 11 Especial, é utilizado por alguns bancos que utilizam o digito 0 quando o reto é 10
    /// </summary>
    /// <param name="Sequencia">Sequencia Numerica a ser calculada</param>
    /// <param name="NumBase">É o Valor do Peso Máximo do multiplicador, 7, se de 7 a 2 (765432), ou 9, se for de 9 a 2 (98765432)</param>
    /// <returns>O Digito Verificador</returns>
    /// <value>teste value</value>
    /// <example>teste exemplo</example>
    /// <remarks>teste remarks</remarks>
    public static int Modulo11Especial(string Sequencia, int NumBase)
    {
        int TotalNumero = Modulo11Total(Sequencia, NumBase);
        TotalNumero *= 10;
        int Resto = TotalNumero % 11;
        if (Resto == 10) // Se Resto = 0 (zero) retorna o normalmente via 'else'
            return 0;
        else
            return Resto;
    }

    /// <summary>
    /// Calcula o Fator de vencimento, que representa o numero de dias corridos desde a datab base 7/10/1997
    /// </summary>
    /// <param name="DataVencimento">Data de Vendimento, a ser calculada (Use: DateTime.MinValue, para retornar 0 (zero) que representa o 'contra apresentação')</param>
    /// <returns>Retorna o numero de dias desde 7/10/1997, ou 0(Zero) se DataVencimento=DateTime.MinValue</returns>
    public static int CalcFatVenc(DateTime DataVencimento)
    {
        // Verifica se é sem data de Vencimento (contra apresentação)
        if (DataVencimento == DateTime.MinValue)
            return 0;

        DateTime dtBase = new DateTime(1997, 10, 7);
        TimeSpan Result = DataVencimento.Subtract(dtBase);
        if (Result.Days < 0 && Result.Days > 9999)
            throw new Exception("Data de vencimento inválida");
        return Result.Days;
    }

    /// <summary>
    /// Calcula a Linha Digitável do Código de Barras
    /// </summary>
    /// <param name="CodBarras">Código de barras com 44 numeros, sem espaços ou pontos, SOMENTE NUMEROS</param>
    /// <returns></returns>
    public static string CalcLinDigitavel(string CodBarras)
    {
        if (CodBarras.Length != 44)
            throw new Exception("O Código de Barras deve ter 44 numeros! Valor Informado: '" + CodBarras + "' Lenth: " + CodBarras.Length.ToString() );

        string cCampo1, cCampo2, cCampo3, cCampo4, cCampo5;

        cCampo1 = CodBarras.Substring(0, 4) + CodBarras.Substring(19, 5);
        cCampo1 += Modulo10(cCampo1);
        cCampo1 = cCampo1.Substring(0, 5) + "." + cCampo1.Substring(5, 5);

        cCampo2 = CodBarras.Substring(24, 10);
        cCampo2 += Modulo10(cCampo2);
        cCampo2 = cCampo2.Substring(0, 5) + "." + cCampo2.Substring(5, 6);

        cCampo3 = CodBarras.Substring(34, 10);
        cCampo3 += Modulo10(cCampo3);
        cCampo3 = cCampo3.Substring(0, 5) + "." + cCampo3.Substring(5, 6);

        cCampo4 = CodBarras.Substring(4, 1);
        cCampo5 = CodBarras.Substring(5, 14);

        return cCampo1 + " " + cCampo2 + " " + cCampo3 + " " + cCampo4 + " " + cCampo5;
    }

    /// <summary>
    /// Obtem o código de barras baseado no IPTE
    /// </summary>
    /// <remarks>Este código apenas extrai, o codigo de barras sem valida-lo</remarks>
    /// <param name="IPTE">Linha digitável</param>
    /// <returns>Código de barras</returns>
    public static string CalcCodBar(string IPTE)
    {
        // Retira pontos e espaços
        IPTE = IPTE.Replace(" ", "");
        IPTE = IPTE.Replace(".", "");

        if (IPTE.Length != 47)
            throw new Exception("IPTE não contem 47 posições");

        string cCodBar;

        // Código do banco e moeda
        cCodBar = IPTE.Substring(0, 3);
        cCodBar += ".";
        cCodBar += IPTE.Substring(3, 1);
        cCodBar += ".";

        // Digito de verificação principal
        cCodBar += IPTE.Substring(32, 1);
        cCodBar += ".";

        // Vencimento 
        cCodBar += IPTE.Substring(33, 4);
        cCodBar += "."; 
        
        // Valor
        cCodBar += IPTE.Substring(37, 10);
        cCodBar += "-";

        // Campo Livre
        cCodBar += IPTE.Substring(4, 5);
        cCodBar += IPTE.Substring(10, 10);
        cCodBar += IPTE.Substring(21, 10);

        return cCodBar;

    }

    /// <summary>
    /// O Modulo 10 representa é um digito baseado mo modulo da soma dos pesos de cada digito multiplicados por 2 ou 1
    /// </summary>
    /// <param name="Cadeia">Sequencia Numerica a ser calculada</param>
    /// <returns>O Digito Verificador</returns>
    public static int Modulo10(string Cadeia)
    {
        int Mult, Total, Pos, Res;
        Mult = Cadeia.Length % 2;
        Mult++;
        Total = 0;
        for (Pos = 0; Pos < Cadeia.Length; Pos++)
        {
            Res = Int32.Parse(Cadeia.Substring(Pos, 1)) * Mult;
            if (Res > 9)
                Res = Res / 10 + (Res % 10);

            Total += Res;

            if (Mult == 2)
                Mult = 1;
            else
                Mult = 2;
        }

        Total = ((10 - (Total % 10)) % 10);

        return Total;
    }

    /// <summary>
    /// Gera uma string que representa um código de barras de um numero especifico
    /// </summary>
    /// <param name="NumTexto">digitos a serem codificados</param>
    /// <returns>retona uma sequancia das ssequencias "bf","bl","pf","pl"</returns>
    /// <remarks>
    /// bf -> Branco Fino
    /// bl -> Branco Largo
    /// pf -> Preto Fino
    /// pl -> Preto Largo
    /// </remarks>
    /// <see cref="http://www.guj.com.br/java.tutorial.artigo.34.1.guj"/>
    public static string BarCode(String NumTexto)
    {
        System.Text.StringBuilder cOut = new System.Text.StringBuilder("");
        string f, texto;
        int fi, f1, f2, i;
        string[] BarCodes = new string[100];
        BarCodes[0] = "00110";
        BarCodes[1] = "10001";
        BarCodes[2] = "01001";
        BarCodes[3] = "11000";
        BarCodes[4] = "00101";
        BarCodes[5] = "10100";
        BarCodes[6] = "01100";
        BarCodes[7] = "00011";
        BarCodes[8] = "10010";
        BarCodes[9] = "01010";

        for (f1 = 9; f1 >= 0; f1--)
        {
            for (f2 = 9; f2 >= 0; f2--)
            {
                fi = f1 * 10 + f2;
                texto = "";
                for (i = 0; i < 5; i++)
                {
                    texto +=
                        BarCodes[f1].Substring(i, 1) +
                        BarCodes[f2].Substring(i, 1);
                }
                BarCodes[fi] = texto;
            }
        }

        // Inicialização padrão
        cOut.Append("pf");
        cOut.Append("bf");
        cOut.Append("pf");
        cOut.Append("bf");

        texto = NumTexto;
        if (texto.Length % 2 != 0)
            texto = "0" + texto;

        //Draw dos dados
        while (texto.Length > 0)
        {
            i = Int32.Parse(texto.Substring(0, 2));
            texto = texto.Substring(2);
            f = BarCodes[i];
            for (i = 0; i < 10; i += 2)
            {
                if (f.Substring(i, 1) == "0")
                    cOut.Append("pf");
                else
                    cOut.Append("pl");

                if (f.Substring(i + 1, 1) == "0")
                    cOut.Append("bf");
                else
                    cOut.Append("bl");

            }
        }

        // Finalização padrão
        cOut.Append("pl");
        cOut.Append("bf");
        cOut.Append("pf");

        return cOut.ToString();
    }
}
