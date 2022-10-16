using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRecords
{
    public static class EnvironmentVariables
    {
        public static void InitializeEnvironmentVariables()
        {
            System.Environment.SetEnvironmentVariable("DATABASE_URL", "server=host.docker.internal;user id=root;password=test;port=3306;database=bookrecords;");
            
        }
    }
}