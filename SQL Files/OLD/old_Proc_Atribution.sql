set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go















ALTER PROCEDURE [dbo].[Proc_Attribution] (@Id_Portfolio int, @StratFlag int, @IniDate datetime, @EndDate datetime, @Is_Cumulative int)  
AS
BEGIN

/*
DECLARE @Id_Portfolio int, @StratFlag int, @IniDate datetime, @EndDate datetime, @Is_Cumulative int

SET @Id_Portfolio =4
SET @StratFlag =-1
SET @IniDate='20100101'
SET @EndDate='20100119'
SET @Is_Cumulative=1
*/

SET @IniDate=DATEADD(d, -1, @IniDate)

CREATE TABLE #ReturnTable ([Date Now] datetime, PortType varchar(40), Book varchar(40), Section varchar(40), SubStrategy varchar(40), Instrument varchar(40), Sector varchar(40), ReportSector varchar(40), [Base Underlying] varchar(40), Perf float)
DECLARE @TempTable table([Date Now] datetime, PortType varchar(40), Book varchar(40), Section varchar(40),[New Sub Strategy] varchar(40), Instrument varchar(40), Sector varchar(40), ReportSector varchar(40), [Base Underlying] varchar(40), Perf float)
DECLARE @Id_Ticker_Fund int
DECLARE @Id_Ticker_Bench int

IF @Id_Portfolio=4 SET @Id_Ticker_Fund=5672
IF @Id_Portfolio=10 SET @Id_Ticker_Fund=21140
IF @Id_Portfolio=18 SET @Id_Ticker_Fund=182029
IF @Id_Portfolio=43 SET @Id_Ticker_Fund=13663
IF @Id_Portfolio=38 SET @Id_Ticker_Fund=81062

IF @Id_Portfolio=4 SET @Id_Ticker_Bench=14244
IF @Id_Portfolio=18 SET @Id_Ticker_Bench=5049
IF @Id_Portfolio=10 SET @Id_Ticker_Bench=1073
IF @Id_Portfolio=43 SET @Id_Ticker_Bench=5049
IF @Id_Portfolio=38 SET @Id_Ticker_Bench=5049


DECLARE @IniNAV float
SELECT TOP 1 @IniNAV=Valor_PL FROM NESTDB.dbo.Tb025_Valor_PL (nolock) WHERE Data_PL<=@IniDate AND Id_Portfolio=@Id_Portfolio ORDER BY Data_PL DESC

DECLARE @Book_Sizes TABLE(Book varchar(40), Book_Size float)
INSERT INTO @Book_Sizes
SELECT Book, A.Book_Size
FROM NESTDB.dbo.tb008_Port_Books (nolock)A
INNER JOIN 
(
	SELECT Id_Portfolio, Id_Book, MAX(Book_CreateDate) AS MAXDate
	FROM NESTDB.dbo.tb008_Port_Books (nolock)
	GROUP BY Id_Portfolio, Id_Book
) B
ON A.Id_Portfolio=B.Id_Portfolio
	AND A.Id_Book=B.Id_Book
	AND A.Book_CreateDate=B.MAXDate
LEFT JOIN NESTDB.dbo.Tb400_Books(nolock) C
	ON A.Id_Book=C.Id_Book
WHERE A.Id_Portfolio=@Id_Portfolio

IF @StratFlag = -1
	BEGIN
		UPDATE @Book_Sizes SET Book_Size=1
	END

INSERT INTO @TempTable
SELECT [Date Now]
	, CASE [Id Asset Class] 
		WHEN 1 THEN  'Long-Short'
		WHEN 2 THEN  'Interest Rates'
		WHEN 3 THEN  'Interest Rates'
		WHEN 4 THEN  'Commodity'
		WHEN 5 THEN  'Cash' 
		WHEN 0 THEN  'Cash' 
		ELSE 'Other'
	  END AS PortType
	, X.Book
	, X.Section
	, X.[New Sub Strategy]
	, [Instrument]
	, [Nest Sector] AS Sector
	, CASE WHEN Commodity_Flag=1 OR [Id Base Underlying Currency]=900 OR [Id Asset Class]<>1 THEN [nest sector] ELSE 'non-BZ non-Comm' END AS ReportSector
	, CASE WHEN [Base underlying]<>'Expenses BRL' THEN [Base underlying] ELSE Ticker END AS [Base underlying]
	, SUM(CASE WHEN [Id Base Underlying Currency]<>[Portfolio Currency] THEN [Asset P/L pC Admin] ELSE [Total P/L Admin] END)/(CASE WHEN @Is_Cumulative=-99 THEN @IniNAV ELSE MAX([NAV pC]) END)/COALESCE(MAX(Book_Size),1) AS Perf
