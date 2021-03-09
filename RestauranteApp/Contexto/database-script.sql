/*
    Autor: Felipe Wesley Basso
    Data de criação: 05/03/2021
    Editado em: 08/03/2021
    Projeto: Restaturante Sutekina Ranch
*/
USE [felipe.basso]

/* Criando a tabela Mesa */
CREATE TABLE Mesa (
    MesaId INT NOT NULL,
    Capacidade INT NOT NULL,
    Ocupada BIT NOT NULL,
    PRIMARY KEY (MesaId)
)

/* Criando a tabela Comanda */
CREATE TABLE Comanda (
    ComandaId INT NOT NULL, /* verificar melhor tipo de dado (guid/nanoid) */
    MesaId INT NOT NULL,
    DataHoraEntrada DATETIME NOT NULL,
    DataHoraSaida DATETIME NULL,
    Valor FLOAT NOT NULL,
    Paga BIT NOT NULL,
	QuantidadeClientes INT NOT NULL,
    PRIMARY KEY (ComandaId)
)

/* Alterando tipo de dado da coluna Valor por inconsistência com os tipos de dados do C# */
--ALTER TABLE Comanda ALTER COLUMN Valor REAL
--ALTER TABLE Comanda ALTER COLUMN Valor FLOAT

/* Adicionando FK à tabela Comanda */
ALTER TABLE Comanda ADD CONSTRAINT FK_Mesa_Comanda FOREIGN KEY (MesaId) REFERENCES Mesa(MesaId)


/* Criando a tabela Status */
CREATE TABLE Status (
    StatusId INT NOT NULL,
    Descricao VARCHAR(50) NOT NULL,
	PRIMARY KEY (StatusId)
)

/* Criando a tabela TipoProduto */
CREATE TABLE TipoProduto (
    Tipo INT NOT NULL,
    Descricao VARCHAR(50) NOT NULL,
	PRIMARY KEY (Tipo)
)

/* Criando a tabela Produto */
CREATE TABLE Produto (
    ProdutoId INT NOT NULL,
    Nome VARCHAR(50) NOT NULL,
    Imagem VARCHAR(500), /* analisar nullable */
    Valor FLOAT NOT NULL, /* analisar nullable */
    Disponivel BIT NOT NULL,
    QuantidadePermitida INT NOT NULl, /* analisar nullable */
    TipoId INT NOT NULL,
    PRIMARY KEY (ProdutoId)
)

/* Alterando tipo de dado da coluna Valor por inconsistência com os tipos de dados do C# */
--ALTER TABLE Produto ALTER COLUMN Valor REAL
--ALTER TABLE Produto ALTER COLUMN Valor FLOAT

/* Adicionando FK à tabela Produto */
ALTER TABLE Produto ADD CONSTRAINT FK_Tipo_Produto_Produto FOREIGN KEY (TipoId) REFERENCES TipoProduto(Tipo)

/* Criando a tabela Pedido */
CREATE TABLE Pedido (
    PedidoId INT NOT NULL,
    ComandaId INT NOT NULL, /* depende do tipo de dado da PK Comanda */
    ProdutoId INT NOT NULL,
    StatusId INT NOT NULL,
    Quantidade INT NOT NULL,
    PRIMARY KEY (PedidoId)
)

/* Adicionando FK de Comanda à tabela Pedido */
ALTER TABLE Pedido ADD CONSTRAINT FK_Comanda_Pedido FOREIGN KEY (ComandaId) REFERENCES Comanda(ComandaId)
/* Adicionando FK de Produto à tabela Produto */
ALTER TABLE Pedido ADD CONSTRAINT FK_Produto_Pedido FOREIGN KEY (ProdutoId) REFERENCES Produto(ProdutoId)
/* Adicionando FK de Status à tabela Pedido */
ALTER TABLE Pedido ADD CONSTRAINT FK_Status_Pedido FOREIGN KEY (StatusId) REFERENCES Status(StatusId)


/* 
    Inserindo dados iniciais nas tabelas que não serão alteradas
    A princípio, estas tabelas irão manter o número constante de registros,
    sendo alteradas apenas algunas colunas
*/

/* 
    Inserindo dados na tabela Mesa 
    * A coluna Ocupada será frequentemente atualizada
*/
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (1, 4, 0)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (2, 4, 0)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (3, 4, 0)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (4, 4, 1)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (5, 4, 0)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (6, 4, 1)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (7, 4, 1)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (8, 4, 1)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (9, 4, 1)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (10, 4, 0)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (11, 4, 1)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (12, 4, 0)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (13, 4, 1)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (14, 4, 1)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (15, 4, 1)
INSERT INTO Mesa (MesaId, Capacidade, Ocupada) VALUES (16, 4, 0)

