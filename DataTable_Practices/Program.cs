using System;
using System.Data;
using System.Linq;


namespace DataTable_Practices
{
    internal class Program
    {
        static void AddColumns(DataTable dtEmployees)
        {
            //add columns
            dtEmployees.Columns.Add("ID", typeof(int));
            dtEmployees.Columns.Add("Name", typeof(string));
            dtEmployees.Columns.Add("Salary", typeof(double));
            dtEmployees.Columns.Add("Country", typeof(string));
            dtEmployees.Columns.Add("Birth Date", typeof(DateTime));
            dtEmployees.Columns.Add("Specialization", typeof(string));

            //set id primary key
            DataColumn[] PK_colmuns = new DataColumn[1];
            PK_colmuns[0] = dtEmployees.Columns["ID"];
            dtEmployees.PrimaryKey = PK_colmuns;

        }
        static void AddRows(DataTable dtEmployees)
        {
            //add rows
            dtEmployees.Rows.Add(1, "Reda Hilal", 5000, "Syria", new DateTime(2004, 8, 6), "Software Engineering");
            dtEmployees.Rows.Add(2, "Ahmed Ali", 3150, "Oman", new DateTime(2005, 6, 1), "Computer Engineering");
            dtEmployees.Rows.Add(3, "Mohammad Ahmed", 4000, "KSA", new DateTime(2006, 5, 19), "Civil Engineering");
            dtEmployees.Rows.Add(4, "Omar Khalid", 9000, "Egypt", new DateTime(2000, 9, 22), "Mechanical Engineering");
            dtEmployees.Rows.Add(5, "Mustafa Saleh", 7400, "Jordan", new DateTime(2001, 12, 6), "Electrical Engineering");
            dtEmployees.Rows.Add(6, "Nasser Marawn", 3100, "UAE", new DateTime(2001, 3, 14), "Chemical Engineering");
            dtEmployees.Rows.Add(7, "Yousef Khalid", 7160, "Qatar", new DateTime(2003, 2, 1), "Aerospace Engineering");

        }

        static void PrintDataTable(DataTable dtEmployees)
        {
            Console.WriteLine("Employees List:");
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("{0,-3} {1,-15} {2,-10} {3,-10} {4,-12} {5,-25}", "ID", "Name", "Salary", "Country", "Birth Date", "Specialization");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            foreach (DataRow row in dtEmployees.Rows)
            {
                Console.WriteLine("{0,-3} {1,-15} ${2,-10} {3,-10} {4,-12:dd-MM-yyyy} {5,-25}",
                    row["ID"],
                    row["Name"],
                    row["Salary"],
                    row["Country"],
                    row["Birth Date"],
                    row["Specialization"]);
            }
            //note: {-3} is for Formatting,
            //{-} means align text to left,
            //{3} means 3-character width.
            //{-3} --> aligns to the left with 3-character width.
        }
        static void PrintAggregateFunctions(DataTable dtEmployees)
        {
            int EmployeesCount = dtEmployees.Rows.Count;
            double TotalSalaries = Convert.ToDouble(dtEmployees.Compute("SUM(Salary)", string.Empty));
            double AverageSalary = Convert.ToDouble(dtEmployees.Compute("AVG(Salary)", string.Empty));
            double MinSalary = Convert.ToDouble(dtEmployees.Compute("MIN(Salary)", string.Empty));
            double MaxSalary = Convert.ToDouble(dtEmployees.Compute("MAX(Salary)", string.Empty));

            Console.WriteLine("\n\nCount of Employees = " + EmployeesCount);
            Console.WriteLine("Total Employees Salaries = $" + TotalSalaries);
            Console.WriteLine("Average Employees Salary = ${0:F3}", AverageSalary); //F3, for 3 digits after "."
            Console.WriteLine("Minimum Employees Salary = $" + MinSalary);
            Console.WriteLine("Maximum Employees Salary = $" + MaxSalary);

        }

