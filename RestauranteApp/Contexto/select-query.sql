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

-- Mesas não ocupadas
SELECT * FROM Mesa
WHERE Mesa.Ocupada = 0

-- Última comanda cadastrada
SELECT TOP(1) * FROM Comanda
ORDER BY Comanda.DataHoraEntrada DESC

-- Últimos 10 pedidos
SELECT TOP(10) * FROM Pedido
ORDER BY Pedido.PedidoId DESC

-- Definir pedidos como 'Entregue'
UPDATE Pedido SET StatusId = 2 WHERE ComandaId = 101

-- Obter pedidos de uma comanda #id
SP_OBTER_PEDIDOS 101