FROM 
(
	SELECT * FROM nestdb.dbo.Tb000_Historical_Positions (nolock)
	UNION ALL 
	SELECT * FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock)
) X
LEFT JOIN @Book_Sizes Y ON X.Book = Y.Book
LEFT JOIN dbo.Tb113_Setores Z ON X.[Nest Sector]=Z.Setor
WHERE [id portfolio]=@Id_Portfolio AND [Date Now]>@IniDate AND [Date Now]<=@EndDate AND [Id Asset Class]<>3
GROUP BY [Date Now], [Id Asset Class], X.Book, X.Section, X.[New Sub Strategy], [Instrument], [Nest Sector], Commodity_Flag,[Id Base Underlying Currency],CASE WHEN [Base underlying]<>'Expenses BRL' THEN [Base underlying] ELSE Ticker END

UNION ALL

SELECT COALESCE(A.[Date Now], B.[Date Now])
		, COALESCE(A.PerfType, B.PerfType) AS PortType
		, COALESCE(A.Book, B.Book) AS Book
		, COALESCE(A.Section, B.Section) AS Section
		, COALESCE(A.[New Sub Strategy], B.[New Sub Strategy])
		, COALESCE(A.Instrument, B.Instrument)
		, COALESCE(A.Sector, B.Sector) AS Sector
		, 'Currencies' AS ReportSector
		, COALESCE(A.Sector, B.Sector)
		, COALESCE(A.Perf, 0)+COALESCE(B.Perf, 0)
FROM
(
	SELECT [Date Now], 'Currencies' AS PerfType, X.Book, X.[New Sub Strategy], X.[Instrument], [Security Currency] AS Sector, 'RepSector' AS ReportSector, Section, SUM([Asset P/L pC Admin])/(CASE WHEN @Is_Cumulative=-99 THEN @IniNAV ELSE MAX([NAV pC]) END)/COALESCE(MAX(Book_Size),1) AS Perf
	FROM (SELECT * FROM nestdb.dbo.Tb000_Historical_Positions (nolock) UNION ALL SELECT * FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock)) X
	LEFT JOIN @Book_Sizes Y ON X.Book = Y.Book
	WHERE [id portfolio]=@Id_Portfolio AND [Date Now]>@IniDate AND [Date Now]<=@EndDate AND [Id Asset Class]=3
	GROUP BY [Date Now], X.Book, X.Section, X.[New Sub Strategy], X.[Instrument], [Security Currency], Section
) A

FULL OUTER JOIN 
(
	SELECT [Date Now], 'Currencies' AS PerfType, X.Book, X.[New Sub Strategy], X.[Instrument], [Security Currency] AS Sector, 'RepSector' AS ReportSector, Section, SUM(CASE WHEN [Id Base Underlying Currency]<>[Portfolio Currency] THEN [Currency P/L Admin] ELSE 0 END)/(CASE WHEN @Is_Cumulative=-99 THEN @IniNAV ELSE MAX([NAV pC])/COALESCE(MAX(Book_Size),1) END) AS Perf
	FROM (SELECT * FROM nestdb.dbo.Tb000_Historical_Positions (nolock) UNION ALL SELECT * FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock)) X
	LEFT JOIN @Book_Sizes Y ON X.Book = Y.Book
	WHERE [id portfolio]=@Id_Portfolio AND [Date Now]>@IniDate AND [Date Now]<=@EndDate 
	GROUP BY [Date Now], X.Book, X.[New Sub Strategy], X.[Instrument], [Security Currency], Section
) B
ON A.Sector=B.Sector AND A.[Date Now]=B.[Date Now] AND A.Book = B.Book AND A.Section = B.Section AND A.[Instrument] = B.[Instrument]


