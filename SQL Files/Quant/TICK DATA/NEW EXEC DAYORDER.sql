
DECLARE @Id_SIM int
DECLARE @Id_Ticker int
DECLARE @Sim_Date datetime

SET @Id_SIM=82
SET @Id_Ticker=1
SET @Sim_Date='2009-10-19'


DECLARE @Trade_Table TABLE(Trade_DateTime datetime, Trade_Price float, Trade_Size int)
DECLARE @Market_Table TABLE(DayOrder int PRIMARY KEY CLUSTERED, Trade_DateTime datetime, TrType char(2), Price1 float, Quantity1 int, Price2 float, Quantity2 int)
DECLARE @BidAsk_Table TABLE(Quote_DateTime datetime, Bid_Price float, Bid_Quantity int, Ask_Price float, Ask_Quantity int)
DECLARE @Exec_Table TABLE(Exec_DateTime datetime, Exec_Price float, Tgt_Price float, Quantity int)

DECLARE @maxTime datetime

DECLARE @curTime datetime
DECLARE @DoneQuantity int
DECLARE @curPrice float

DECLARE @tgtQuantity int
DECLARE @tgtPrice float

DECLARE @TradeQuantity int
DECLARE @newQuantity int
DECLARE @ordPrice float

DECLARE @Send_Quantity int
DECLARE @Send_Price float
DECLARE @prevSend_Price float 
DECLARE @BidCue int

DECLARE @ExecQuantity int

DECLARE @curOrder int
DECLARE @minOrder int
DECLARE @LastTradeTime datetime

DECLARE @TempTime datetime
DECLARE @curType char(2)

INSERT INTO @Trade_Table
SELECT Trade_DateTime, Trade_Price, Trade_Size 
FROM NESTSIM.dbo.Trade_SIM 
WHERE Id_Ticker=@Id_Ticker AND Id_SIM=@Id_SIM 
AND Trade_DateTime>=@Sim_Date AND Trade_DateTime<=DATEADD(d,1,@Sim_Date)
ORDER BY Trade_DateTime

INSERT INTO @Market_Table  
SELECT * FROM 
(
	SELECT DayOrder, Trade_DateTime, 'T' AS TrType, Price AS Price1, Quantity AS Quantity1, 0 AS Price2, 0 AS Quantity2
	FROM NESTTICK.dbo.Tb001_Quote_Recap 
	WHERE Id_Ticker=@Id_Ticker AND Condition='   '
	AND Trade_DateTime>=@Sim_Date AND Trade_DateTime<=DATEADD(d,1,@Sim_Date)
	UNION
	SELECT DayOrder, Quote_DateTime, 'BA' AS TrType, Bid_Price, Bid_Size, Ask_Price, Ask_Size
	FROM NESTTICK.dbo.Tb002_Quote_Recap_BA 
	WHERE Id_Ticker=@Id_Ticker 
	AND Quote_DateTime>=@Sim_Date AND Quote_DateTime<=DATEADD(d,1,@Sim_Date)
) A
ORDER BY DayOrder DESC


SELECT @curTime=MIN(Trade_DateTime) FROM @Trade_Table
SELECT @maxTime=DATEADD(mi,(16*60+45),@Sim_Date)

SET @DoneQuantity=0
SET @curPrice=0
SET @tgtQuantity=0

SELECT @curOrder=MAX(DayOrder) FROM @Market_Table
SELECT @minOrder=MIN(DayOrder) FROM @Market_Table

SELECT @LastTradeTime=MIN(Trade_DateTime)-1 FROM @Trade_Table

DECLARE @curBidPrice float
DECLARE @curBidSize int
DECLARE @curAskPrice float
DECLARE @curAskSize int
DECLARE @curTradePrice float
DECLARE @curTradeSize int

DECLARE @tempQuantity int
DECLARE @tempPrice float

SET @curTradePrice=0
SET @curTradeSize=0

