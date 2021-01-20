ALTER PROCEDURE [dbo].[Device_List]  
(  
	@pageNumber int=1,
	@pageSize int=50,
	@orderBy nvarchar(500)='',
	@search nvarchar(500)='',
	@companyGuid UNIQUEIDENTIFIER
)  
AS BEGIN  
    SET NOCOUNT ON;  
  
    DECLARE @totalDevices int = 0
	DECLARE @totalGateways int = 0
  
    SET @search = LTRIM(RTRIM(lower(@search)))
	DECLARE @SortColumn varchar(20) = ''
	DECLARE @SortOrder varchar(20) = ''

	if(ISNULL(@OrderBy,'') = '')
	BEGIN
		SET @SortColumn = 'deviceId'
		SET @SortOrder = 'asc'
	END
	ELSE
	BEGIN
		
		set @sortColumn=SUBSTRING(@orderBy,0,CHARINDEX(',',@orderBy,0)) 

		set @sortColumn=SUBSTRING(@orderBy,0,CHARINDEX(' ',@orderBy,0)) 
		set @sortOrder=SUBSTRING(@orderBy,CHARINDEX(' ',@orderBy,0)+1,LEN(@orderBy)) 
	
		set @sortColumn=LTRIM(RTRIM(LOWER(IsNull(@sortColumn,''))))
		set @sortOrder=LTRIM(RTRIM(LOWER(IsNull(@sortOrder,''))))
	END

		SELECT @totalDevices = count(d.[guid]) from dbo.[Device] d
		WHERE d.isdeleted = 0  
		AND d.companyguid = @companyGuid
		AND (rtrim(ltrim(lower(d.uniqueId))) LIKE '%' + @search + '%' OR rtrim(ltrim(lower(d.name))) LIKE '%' + @search + '%')

		SELECT @totalGateways = count(g.[guid]) from dbo.[Gateway] g
		WHERE g.isdeleted = 0  
		AND g.companyguid = @companyGuid
		AND (rtrim(ltrim(lower(g.uniqueId))) LIKE '%' + @search + '%' OR rtrim(ltrim(lower(g.name))) LIKE '%' + @search + '%')

		  
    ; WITH CTE_Results AS   
    (  
		SELECT Convert(bit,1) as IsGateway,g.[guid],g.uniqueid deviceId,g.name deviceName,'' pumpId,
		'' location,g.isactive from Gateway g
		WHERE g.isdeleted = 0  
		AND g.companyguid = @companyGuid
		AND (rtrim(ltrim(lower(g.uniqueId))) LIKE '%' + @search + '%' OR rtrim(ltrim(lower(g.name))) LIKE '%' + @search + '%')
		
		UNION ALL

        SELECT Convert(bit,0) as IsGateway,d.[guid],d.uniqueid deviceId,d.name deviceName,ISNULL(p.uniqueid,'') pumpId,
				ISNULL(loc.name,'') location,d.isactive from dbo.[Device] d
		
		LEFT JOIN dbo.[Pump] p on d.pumpguid = p.[guid]
		LEFT JOIN dbo.[Location] loc on p.locationId = loc.[guid] 
		WHERE d.isdeleted = 0  
		AND p.isdeleted = 0 AND p.isactive = 1
		AND loc.isdeleted = 0 AND loc.isactive = 1
		AND d.companyguid = @companyGuid
		AND (rtrim(ltrim(lower(d.uniqueId))) LIKE '%' + @search + '%' OR rtrim(ltrim(lower(d.name))) LIKE '%' + @search + '%')
        
    ) 

	select @totalDevices + @totalGateways as 'Count',* from CTE_Results
	ORDER BY 
		CASE WHEN (@SortColumn = 'isGateway' AND @SortOrder='ASC')  
                    THEN isGateway
		END ASC,
		CASE WHEN (@SortColumn = 'isGateway' AND @SortOrder='desc')  
                    THEN isGateway
		END DESC, 
        CASE WHEN (@SortColumn = 'deviceId' AND @SortOrder='ASC')  
                    THEN deviceId
		END ASC,
		CASE WHEN (@SortColumn = 'deviceId' AND @SortOrder='desc')  
                    THEN deviceId
		END DESC,
		CASE WHEN (@SortColumn = 'deviceName' AND @SortOrder='ASC')  
                    THEN deviceName
		END ASC,
		CASE WHEN (@SortColumn = 'deviceName' AND @SortOrder='desc')  
                    THEN deviceName
		END DESC,
		CASE WHEN (@SortColumn = 'pumpId' AND @SortOrder='ASC')  
                    THEN pumpId
		END ASC,
		CASE WHEN (@SortColumn = 'pumpId' AND @SortOrder='desc')  
                    THEN pumpId
		END DESC,
		CASE WHEN (@SortColumn = 'location' AND @SortOrder='ASC')  
                    THEN location
		END ASC,
		CASE WHEN (@SortColumn = 'location' AND @SortOrder='desc')  
                    THEN location
		END DESC,
		CASE WHEN (@SortColumn = 'isactive' AND @SortOrder='ASC')  
                    THEN isactive
		END ASC,
		CASE WHEN (@SortColumn = 'isactive' AND @SortOrder='desc')  
                    THEN isactive
		END DESC

        OFFSET @pageSize * (@pageNumber - 1) ROWS  
        FETCH NEXT @pageSize ROWS ONLY 
  
    OPTION (RECOMPILE)  
END
