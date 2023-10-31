SELECT A.DT_ATU_F, AVG(datediff(d, A.DT_ATU_F, A.KnowDateTime)), count(DT_ATU_F)
	, CAST(sum(case when datediff(d, A.DT_ATU_F, A.KnowDateTime)<=60 THEN 1 ELSE 0 END) AS float)/count(DT_ATU_F) *100
FROM nestother.dbo.Tb023_CVM_ITR_HDR A
GROUP BY DT_ATU_F
HAVING count(DT_ATU_F)>15
ORDER BY DT_ATU_F
