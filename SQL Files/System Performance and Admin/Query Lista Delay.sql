

SELECT * FROM (
	Select A.[Id Position], A.[Id Portfolio], A.[Id Ticker], A.[Last Calc], B.Feeder_TimeStamp, DATEDIFF("s", A.[Last Calc], Feeder_TimeStamp) AS Delay
	from 
		dbo.Tb000_Posicao_Atual A
	INNER JOIN 
		(
		SELECT * FROM 
			(SELECT * FROM dbo.Tb065_Ultimo_Preco WHERE Tipo_Preco=1 AND day(Feeder_TimeStamp)=day(getdate())) as q
		WHERE DATEDIFF("s", Feeder_TimeStamp, getdate())<15
		) AS B
		ON A.[Id Ticker]=B.Id_Ativo
		WHERE day(A.[Last Calc])=day(getdate())
) AS C
WHERE Delay>0
ORDER By Delay DESC



/*
SELECT *, DATEDIFF("s", Feeder_TimeStamp, getdate()) FROM 
(SELECT * FROM dbo.Tb065_Ultimo_Preco 
WHERE Tipo_Preco=1 
AND day(Feeder_TimeStamp)=day(getdate())) as q
WHERE DATEDIFF("s", Feeder_TimeStamp, getdate())<15
ORDER BY  DATEDIFF("s", Feeder_TimeStamp, getdate()) desc

*/