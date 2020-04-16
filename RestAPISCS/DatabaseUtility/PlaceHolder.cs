using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBUtility;

namespace RestAPISCS.DatabaseUtility
{
    public class PlaceHolder
    {
        private void Test()
        {
            DataBases.Access<object>("test", "testTable").Delete(new Dictionary<string, object>());
        }
    }
}