IF @StratFlag = -1
	BEGIN
		INSERT INTO @TempTable
		SELECT [Date Now], '0-Error' AS PerfType, 'Error' AS [Base underlying], 'Error' AS [New Sub Strategy], 'Error' AS Instrument, 'Error' AS Book, 'Error' AS Section, 'Error' AS Sector, 'Error' AS ReportSector, (NESTDB.dbo.FCN_GET_PRICE_Value_Only(@Id_Ticker_Fund, [Date Now], 1, 0, 2, 0, 0)/ NESTDB.dbo.FCN_GET_PRICE_Value_Only(@Id_Ticker_Fund, [Date Now]-1, 1, 0, 2, 0, 0)-1)-SUM(Perf) AS Perf
		FROM @TempTable
		WHERE [Date Now]<>(SELECT TOP 1 [Date Now] FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock))
		GROUP BY [Date Now]
		ORDER BY [Date Now]
	END

INSERT INTO @TempTable
		SELECT SrDate, 'Bench' AS PerfType, 'Bench' AS [Base underlying], 'Bench' AS [New Sub Strategy], 'Bench' AS Instrument, 'Bench' AS Book, 'Bench' AS Section, 'Bench' AS Sector, 'Bench' AS ReportSector, SrValue
		FROM dbo.Tb053_Precos_Indices 
		WHERE IdSecurity=@Id_Ticker_Bench AND srType=100 AND Source=1 AND SrDate>@IniDate AND SrDate<=@EndDate 
		UNION ALL
		SELECT CONVERT(varchar,getdate(),112), 'Bench' AS PerfType, 'Bench' AS [Base underlying], 'Bench' AS Instrument, 'Bench' AS [New Sub Strategy], 'Bench' AS Book, 'Bench' AS Section, 'Bench' AS Sector, 'Bench' AS ReportSector, (NESTDB.dbo.FCN_GETD_RT_Value_Only(@Id_Ticker_Bench, 1, 0, 0)/ NESTDB.dbo.FCN_GET_PRICE_Value_Only(@Id_Ticker_Bench, getdate()-1, 1, 0, 2, 0, 0)-1) AS Valor
		ORDER BY SrDate

IF @EndDate<CONVERT(varchar,getdate(),112)
	BEGIN
		DELETE FROM @TempTable WHERE [Date Now]=CONVERT(varchar,getdate(),112)
	END

IF @Is_Cumulative=1
	BEGIN
		INSERT INTO #ReturnTable
		SELECT [Date Now]
			, PortType
			, Book
			, Section
			, [New Sub Strategy]
			, Instrument
			, Sector
			, ReportSector AS ReportSector
			,[Base underlying]
			--,(SELECT EXP(SUM(log(1+COALESCE(Perf,0))))-1 FROM @TempTable WHERE [Date Now]<=B.[Date Now] AND [Date Now]<=@EndDate AND PortType=A.PortType AND [New Sub Strategy]=A.[New Sub Strategy] AND Book=A.Book AND Sector=A.Sector AND [Base underlying]=A.[Base underlying]) AS Perf
			,(SELECT SUM(COALESCE(Perf,0)) FROM @TempTable WHERE [Date Now]<=B.[Date Now] AND [Date Now]<=@EndDate AND PortType=A.PortType AND [New Sub Strategy]=A.[New Sub Strategy] AND Book=A.Book AND Section=A.Section AND Sector=A.Sector AND [Base underlying]=A.[Base underlying] AND Instrument=A.Instrument) AS Perf
			--,(SELECT COALESCE(Perf,0) FROM @TempTable WHERE [Date Now]=B.[Date Now] AND PortType=A.PortType AND [New Sub Strategy]=A.[New Sub Strategy] AND Book=A.Book AND Sector=A.Sector AND [Base underlying]=A.[Base underlying] AND Instrument=A.Instrument) AS Perf
		FROM
		(SELECT PortType, [New Sub Strategy], Book, Section, Sector, ReportSector, [Base underlying], Instrument FROM @TempTable GROUP BY PortType, [New Sub Strategy], Book, Section, Sector, ReportSector, [Base underlying], Instrument) A
		,
		(SELECT [Date Now] FROM @TempTable GROUP BY [Date Now]) B
	END
ELSE
	BEGIN
		INSERT INTO #ReturnTable
		SELECT * FROM @TempTable A
	END

SELECT * FROM #ReturnTable ORDER BY [Date Now], Book, [Base underlying]

DROP TABLE #ReturnTable

END













































