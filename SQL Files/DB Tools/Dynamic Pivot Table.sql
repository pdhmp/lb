DECLARE @listCol VARCHAR(2000)
DECLARE @query VARCHAR(4000)
SELECT  @listCol = STUFF(( SELECT DISTINCT

                                '],[' + ltrim(str(YEAR(OrderDate)))

                        FROM    Sales.SalesOrderHeader

                        ORDER BY '],[' + ltrim(str(YEAR(OrderDate)))

                        FOR XML PATH('')

                                    ), 1, 2, '') + ']'

SET @query = 'SELECT * FROM 

      (SELECT CustomerID, YEAR(OrderDate) OrderYear, SubTotal

            FROM Sales.SalesOrderHeader

            WHERE CustomerID >=1 and CustomerID <=35) src

PIVOT (SUM(SubTotal) FOR OrderYear 

IN ('+@listCol+')) AS pvt'

EXECUTE (@query)
