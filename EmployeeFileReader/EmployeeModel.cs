using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;

namespace EmployeeFileReader
{
    public class Employees
    {
        public static Dictionary<string, Employees> dict = new Dictionary<string, Employees>()
        {
               { "EMPLOYEE_ID", new Employees() { columnName = "EMPLOYEE_ID", pattern = @"[A-Za-z0-9 ]+", positveNegative = true, required = true, IsNull=false}},
               { "MANAGER_ID", new Employees() { columnName = "MANAGER_ID", pattern = @"[A-Za-z0-9 ]+", positveNegative = true, required = false, IsNull=true}},
               { "SALARY", new Employees() { columnName = "SALARY", pattern = @"[^\d]+", positveNegative = false, required = true, IsNull=false }},
        };

        string columnName { get; set; }
        string pattern { get; set; }
        Boolean positveNegative { get; set; }
        Boolean required { get; set; }
        Boolean IsNull { get; set; }

        public DataTable Get_Error_Rows(DataTable dt)
        {
            DataTable dtError = null;
            foreach (DataRow row in dt.AsEnumerable())
            {
                Boolean error = false;
                foreach (DataColumn col in dt.Columns)
                {
                    Employees Employees = dict[col.ColumnName];
                    object colValue = row.Field<object>(col.ColumnName);
                    if (Employees.required)
                    {
                        if (colValue == null)
                        {
                            error = true;
                            break;
                        }
                    }
                    else
                    {
                        if (colValue == null)
                            continue;
                    }
                    string colValueStr = colValue.ToString();
                    Match match = Regex.Match(colValueStr, Employees.pattern);
                    if (Employees.positveNegative)
                    {
                        if (!match.Success)
                        {
                            error = true;
                            break;
                        }
                        if (colValueStr.Length != match.Value.Length)
                        {
                            error = true;
                            break;
                        }
                    }
                    else
                    {
                        if (match.Success)
                        {
                            error = true;
                            break;
                        }
                    }

                }

                if (error)
                {
                    if (dtError == null) dtError = dt.Clone();
                    dtError.Rows.Add(row.ItemArray);
                }
            }
            return dtError;
        }
    }
}
