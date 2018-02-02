using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MapAPITest.Controllers
{
    public class BaseAPIController : ApiController
    {
        protected JsonResult GetJsonResult()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.ContentEncoding = System.Text.Encoding.UTF8;
            result.ContentType = "application/json";
            return result;
        }

        public JsonResult CreateJsonResult(bool isSuccess, string response = "")
        {
            var json = new JsonResult();
            json.ContentType = "text/plain";

            if (!isSuccess)
            {
                json.Data = new { success = isSuccess, error = response };
            }
            else
            {
                json.Data = new { success = isSuccess };
            }

            return json;
        }
    }
}
