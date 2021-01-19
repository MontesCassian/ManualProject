CREATE PROCEDURE [dbo].[sp_CreateEmptyTutorial]
	@Id int OUT,
	@MenuTitle nvarchar(100),
	@Title nvarchar(100),
	@Text nvarchar(max),
	@Hometask nvarchar(max),
	@Turn int 
AS
	UPDATE Tutorial SET MenuTitle = @MenuTitle, 
	Title = @Title,
	Text = @Text,
	Hometask = @Hometask,
	Turn = @Turn
	WHERE Id= @Id
RETURN 0
