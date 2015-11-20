using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Impactro.Layout;

// Exemplo de geração de RPS para prefeitura de São Paulo/SP
// [ComVisible(false)] - Adicione esta classe a um namespace, e gerando uma DLL você pode também exportar como objeto COM
public class RPSLote
{
    Reg<RPS1Cabecalho> _Cabecalho;
    RPSLoteItens _Itens;
    Reg<RPS9Rodape> _rodape;

    public RPSLote()
    {
        _Cabecalho = new Reg<RPS1Cabecalho>();
        _Itens = new RPSLoteItens();
        _rodape = new Reg<RPS9Rodape>();
    }

    public RPSLote(int nInscricao)
        : this()
    {
        _Cabecalho[RPS1Cabecalho.Inscricao] = nInscricao;
    }

    /// <summary>
    /// Define o cabeçalho, ou obtem calculando o perido baseado nos itens
    /// </summary>
    public Reg<RPS1Cabecalho> Cabecalho
    {
        get
        {
            if (_Itens.Count == 0)
                throw new Exception("Não há itens");

            DateTime dMin = (DateTime)_Itens[_Itens.Numeros[0]][RPS2Detalhe.Data];
            DateTime dMax = dMin;
            foreach (int n in _Itens.Numeros)
            {
                if (dMin > (DateTime)_Itens[n][RPS2Detalhe.Data])
                    dMin = (DateTime)_Itens[n][RPS2Detalhe.Data];
                if (dMax < (DateTime)_Itens[n][RPS2Detalhe.Data])
                    dMax = (DateTime)_Itens[n][RPS2Detalhe.Data];
            }
            _Cabecalho[RPS1Cabecalho.DataInicio] = dMin;
            _Cabecalho[RPS1Cabecalho.DataFim] = dMax;
            return _Cabecalho;
        }
        set
        {
            _Cabecalho = value;
        }
    }

    /// <summary>
    /// Retorna uma coleção de RPS para a geração do lote
    /// </summary>
    public RPSLoteItens Itens { get { return _Itens; } }

    /// <summary>
    /// Retorna a estrutura do rodapé
    /// </summary>
    public Reg<RPS9Rodape> Rodape
    {
        get
        {
            if (_Itens.Count == 0)
                throw new Exception("Não há itens");

            double nTotal = 0;
            double nDeducoes = 0;
            foreach (int n in _Itens.Numeros)
            {
                nTotal += (double)_Itens[n][RPS2Detalhe.Valor];
                nDeducoes += (double)_Itens[n][RPS2Detalhe.Deducoes];
            }
            _rodape[RPS9Rodape.Linhas] = _Itens.Count;
            _rodape[RPS9Rodape.Total] = nTotal;
            _rodape[RPS9Rodape.Deducoes] = nDeducoes;
            return _rodape;
        }
    }

    /// <summary>
    /// Retorna o texto de saida do lote
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string cOut;

        cOut = this.Cabecalho.Line + "\r\n";

        foreach (int n in this.Itens.Numeros)
            cOut += this.Itens[n].Line + "\r\n";

        cOut += this.Rodape.Line + "\r\n";

