using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataSet_DataAdapter_Practices
{
    internal class Program
    {
        static DataTable CreateEmployeeDataTable()
        {
            DataTable dtEmployees = new DataTable("Employee"); //here you can give a name for the table

            //ID Column
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

            //set id primary key
            DataColumn[] PK_colmuns = new DataColumn[1];
            PK_colmuns[0] = dtEmployees.Columns["ID"];
            dtEmployees.PrimaryKey = PK_colmuns;

            //other columns
            dtEmployees.Columns.Add("Name", typeof(string));
            dtEmployees.Columns.Add("Salary", typeof(double));
            dtEmployees.Columns.Add("Country", typeof(string));
            dtEmployees.Columns.Add("Birth Date", typeof(DateTime));
            dtEmployees.Columns.Add("Specialization", typeof(string));


            //Add Sample DataRows
            //add rows
            dtEmployees.Rows.Add(null, "Reda Hilal", 5000, "Syria", new DateTime(2004, 8, 6), "Software Engineering");
            dtEmployees.Rows.Add(null, "Ahmed Ali", 3150, "Oman", new DateTime(2005, 6, 1), "Computer Engineering");
            dtEmployees.Rows.Add(null, "Mohammad Ahmed", 4000, "KSA", new DateTime(2006, 5, 19), "Civil Engineering");
            dtEmployees.Rows.Add(null, "Omar Khalid", 9000, "Egypt", new DateTime(2000, 9, 22), "Mechanical Engineering");
            dtEmployees.Rows.Add(null, "Mustafa Saleh", 7400, "Jordan", new DateTime(2001, 12, 6), "Electrical Engineering");
            dtEmployees.Rows.Add(null, "Nasser Marawn", 3100, "UAE", new DateTime(2001, 3, 14), "Chemical Engineering");
            dtEmployees.Rows.Add(null, "Yousef Khalid", 7160, "Qatar", new DateTime(2003, 2, 1), "Aerospace Engineering");


            return dtEmployees;
        }
        static DataTable CreateDepartmentDataTable()
        {
            DataTable dtDepartment = new DataTable("Department");

            //ID Column
            DataColumn column = new DataColumn();
            column.ColumnName = "ID";
            column.DataType = typeof(int);
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            column.Caption = "Department ID";
            column.ReadOnly = true;
            column.Unique = true;
            dtDepartment.Columns.Add(column);

            //set id primary key
            DataColumn[] PK_colmuns = new DataColumn[1];
            PK_colmuns[0] = dtDepartment.Columns["ID"];
            dtDepartment.PrimaryKey = PK_colmuns;

            //other columns
            dtDepartment.Columns.Add("Name", typeof(string));

            //Add sample DataRows
            dtDepartment.Rows.Add(null, "IT");
            dtDepartment.Rows.Add(null, "Engineering");
            dtDepartment.Rows.Add(null, "HR");
            dtDepartment.Rows.Add(null, "Marketing");

            return dtDepartment;
        }

        static void PrintEmployeeDataTable_FromDataSet(DataSet dataSet1)
        {
            Console.WriteLine("Employees List:");
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("{0,-3} {1,-15} {2,-10} {3,-10} {4,-12} {5,-25}", "ID", "Name", "Salary", "Country", "Birth Date", "Specialization");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            foreach (DataRow row in dataSet1.Tables["Employee"].Rows)
            {
                Console.WriteLine("{0,-3} {1,-15} ${2,-10}{3,-10} {4,-12:dd-MM-yyyy} {5,-25}",
                    row["ID"],
                    row["Name"],
                    row["Salary"],
                    row["Country"],
                    row["Birth Date"],
                    row["Specialization"]);
            }
        }
        static void PrintDepartmentDataTable_FromDataSet(DataSet dataSet1)
        {
            Console.WriteLine("\n\nDepartment List:");
            Console.WriteLine("--------------------------");
            Console.WriteLine("{0,-3} {1,-15}", "ID", "Department");
            Console.WriteLine("--------------------------");

            foreach (DataRow row in dataSet1.Tables["Department"].Rows)
            {
                Console.WriteLine("{0,-3} {1,-15}",
                    row["ID"],
                    row["Name"]);
            }
        }

        static void DataSetMain()
        {
            DataTable dtEmployee = CreateEmployeeDataTable();
            DataTable dtDepartment = CreateDepartmentDataTable();


            //Create DataSet 
            DataSet dataSet1 = new DataSet();

            dataSet1.Tables.Add(dtEmployee);
            dataSet1.Tables.Add(dtDepartment);

            PrintEmployeeDataTable_FromDataSet(dataSet1);
            PrintDepartmentDataTable_FromDataSet(dataSet1);
        }


        static void DataAdapterMain()
        {
            DataSet HR_DataSet = new DataSet();

            string ConnectionString =
                "Server = .; Database = HR_DataBase; User ID = sa; Password = sa123456;";

            string query = "SELECT * FROM Employees";

            // Create a SqlDataAdapter with the query and connection string
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, ConnectionString);

            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();

                dataAdapter.SelectCommand.Connection = connection;

                // Fill the DataSet with the results of the SQL query, store it in the "Employees" table
                dataAdapter.Fill(HR_DataSet, "Employees");


                //print data from HR_DataSet --> Employees Tabel
                foreach (DataRow row in HR_DataSet.Tables["Employees"].Rows)
                {
                    Console.WriteLine("ID: {0}, FirstName: {1}, LastName: {2}, MonthlySalary: {3}",
                                  row["ID"], row["FirstName"], row["LastName"], row["MonthlySalary"]);
                }
            }
            catch (Exception ex) { }
            finally { connection.Close(); }

            //now we have all the data in a DataSet --> "HR_DataSet"
            //we can do changes like (add, delete, update, ...)
            //and then sync the DataSet with the database to save the changes

            //we save changes by connecting to the database again:

            try
            {
                connection.Open();

                dataAdapter.UpdateCommand.Connection = connection;
                dataAdapter.Update(HR_DataSet, "Employees");
            }
            catch (Exception e) {}
            finally
            { connection.Close(); }
        }

        static void Main(string[] args)
        {
            DataSetMain();
            DataAdapterMain();

            Console.ReadLine();
        }
    }
}