        static void PrintFilterdRowsFromDataTable(DataTable dtEmployees)
        {
            DataRow[] ResultRows = dtEmployees.Select("Country = 'KSA' OR Salary >= 7000");

            Console.WriteLine("Filter Result: (Country = 'KSA' OR Salary >= 7000)");

            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("{0,-3} {1,-15} {2,-10} {3,-10} {4,-12} {5,-25}", "ID", "Name", "Salary", "Country", "Birth Date", "Specialization");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            foreach (DataRow row in ResultRows)
            {
                Console.WriteLine("{0,-3} {1,-15} ${2,-10} {3,-10} {4,-12:dd-MM-yyyy} {5,-25}",
                    row["ID"],
                    row["Name"],
                    row["Salary"],
                    row["Country"],
                    row["Birth Date"],
                    row["Specialization"]);
            }
        }
        static void PrintAggregateFunctionsForFilterdRows(DataTable dtEmployees)
        {
            string Filter = "Country = 'KSA' OR Salary >= 7000";
            Console.WriteLine("\n\nFilter Result: " + Filter);

            int EmployeesCount = dtEmployees.Rows.Count;
            double TotalSalaries = Convert.ToDouble(dtEmployees.Compute("SUM(Salary)", Filter));
            double AverageSalary = Convert.ToDouble(dtEmployees.Compute("AVG(Salary)", Filter));
            double MinSalary = Convert.ToDouble(dtEmployees.Compute("MIN(Salary)", Filter));
            double MaxSalary = Convert.ToDouble(dtEmployees.Compute("MAX(Salary)", Filter));

            Console.WriteLine("Count of Employees = " + EmployeesCount);
            Console.WriteLine("Total Employees Salaries = $" + TotalSalaries);
            Console.WriteLine("Average Employees Salary = ${0:F3}", AverageSalary); //F3, for 3 digits after "."
            Console.WriteLine("Minimum Employees Salary = $" + MinSalary);
            Console.WriteLine("Maximum Employees Salary = $" + MaxSalary);
        }

        static void SortTableBy_ID_DESC(DataTable dtEmployees)
        {
            dtEmployees.DefaultView.Sort = "ID DESC";
            dtEmployees = dtEmployees.DefaultView.ToTable();

            PrintDataTable(dtEmployees);
        }       
        static void SortTableBy_Name_ASC(DataTable dtEmployees)
        {
            dtEmployees.DefaultView.Sort = "Name ASC";
            dtEmployees = dtEmployees.DefaultView.ToTable();

            PrintDataTable(dtEmployees);
        }
        static void SortTableBy_Salary_DESC(DataTable dtEmployees)
        {
            dtEmployees.DefaultView.Sort = "Salary DESC";
            dtEmployees = dtEmployees.DefaultView.ToTable();

            PrintDataTable(dtEmployees);
        }

        static void DeleteRows(DataTable dtEmployees)
        {
            DataRow[] rows = dtEmployees.Select("ID = 5 OR ID = 6");

            foreach (DataRow datarow in rows)
            {
                datarow.Delete();
            }

            //commit the changes to the database. (only if the DataTable is sync with a database)
            //dtEmployees.AcceptChanges(); 

            PrintDataTable(dtEmployees);
        }
        static void UpdateRows(DataTable dtEmployees)
        {
            DataRow[] rows = dtEmployees.Select("ID = 2");

            foreach (DataRow datarow in rows)
            {
                datarow["Name"] = "Maher Abduallah";
                datarow["Salary"] = 8500.5;
                datarow["Birth Date"] = new DateTime(1999, 5, 29);
            }

            //commit the changes to the database. (only if the DataTable is sync with a database)
            //dtEmployees.AcceptChanges(); 

            PrintDataTable(dtEmployees);
        }

