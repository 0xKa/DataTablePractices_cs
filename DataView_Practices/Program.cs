using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataView_Practices
{
    internal class Program
    {
        static DataTable CreateSampleEmployeeDataTable()
        {
            DataTable dtEmployees = new DataTable();

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
        static void PrintDataTable(DataTable dtEmployees)
        {
            Console.WriteLine("Employees List:");
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("{0,-3} {1,-15} {2,-10} {3,-10} {4,-12} {5,-25}", "ID", "Name", "Salary", "Country", "Birth Date", "Specialization");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            foreach (DataRow row in dtEmployees.Rows)
            {
                Console.WriteLine("{0,-3} {1,-15} ${2,-10}{3,-10} {4,-12:dd-MM-yyyy} {5,-25}",
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

        static void PrintDataView(DataView dtEmployees_view1)
        {

            Console.WriteLine("Employees List:");
            Console.WriteLine("---------------------------------------------------------------------------------------------");
            Console.WriteLine("{0,-3} {1,-15} {2,-10} {3,-10} {4,-12} {5,-25}", "ID", "Name", "Salary", "Country", "Birth Date", "Specialization");
            Console.WriteLine("---------------------------------------------------------------------------------------------");

            foreach (DataRowView row in dtEmployees_view1)
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

        static void DataViewFilter(DataView dtEmployees_view1)
        {
            dtEmployees_view1.RowFilter = "Country = 'KSA' OR Country = 'Syria'";

            PrintDataView(dtEmployees_view1);
        }

        static void DataViewSort(DataView dtEmployees_view1)
        {
            dtEmployees_view1.Sort = "Salary DESC";

            PrintDataView(dtEmployees_view1);
        }


        static void Main(string[] args)
        {
            DataTable dtEmployees = CreateSampleEmployeeDataTable();
            //PrintDataTable(dtEmployees);

            //create DataView
            DataView dtEmployees_view1 = dtEmployees.DefaultView;

            //PrintDataView(dtEmployees_view1);

            //DataViewFilter(dtEmployees_view1);

            //DataViewSort(dtEmployees_view1);

            Console.ReadLine();
        }
    }
}
