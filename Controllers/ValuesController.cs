using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ProjectTemp.Helpers;
using System.Data;

namespace ProjectTemp.Controllers
{
    [Route("api/ValuesController")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/ValuesController/GetValues
        [HttpGet]
        [Route("GetValues")]
        public ActionResult<IEnumerable<string>> GetValues()
        {
            List<string> myEMps = new List<string>();
            DatabaseModel dbm = new DatabaseModel();
            DataTable dt = dbm.GetEmpsInfo();
            foreach (DataRow dr in dt.Rows)
            {
                //string name = dr[0].ToString();
                myEMps.Add(dr[0].ToString());
            }
            return Ok(myEMps);
        }

        // GET api/ValuesController/GetValuesById?id=5
        [HttpGet]
        [Route("GetValuesById")]
        public ActionResult<IEnumerable<string>> GetValuesById(int id)
        {
            return new string[] { "value1" };
        }

        // POST api/ValuesController/InsertEmployee
        [HttpPost]
        [Route("InsertEmployee")]
        public ActionResult<IEnumerable<string>> InsertEmployee([FromBody] JObject emp)
        {
            string empName = (string)emp["empName"];
            string empLastName = (string)emp["empLastName"];

            DatabaseModel dbm = new DatabaseModel();
            int res = dbm.insertPerson(empName, empLastName);

            return Ok(res);
        }

        // PUT api/ValuesController/UpdateEmployee
        [HttpPut]
        [Route("UpdateEmployee")]
        public void UpdateEmployee([FromBody] JObject emp)
        {
            int empId = (int)emp["empId"];
            string empName = (string)emp["empName"];
            string empBdate = (string)emp["empBdate"];
        }



        [HttpPut]
        [Route("UpdateSalary")]
        public ActionResult<IEnumerable<string>> UpdateSalary()
        {
           
            DatabaseModel dbm = new DatabaseModel();
            int res=dbm.updateSalaries();
            return Ok(res);
        }
        


    }
}
