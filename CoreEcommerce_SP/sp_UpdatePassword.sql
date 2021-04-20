USE [CoreEcommerce]
GO
/****** Object:  StoredProcedure [dbo].[sp_loginUser]    Script Date: 2021-04-14 4:56:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Thanh Ty Nguyen>
-- Create date:
-- Description:	Update Password
-- =============================================
CREATE PROCEDURE [dbo].[sp_updatePassword]
	-- Add the parameters for the stored procedure here
	@email Nvarchar(50),
	@password nvarchar(200),
	@returnValue int OUTPUT 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Accounts 
			SET
				Password = CONVERT(VARCHAR(32), HashBytes('MD5', @password), 2)
			WHERE 
				Email = @email
	if(@@ROWCOUNT > 0)
	BEGIN
		SET @returnValue= 200
	END
	ELSE
	BEGIN
		SET @returnValue = 500
	END
END