        return cOut;
    }

    /// <summary>
    /// Gera o arquivo de Lote RPS, já no formato correto, gravado em texto "ISO-8859-1"
    /// </summary>
    /// <param name="cFile">Arquivo a ser gerado</param>
    public void Write(string cFile)
    {
        // 2. Especificações - Lation 9 (ISO) 28605 - ISO-8859-1
        // 2.1. O arquivo tem o formato texto (Text Encoding = ISO-8859-1),  
        // podendo ser gerado com qualquer nome, a critério do contribuinte, 
        // devendo possuir no máximo 10 MB (10240 Kbytes) de tamanho.
        string cOut = this.ToString();
        StreamWriter sw = new StreamWriter(cFile, false, Encoding.GetEncoding("ISO-8859-1"));
        sw.Write(cOut);
        sw.Close();
    }
}

    /// <summary>
    /// Lista de RPS
    /// </summary>
    public class RPSLoteItens
    {
        SortedList<int, Reg<RPS2Detalhe>> _itens;

        /// <summary>
        /// Lista e gerencia um grupo de itens de RPS para gerar o rodapé
        /// </summary>
        public RPSLoteItens()
        {
            _itens = new SortedList<int, Reg<RPS2Detalhe>>();
        }

        /// <summary>
        /// Adiciona um item completo já predefinido
        /// </summary>
        /// <param name="rItem"></param>
        public void Add(Reg<RPS2Detalhe> rItem)
        {
            if (_itens.ContainsKey((int)rItem[RPS2Detalhe.Numero]))
                throw new Exception("Já existe uma RPS com esse numero");
            else
                _itens.Add((int)rItem[RPS2Detalhe.Numero], rItem);
        }

        /// <summary>
        /// Retorna uma RPS definida pelo seu numero
        /// </summary>
        /// <param name="nNumero"></param>
        /// <returns></returns>
        public Reg<RPS2Detalhe> this[int nNumero]
        {
            get
            {
                if (_itens.ContainsKey(nNumero))
                    return _itens[nNumero];
                else
                    throw new Exception("Este numero de RPS não existe");
            }
        }

        /// <summary>
        /// Adiciona as informações basicas da notafiscal, apos esta etapa é necessário definir o endereço
        /// </summary>
        /// <param name="nNumero">Número da RPS</param>
        /// <param name="cCPFCNPJ">CPF ou CNPJ do tomador do serviço</param>
        /// <param name="cNomeRazao">Nome ou Razão Social</param>
        /// <param name="nCodigo">Código do Serviço</param>
        /// <param name="nAliquota">Aliquota</param>
        /// <param name="cDiscriminacao">Disciminação dos serviços (as quebras de linha serão convertidas no formato exigido pela RPS/NF-e)</param>
        /// <param name="nValor">Valor</param>
        /// <param name="nDeducoes">Deduções</param>
        /// <param name="ISSretido">Reter ISS</param>
        /// <param name="dData">Data de emissão</param>
        public Reg<RPS2Detalhe> Add(int nNumero, string cCPFCNPJ, string cNomeRazao, int nCodigo, double nAliquota, string cDiscriminacao, double nValor, double nDeducoes, bool ISSretido, DateTime dData)
        {
            Reg<RPS2Detalhe> item = new Reg<RPS2Detalhe>();
            cCPFCNPJ = cCPFCNPJ.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            item[RPS2Detalhe.Numero] = nNumero;
            item[RPS2Detalhe.Data] = dData;
            item[RPS2Detalhe.Indicador] = (cCPFCNPJ.Length == 11) ? 1 : 2;
            item[RPS2Detalhe.Tomador] = cCPFCNPJ;
            item[RPS2Detalhe.RazaoSocial] = cNomeRazao;
            item[RPS2Detalhe.Aliquota] = nAliquota;
            item[RPS2Detalhe.Codigo] = nCodigo;
            item[RPS2Detalhe.Discriminacao] = cDiscriminacao.Replace("\n", "").Replace("\r", "|");
            item[RPS2Detalhe.Valor] = nValor;
            item[RPS2Detalhe.Deducoes] = nDeducoes;
            item[RPS2Detalhe.ISS] = (ISSretido) ? 1 : 2;
            Add(item);
            return item;
        }

        /// <summary>
        /// Define o endereço de uma RPS
        /// </summary>
        /// <param name="nNumero">Numero da RPS</param>
        /// <param name="cEndereco">Endereço com o Logradouro, exemplo: rua Nome</param>
        /// <param name="cNumero">Numero do endereço, ou deixe NULL, para extrair do endereço, exemplo: rua Nome, Numero</param>
        /// <param name="cComplemento">Complemento do endereço, ou deixe NULL para extrair do endereço: Rua Nome, Numero Complemento</param>
        public void SetEndereco(int nNumero, string cEndereco, string cNumero, string cComplemento)
        {
            if (!_itens.ContainsKey(nNumero))
                throw new Exception("Não existe este numero de RPS");

            Reg<RPS2Detalhe> rItem = _itens[nNumero];
            string cTipo = null;
            string cTemp = cEndereco.Trim().Split(' ')[0].ToLower();
            if (cTemp.StartsWith("av"))
                cTipo = "AV"; // Avenida
            else if (cTemp.StartsWith("pr"))
                cTipo = "PR"; // Praça
            else if (cTemp.StartsWith("r"))
                cTipo = "R"; // Rua
            else if (cTemp.Length <= 3)
                cTipo = cTemp;

            if (cTipo != null)
                cEndereco = cEndereco.Substring(cEndereco.IndexOf(" ") + 1);
            else
                cTipo = "R";

            if (cNumero == null)
            {
                string[] cParts = cEndereco.Split(',');
                if (cParts.Length >= 2)
                {
                    cEndereco = cParts[0].Trim();
                    cNumero = cParts[1].Trim();
                    if (cComplemento == null)
                    {
                        int n = cNumero.IndexOf(" ");
                        if (n > 1)
                        {
                            cComplemento = cNumero.Substring(n + 1);
                            cNumero = cNumero.Substring(0, n);
                        }
                    }
                }
                else
                    throw new Exception("Endereço do item '" + rItem[RPS2Detalhe.Numero].ToString() + "' sem numero: '" + cEndereco + "' especifique o endereço no formato: 'Rua Nome do Local, Numero Complemento'");
            }

            rItem[RPS2Detalhe.EnderecoTipo] = cTipo;
            rItem[RPS2Detalhe.EnderecoNome] = cEndereco;
            rItem[RPS2Detalhe.EnderecoNumero] = cNumero;
            rItem[RPS2Detalhe.EnderecoComplemento] = cComplemento;
        }

        /// <summary>
        /// Remove uma RPS da lista
        /// </summary>
        /// <param name="nItem">Numero da RPS</param>
        public void Remove(int nItem)
        {
            if (_itens.ContainsKey(nItem))
                _itens.Remove(nItem);
        }

        /// <summary>
        /// Conta o numero de RPS atuais
        /// </summary>
        public int Count { get { return _itens.Count; } }

        /// <summary>
        /// Retorna o registro de uma RPS
        /// </summary>
        public IList<int> Numeros { get { return _itens.Keys; } }
 
    }

