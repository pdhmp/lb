

EXEC NESTRT.dbo.Delete_Incorrect_RT 4
EXEC NESTRT.dbo.Delete_Incorrect_RT 43
EXEC NESTRT.dbo.Delete_Incorrect_RT 10
EXEC NESTRT.dbo.Delete_Incorrect_RT 18
EXEC NESTRT.dbo.Delete_Incorrect_RT 38

RETURN

--DECLARE @FundNumber int 
--SET @FundNumber=4


--DECLARE @tempTable TABLE(refRow int, TableRow int, Performance float, MarkDelete int)

--INSERT INTO @tempTable
--SELECT RowNumber = ROW_NUMBER() OVER (order by Id_RT_Performance ASC), Id_RT_Performance, curPerformance, 0
--from NESTRT.dbo.Tb001_Intraday_Performance
--where convert(varchar, Perf_DateTime, 112)=convert(varchar, getdate(), 112)
--AND id_Portfolio=@FundNumber ORDER BY Perf_DateTime

----SELECT * FROM @tempTable

--DECLARE @curRow int
--DECLARE @prevPerf float
--DECLARE @curPerf float
--DECLARE @cumDelete int

--SET @cumDelete=0
--SET @curRow=0

--WHILE @curRow IS NOT NULL
--BEGIN
--	SET @curRow=(SELECT TOP 1 refRow FROM @tempTable WHERE refRow>@curRow ORDER BY refRow)
--	SET @curPerf=(SELECT TOP 1 Performance FROM @tempTable WHERE refRow=@curRow)
--	if @curPerf-@prevPerf>0.01 SET @cumDelete=@cumDelete+1
--	if @curPerf-@prevPerf<-0.01 SET @cumDelete=@cumDelete-1
--	UPDATE @tempTable SET MarkDelete=@cumDelete WHERE refRow=@curRow
--	SET @prevPerf=@curPerf
--END

--SELECT 'PORTFOLIO:' + @FundNumber, 'DELETED:' + COUNT(*) FROM @tempTable WHERE MarkDelete<>0

--IF (SELECT TOP 1 MarkDelete FROM @tempTable ORDER BY refRow DESC) = 0
--	BEGIN
--		--DELETE FROM @tempTable WHERE TableRow IN 
--		DELETE FROM NESTRT.dbo.Tb001_Intraday_Performance WHERE Id_RT_Performance IN 
--		(SELECT TableRow FROM @tempTable WHERE MarkDelete=1 OR MarkDelete=-1)
--	END

----SELECT * FROM @tempTable

