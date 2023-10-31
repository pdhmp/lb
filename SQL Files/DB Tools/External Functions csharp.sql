/*
DROP FUNCTION Get_RT_Quote
DROP FUNCTION Set_RT_Quote
DROP FUNCTION Get_isConnected

DROP ASSEMBLY DatabaseFunc


CREATE ASSEMBLY DatabaseFunc
FROM 'C:\DatabaseFunctions.dll'
WITH PERMISSION_SET=UNSAFE

CREATE FUNCTION Set_RT_Quote(@Id_Ticker int, @Tipo_Preco int, @Set_Value float)
RETURNS int
AS EXTERNAL NAME DatabaseFunc.[RTTools.Get_RT_Data].Set_RT_Quote

CREATE FUNCTION Get_RT_Quote(@Id_Ticker int, @Tipo_Preco int)
RETURNS float
AS EXTERNAL NAME DatabaseFunc.[RTTools.Get_RT_Data].Get_RT_Quote

CREATE FUNCTION Get_isConnected()
RETURNS int
AS EXTERNAL NAME DatabaseFunc.[RTTools.Get_RT_Data].Get_isConnected

*/

SELECT dbo.Get_RT_Quote(1, 9),dbo.Get_isConnected(), dbo.Get_RT_Quote(1, 22)
SELECT dbo.Get_isConnected()


ALTER ASSEMBLY DatabaseFunc WITH PERMISSION_SET=EXTERNAL_ACCESS

select datepart("hh",Date_Calc), datepart("mi",Date_Calc),AVG(CalcTime)
from dbo.Tb915_Calc_Log A
inner join dbo.Tb001_Ativos B
ON A.Id_Ticker=B.Id_Ativo
WHERE Date_Calc>'20090531' AND B.Id_Instrumento<>3
GROUP BY datepart("hh",Date_Calc), datepart("mi",Date_Calc)
order by datepart("hh",Date_Calc), datepart("mi",Date_Calc)
