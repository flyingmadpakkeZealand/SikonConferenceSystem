using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBUtility;
using RestAPISCS.App_Start;

[assembly: PreApplicationStartMethod(typeof(SetupDatabase), "Setup")]

namespace RestAPISCS.App_Start
{
    enum BaseNames
    {
        SikonDatabase
    }
    public static class SetupDatabase
    {
        public static void Setup()
        {
            DataBases.RegisterDataBase("SikonDatabase",
                @"Data Source=magnusserverdb.database.windows.net;Initial Catalog=MagnusDataBase;User ID=MagnusAdmin;Password=Secret1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            DataBases.RegisterDataBase(BaseNames.SikonDatabase, 
                @"Data Source=magnusserverdb.database.windows.net;Initial Catalog=MagnusDataBase;User ID=MagnusAdmin;Password=Secret1234;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}