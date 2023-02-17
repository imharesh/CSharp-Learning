using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class Program
{
    //  IList employeeList;
    //  IList salaryList;


    IList<Employee> employeeList;
    IList<Salary> salaryList;

    public Program()
    {
        employeeList = new List<Employee> {
            new Employee(){ EmployeeID = 1, EmployeeFirstName = "Rajiv", EmployeeLastName = "Desai", Age = 49},
            new Employee(){ EmployeeID = 2, EmployeeFirstName = "Karan", EmployeeLastName = "Patel", Age = 32},
            new Employee(){ EmployeeID = 3, EmployeeFirstName = "Sujit", EmployeeLastName = "Dixit", Age = 28},
            new Employee(){ EmployeeID = 4, EmployeeFirstName = "Mahendra", EmployeeLastName = "Suri", Age = 26},
            new Employee(){ EmployeeID = 5, EmployeeFirstName = "Divya", EmployeeLastName = "Das", Age = 20},
            new Employee(){ EmployeeID = 6, EmployeeFirstName = "Ridhi", EmployeeLastName = "Shah", Age = 60},
            new Employee(){ EmployeeID = 7, EmployeeFirstName = "Dimple", EmployeeLastName = "Bhatt", Age = 53}
        };
        
        salaryList = new List<Salary> {
            new Salary(){ EmployeeID = 1, Amount = 1000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 1, Amount = 500, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 1, Amount = 100, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 2, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 2, Amount = 1000, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 3, Amount = 1500, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 4, Amount = 2100, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 2800, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 5, Amount = 600, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 5, Amount = 500, Type = SalaryType.Bonus},
            new Salary(){ EmployeeID = 6, Amount = 3000, Type = SalaryType.Monthly},
            new Salary(){ EmployeeID = 6, Amount = 400, Type = SalaryType.Performance},
            new Salary(){ EmployeeID = 7, Amount = 4700, Type = SalaryType.Monthly}
        };
    }

    public static void Main()
    {
      // Create an instance of the Program class.
        Program program = new Program();
        // method 1 
        program.Task1();

        // method 2

        program.Task2();

        // method 3 

        program.Task3();
    }

    public void Task1()
    { 
        //  join both list ,  EmployeeID are comman , and return new collaction of empSalaries
        var empSal = from emp in employeeList
                     join sal in salaryList on emp.EmployeeID equals sal.EmployeeID   into empSalaries
                     
                 //   use Sum method to sum the salaries based on the SalaryType.
        
                     select new
                     { 
                        
                         emp.EmployeeFirstName,
                         emp.EmployeeLastName,
                         TotalSalary = empSalaries.Sum(sal => sal.Type == SalaryType.Monthly ? sal.Amount : 0) +
                                       empSalaries.Sum(sal => sal.Type == SalaryType.Performance ? sal.Amount : 0) +
                                       empSalaries.Sum(sal => sal.Type == SalaryType.Bonus ? sal.Amount : 0)
                     };


        // sort the emp based on salary 
        var sortEmpSal = empSal.OrderBy(emp => emp.TotalSalary);
        Console.WriteLine("*****************************************************************");
        Console.WriteLine(" Total Salary Names in Ascending order of salary");
        Console.WriteLine("*****************************************************************");


        // after the sorting we print the collection of sortEmpSal properites 
        foreach (var empSalary in sortEmpSal)
        {
            Console.WriteLine($"{empSalary.EmployeeFirstName} {empSalary.EmployeeLastName} - {empSalary.TotalSalary}\n");

        }
    }

    public void Task2()
    {
        //  second oldest employee from the employeeList.
        //  second oldest employee monthly salary from the salaryList 

        // sort Age in Decending Order 
        var secOldEmp = employeeList.OrderByDescending(emp => emp.Age)
            // skip the first emp 
                             .Skip(1)
                             .FirstOrDefault();

        // second oldest employee And type Monthly
        // Sum




        var monthSal = salaryList.Where(sal => sal.EmployeeID == secOldEmp.EmployeeID && sal.Type == SalaryType.Monthly)
                                 .Sum(sal => sal.Amount);

        Console.WriteLine();
        Console.WriteLine("*****************************************************************");

        Console.WriteLine($" 2nd oldest employee &  total month salary:");
        Console.WriteLine("*****************************************************************");

        Console.WriteLine($"Name: {secOldEmp.EmployeeFirstName} {secOldEmp.EmployeeLastName}");
        Console.WriteLine($"Age: {secOldEmp.Age}");
        Console.WriteLine($"Total Month Salary: {monthSal}");
    }
   
    public void Task3()
    {

        // use join 
        // where for  30 years old.
       
        var empSalMeans = from emp in employeeList
                          join sal in salaryList on emp.EmployeeID equals sal.EmployeeID into empSalaries
                          where emp.Age > 30
                          // create collection for salary 
                          select new
                          {

                              emp.EmployeeFirstName,
                              emp.EmployeeLastName,

                              // Average of total 3 salary type 
                              TotalSalary = empSalaries.Average(sal => sal.Type == SalaryType.Monthly ? sal.Amount : 0) +
                                            empSalaries.Average(sal => sal.Type == SalaryType.Performance ? sal.Amount : 0) +
                                            empSalaries.Average(sal => sal.Type == SalaryType.Bonus ? sal.Amount : 0)

                          };
        

        Console.WriteLine();
        Console.WriteLine("*****************************************************************");

        Console.WriteLine($"Average salary & Age greater than 30:");
        Console.WriteLine("*****************************************************************");

        foreach (var empSalary in empSalMeans)
        {
            Console.WriteLine($"{empSalary.EmployeeFirstName} {empSalary.EmployeeLastName} - {empSalary.TotalSalary}\n");

        }
    }

}

public enum SalaryType
{
    Monthly,
    Performance,
    Bonus
}

public class Employee
{
    public int EmployeeID { get; set; }
    public string EmployeeFirstName { get; set; }
    public string EmployeeLastName { get; set; }    
    public int Age { get; set; }
}
public class Salary
{
    public int EmployeeID { get; set; }
    public int Amount { get; set; }
    public SalaryType Type { get; set; }
}