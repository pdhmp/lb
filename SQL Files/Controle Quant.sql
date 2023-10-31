DECLARE @DM1 DATETIME
DECLARE @DM2 DATETIME

SET @DM1 = '2012-05-03'
SET @DM2 = '2012-05-02'

SELECT Preco, Erros.* 
FROM
(
	SELECT PCP.*, 'Falta' AS TipoErro
	FROM
	(
		SELECT P.*, S.NestTicker
		FROM	
		( 
			SELECT A.IDSECURITY, A.SrType, A.SOURCE 
			FROM 
			(
				SELECT * 
				FROM nestsrv06.NESTDB.DBO.TB050_PRECO_ACOES_ONSHORE P (NOLOCK)
				WHERE	SRTYPE IN (101, 102, 312, 313, 332, 350, 351, 352, 360, 361, 370, 371, 330, 331) AND
						SRDATE = @DM2 AND
						[SOURCE] IN (22, 7)

				UNION
				SELECT * 
				FROM nestsrv02.RTICKDB.DBO.TB001_Precos_RTICK R (NOLOCK)
				WHERE	R.SRTYPE IN (420, 421) AND
						R.SRDATE = @DM2 AND
						R.[SOURCE] IN (22)

				UNION

				SELECT * 
				FROM nestsrv06.NESTDB.DBO.TB050_PRECO_ACOES_ONSHORE P (NOLOCK)
				WHERE	SRTYPE IN (100,101) AND
						SRDATE = @DM2 AND
						[SOURCE] IN (1, 7)
			) A
			LEFT OUTER JOIN
			(	SELECT * 
				FROM nestsrv06.NESTDB.DBO.TB050_PRECO_ACOES_ONSHORE P (NOLOCK)
				WHERE	SRTYPE IN (101, 102, 312, 313, 332, 350, 351, 352, 360, 361, 370, 371, 330, 331) AND
						SRDATE = @DM1 AND
						[SOURCE]IN (22, 7)

				UNION
				SELECT * 
				FROM nestsrv02.RTICKDB.DBO.TB001_Precos_RTICK R (NOLOCK)
				WHERE	R.SRTYPE IN (420, 421) AND
						R.SRDATE = @DM1 AND
						R.[SOURCE] = 22

				UNION

				SELECT * 
				FROM nestsrv06.NESTDB.DBO.TB050_PRECO_ACOES_ONSHORE P (NOLOCK)
				WHERE	SRTYPE IN (100,101) AND
						SRDATE = @DM1 AND
						[SOURCE] IN (1, 7)
						
			) B
				ON A.IDSECURITY = B.IDSECURITY and a.srtype = b.srtype AND A.SOURCE = B.SOURCE
			WHERE B.IDSECURITY IS NULL
		)P
		JOIN
		(
			SELECT S.NestTicker, S.IdSecurity
			FROM nestsrv06.NESTDB.dbo.Tb001_Securities S (NOLOCK)
		)S
		ON P.IdSecurity = S.IdSecurity
	)PCP
	JOIN
	(
		SELECT DISTINCT([Id Ticker]) FROM NESTSRV06.nestdb.dbo.Tb000_Historical_Positions HP (NOLOCK)
		WHERE	HP.[Id Portfolio] = 18 AND
				HP.[Date Now] > DATEADD(DD,-10,GETDATE())
	)HP
	ON HP.[Id Ticker] = PCP.IdSecurity	

	UNION

	SELECT PCP.*, 'Sobra' AS TipoErro
	FROM
	(
		SELECT P.*, S.NestTicker
		FROM	
		( 
			SELECT A.IDSECURITY, A.SrType, A.SOURCE 
			FROM 
			(
				SELECT * 
				FROM nestsrv06.NESTDB.DBO.TB050_PRECO_ACOES_ONSHORE P (NOLOCK)
				WHERE	SRTYPE IN (101, 102, 312, 313, 332, 350, 351, 352, 360, 361, 370, 371, 330, 331) AND
						SRDATE = @DM1 AND
						SOURCE IN (22, 7)

				UNION
				SELECT * 
				FROM nestsrv02.RTICKDB.DBO.TB001_Precos_RTICK R(NOLOCK)
				WHERE	R.SRTYPE IN (420, 421) AND
						R.SRDATE = @DM1 AND
						R.[SOURCE] = 22	

				UNION

				SELECT * 
				FROM nestsrv06.NESTDB.DBO.TB050_PRECO_ACOES_ONSHORE P (NOLOCK)
				WHERE	SRTYPE IN (100,101) AND
						SRDATE = @DM1 AND
						SOURCE IN (1, 7)					
			) A
			LEFT OUTER JOIN
			(	SELECT * 
				FROM nestsrv06.NESTDB.DBO.TB050_PRECO_ACOES_ONSHORE P (NOLOCK)
				WHERE	SRTYPE IN (101, 102, 312, 313, 332, 350, 351, 352, 360, 361, 370, 371, 330, 331) AND
						SRDATE = @DM2 AND
						[SOURCE] IN (22, 7)

				UNION
				SELECT * 
				FROM nestsrv02.RTICKDB.DBO.TB001_Precos_RTICK R (NOLOCK)
				WHERE	R.SRTYPE IN (420, 421) AND
						R.SRDATE = @DM2 AND
						R.[SOURCE] = 22

				UNION

				SELECT * 
				FROM nestsrv06.NESTDB.DBO.TB050_PRECO_ACOES_ONSHORE P (NOLOCK)
				WHERE	SRTYPE IN (100,101) AND
						SRDATE = @DM2 AND
						[SOURCE] IN (1, 7)
			) B
				ON A.IDSECURITY = B.IDSECURITY and a.srtype = b.srtype AND A.SOURCE = B.SOURCE
			WHERE B.IDSECURITY IS NULL
		)P
		JOIN
		(
			SELECT S.NestTicker, S.IdSecurity
			FROM nestsrv06.NESTDB.dbo.Tb001_Securities S (NOLOCK)
		)S
		ON P.IdSecurity = S.IdSecurity
	)PCP
	JOIN
	(
		SELECT DISTINCT([Id Ticker]) FROM NESTSRV06.nestdb.dbo.Tb000_Historical_Positions HP (NOLOCK)
		WHERE	HP.[Id Portfolio] = 18 AND
				HP.[Date Now] > DATEADD(DD,-10,GETDATE())
	)HP
	ON HP.[Id Ticker] = PCP.IdSecurity	
)ERROS
JOIN
NESTSRV06.nestdb.dbo.Tb116_Tipo_Preco TP
ON TP.Id_Tipo_Preco = ERROS.SrType
ORDER BY TipoErro, IdSecurity, SrType
	
