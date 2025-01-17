CREATE PROCEDURE dbo.SP_SEL_COUNTRY_STATS
    
AS
BEGIN
   SELECT 
    C.Name AS 'CountryName', 
    COUNT(IP.Id) AS 'AddressesCount', 
    MAX(IP.UpdatedAt) AS 'LastAddressUpdated' 
   FROM 
    Countries C
   INNER JOIN 
    IpAddresses IP ON C.Id = ip.CountryId
    GROUP BY
    C.Name
END;
GO