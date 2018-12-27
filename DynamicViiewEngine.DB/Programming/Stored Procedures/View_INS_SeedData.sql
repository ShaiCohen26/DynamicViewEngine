CREATE PROCEDURE [dbo].[View_INS_SeedData]
AS
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