/// <summary>
/// Cabeçalho da RPS para gerar NFe
/// </summary>
public enum RPS1Cabecalho
{

    /// <summary>
    /// 1) Tipo de registro (fixo "1")
    /// </summary>
    [RegFormat(RegType.P9, 1, Default = "1")]
    Tipo,

    /// <summary>
    /// 2) Versão do Arquivo (fixo "1")
    /// Indica a versão do lay-out a ser utilizada. Deve ser preenchido com o número da versão atual. A versão atual é a 001.
    /// </summary>
    [RegFormat(RegType.P9, 3, Default = "1")]
    Versao,

    /// <summary>
    /// 3) Inscrição Municipal do Prestador a que se refere o arquivo.
    /// </summary>
    [RegFormat(RegType.P9, 8)]
    Inscricao,

    /// <summary>
    /// 4) Data de Início do Período Transferido no Arquivo (AAAAMMDD)
    /// O arquivo de transferência deverá conter todos os RPS referentes a um período. Informe neste campo a Data INICIAL desse período no formato AAAAMMDD.
    /// </summary>
    [RegFormat(RegType.PD, 8)]
    DataInicio,

    /// <summary>
    /// 5) Data de Fim do Período Transferido no Arquivo (AAAAMMDD)
    /// O arquivo de transferência deverá conter todos os RPS referentes a um período. Informe neste campo a Data FINAL desse período no formato AAAAMMDD.
    /// </summary>
    [RegFormat(RegType.PD, 8)]
    DataFim
}

/// <summary>
/// Detalhe de cada RPS que irá gerar a NFe
/// </summary>
public enum RPS2Detalhe
{
    /// <summary>
    /// 1) Tipo de registro (fixo "2")
    /// </summary>
    [RegFormat(RegType.P9, 1, Default = "2")]
    Tipo,

    /// <summary>
    /// 2) Tipo do RPS (fixo "RPS")
    /// Informe o Tipo do RPS emitido com 05 posições.
    /// Tipos Válidos: Apenas RPS – Recibo Provisório de Serviços
    /// </summary>
    [RegFormat(RegType.PX, 5, Default = "RPS")]
    RPS,

    /// <summary>
    /// 3) Série do RPS
    /// Informe a Série do RPS com 05 posições.
    /// </summary>
    [RegFormat(RegType.PX, 5, Default = "A")]
    Serie,

    /// <summary>
    /// 4) Número do RPS
    /// Informe o Número do RPS com 12 posições.
    /// </summary>
    [RegFormat(RegType.P9, 12)]
    Numero,

    /// <summary>
    /// 5) Data de Emissão do RPS
    /// Informe a Data de emissão do RPS no formato AAAAMMDD.
    /// </summary>
    [RegFormat(RegType.PD, 8)]
    Data,

    /// <summary>
    /// 6) Situação do RPS
    /// Informe a Situação do RPS com 01 posição, de acordo com o tipo do RPS.
    /// T - Operação normal (tributação conforme documento emitido)
    /// I - Operação isenta ou não tributável, executadas no Município de São Paulo
    /// F – Operação isenta ou não tributável pelo Município de São Paulo, executada em outro Município
    /// C - Cancelado
    /// E – Extraviado
    /// J – ISS Suspenso por Decisão Judicial (neste caso, informar no campo Discriminação dos Serviços, o número do processo judicial na 1a. instância).
    /// </summary>
    [RegFormat(RegType.PX, 1, Default = "T")]
    Situacao,

