-- Script de criação de banco MySQL


CREATE TABLE `clientes` (
  `id_Cliente` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) CHARACTER SET latin1 NOT NULL,
  `Endereco` varchar(100) CHARACTER SET latin1 NOT NULL,
  `Bairro` varchar(50) CHARACTER SET latin1 NOT NULL,
  `Cidade` varchar(50) CHARACTER SET latin1 NOT NULL,
  `UF` varchar(2) CHARACTER SET latin1 NOT NULL,
  `Documento` varchar(45) COLLATE latin1_general_ci DEFAULT NULL,
  PRIMARY KEY (`id_Cliente`)
);

INSERT INTO `clientes` (`id_Cliente`,`Nome`,`Endereco`,`Bairro`,`Cidade`,`UF`,`Documento`) VALUES (1,'Fabio','Minha Rua','No meu bairro','Minha Cidade','SP','111.222.333-56');
INSERT INTO `clientes` (`id_Cliente`,`Nome`,`Endereco`,`Bairro`,`Cidade`,`UF`,`Documento`) VALUES (2,'Outra pessoa','Avenida sei lá aonde','XYZ','Luz','RJ','05.006.007/0001-12');

CREATE TABLE `cobrancas` (
  `id_Cobranca` int(11) NOT NULL AUTO_INCREMENT,
  `id_Cliente` int(11) NOT NULL,
  `Emissao` datetime NOT NULL,
  `Documento` int(11) NOT NULL,
  `Descricao` varchar(100) CHARACTER SET latin1 NOT NULL,
  `Valor` double NOT NULL,
  `Vencimento` date NOT NULL,
  `Pago` bit(1) DEFAULT b'0',
  `Cancelada` bit(1) DEFAULT b'0',
  `Remessa` bit(1) DEFAULT b'0',
  PRIMARY KEY (`id_Cobranca`)
);

INSERT INTO `cobrancas` (`id_Cobranca`,`id_Cliente`,`Emissao`,`Documento`,`Descricao`,`Valor`,`Vencimento`,`Pago`,`Cancelada`,`Remessa`) VALUES (1,1,'2016-06-01 10:12:13',1122,'Meu produto online',123.45,'2016-06-01',false,false,false);
INSERT INTO `cobrancas` (`id_Cobranca`,`id_Cliente`,`Emissao`,`Documento`,`Descricao`,`Valor`,`Vencimento`,`Pago`,`Cancelada`,`Remessa`) VALUES (2,1,'2016-06-01 10:12:13',1123,'incrição online no curso de XYZ',342112.34,'2016-06-01',false,false,false);
INSERT INTO `cobrancas` (`id_Cobranca`,`id_Cliente`,`Emissao`,`Documento`,`Descricao`,`Valor`,`Vencimento`,`Pago`,`Cancelada`,`Remessa`) VALUES (3,2,'2016-06-01 10:12:13',23,'Bateria mental  (renegociado)',21,'2016-06-01',false,false,false);

select cob.id_Cobranca NossoNumero, cob.Emissao, cob.Documento NumeroDocumento, cob.Valor, cob.vencimento,
cli.Nome Pagador, cli.Endereco, cli.Bairro, cli.Cidade, cli.UF
from cobrancas cob
inner join clientes cli using(id_cliente)