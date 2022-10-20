USE [B1Data]
GO
/****** Object:  StoredProcedure [dbo].[SumOfIntegers]    Script Date: 10/15/2022 8:03:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SumOfIntegers]
(@sum BIGINT OUTPUT)
As
SELECT @sum = SUM(CAST(PositiveEvenInteger as BIGINT)) from DATA;