
SELECT StDate,
(
	SELECT top 1 Price 
	FROM nesttick.dbo.Tb001_Quote_Recap 
	WHERE Id_Ticker=1 AND Trade_DateTime<=StDate AND Trade_DateTime>=CONVERT(varchar,StDate,112) AND Condition='   '
	ORDER BY Trade_DateTime DESC
) AS [PETR4],
(
	SELECT top 1 Price 
	FROM nesttick.dbo.Tb001_Quote_Recap 
	WHERE Id_Ticker=3 AND Trade_DateTime<=StDate AND Trade_DateTime>=CONVERT(varchar,StDate,112) AND Condition='   ' 
		ORDER BY Trade_DateTime DESC
) AS [VALE5],
(
	SELECT top 1 Price 
	FROM nesttick.dbo.Tb001_Quote_Recap 
	WHERE Id_Ticker=444	AND Trade_DateTime<=StDate AND Trade_DateTime>=CONVERT(varchar,StDate,112) AND Condition='   '
	ORDER BY Trade_DateTime DESC
) AS [ITUB4],
(
	SELECT top 1 Price 
	FROM nesttick.dbo.Tb001_Quote_Recap 
	WHERE Id_Ticker=4660 AND Trade_DateTime<=StDate AND Trade_DateTime>=CONVERT(varchar,StDate,112) AND Condition='   '
	ORDER BY Trade_DateTime DESC
) AS [BVMF3],
(
	SELECT top 1 Price 
	FROM nesttick.dbo.Tb001_Quote_Recap 
	WHERE Id_Ticker=89 AND Trade_DateTime<=StDate AND Trade_DateTime>=CONVERT(varchar,StDate,112) AND Condition='   '
	ORDER BY Trade_DateTime DESC
) AS [BBDC4],
(
	SELECT top 1 Price 
	FROM nesttick.dbo.Tb001_Quote_Recap 
	WHERE Id_Ticker=384 AND Trade_DateTime<=StDate AND Trade_DateTime>=CONVERT(varchar,StDate,112) AND Condition='   '
	ORDER BY Trade_DateTime DESC
) AS [GGBR4],
(
	SELECT top 1 Price 
	FROM nesttick.dbo.Tb001_Quote_Recap 
	WHERE Id_Ticker=260 AND Trade_DateTime<=StDate AND Trade_DateTime>=CONVERT(varchar,StDate,112) AND Condition='   '
	ORDER BY Trade_DateTime DESC
) AS [CSNA3],
(
	SELECT top 1 Price 
	FROM nesttick.dbo.Tb001_Quote_Recap 
	WHERE Id_Ticker=801 AND Trade_DateTime<=StDate AND Trade_DateTime>=CONVERT(varchar,StDate,112) AND Condition='   '
	ORDER BY Trade_DateTime DESC
) AS [USIM5]
FROM [FCN_STD_TIMES]('20090910')

