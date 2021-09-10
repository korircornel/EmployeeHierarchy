using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.IO;
using EmployeeFileReader;

namespace E
{
    class Program
    {
        static readonly string FILENAME = @"C:\Users\KORIR\source\repos\GitRepos\EmployeeHierarchy\test.csv";
        static void Main(string[] args)
        {
            var employee = new Employee(FILENAME);
        }
    }
}
