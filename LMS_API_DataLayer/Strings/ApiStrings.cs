using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_API_DataLayer.Strings
{
    public static class ApiStrings
    {
        public static string getAuthors = "select * from Authors";
        public static string getBooks = "select * from Books";
        public static string getIssues = "select * from Issues";
        public static string getMembers = "select * from Members";
        public static string getSubjects = "select * from Subjects";
    }
}