SELECT Preco AS Precos_QSEGS, Yesterday, BeforeYesterday, Diff
FROM
(		
	SELECT PInY.SrType as [Type], YQuant as Yesterday, BYQuant as BeforeYesterday, YQuant - BYQuant as Diff
	FROM
	(
		SELECT COUNT(*) AS YQuant, SrDate as Date , SrType FROM 
			nestsrv06.NESTDB.dbo.Tb053_Precos_Indices PA (NOLOCK) 
		WHERE	PA.SrType IN (102) AND
				PA.SrDate = @DM1 AND
				PA.Source = 7
		GROUP BY SrType,SrDate
	)PInY
	RIGHT JOIN
	(
		SELECT COUNT(*) AS BYQuant, SrDate as prevDate, SrType FROM 
			nestsrv06.NESTDB.dbo.Tb053_Precos_Indices PA (NOLOCK) 
		WHERE	PA.SrType IN (102) AND
				PA.SrDate = @DM2 AND
				PA.Source = 7
		GROUP BY SrType,SrDate
	)PInP
	ON	PInY.SrType = PInP.SrType
)Tb
RIGHT JOIN
(
	SELECT * 
	FROM NESTSRV06.NESTDB.dbo.Tb116_Tipo_Preco TP
	WHERE TP.Id_Tipo_Preco IN(102)
)TP
ON TP.Id_Tipo_Preco = [Type]
ORDER BY [Preco]

