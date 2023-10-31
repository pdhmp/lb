select count(*) FROM nesttick.dbo.Tb001_Quote_Recap
UNION
select count(*) FROM nesttick.dbo.Tb002_Quote_Recap_BA

select Date_Imported,count(*) FROM nesttick.dbo.Tb010_Imported_Tickers 
group by Date_Imported 
order by Date_Imported 

--SELECT DISTINCT BatchNo FROM nesttick.dbo.Tb001_Quote_Recap ORDER BY BatchNo

select * from nestlog.dbo.Tb901_Event_Log where program_id=31 
order by event_datetime desc

--SELECT * FROM nesttick.dbo.Tb010_Imported_Tickers where Date_Imported='20090910'