        static void AddColumns_Detailed(DataTable dtEmployees)
        {
            //adding ID column
            DataColumn column = new DataColumn();

            column.ColumnName = "ID";
            column.DataType = typeof(int);
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            column.Caption = "Employee ID";
            column.ReadOnly = true;
            column.Unique = true;

            dtEmployees.Columns.Add(column);
            

            //adding Name column
            column = new DataColumn();
            column.ColumnName = "Name";
            column.DataType = typeof(string);
            column.AutoIncrement = false;
            column.Caption = "Full Name";
            column.ReadOnly = false;
            column.Unique = false;

            dtEmployees.Columns.Add(column);
            
            //adding Salary column
            column = new DataColumn();
            column.ColumnName = "Salary";
            column.DataType = typeof(double);
            column.AutoIncrement = false;
            column.Caption = "Salary";
            column.ReadOnly = false;
            column.Unique = false;

            dtEmployees.Columns.Add(column);
            
            //adding country column
            column = new DataColumn();
            column.ColumnName = "Country";
            column.DataType = typeof(string);
            column.AutoIncrement = false;
            column.Caption = "Country";
            column.ReadOnly = false;
            column.Unique = false;

            dtEmployees.Columns.Add(column);
            
            //adding BirthDate column
            column = new DataColumn();
            column.ColumnName = "Birth Date";
            column.DataType = typeof(DateTime);
            column.AutoIncrement = false;
            column.Caption = "Birth Date";
            column.ReadOnly = false;
            column.Unique = false;

            dtEmployees.Columns.Add(column);

            //adding Specialization column
            column = new DataColumn();
            column.ColumnName = "Specialization";
            column.DataType = typeof(string);
            column.AutoIncrement = false;
            column.Caption = "Specialization";
            column.ReadOnly = false;
            column.Unique = false;

            dtEmployees.Columns.Add(column);

        }
        static void AddRows_Detailed(DataTable dtEmployees)
        {
            //add rows, but send id as null because it is auto increament 
            dtEmployees.Rows.Add(null, "Reda Hilal", 5000, "Syria", new DateTime(2004, 8, 6), "Software Engineering");
            dtEmployees.Rows.Add(null, "Ahmed Ali", 3150, "Oman", new DateTime(2005, 6, 1), "Computer Engineering");
            dtEmployees.Rows.Add(null, "Mohammad Ahmed", 4000, "KSA", new DateTime(2006, 5, 19), "Civil Engineering");
            dtEmployees.Rows.Add(null, "Omar Khalid", 9000, "Egypt", new DateTime(2000, 9, 22), "Mechanical Engineering");
            dtEmployees.Rows.Add(null, "Mustafa Saleh", 7400, "Jordan", new DateTime(2001, 12, 6), "Electrical Engineering");
            dtEmployees.Rows.Add(null, "Nasser Marawn", 3100, "UAE", new DateTime(2001, 3, 14), "Chemical Engineering");
            dtEmployees.Rows.Add(null, "Yousef Khalid", 7160, "Qatar", new DateTime(2003, 2, 1), "Aerospace Engineering");

        }

        static void Main(string[] args)
        {
            DataTable dtEmployees = new DataTable();


            //AddColumns(dtEmployees);
            //AddRows(dtEmployees);

            //PrintDataTable(dtEmployees);
            //PrintAggregateFunctions(dtEmployees);

            //PrintFilterdRowsFromDataTable(dtEmployees);
            //PrintAggregateFunctionsForFilterdRows(dtEmployees);

            //SortTableBy_ID_DESC(dtEmployees);
            //SortTableBy_Name_ASC(dtEmployees);
            //SortTableBy_Salary_DESC(dtEmployees);

            //DeleteRows(dtEmployees);
            //UpdateRows(dtEmployees);

            //dtEmployees.Clear();
            //PrintDataTable(dtEmployees);

            //AddColumns_Detailed(dtEmployees);
            //AddRows_Detailed(dtEmployees);
            //PrintDataTable(dtEmployees);

            Console.ReadLine();

        }
    }
}