WHILE @curOrder>@minOrder 
	BEGIN
		SELECT @curTime=Trade_DateTime, @curType=TrType FROM @Market_Table WHERE DayOrder=@curOrder
		IF @curType='BA' SELECT @curBidPrice=Price1, @curBidSize=Quantity1, @curAskPrice=Price2, @curAskSize=Quantity2 FROM @Market_Table WHERE DayOrder=@curOrder
		IF @curType='T' SELECT @curTradePrice=Price1, @curTradeSize=Quantity1 FROM @Market_Table WHERE DayOrder=@curOrder
		
		set @tempQuantity=0
		SELECT @tempQuantity=Trade_Size, @tempPrice=Trade_Price FROM @Trade_Table WHERE Trade_DateTime>@LastTradeTime AND Trade_DateTime<=@curTime ORDER BY Trade_DateTime

		SET @newQuantity=0
		--IF (@tempQuantity>0 AND @tempPrice>=@curAskPrice) OR (@tempQuantity<0 AND @tempPrice<=@curBidPrice)
			BEGIN		  
				SELECT TOP 1 @LastTradeTime=Trade_DateTime, @newQuantity=Trade_Size, @ordPrice=Trade_Price FROM @Trade_Table WHERE Trade_DateTime>@LastTradeTime AND Trade_DateTime<=@curTime ORDER BY Trade_DateTime
			END 

		IF @newQuantity<>0
			BEGIN
				SET @tgtQuantity=@newQuantity+(@tgtQuantity-@DoneQuantity)
				print CONVERT(varchar,@curTime,25)+'___'+'NEWO___' + CAST(COALESCE(@newQuantity,0) as varchar)+'___@___'+CAST(COALESCE(@ordPrice,0) as varchar)+'_______SENDING_'+CAST(COALESCE(@tgtQuantity,0) as varchar)
				SET @DoneQuantity=0
				IF @tgtQuantity>0 AND @curBidPrice>0 SET @prevSend_Price=@curAskPrice-0.01
				IF @tgtQuantity<0 AND @curAskPrice>0 SET @prevSend_Price=@curBidPrice+0.01
			END

		SET @Send_Quantity=@tgtQuantity-@DoneQuantity

		--=================== TRADE ON BID ===================
		IF @Send_Quantity>0
			BEGIN
				SET @Send_Price=@prevSend_Price
				
				--IF @curBidPrice>@prevSend_Price + 0.01 SET @Send_Price=@curBidPrice

				--IF @curAskPrice-@curBidPrice>0.01 SET @Send_Price=@curBidPrice + 0.01

				IF @curAskPrice>=@ordPrice + 0.01 SET @Send_Price=@curBidPrice 

				print CONVERT(varchar,@curOrder)+'___'+CONVERT(varchar,@curTime,25) +'___'+ 'BID____'+ CAST(COALESCE(@Send_Quantity,0) as varchar)+'___' + CAST(COALESCE(@Send_Price,0) as varchar)+'___MKT___'+ CAST(COALESCE(@curBidPrice,0) as varchar)+'-'+ CAST(COALESCE(@curAskPrice,0) as varchar)
				IF @curType='T' AND @curTradePrice<@Send_Price 
					BEGIN
						SET @ExecQuantity=@Send_Quantity
						IF @curTradeSize<@ExecQuantity SET @ExecQuantity=@curTradeSize
						print CONVERT(varchar,@curTime,25) +'___'+ 'FILL_T_'+ CAST(COALESCE(@ExecQuantity,0) as varchar)+'___fil:'+ CAST(COALESCE(@Send_Price,0) as varchar)+'___ord:'+ CAST(COALESCE(@ordPrice,0) as varchar)+'___trdsz:'+ CAST(COALESCE(@TradeQuantity,0) as varchar)+'___trdpr:'+ CAST(COALESCE(@curTradeSize,0) as varchar)
						INSERT INTO @Exec_Table VALUES(@curTime,@Send_Price,@ordPrice,@ExecQuantity)
						SET @DoneQuantity=@DoneQuantity+@ExecQuantity
					END
				IF @curType='BA' AND @curAskPrice<=@Send_Price 
					BEGIN
						SET @ExecQuantity=@Send_Quantity
						IF @curAskSize<@ExecQuantity SET @ExecQuantity=@curAskSize
						print CONVERT(varchar,@curTime,25) +'___'+ 'FILL_A_'+ CAST(COALESCE(@ExecQuantity,0) as varchar)+'___fil:'+ CAST(COALESCE(@Send_Price,0) as varchar)+'___ord:'+ CAST(COALESCE(@ordPrice,0) as varchar)+'___trdsz:'+ CAST(COALESCE(@TradeQuantity,0) as varchar)+'___trdpr:'+ CAST(COALESCE(@curTradeSize,0) as varchar)
						INSERT INTO @Exec_Table VALUES(@curTime,@Send_Price,@ordPrice,@ExecQuantity)
						SET @DoneQuantity=@DoneQuantity+@ExecQuantity
					END
			END
	
		--=================== TRADE ON ASK ===================
		IF @Send_Quantity<0
			BEGIN
				SET @Send_Price=@prevSend_Price
				
				--IF @curAskPrice<@prevSend_Price - 0.01 SET @Send_Price=@curAskPrice
				
				--IF @curAskPrice-@curBidPrice>0.01 SET @Send_Price=@curAskPrice - 0.01

				IF @curBidPrice<=@ordPrice - 0.01 SET @Send_Price=@curAskPrice 

				print  CONVERT(varchar,@curTime,25) +'___'+ 'ASK____'+ CAST(COALESCE(@Send_Quantity,0) as varchar)+'___' + CAST(COALESCE(@Send_Price,0) as varchar)+'___MKT___'+ CAST(COALESCE(@curBidPrice,0) as varchar)+'-'+ CAST(COALESCE(@curAskPrice,0) as varchar)
				IF @curType='T' AND @curTradePrice>@Send_Price 
					BEGIN
						SET @ExecQuantity=@Send_Quantity
						IF @curTradeSize<@ExecQuantity SET @ExecQuantity=@curTradeSize
						print CONVERT(varchar,@curTime,25) +'___'+ 'FILL_T_'+ CAST(COALESCE(@ExecQuantity,0) as varchar)+'___fil:'+ CAST(COALESCE(@Send_Price,0) as varchar)+'___ord:'+ CAST(COALESCE(@ordPrice,0) as varchar)+'___trdsz:'+ CAST(COALESCE(@TradeQuantity,0) as varchar)+'___trdpr:'+ CAST(COALESCE(@curTradeSize,0) as varchar)
						INSERT INTO @Exec_Table VALUES(@curTime,@Send_Price,@ordPrice,@ExecQuantity)
						SET @DoneQuantity=@DoneQuantity+@ExecQuantity
					END

				IF @curType='BA' AND @curBidPrice>=@Send_Price 
					BEGIN
						SET @ExecQuantity=@Send_Quantity
						IF @curBidSize<@ExecQuantity SET @ExecQuantity=@curBidSize
						print CONVERT(varchar,@curTime,25) +'___'+ 'FILL_B_'+ CAST(COALESCE(@ExecQuantity,0) as varchar)+'___fil:'+ CAST(COALESCE(@Send_Price,0) as varchar)+'___ord:'+ CAST(COALESCE(@ordPrice,0) as varchar)+'___trdsz:'+ CAST(COALESCE(@TradeQuantity,0) as varchar)+'___trdpr:'+ CAST(COALESCE(@curTradeSize,0) as varchar)
						INSERT INTO @Exec_Table VALUES(@curTime,@Send_Price,@ordPrice,@ExecQuantity)
						SET @DoneQuantity=@DoneQuantity+@ExecQuantity
					END
			END

		--SET @curTime=DATEADD(ss,1,@curTime)
		--IF @Send_Quantity=0 SELECT @curOrder=MIN(DayOrder)+1 FROM @Market_Table WHERE Trade_DateTime<(SELECT MIN(Trade_DateTime) FROM @Trade_Table WHERE Trade_DateTime>@LastTradeTime)
		--declare @tempsTT datetime
		--declare @nbar int

		--SELECT @tempsTT=MIN(Trade_DateTime) FROM @Trade_Table WHERE Trade_DateTime>@LastTradeTime
		--SELECT @nbar=MIN(DayOrder) FROM @Market_Table WHERE Trade_DateTime<@tempsTT

		--print @tempsTT

		--print '=========='
		--print @curOrder
		--print @nbar

		--IF @curOrder>@nbar+10 AND @Send_Quantity=0SET @curOrder=@nbar+10
		SET @prevSend_Price=@Send_Price


		SET @curOrder=@curOrder-1
	END
