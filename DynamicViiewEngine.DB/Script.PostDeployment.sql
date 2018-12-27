/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--Reference Data for Views

MERGE INTO [Views] AS Target 
USING (VALUES 
  (1, 1, N'_MotorcycleTableViewPartial', NULL, N'@using DynamicViewEngine.Data;
																	@model List<Motorcycle>
																	<div class="row">
																		@foreach (var motorcycle in Model)
																		{
																			<div class="col-md-4">
																				<h2>@string.Format($"{motorcycle.Model} Class {motorcycle.Name}")</h2>
																				<p><a class="btn btn-default" href="#">Learn more &raquo;</a></p>
																			</div>
																		}
																	</div>')
  , (2, 0, N'_MotorcycleBulletListPartial', N'../Views/Home/_MotorcycleBulletListPartial.cshtml', NULL)
  , (3, 0, N'_MotorcycleListPartial', N'../Views/Home/_MotorcycleListPartial.cshtml', NULL)
) 
AS Source (Id, IsVirtual, ViewName, ViewPath, ViewContent) 
ON Target.Id = Source.Id
-- update matched rows 
WHEN MATCHED THEN 
UPDATE SET IsVirtual = SOURCE.IsVirtual
, ViewName = SOURCE.ViewName
, ViewPath = SOURCE.ViewPath
, ViewContent = SOURCE.ViewContent
 --insert new rows 
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, IsVirtual, ViewName, ViewPath, ViewContent) 
VALUES (Id, IsVirtual, ViewName, ViewPath, ViewContent) 
 --delete rows that are in the target but not the source 
WHEN NOT MATCHED BY SOURCE THEN 
DELETE;

IF NOT EXISTS (SELECT 1 FROM [dbo].Motorcycle)
	INSERT INTO dbo.Motorcycle VALUES (1, N'Ninja ZX-6R', N'Super Sport')
	INSERT INTO dbo.Motorcycle VALUES (2, N'Ninja 1000 ABS', N'Sport')
	INSERT INTO dbo.Motorcycle VALUES (3, N'Ninja H2 R', N'Hyper Sport')
