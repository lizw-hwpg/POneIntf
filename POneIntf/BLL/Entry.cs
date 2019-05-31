using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using POneIntf.Common;

namespace POneIntf.BLL
{
    public interface IHandleReq
    {        
        string InvokeRequest(string postStr);
    }

    public class Entry
    {
        public static IHandleReq GetHandler(string act)
        {
            if (act == "logincheck")
            {
                return new SI31();
            }
            else if (act=="forgetpsw")
            {
                return new SI32();
            }
            else
            {
                throw new Exception("No handler exists!");
            }
        }
    }
}