    /// <summary>
    /// 7) Valor dos Serviços
    /// Informe o Valor dos Serviços com 15 posições. Campo obrigatório caso a situação do RPS seja diferente de “C” (Cancelado) e “E” (Extraviado).
    /// </summary>
    [RegFormat(RegType.PV, 15)]
    Valor,

    /// <summary>
    /// 8) Valor das Deduções
    /// </summary>
    [RegFormat(RegType.PV, 15)]
    Deducoes,

    /// <summary>
    /// 9) Código do Serviço Prestado
    /// Informe o Código do Serviço do RPS com 05 posições. Este código deve pertencer à lista de serviços.
    /// </summary>
    [RegFormat(RegType.P9, 5)]
    Codigo,

    /// <summary>
    /// 10) Alíquota
    /// Obs.: O conteúdo deste campo será ignorado caso a tributação ocorra no município (Situação do RPS = T)
    /// </summary>
    [RegFormat(RegType.PV, 4)]
    Aliquota,

    /// <summary>
    /// 11) ISS Retido
    /// 1 para ISS Retido.
    /// 2 para Nota Fiscal sem ISS Retido.
    /// </summary>
    [RegFormat(RegType.P9, 1)]
    ISS,

    /// <summary>
    /// 12) Indicador de CPF/CNPJ do Tomador
    /// Este campo indica o tipo de dados que será fornecido no campo CPF/CNPJ do Tomador
    /// 1 para CPF.
    /// 2 para CNPJ.
    /// 3 para CPF não-informado.
    /// </summary>
    [RegFormat(RegType.P9, 1)]
    Indicador,

    /// <summary>
    /// 13) CPF ou CNPJ do Tomador
    /// Informe o CNPJ do tomador com 14 posições ou CPF do tomador com 11 posições.
    /// O conteúdo deste campo será ignorado caso o campo 14 esteja preenchido.
    /// </summary>
    [RegFormat(RegType.P9, 14)]
    Tomador,

    /// <summary>
    /// 14) Inscrição Municipal do Tomador
    /// Informe a Inscrição Municipal do Tomador, com 8 posições.
    /// ATENÇÃO!!! Este campo só deverá ser preenchido para tomadores estabelecidos no município de São Paulo (CCM).
    /// Quando este campo for preenchido, seu conteúdo será considerado como prioritário com relação ao campo de CPF/CNPJ do Tomador, sendo utilizado para identificar o Tomador e recuperar seus dados da base de dados da Prefeitura.
    /// </summary>
    [RegFormat(RegType.P9, 8)]
    Inscricao,

    /// <summary>
    /// 15) Inscrição Estadual do Tomador
    /// Este campo será ignorado caso seja fornecido um CPF/CNPJ ou a Inscrição Municipal do tomador pertença ao município de São Paulo.
    /// </summary>
    [RegFormat(RegType.P9, 12)]
    IE,

    /// <summary>
    /// 16) Nome / Razão Social do Tomador
    /// Este campo é obrigatório apenas para tomadores Pessoa Jurídica (CNPJ)
    /// Este campo será ignorado caso seja fornecido um CPF/CNPJ ou a Inscrição Municipal do tomador pertença ao município de São Paulo.
    /// </summary>
    [RegFormat(RegType.PX, 75)]
    RazaoSocial,

    /// <summary>
    /// 17) Tipo do Endereço do Tomador (Rua, Av, ...)
    /// </summary>
    [RegFormat(RegType.PX, 3)]
    EnderecoTipo,

    /// <summary>
    /// 18) Endereço do Tomador
    /// </summary>
    [RegFormat(RegType.PX, 50)]
    EnderecoNome,

    /// <summary>
    /// 19) Número do Endereço do Tomador
    /// </summary>
    [RegFormat(RegType.PX, 10)]
    EnderecoNumero,

    /// <summary>
    /// 20) Complemento do Endereço do Tomador
    /// </summary>
    [RegFormat(RegType.PX, 30)]
    EnderecoComplemento,

    /// <summary>
    /// 21) Bairro do Tomador
    /// </summary>
    [RegFormat(RegType.PX, 30)]
    Bairro,

    /// <summary>
    /// 22) Cidade do Tomador
    /// Se a Cidade/UF forem preenchidos e não forem encontrados na base de dados da Prefeitura, o sistema irá pesquisar a cidade correspondente ao CEP (se este for informado).
    /// Note que apenas tomadores cuja Cidade / UF seja igual a SÃO PAULO / SP, irão receber créditos.
    /// </summary>
    [RegFormat(RegType.PX, 50)]
    Cidade,

