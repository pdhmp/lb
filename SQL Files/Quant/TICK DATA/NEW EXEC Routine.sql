

CREATE FUNCTION [dbo].[FCN_Get_Exec_Price_Day] 
(
	@Id_Ticker int,
	@Sim_Date datetime,
	@TimeLimit int,
	@Improve_Price float
)
RETURNS decimal(18,6)
AS
BEGIN

/*
DECLARE @Id_Ticker int
DECLARE @Sim_Date datetime
SET @Id_Ticker=3
SET @Sim_Date='2009-09-16'
*/

DECLARE @Trade_Table TABLE(Trade_DateTime datetime, Trade_Price float, Trade_Size int)
DECLARE @Last_Table TABLE(Trade_DateTime datetime, Price float, Quantity int)
DECLARE @BidAsk_Table TABLE(Quote_DateTime datetime, Bid_Price float, Bid_Quantity int, Ask_Price float, Ask_Quantity int)
DECLARE @Exec_Table TABLE(Exec_DateTime datetime, Exec_Price float, Tgt_Price float, Quantity int)

DECLARE @maxTime datetime

DECLARE @curTime datetime
DECLARE @DoneQuantity int
DECLARE @curPrice float

DECLARE @tgtQuantity int
DECLARE @tgtPrice float

DECLARE @newQuantity int
DECLARE @newPrice float

DECLARE @Send_Quantity int
DECLARE @Send_Price float

DECLARE @maxBid float
DECLARE @minAsk float
DECLARE @maxTrade float
DECLARE @minTrade float

--DROP TABLE @BidAsk_Table
--DROP TABLE @Last_Table
--DROP TABLE @Trade_Table

INSERT INTO @Trade_Table
SELECT Trade_DateTime, Trade_Price, Trade_Size 
FROM NESTSIM.dbo.Trade_SIM 
WHERE Id_Ticker=@Id_Ticker 
AND Trade_DateTime>=@Sim_Date AND Trade_DateTime<=DATEADD(d,1,@Sim_Date)
ORDER BY Trade_DateTime

INSERT INTO @Last_Table  
SELECT Trade_DateTime, Price, Quantity 
FROM NESTTICK.dbo.Tb001_Quote_Recap 
WHERE Id_Ticker=@Id_Ticker AND Condition='   '
AND @Sim_Date>=@Sim_Date AND Trade_DateTime<=DATEADD(d,1,@Sim_Date)
ORDER BY Trade_DateTime

INSERT INTO @BidAsk_Table 
SELECT Quote_DateTime, Bid_Price, Bid_Size, Ask_Price, Ask_Size  
FROM NESTTICK.dbo.Tb002_Quote_Recap_BA
WHERE Id_Ticker=@Id_Ticker 
AND Quote_DateTime>=@Sim_Date AND Quote_DateTime<=DATEADD(d,1,@Sim_Date)
ORDER BY Quote_DateTime

--CREATE INDEX tempBidAsk ON @BidAsk_Table(Quote_DateTime)

SELECT @curTime=MIN(Trade_DateTime) FROM @Trade_Table
SELECT @maxTime=DATEADD(mi,(60*16+45),@Sim_Date)

--SELECT * from @Trade_Table order by Trade_DateTime

SET @DoneQuantity=0
SET @curPrice=0
SET @tgtQuantity=0

WHILE @curTime<@maxTime 
	BEGIN
		SELECT @newQuantity=Trade_Size, @newPrice=Trade_Price FROM @Trade_Table WHERE Trade_DateTime=@curTime
		SELECT @maxBid=MAX(Bid_Price), @minAsk=MIN(Ask_Price) FROM @BidAsk_Table WHERE Quote_DateTime=@curTime
		SELECT @maxTrade=MAX(Price), @minTrade=MIN(Price) FROM @Last_Table WHERE Trade_DateTime=@curTime
		
		IF @newQuantity <>0
			BEGIN
				SET @tgtQuantity=@newQuantity+(@tgtQuantity-@DoneQuantity)
				--print CONVERT(varchar,@curTime,25)+'____________'+CAST(COALESCE(@newQuantity,0) as varchar)+'____________'+CAST(COALESCE(@tgtQuantity,0) as varchar)
				SET @DoneQuantity=0
			END

		SET @Send_Quantity=@tgtQuantity-@DoneQuantity

		IF @Send_Quantity>0
			BEGIN
				SET @Send_Price=@maxBid
				--print 'BID_'+CONVERT(varchar,@curTime,25) +'___'+ CAST(COALESCE(@Send_Quantity,0) as varchar)+'___'+ CAST(COALESCE(@Send_Price,0) as varchar)
				IF @minTrade<=@Send_Price
					BEGIN
						--print 'EXEC_'+CONVERT(varchar,@curTime,25) +'___'+ CAST(COALESCE(@Send_Quantity,0) as varchar)+'___'+ CAST(COALESCE(@Send_Price,0) as varchar)+'___'+ CAST(COALESCE(@newPrice,0) as varchar)
						INSERT INTO @Exec_Table VALUES(@curTime,@Send_Price,@newPrice,@Send_Quantity)
						SET @DoneQuantity=@DoneQuantity+@Send_Quantity
					END
			END
		
		IF @Send_Quantity<0
			BEGIN
				SET @Send_Price=@minAsk
				--print 'ASK_'+ CONVERT(varchar,@curTime,25) +'___'+ CAST(COALESCE(@Send_Quantity,0) as varchar)+'___'+ CAST(COALESCE(@Send_Price,0) as varchar)
				IF @maxTrade<=@Send_Price
					BEGIN
						--print 'EXEC_'+CONVERT(varchar,@curTime,25) +'___'+ CAST(COALESCE(@Send_Quantity,0) as varchar)+'___'+ CAST(COALESCE(@Send_Price,0) as varchar)+'___'+ CAST(COALESCE(@newPrice,0) as varchar)
						INSERT INTO @Exec_Table VALUES(@curTime,@Send_Price,@newPrice,@Send_Quantity)
						SET @DoneQuantity=@DoneQuantity+@Send_Quantity
					END
			END

		SET @curTime=DATEADD(ss,1,@curTime)
		IF @Send_Quantity=0 SELECT @curTime=MIN(Trade_DateTime) FROM @Trade_Table WHERE Trade_DateTime>@curTime

		SET @newQuantity=0
	END

DECLARE @tempReturn float
SELECT @tempReturn=SUM(-Exec_Price*Quantity) FROM @Exec_Table

RETURN @tempReturn

END