SELECT Transaction_Id,Transaction_Type,Trade_Date,Settlement_Date,Id_Account AS Id_Account1, Id_Ticker1, [Id Book] AS [Id Book1],[Id Section] AS  [Id Section1], Quantity1,Id_Account AS Id_Account2, Id_Ticker2, 5 AS [Id Book2], 1 AS [Id Section2], Quantity2  FROM 
(
	SELECT Id_Foward AS Transaction_Id,80 AS Transaction_Type, Trade_Date, 
		x.Vencimento AS Settlement_Date, Id_Account,v.Id_Ticker AS Id_Ticker1, 
		Quantity As Quantity1, 
		CASE WHEN x.Moeda=900 THEN 1844 
			WHEN x.Moeda=1042 THEN 5791
		END AS Id_Ticker2, 
		0 As Quantity2,
		CASE WHEN (Quantity)>0 THEN 1 ELSE -1 END AS Side, [Id_Book] as [Id Book], [Id_Section] as [Id Section]
	FROM dbo.Tb725_Fowards V
	LEFT JOIN dbo.Tb001_Ativos X
	ON v.Id_Ticker=x.Id_Ativo
	WHERE Status_Foward<>4
) AS B
UNION ALL

SELECT Transaction_Id,Transaction_Type,Trade_Date,Settlement_Date,Id_Account AS Id_Account1, Id_Ticker1, [Id Book] AS [Id Book1],[Id Section] AS  [Id Section1], -(Quantity1-dbo.FCN_Closed_Foward_Amount(Transaction_Id,Trade_Date)),Id_Account AS Id_Account2, Id_Ticker2, 5 AS [Id Book2], 1 AS [Id Section2], -Quantity2  FROM 
(
	SELECT Id_Foward AS Transaction_Id,80 AS Transaction_Type, dbo.FCN_NDATEADD('du',-3,x.Vencimento,99,v.Id_Ticker) Trade_Date, 
		x.Vencimento AS Settlement_Date, 
CASE Z.Id_Portfolio 
WHEN  5  THEN 1046
WHEN  41 THEN 1086
WHEN  11 THEN 1073
END Id_Account,
v.Id_Ticker AS Id_Ticker1, 
		Quantity As Quantity1, 
		CASE WHEN x.Moeda=900 THEN 1844 
			WHEN x.Moeda=1042 THEN 5791
		END AS Id_Ticker2, 
		0 As Quantity2,
		CASE WHEN (Quantity)>0 THEN 1 ELSE -1 END AS Side, [Id_Book] as [Id Book], [Id_Section] as [Id Section]
	FROM dbo.Tb725_Fowards V
	LEFT JOIN dbo.Tb001_Ativos X
	ON v.Id_Ticker=x.Id_Ativo
	INNER JOIN dbo.VW_PortAccounts Z
	ON V.Id_Account = Z.Id_Account
	WHERE Status_Foward<>4 AND Id_Port_Type=1
) AS B

UNION ALL

SELECT Transaction_Id,Transaction_Type,Trade_Date,Settlement_Date,Id_Account AS Id_Account1, Id_Ticker1, [Id Book] AS [Id Book1],[Id Section] AS  [Id Section1], Quantity1,Id_Account AS Id_Account2, Id_Ticker2, 5 AS [Id Book2], 1 AS [Id Section2], Price * (Quantity1/Lote_padrao) as Quantity2 FROM 
(
	SELECT Id_Foward AS Transaction_Id,10 AS Transaction_Type, dbo.FCN_NDATEADD('du',-3,x.Vencimento,99,v.Id_Ticker) Trade_Date,
		x.Vencimento AS Settlement_Date,
CASE Z.Id_Portfolio 
WHEN  5  THEN 1046
WHEN  41 THEN 1086
WHEN  11 THEN 1073
END Id_Account,
v.Id_Ticker AS Id_Ticker1, 
		Quantity - dbo.FCN_Closed_Foward_Amount(Id_Foward,dbo.FCN_NDATEADD('du',-3,x.Vencimento,99,v.Id_Ticker))As Quantity1, 
		CASE WHEN x.Moeda=900 THEN 1844 
			WHEN x.Moeda=1042 THEN 5791
		END AS Id_Ticker2, 
		Cash As Quantity2,
		CASE WHEN (Quantity)>0 THEN 1 ELSE -1 END AS Side,Price, [Id_Book] as [Id Book], [Id_Section] as [Id Section],Lote_Padrao
	FROM dbo.Tb725_Fowards V
	LEFT JOIN dbo.Tb001_Ativos X
	ON v.Id_Ticker=x.Id_Ativo
	INNER JOIN dbo.VW_PortAccounts Z
	ON V.Id_Account = Z.Id_Account
	WHERE Status_Foward<>4 AND Id_Port_Type=1
) AS B

