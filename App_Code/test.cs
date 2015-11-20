using Impactro.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// Exemplo de código resultado da colagem gerada pelo programa: GeraLayoutCSV-CS.aspx

[RegLayout(@"^1")]
public enum GenerateLayout
{
    /// <summary>
    /// IDENTIFICAÇÃO DO REGISTRO HEADER 
    /// </summary>
    [RegFormat(RegType.P9, 1, Default = "0")] // 1-1
    Tipo,

    /// <summary>
    /// TIPO DE OPERAÇÃO - REMESSA 
    /// </summary>
    [RegFormat(RegType.P9, 1, Default = "1")] // 2-2
    Operacao,

    /// <summary>
    /// IDENTIFICAÇÃO POR EXTENSO DO MOVIMENTO 
    /// </summary>
    [RegFormat(RegType.PX, 7, Default = "REMESSA")] // 3-9
    Remessa,

    /// <summary>
    /// IDENTIFICAÇÃO DO TIPO DE SERVIÇO 
    /// </summary>
    [RegFormat(RegType.P9, 2, Default = "1")] // 10-11
    CodServ,

    /// <summary>
    /// IDENTIFICAÇÃO POR EXTENSO DO TIPO DE SERVIÇO 
    /// </summary>
    [RegFormat(RegType.PX, 15, Default = "COBRANCA")] // 12-26
    Cobranca,

    /// <summary>
    /// AGÊNCIA MANTENEDORA DA CONTA 
    /// </summary>
    [RegFormat(RegType.P9, 4)] // 27-30
    Agencia,

    /// <summary>
    /// COMPLEMENTO DE REGISTRO 
    /// </summary>
    [RegFormat(RegType.P9, 2)] // 31-32
    ZEROS,

    /// <summary>
    /// NÚMERO DA CONTA CORRENTE DA EMPRESA 
    /// </summary>
    [RegFormat(RegType.P9, 5)] // 33-37
    Conta,

    /// <summary>
    /// DÍGITO DE AUTO CONFERÊNCIA AG/CONTA EMPRESA 
    /// </summary>
    [RegFormat(RegType.P9, 1)] // 38-38
    DAC,

    /// <summary>
    /// COMPLEMENTO DO REGISTRO 
    /// </summary>
    [RegFormat(RegType.PX, 8)] // 39-46
    BRANCOS1,

    /// <summary>
    /// NOME POR EXTENSO DA EMPRESA MÃE 
    /// </summary>
    [RegFormat(RegType.PX, 30)] // 47-76
    Empresa,

    /// <summary>
    /// Nº DO BANCO NA CÂMARA DE COMPENSAÇÃO 
    /// </summary>
    [RegFormat(RegType.P9, 3, Default = "341")] // 77-79
    CodigoBanco,

    /// <summary>
    /// NOME POR EXTENSO DO BANCO COBRADOR 
    /// </summary>
    [RegFormat(RegType.PX, 15, Default = "BANCO ITAU SA")] // 80-94
    NomeBanco,

    /// <summary>
    /// DATA DE GERAÇÃO DO ARQUIVO 
    /// </summary>
    [RegFormat(RegType.P9, 6)] // 95-100
    Geracao,

    /// <summary>
    /// COMPLEMENTO DO REGISTRO 
    /// </summary>
    [RegFormat(RegType.PX, 294)] // 101-394
    BRANCOS2,

    /// <summary>
    /// NÚMERO SEQÜENCIAL DO REGISTRO NO ARQUIVO 
    /// </summary>
    [RegFormat(RegType.P9, 6, Default = "1")] // 395-400
    Sequencia

}