print @Send_Quantity
IF @Send_Quantity<0 
	BEGIN
		print CONVERT(varchar,@curTime,25) +'___'+ 'ENDFL__'+ CAST(COALESCE(@Send_Quantity,0) as varchar)+'___fil:'+ CAST(COALESCE(@curBidPrice,0) as varchar)+'___ord:'+ CAST(COALESCE(@ordPrice,0) as varchar)+'___trdsz:'+ CAST(COALESCE(@TradeQuantity,0) as varchar)+'___trdpr:'+ CAST(COALESCE(@curTradeSize,0) as varchar)
		INSERT INTO @Exec_Table VALUES(@curTime,@curBidPrice,@ordPrice,@Send_Quantity)
	END

IF @Send_Quantity>0 
	BEGIN
		print CONVERT(varchar,@curTime,25) +'___'+ 'ENDFL__'+ CAST(COALESCE(@Send_Quantity,0) as varchar)+'___fil:'+ CAST(COALESCE(@curAskPrice,0) as varchar)+'___ord:'+ CAST(COALESCE(@ordPrice,0) as varchar)+'___trdsz:'+ CAST(COALESCE(@TradeQuantity,0) as varchar)+'___trdpr:'+ CAST(COALESCE(@curTradeSize,0) as varchar)
		INSERT INTO @Exec_Table VALUES(@curTime,@curAskPrice,@ordPrice,@Send_Quantity)
	END

SELECT SUM(-Tgt_Price*Quantity) AS EQUIV, (SELECT SUM(-Trade_Size*Trade_Price) FROM @Trade_Table) AS TARGET, SUM(-Exec_Price*Quantity) AS DONE, SUM(Quantity) FROM @Exec_Table AS REMAINDER
SELECT (SELECT SUM(ABS(Trade_Size)) FROM @Trade_Table)AS TARGET , (SELECT SUM(ABS(Quantity)) FROM @Exec_Table) AS DONE,  (0.01+(SELECT SUM(ABS(Quantity)) FROM @Exec_Table))/(SELECT SUM(ABS(Trade_Size)) FROM @Trade_Table) AS Ratio 
SELECT * FROM @Exec_Table


DECLARE @tempReturn float
SELECT @tempReturn=SUM(-Exec_Price*Quantity) FROM @Exec_Table

