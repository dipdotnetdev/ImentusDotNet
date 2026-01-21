CREATE OR ALTER PROCEDURE Employee_Update
    @EmployeeId INT,
    @Email NVARCHAR(MAX)
AS
BEGIN
    Update Employees
    Set
    Email = @Email
    where Id = @EmployeeId
END