

DECLARE @tempTable TABLE(Id_Pos int)
DECLARE @Id_Pos int

INSERT INTO @tempTable
SELECT [Id Position] FROM dbo.Tb000_Historical_Positions
where [id portfolio]=43 and [date now]>='20100101' and ticker like '%ODF%'

SET @Id_Pos=1

WHILE exists(SELECT TOP 1 Id_Pos FROM @tempTable)
	BEGIN
		SELECT TOP 1 @Id_Pos=Id_Pos FROM @tempTable ORDER BY Id_Pos
		exec nestdb.dbo.proc_Calc_LB2_Cost_Close_Historical @Id_Pos
		exec nestdb.dbo.PROC_GET_CALCULATE_FIELDS_HIST @Id_Pos,1
		DELETE FROM @tempTable WHERE Id_Pos=@Id_Pos
	END