SELECT Missing_QSEGS, P.IdSecurity
FROM 
(
	(
		SELECT NestTicker as Missing_QSEGS, IdSecurity
		FROM nestsrv06.NESTDB.dbo.Tb001_Securities S (NOLOCK)
		WHERE	S.IdSecurity IN
						(
							SELECT distinct(Id_Ticker_Component)
							FROM NESTSRV06.NESTDB.dbo.Tb023_Securities_CompositiON SC (NOLOCK)
								 JOIN
								 nestsrv06.NESTDB.dbo.Tb001_Securities S (NOLOCK)
								 ON SC.Id_Ticker_Component = S.IdSecurity
							WHERE  SC.Id_Ticker_Composite = 21350
						) 
	)S
	JOIN
	(
		SELECT DA.IdSecurity
		FROM
		(
			(
				SELECT PA.IdSecurity FROM 
					nestsrv06.NESTDB.dbo.Tb053_Precos_Indices PA (NOLOCK)
				WHERE	PA.SrType IN (102) AND
						Pa.SrDate = @DM1 AND
						PA.Source = 7
			)DM1
			RIGHT JOIN
			(
					SELECT DISTINCT(PA.IdSecurity) FROM 
						nestsrv06.NESTDB.dbo.Tb053_Precos_Indices PA (NOLOCK) 
					WHERE	PA.SrType IN (102) AND
							PA.SrDate IN (@DM2) AND
							PA.Source = 7
			)DA
			ON DM1.IdSecurity = DA.IdSecurity
		)
		WHERE DM1.IdSecurity IS NULL			
	)P
	ON S.IdSecurity = P.IdSecurity
)

SELECT Preco AS Precos_DI, Yesterday, BeforeYesterday, Diff
FROM
(	
	SELECT PInY.SrType as [Type], YQuant as Yesterday, BYQuant as BeforeYesterday, YQuant - BYQuant as Diff
	FROM
	(
		SELECT COUNT(*) AS YQuant, SrDate as Date , SrType FROM 
			nestsrv06.NESTDB.dbo.Tb059_Precos_Futuros PA (NOLOCK) 
		WHERE	PA.SrType IN (312) AND
				PA.SrDate = @DM1 AND
				PA.Source = 22 AND
				PA.IdSecurity IN (
								5007, 1571, 5261, 5265, 5267, 5269, 5270, 5271, 
								4637, 4903, 5259, 5263, 5568, 5262, 5266, 5268, 5260
							)
		GROUP BY PA.SrType,PA.SrDate
	)PInY
	RIGHT JOIN
	(
		SELECT COUNT(*) AS BYQuant, SrDate as prevDate, SrType FROM 
			nestsrv06.NESTDB.dbo.Tb059_Precos_Futuros PA (NOLOCK) 
		WHERE	PA.SrType IN (312) AND
				PA.SrDate = @DM2 AND
				PA.Source = 22 AND
				PA.IdSecurity IN (
								5007, 1571, 5261, 5265, 5267, 5269, 5270, 5271, 
								4637, 4903, 5259, 5263, 5568, 5262, 5266, 5268, 5260
							)
		GROUP BY SrType,SrDate
	)PInP
	ON	PInY.SrType = PInP.SrType

)Tb
RIGHT JOIN
(
	SELECT * 
	FROM NESTSRV06.NESTDB.dbo.Tb116_Tipo_Preco TP
	WHERE TP.Id_Tipo_Preco IN(312)
)TP
ON TP.Id_Tipo_Preco = [Type]
ORDER BY [Preco]

