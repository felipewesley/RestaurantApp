USE [felipe.basso]

-- Stored Procedure para obter todos os pedidos de uma comanda
/*
-- Chamar a procedure: SP_OBTER_PEDIDOS 0

CREATE PROCEDURE SP_OBTER_PEDIDOS @ComandaId INT
AS
	SELECT * FROM Pedido
	INNER JOIN Comanda
	ON Comanda.ComandaId = @ComandaId
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

-- Obter pedidos de uma comanda #id
SP_OBTER_PEDIDOS #id