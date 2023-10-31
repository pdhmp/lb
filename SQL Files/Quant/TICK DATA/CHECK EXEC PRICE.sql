
DECLARE @Id_Ticker int
DECLARE @Trade_Date datetime
DECLARE @Price float
DECLARE @Quantity numeric
DECLARE @TimeLimit int

DECLARE @Return_Table TABLE(AvgPrice float, EndTime datetime, BestPrice float, BestPriceTime datetime)
SET @Id_Ticker=3
SET @Trade_Date='20090910 09:16:30'
SET @Price=33.82
SET @Quantity=-2600
SET @TimeLimit=120

DECLARE @Bid_Price float
DECLARE @Bid_Size int
DECLARE @Ask_Price float
DECLARE @Ask_Size int
DECLARE @Trade_Price float
DECLARE @Trade_Size int

DECLARE @Buy_Flag as int

DECLARE @CheckTime datetime
DECLARE @Rem_Quantity numeric
DECLARE @Done_Quantity numeric
DECLARE @Done_Value numeric
DECLARE @Tot_Quantity numeric
DECLARE @Tot_Done_Value numeric

DECLARE @PriceFlag int

DECLARE @Last_Table TABLE(Trade_DateTime datetime, Price float, Quantity int)
DECLARE @Ask_Table TABLE(Ask_DateTime datetime, Ask float, Quantity int)
DECLARE @Bid_Table TABLE(Bid_DateTime datetime, Bid float, Quantity int)

IF @Quantity>0
	BEGIN
		SET @Buy_Flag=1
	END
ELSE
	BEGIN
		SET @Buy_Flag=0
		SET @Quantity=-@Quantity
	END

SET @Done_Quantity=0
SET @Done_Value=0
SET @Tot_Done_Value=0
SET @Tot_Quantity=0
SET @Rem_Quantity=@Quantity

INSERT INTO @Last_Table 
SELECT Trade_DateTime, Price, Quantity 
FROM NESTTICK.dbo.Tb001_Quote_Recap 
WHERE Id_Ticker=@Id_Ticker AND Condition='   '
AND Trade_DateTime>=DATEADD(ss,1,@Trade_Date) AND Trade_DateTime<=DATEADD(ss,@TimeLimit,@Trade_Date)
ORDER BY Trade_DateTime

SET @CheckTime=@Trade_Date

IF @Buy_Flag=1
	BEGIN
		INSERT INTO @Ask_Table 
		SELECT Quote_DateTime, Ask_Price, Ask_Size  FROM (
		SELECT *, Rank() OVER (PARTITION BY Quote_DateTime ORDER BY ask_price DESC, Ask_Size, Id_QRM_BA) AS AskFlag
		FROM NESTTICK.dbo.Tb002_Quote_Recap_BA
		WHERE Id_Ticker=@Id_Ticker 
		AND Quote_DateTime>=DATEADD(ss,1,@Trade_Date) AND Quote_DateTime<=DATEADD(ss,@TimeLimit,@Trade_Date)
		) AS B
		WHERE AskFlag=1
		ORDER BY Quote_DateTime

		WHILE @Rem_Quantity>0
			BEGIN
				IF DATEDIFF(ss,@Trade_Date,@CheckTime)<=@TimeLimit 
					SET @PriceFlag=1
				ELSE 
					SET @PriceFlag=0

				SELECT @Ask_Price=Ask, @Ask_Size=Quantity FROM @Ask_Table WHERE Ask_DateTime=@CheckTime
				IF @Ask_Price<=@Price OR @PriceFlag=0
					BEGIN
						IF @Rem_Quantity<=@Ask_Size	SET @Done_Quantity=@Rem_Quantity
						IF @Rem_Quantity>@Ask_Size SET @Done_Quantity=@Ask_Size
						SET @Rem_Quantity=@Rem_Quantity-@Done_Quantity
						IF @PriceFlag=1	SET @Done_Value=(@Done_Quantity*@Price)
						IF @PriceFlag=0	SET @Done_Value=(@Done_Quantity*@Ask_Price)
						SET @Tot_Quantity=@Tot_Quantity+@Done_Quantity
						SET @Tot_Done_Value=@Tot_Done_Value+@Done_Value
					END

				SELECT @Trade_Price=Price, @Trade_Size=Quantity FROM @Last_Table WHERE Trade_DateTime=@CheckTime
				IF @Trade_Price<=@Price
					BEGIN
						IF @Rem_Quantity<=@Trade_Size SET @Done_Quantity=@Rem_Quantity
						IF @Rem_Quantity>@Trade_Size SET @Done_Quantity=@Trade_Size
						SET @Rem_Quantity=@Rem_Quantity-@Done_Quantity
						SET @Done_Value=(@Done_Quantity*@Price)
						SET @Tot_Quantity=@Tot_Quantity+@Done_Quantity
						SET @Tot_Done_Value=@Tot_Done_Value+@Done_Value
					END

				SET @CheckTime=DATEADD(ss,1,@CheckTime)
			END
	END