/* 
    Inserindo dados na tabela Status
    * Esta tabela não deverá sofrer alterações
*/
INSERT INTO Status (StatusId, Descricao) VALUES (1, 'Em andamento')
INSERT INTO Status (StatusId, Descricao) VALUES (2, 'Entregue')
INSERT INTO Status (StatusId, Descricao) VALUES (3, 'Cancelado')


/*
    Inserindo dados na tabela TipoProduto
    * Esta tabela, a princípio, não deverá sofrer alterações
*/
INSERT INTO TipoProduto (Tipo, Descricao) VALUES (1, 'Bebidas')
INSERT INTO TipoProduto (Tipo, Descricao) VALUES (2, 'Agenomo - Frituras')
INSERT INTO TipoProduto (Tipo, Descricao) VALUES (3, 'Yakimono - Grelhados')
INSERT INTO TipoProduto (Tipo, Descricao) VALUES (4, 'Nabemono - Cozidos')
INSERT INTO TipoProduto (Tipo, Descricao) VALUES (5, 'Sashimi - Carne/Peixe cru')
INSERT INTO TipoProduto (Tipo, Descricao) VALUES (6, 'Sushi - Bolinhos de arroz temperado')



/*
    Inserindo dados na tabela Produto
    * Esta tabela, a principio, não deverá sofrer alterações
    * As inserções a seguir são realizadas por tipo de produto
*/

/* PRODUTOS TIPO: Bebidas */
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (1, 'Suco natural laranja', '...', 7.0, 1, 0, 1)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (2, 'Limonada', '...', 9.0, 1, 0, 1)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (3, 'Água mineral', '...', 4.0, 1, 0, 1)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (4, 'Mate', '...', 4.5, 1, 0, 1)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (5, 'Coca-Cola', '...', 5.99, 1, 0, 1)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (6, 'Guaraná', '...', 5.59, 1, 0, 1)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (7, 'H20', '...', 6.79, 1, 0, 1)
/*------------------------------------------------------------------------------------------------------------*/

/* PRODUTOS TIPO: Agenomo */
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (8, 'Karaage', '...', 0, 1, 0, 2)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (9, 'Korokke', '...', 0, 1, 0, 2)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (10, 'Kushiage', '...', 0, 1, 0, 2)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (11, 'Tonkatsu', '...', 0, 1, 0, 2)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (12, 'Oyakodon', '...', 0, 1, 0, 2)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (13, 'Gyudon', '...', 0, 1, 0, 2)
/*------------------------------------------------------------------------------------------------------------*/

/* PRODUTOS TIPO: Yakimono */
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (14, 'Gyoza', '...', 0, 1, 0, 3)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (15, 'Kushiyaki', '...', 0, 1, 0, 3)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (16, 'Okonomiyaki', '...', 0, 1, 0, 3)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (17, 'Omu-raisu', '...', 0, 1, 0, 3)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (18, 'Omu-soba', '...', 0, 1, 0, 3)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (19, 'Takoyaki', '...', 0, 1, 0, 3)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (20, 'Yakisoba', '...', 0, 1, 4, 3)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (21, 'Yakitori', '...', 0, 1, 0, 3)
/*------------------------------------------------------------------------------------------------------------*/

/* PRODUTOS TIPO: Nabemono */
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (22, 'Motsunabe', '...', 0, 1, 0, 4)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (23, 'Sukiyak', '...', 0, 1, 0, 4)
/*------------------------------------------------------------------------------------------------------------*/

/* PRODUTOS TIPO: Sashimi */
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (24, 'Tataki', '...', 0, 1, 0, 5)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (25, 'Fugu', '...', 0, 1, 0, 5)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (26, 'Basashi', '...', 0, 1, 0, 5)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (27, 'Rebasashi', '...', 0, 1, 0, 5)
/*------------------------------------------------------------------------------------------------------------*/

/* PRODUTOS TIPO: Sushi */
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (28, 'Chirashizushi', '...', 0, 1, 0, 6)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (29, 'Makizushi', '...', 0, 1, 0, 6)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (30, 'Nigirizushi', '...', 0, 1, 0, 6)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (31, 'Oshizushi', '...', 0, 1, 0, 6)
INSERT INTO Produto (ProdutoId, Nome, Imagem, Valor, Disponivel, QuantidadePermitida, TipoId)
VALUES (32, 'Temakizushi', '...', 0, 1, 0, 6)
/*------------------------------------------------------------------------------------------------------------*/