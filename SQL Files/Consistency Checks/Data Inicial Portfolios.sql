

SELECT [id portfolio], MIN([Date Now]) 
FROM  dbo.Tb000_Historical_Positions
GROUP BY [id portfolio]

SELECT MIN([Date Now]) FROM dbo.Tb000_Historical_Positions WHERE  [Id Portfolio]=43