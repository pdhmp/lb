select Transaction_Type,Id_Ticker,Declared_Date, Record_Date,Payment_Date,Per_Share_Gross , b.[Date Now],b.Position,b.Ticker, Position*Per_Share_Gross
from dbo.Tb720_Dividends A
RIGHT JOIN (SELECT [Date Now], [Id Portfolio],[Id Ticker],Ticker,SUM(Position) Position FROM dbo.Tb000_Historical_Positions GROUP BY [Date Now], [Id Portfolio],[Id Ticker],Ticker) B
ON A.Record_Date=B.[Date Now]
and a.Id_Ticker=b.[Id Ticker]
where Ex_Date<'2013-06-06' AND Payment_Date>'2013-06-06' and Transaction_Type<>23 and [Id Portfolio]=4 and Position<>0
order by Declared_Date