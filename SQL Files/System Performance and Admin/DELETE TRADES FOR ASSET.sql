DECLARE @Tabela_Posicao TABLE(Id_Ordem int)
DECLARE @Id_Ordem int

insert into @Tabela_Posicao
select Id_ordem
from dbo.Tb012_Ordens where data_abert_ordem='20090707' and Id_ativo=846
and Id_Account=1061 

SELECT * FROM @Tabela_Posicao

While exists(select top 1 Id_Ordem from @Tabela_Posicao)
	BEGIN
		Select top 1 @Id_Ordem = Id_Ordem 	from @Tabela_Posicao order by Id_Ordem

		EXEC dbo.proc_Cancel_Ordem @Id_Ordem=@Id_Ordem

		delete from @Tabela_Posicao Where Id_Ordem = @Id_Ordem
	END

