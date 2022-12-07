SELECT [order].OrderID,[order].CustomerName,[item].DESCRIPTION,[item].amount,[item].price ,[order].Total
from [Order] , [item] 
where [order].OrderID = [item].OrderID;

