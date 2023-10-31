
SELECT *, CASE WHEN Cash<> 0 THEN Amount/Cash ELSE 0 END PercOut
FROM
(
	SELECT Id_Portfolio,File_Date, DT,SUM(Quantidade*Cot_Fechamento) AS Amount, SUM((Quantidade*Cot_Liquid)*(power(1+Taxa,1/252.0)-1)) AS Custo
	FROM dbo.Tb710_Stock_Loans_Import A
	LEFT JOIN VW_PortAccounts B
	ON a.Id_Account=B.Id_Account
	WHERE Id_Port_Type=2
	GROUP BY Id_Portfolio, File_Date, DT
	--ORDER BY Id_Portfolio,DT, File_date
) X
LEFT JOIN
(
	SELECT [date now], [Id Portfolio], SUM(Cash) Cash, CASE WHEN Position>0 THEN 'D' ELSE 'T' END AS Side FROM dbo.Tb000_Historical_Positions 
	WHERE [Id Instrument] IN (1,2) and [Id Currency Ticker]=900
	AND [id portfolio] IN (4,10,18,38,43,50,60)
	GROUP BY [date now], [Id Portfolio],CASE WHEN Position>0 THEN 'D' ELSE 'T' END
	--ORDER BY [date now], [Id Portfolio],CASE WHEN Position>0 THEN 'D' ELSE 'T' END
) Y
ON X.DT =Y.Side
AND X.File_Date=Y.[Date Now]
AND X.Id_Portfolio=Y.[Id Portfolio]
ORDER BY Id_Portfolio,DT, File_date