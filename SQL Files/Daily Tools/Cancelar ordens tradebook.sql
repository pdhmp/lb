
DECLARE @tempTable TABLE(Id_Order_To_Cancel int)
DECLARE @Id_Order_To_Cancel int

INSERT INTO @tempTable
SELECT A.Id_Ordem 
FROM dbo.Tb012_Ordens A
INNER JOIN NESTDB.dbo.VW_PortAccounts B
ON A.Id_Account=B.Id_Account
WHERE convert(varchar,Data_Insercao,112)=convert(varchar,GETDATE(),112) AND Operador=77 AND Id_Corretora=36 
SET @Id_Order_To_Cancel=1

WHILE exists(SELECT TOP 1 Id_Order_To_Cancel FROM @tempTable)
	BEGIN
		SELECT TOP 1 @Id_Order_To_Cancel=Id_Order_To_Cancel FROM @tempTable ORDER BY Id_Order_To_Cancel
		exec Proc_Cancel_All_Trades_Order @Id_Order_To_Cancel
		DELETE FROM @tempTable WHERE Id_Order_To_Cancel=@Id_Order_To_Cancel
	END

--"exec Proc_Cancel_All_Trades_Order @Id_ordem ="