UNION ALL

SELECT Transaction_Id,Transaction_Type,Trade_Date,Settlement_Date,Id_Account AS Id_Account1, Id_Ticker1, [Id Book] AS [Id Book1],[Id Section] AS  [Id Section1], Quantity1,Id_Account AS Id_Account2, Id_Ticker2, 5 AS [Id Book2], 1 AS [Id Section2], Price * (Quantity1/Lote_padrao) as Quantity2 FROM 
(
	SELECT v.Id_Foward AS Transaction_Id,81 AS Transaction_Type,Close_Date as  Trade_Date,
		dbo.FCN_NDATEADD('du',-3,Close_Date,99,v.Id_Ticker) AS Settlement_Date,
CASE Z.Id_Portfolio 
WHEN  5  THEN 1046
WHEN  41 THEN 1086
WHEN  11 THEN 1073
END Id_Account,

v.Id_Ticker AS Id_Ticker1, 
		Close_Quantity As Quantity1, 
		CASE WHEN x.Moeda=900 THEN 1844 
			WHEN x.Moeda=1042 THEN 5791
		END AS Id_Ticker2, 
		Price * (Close_Quantity/Lote_Padrao) As Quantity2,
		CASE WHEN (Quantity)>0 THEN 1 ELSE -1 END AS Side,Price, [Id_Book] as [Id Book], [Id_Section] as [Id Section],Lote_Padrao
	FROM dbo.Tb725_Fowards V
	LEFT JOIN dbo.Tb001_Ativos X
	ON v.Id_Ticker=x.Id_Ativo
	INNER JOIN dbo.Tb726_Fowards_Early_Close Y
	ON v.Id_Foward = y.Id_Foward
	INNER JOIN dbo.VW_PortAccounts Z
	ON V.Id_Account = Z.Id_Account
	WHERE Status_Foward<>4 AND Id_Port_Type=1
) AS B

UNION ALL

SELECT Transaction_Id,Transaction_Type,Trade_Date,Settlement_Date,Id_Account AS Id_Account1, Id_Ticker1, [Id Book] AS [Id Book1],[Id Section] AS  [Id Section1], Quantity1,Id_Account AS Id_Account2, Id_Ticker2, 5 AS [Id Book2], 1 AS [Id Section2], Quantity2 FROM 
(
	SELECT v.Id_Foward AS Transaction_Id,10 AS Transaction_Type, Close_Date as Trade_Date,
		dbo.FCN_NDATEADD('du',-3,Close_Date,99,v.Id_Ticker) AS Settlement_Date, 
CASE Z.Id_Portfolio 
WHEN  5  THEN 1046
WHEN  41 THEN 1086
WHEN  11 THEN 1073
END Id_Account,

v.Id_Ticker AS Id_Ticker1, 
		-Close_Quantity As Quantity1, 
		CASE WHEN x.Moeda=900 THEN 1844 
			WHEN x.Moeda=1042 THEN 5791
		END AS Id_Ticker2, 
		0 As Quantity2,
		CASE WHEN (Quantity)>0 THEN 1 ELSE -1 END AS Side,Price, [Id_Book] as [Id Book], [Id_Section] as [Id Section],Lote_Padrao
	FROM dbo.Tb725_Fowards V
	LEFT JOIN dbo.Tb001_Ativos X
	ON v.Id_Ticker=x.Id_Ativo
	INNER JOIN dbo.Tb726_Fowards_Early_Close y
	ON v.Id_Foward = y.Id_Foward
	INNER JOIN dbo.VW_PortAccounts Z
	ON V.Id_Account = Z.Id_Account
	WHERE Status_Foward<>4 AND Id_Port_Type=1
) AS B
Order by Trade_Date,Quantity1