IF @Buy_Flag=0
	BEGIN
		INSERT INTO @Bid_Table 
		SELECT Quote_DateTime, Bid_Price, Bid_Size  FROM (
		SELECT *, Rank() OVER (PARTITION BY Quote_DateTime ORDER BY Bid_price DESC, Bid_Size, Id_QRM_BA) AS BidFlag
		FROM NESTTICK.dbo.Tb002_Quote_Recap_BA
		WHERE Id_Ticker=@Id_Ticker 
		AND Quote_DateTime>=DATEADD(ss,1,@Trade_Date) AND Quote_DateTime<=DATEADD(ss,@TimeLimit,@Trade_Date)
		) AS B
		WHERE BidFlag=1
		ORDER BY Quote_DateTime

		WHILE @Rem_Quantity>0
			BEGIN
				IF DATEDIFF(ss,@Trade_Date,@CheckTime)<=@TimeLimit 
					SET @PriceFlag=1
				ELSE 
					SET @PriceFlag=0

				SELECT @Bid_Price=Bid, @Bid_Size=Quantity FROM @Bid_Table WHERE Bid_DateTime=@CheckTime
				IF @Bid_Price>=@Price OR @PriceFlag=0
					BEGIN
						IF @Rem_Quantity<=@Bid_Size	SET @Done_Quantity=@Rem_Quantity
						IF @Rem_Quantity>@Bid_Size SET @Done_Quantity=@Bid_Size
						SET @Rem_Quantity=@Rem_Quantity-@Done_Quantity
						IF @PriceFlag=1	SET @Done_Value=(@Done_Quantity*@Price)
						IF @PriceFlag=0	SET @Done_Value=(@Done_Quantity*@Bid_Price)
						SET @Tot_Quantity=@Tot_Quantity+@Done_Quantity
						SET @Tot_Done_Value=@Tot_Done_Value+@Done_Value
					END

				SELECT @Trade_Price=Price, @Trade_Size=Quantity FROM @Last_Table WHERE Trade_DateTime=@CheckTime
				IF @Trade_Price>=@Price
					BEGIN
						IF @Rem_Quantity<=@Trade_Size SET @Done_Quantity=@Rem_Quantity
						IF @Rem_Quantity>@Trade_Size SET @Done_Quantity=@Trade_Size
						SET @Rem_Quantity=@Rem_Quantity-@Done_Quantity
						SET @Done_Value=(@Done_Quantity*@Price)
						SET @Tot_Quantity=@Tot_Quantity+@Done_Quantity
						SET @Tot_Done_Value=@Tot_Done_Value+@Done_Value
					END

				SET @CheckTime=DATEADD(ss,1,@CheckTime)
			END
	END

SELECT @Rem_Quantity, @Tot_Quantity, @Tot_Done_Value, CASE WHEN @Tot_Done_Value>0 THEN @Tot_Done_Value/@Tot_Quantity ELSE 0 END, DATEDIFF(ss,@Trade_Date,@CheckTime)
