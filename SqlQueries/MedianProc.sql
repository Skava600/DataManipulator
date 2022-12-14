USE [B1Data]
GO
/****** Object:  StoredProcedure [dbo].[MedianFloatingNumbers]    Script Date: 10/15/2022 7:56:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[MedianFloatingNumbers](
@Median float output)
AS
SELECT @Median = 
(
 (SELECT MAX(PositiveDouble) FROM
   (SELECT TOP 50 PERCENT PositiveDouble FROM DATA ORDER BY positiveDouble) AS BottomHalf)
 +
 (SELECT MIN(PositiveDouble) FROM
   (SELECT TOP 50 PERCENT PositiveDouble FROM DATA ORDER BY positiveDouble DESC) AS TopHalf)
) / 2;