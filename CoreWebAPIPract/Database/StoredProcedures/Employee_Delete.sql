create or alter procedure Employee_Delete
@EmployeeId int

as 
begin
delete from employees
where id = @EmployeeId
end