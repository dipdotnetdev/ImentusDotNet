import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../services/employee.service';
import { Employee } from '../models/employee';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html'
})
export class EmployeesComponent implements OnInit {

  employees: Employee[] = [];

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees() {
    this.employeeService.getAll().subscribe({
      next: data => this.employees = data,
      error: err => console.error("error", err)
    });
  }
}