    /// <summary>
    /// 23) UF do Tomador
    /// </summary>
    [RegFormat(RegType.PX, 2)]
    UF,

    /// <summary>
    /// 24) CEP do Tomador
    /// </summary>
    [RegFormat(RegType.PX, 8)]
    CEP,

    /// <summary>
    /// 25) Email do Tomador
    /// Campo contendo o e-mail do tomador
    /// </summary>
    [RegFormat(RegType.PX, 75)]
    eMail,

    /// <summary>
    /// 26) Discriminação dos Serviços
    /// Texto contínuo descritivo dos serviços. O conjunto de caracteres correspondentes ao código ASCII 13 e ASCII 10 deverá ser substituído pelo caracter | (pipe ou barra vertical. ASCII 124).
    /// Exemplo: “Lavagem de carro|com lavagem de motor”
    /// Não devem ser colocados espaços neste campo para completar seu tamanho máximo, devendo o campo ser preenchido apenas com conteúdo a ser processado / armazenado.
    /// (*) Este campo é impresso num retângulo com 95 caracteres (largura) e 24 linhas (altura). É permitido (não recomendável), o uso de mais de 1000 caracteres. Caso seja ultrapassado o limite de 24 linhas, o conteúdo será truncado durante a impressão da Nota.
    /// </summary>
    [RegFormat(RegType.PX, 0)]
    Discriminacao
}

/// <summary>
/// Cabeçalho da RPS
/// </summary>
//[ComVisible(false)]
public enum RPS9Rodape
{
    /// <summary>
    /// Tipo de registro (opcional: fixo "9")
    /// </summary>
    [RegFormat(RegType.P9, 1, Default = "9")]
    Tipo,

    /// <summary>
    /// Número de linhas de detalhe do arquivo
    /// Número de linhas de detalhe (Tipo 2 +Tipo 3) contidas no arquivo.
    /// </summary>
    [RegFormat(RegType.P9, 7)]
    Linhas,

    /// <summary>
    /// Valor total dos serviços contido no arquivo
    /// Informe a soma dos valores dos serviços das linhas de detalhe (Tipo 2 + Tipo 3) contidas no arquivo.
    /// </summary>
    [RegFormat(RegType.PV, 15)]
    Total,

    /// <summary>
    /// Valor total das deduções contidas no arquivo
    /// Informe a soma dos valores das deduções das linhas de detalhe (Tipo 2 + Tipo 3) contidas no arquivo.
    /// </summary>
    [RegFormat(RegType.PV, 15)]
    Deducoes
}

/// <summary>
/// Estrutura basica de leitura do retorno de uma NFEe
/// </summary>
//[ComVisible(false)]
[RegLayout(@"^2\d+", DateFormat8 = "yyyyMMdd", DateFormat14 = "yyyyMMddHHmmss")]
public enum NFeV2detalhe
{
    /// <summary>
    /// Tipo de registro
    /// Será preenchido com valor “2”, indicando linha de detalhe
    /// </summary>
    [RegFormat(RegType.P9, 1)]
    Tipo,

    /// <summary>
    /// Número da NF-e
    /// </summary>
    [RegFormat(RegType.P9, 8)]
    NFe,

    /// <summary>
    /// Data/hora de emissão da NF-e no formato AAAAMMDDHHmmSS.
    /// </summary>
    [RegFormat(RegType.PD, 14)]
    DataHora,

    /// <summary>
    /// Código de verificação da NF-e
    /// </summary>
    [RegFormat(RegType.PX, 8)]
    Verificacao,

    /// <summary>
    /// Tipo de RPS
    /// RPS – Recibo Provisório de Serviços (equivalente às extintas NFS, NFFS e NFSS).
    /// RPS-M – Recibo Provisório de Serviços provenientes de Nota Fiscal Conjugada (Mista).
    /// RPS-C – Recibo Provisório de Serviços provenientes de Cupom Fiscal.
    /// </summary>
    [RegFormat(RegType.PX, 5)]
    RPS_Tipo,

    /// <summary>
    /// Série do RPS com 05 posições
    /// </summary>
    [RegFormat(RegType.PX, 5)]
    RPS_Serie,

    /// <summary>
    /// Numero da RPS
    /// </summary>
    [RegFormat(RegType.P9, 12)]
    RPS_Numero,

    /// <summary>
    /// Data de emissão do RPS no formato AAAAMMDD.
    /// </summary>
    [RegFormat(RegType.PD, 8)]
    RPS_Data
}