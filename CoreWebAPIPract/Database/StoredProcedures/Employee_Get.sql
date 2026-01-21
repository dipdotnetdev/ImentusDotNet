CREATE OR ALTER PROCEDURE Employee_Get
    @EmployeeId INT
AS
BEGIN
    SELECT *
    FROM Employees
    WHERE Id = @EmployeeId
END