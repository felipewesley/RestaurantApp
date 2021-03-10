USE [felipe.basso]

-- Stored Procedure para obter todos os pedidos de uma comanda
/*
-- Chamar a procedure: SP_OBTER_PEDIDOS 0
DROP PROCEDURE SP_OBTER_PEDIDOS
CREATE PROCEDURE SP_OBTER_PEDIDOS @ComandaId INT
AS
	SELECT Comanda.MesaId, Pedido.PedidoId, Comanda.ComandaId, Produto.Nome AS 'Produto', Pedido.Quantidade, Produto.Valor, Status.Descricao AS 'Status'
	FROM Pedido
	INNER JOIN Comanda
	ON
	Comanda.ComandaId = Pedido.ComandaId
	AND Comanda.ComandaId = @ComandaId
	INNER JOIN Status
	ON
	Pedido.StatusId = Status.StatusId
	INNER JOIN Produto
	ON
	Pedido.ProdutoId = Produto.ProdutoId
GO
*/

-- Mesas n�o ocupadas
SELECT * FROM Mesa
WHERE Mesa.Ocupada = 0

UPDATE Mesa SET Ocupada = 0 WHERE MesaId IN (1, 2, 5, 10, 12, 16)

-- �ltima comanda cadastrada
SELECT TOP(1) * FROM Comanda
ORDER BY Comanda.DataHoraEntrada DESC

-- �ltimos 10 pedidos
SELECT TOP(10) * FROM Pedido
ORDER BY Pedido.PedidoId DESC

-- Definir pedidos como 'Entregue'
UPDATE Pedido SET StatusId = 1 WHERE ComandaId = 107

-- Obter pedidos de uma comanda #id
SP_OBTER_PEDIDOS 109

ALTER TABLE Comanda ALTER COLUMN Valor REAL
ALTER TABLE Produto ALTER COLUMN Valor REAL