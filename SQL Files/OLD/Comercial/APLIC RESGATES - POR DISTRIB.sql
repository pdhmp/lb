SELECT  X.Id_Contact, Contact_Name,SUM(Quantity) * NESTDB.dbo.FCN_GET_PRICE_Value_Only(4946, '2020-01-01', 1, 0, 2, 0, 0), Id_Distributor
FROM
(
	SELECT A.Id_Contact, A.Transaction_Id, A.Trade_Date,
	CASE WHEN Transaction_Type=30 THEN Quantity ELSE -Quantity END AS Quantity,
	Transaction_NAV AS Transaction_NAV, Transaction_Type
	from  NESTDB.dbo.Tb760_Subscriptions_Mellon  A
	LEFT JOIN NESTDB.dbo.Tb002_Portfolios B
	ON A.Id_Portfolio=B.Id_Portfolio
	WHERE a.Id_Portfolio=3 
	--ORDER BY  A.Trade_Date
) X
	LEFT JOIN
	NESTDB.dbo.Tb752_DistContacts E
	ON X.Id_Contact=E.Id_Contact
	LEFT JOIN NESTDB.dbo.Tb751_Contacts C
	ON X.Id_Contact=C.Id_Contact

GROUP BY X.Id_Contact, Contact_Name,Id_Distributor
ORDER BY SUM(Quantity) * NESTDB.dbo.FCN_GET_PRICE_Value_Only(4946, '2020-01-01', 1, 0, 2, 0, 0) DESC


/*
SELECT  X.Id_Contact, SUM(Quantity) * NESTDB.dbo.FCN_GET_PRICE_Value_Only(4946, '2020-01-01', 1, 0, 2, 0, 0), Id_Distributor
FROM
(
	SELECT C.Id_Contact, A.Transaction_Id, A.Trade_Date,
	CASE WHEN Transaction_Type=30 THEN Quantity ELSE -Quantity END AS Quantity,
	Transaction_NAV AS Transaction_NAV, Transaction_Type
	from  NESTDB.dbo.Tb760_Subscriptions_Mellon  A
	LEFT JOIN NESTDB.dbo.Tb002_Portfolios B
	ON A.Id_Portfolio=B.Id_Portfolio
	LEFT JOIN NESTDB.dbo.Tb751_Contacts C
	ON A.Id_Contact=C.Id_Contact
	LEFT JOIN NESTDB.dbo.Tb752_DistContacts D
	ON A.Id_Portfolio=B.Id_Portfolio
	WHERE a.Id_Portfolio=10 
	--ORDER BY  A.Trade_Date
) X
	LEFT JOIN
	NESTDB.dbo.Tb752_DistContacts E
	ON X.Id_Contact=E.Id_Contact

GROUP BY X.Id_Contact, Id_Distributor
ORDER BY SUM(Quantity) * NESTDB.dbo.FCN_GET_PRICE_Value_Only(4946, '2020-01-01', 1, 0, 2, 0, 0) DESC
*/