SELECT Missing_DIs, P.IdSecurity
FROM 
(
	(
		SELECT NestTicker as Missing_DIs, IdSecurity
		FROM nestsrv06.NESTDB.dbo.Tb001_Securities S (NOLOCK)
		WHERE S.IdSecurity IN (
								5007, 1571, 5261, 5265, 5267, 5269, 5270, 5271, 
								4637, 4903, 5259, 5263, 5568, 5262, 5266, 5268, 5260
							)
	)S
	JOIN
	(
		SELECT DM2.IdSecurity
		FROM
		(
			(
				SELECT IdSecurity FROM 
					nestsrv06.NESTDB.dbo.Tb059_Precos_Futuros PF (NOLOCK)
				WHERE	PF.SrType IN (312) AND
						PF.SrDate = @DM1 AND
						PF.Source = 22
			)DM1
			RIGHT JOIN
			(
				SELECT IdSecurity FROM 
					nestsrv06.NESTDB.dbo.Tb059_Precos_Futuros PF (NOLOCK) 
				WHERE	PF.SrType IN (312) AND
						PF.SrDate = @DM2 AND
						PF.Source = 22
			)DM2
			ON DM1.IdSecurity = DM2.IdSecurity			
		)		
		WHERE DM1.IdSecurity IS NULL
	)P
	ON S.IdSecurity = P.IdSecurity
)	


SELECT Preco AS Precos_ICF, Yesterday, BeforeYesterday, Diff
FROM
(	
	SELECT PInY.SrType as [Type], YQuant as Yesterday, BYQuant as BeforeYesterday, YQuant - BYQuant as Diff
	FROM
	(
		SELECT COUNT(*) AS YQuant, SrDate as Date , SrType FROM 
			nestsrv06.NESTDB.dbo.Tb057_Precos_Commodities PA (NOLOCK) 
		WHERE	PA.SrType IN (312) AND
				PA.SrDate = @DM1 AND
				PA.Source = 22 AND
				PA.IdSecurity IN 
								(
									SELECT IdSecurity FROM NESTDB.DBO.Tb001_Securities where IdUnderlying = 1638
								)
		GROUP BY PA.SrType,PA.SrDate
	)PInY
	RIGHT JOIN
	(
		SELECT COUNT(*) AS BYQuant, SrDate as prevDate, SrType FROM 
			nestsrv06.NESTDB.dbo.Tb057_Precos_Commodities PA (NOLOCK) 
		WHERE	PA.SrType IN (312) AND
				PA.SrDate = @DM2 AND
				PA.Source = 22 AND
				PA.IdSecurity IN 
								(
									SELECT IdSecurity FROM NESTDB.DBO.Tb001_Securities where IdUnderlying = 1638
								)
		GROUP BY SrType,SrDate
	)PInP
	ON	PInY.SrType = PInP.SrType

)Tb
RIGHT JOIN
(
	SELECT * 
	FROM NESTSRV06.NESTDB.dbo.Tb116_Tipo_Preco TP
	WHERE TP.Id_Tipo_Preco IN(312)
)TP
ON TP.Id_Tipo_Preco = [Type]
ORDER BY [Preco]

SELECT Missing_ICFs, P.IdSecurity
FROM 
(
	(
		SELECT NestTicker as Missing_ICFs, IdSecurity
		FROM nestsrv06.NESTDB.dbo.Tb001_Securities S (NOLOCK)
		WHERE S.IdSecurity IN 
							(
								SELECT IdSecurity FROM NESTDB.DBO.Tb001_Securities where IdUnderlying = 1638
							)
	)S
	JOIN
	(
		SELECT DM2.IdSecurity
		FROM
		(
			(
				SELECT IdSecurity FROM 
					nestsrv06.NESTDB.dbo.Tb057_Precos_Commodities PF (NOLOCK)
				WHERE	PF.SrType IN (312) AND
						PF.SrDate = @DM1 AND
						PF.Source = 22
			)DM1
			RIGHT JOIN
			(
				SELECT IdSecurity FROM 
					nestsrv06.NESTDB.dbo.Tb057_Precos_Commodities PF (NOLOCK) 
				WHERE	PF.SrType IN (312) AND
						PF.SrDate = @DM2 AND
						PF.Source = 22
			)DM2
			ON DM1.IdSecurity = DM2.IdSecurity			
		)		
		WHERE DM1.IdSecurity IS NULL
	)P
	ON S.IdSecurity = P.IdSecurity
)	