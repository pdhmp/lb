SELECT CONVERT(varchar, getdate(),120), COUNT(*) FROM nestother.dbo.Tb023_CVM_ITR_HDR
SELECT CONVERT(varchar, getdate(),120), COUNT(*) FROM nestother.dbo.Tb021_CVM_ITR_CTL
SELECT CONVERT(varchar, getdate(),120), COUNT(*) FROM nestother.dbo.Tb022_CVM_ITR_CONFIG
SELECT CONVERT(varchar, getdate(),120), COUNT(*) FROM nestother.dbo.Tb025_CVM_ITR_DATA

/*
SELECT TOP 1 * FROM nestother.dbo.Tb023_CVM_ITR_HDR ORDER BY Id_CTL DESC
SELECT TOP 1 * FROM nestother.dbo.Tb021_CVM_ITR_CTL ORDER BY Id_CTL DESC
SELECT TOP 1 * FROM nestother.dbo.Tb022_CVM_ITR_CONFIG ORDER BY Id_CTL DESC
SELECT TOP 1 * FROM nestother.dbo.Tb025_CVM_ITR_DATA ORDER BY Id_CTL DESC
*/

/*
DECLARE @ZIPNAME varchar(100)
SET @ZIPNAME = '10123_10880_ITR_20011231_000000000.zip'

DELETE FROM nestother.dbo.Tb023_CVM_ITR_HDR WHERE ZipName=@ZIPNAME
DELETE FROM nestother.dbo.Tb021_CVM_ITR_CTL WHERE ZipName=@ZIPNAME
DELETE FROM nestother.dbo.Tb022_CVM_ITR_CONFIG WHERE ZipName=@ZIPNAME
DELETE FROM nestother.dbo.Tb025_CVM_ITR_DATA WHERE ZipName=@ZIPNAME
*/
SELECT COUNT(*) FROM (SELECT DISTINCT(ZipName) FROM nestother.dbo.Tb025_CVM_ITR_DATA WHERE FileName LIKE '%DER%') A

SELECT COUNT(*) FROM (SELECT DISTINCT(ZipName) FROM nestother.dbo.Tb025_CVM_ITR_DATA WHERE FileName LIKE '%BPA%') A
