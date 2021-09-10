using System;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace EmployeeFileReader
{
    public class Employee
    {
        public Employee(string employeeCsv)
        {
            if (!string.IsNullOrEmpty(employeeCsv))
            {
                CSVReader csvReader = new CSVReader();
                DataSet ds = csvReader.ReadCSVFile(employeeCsv, true);
                Employees compare = new Employees();
                DataTable errors = compare.Get_Error_Rows(ds.Tables[0]);
            }
            else
            {
                // alert => no file
            }


        }

        public long CalculateSalaryBudget(string managerId)
        {
            //get employees by manager id
            //sum employee salary
            return 0;
        }
    }
    public class CSVReader
    {
        public DataSet ReadCSVFile(string fullPath, bool headerRow)
        {
            string path = fullPath.Substring(0, fullPath.LastIndexOf("\\") + 1);
            string filename = fullPath.Substring(fullPath.LastIndexOf("\\") + 1);
            DataSet ds = new DataSet();

            try
            {
                if (File.Exists(fullPath))
                {
                    string ConStr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}" + ";Extended Properties=\"Text;HDR={1};FMT=Delimited\\\"", path, headerRow ? "Yes" : "No");
                    string SQL = string.Format("SELECT * FROM {0}", filename);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(SQL, ConStr);
                    adapter.Fill(ds, "TextFile");
                    ds.Tables[0].TableName = "Table1";
                }
                foreach (DataColumn col in ds.Tables["Table1"].Columns)
                {
                    col.ColumnName = col.ColumnName.Replace(" ", "_");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }
    }
}
