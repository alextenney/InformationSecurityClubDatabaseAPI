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

    [Route("api/InfoSecDB")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // is this how we call this endpoint?
        // GET api/InfoSecDB/adminSelectsParticipant
        [HttpGet] // guessing this means that it's specified as a GET request
        [Route("GetParticipant")] // not 100%, corresponds to the method name
        public ActionResult<IEnumerable<string>> GetParticipant([FromBody] JObject data)
        {
            {
                string participantName = (string)data["participantName"];  //////////THIS IS WHERE I WAS ON THIS ENDPOINT
                List<string> myP = new List<string>();
                DatabaseModel dbm = new DatabaseModel(); // creates a database model


                DataTable dt = dbm.GetParticipant(participantName); // calls the GetEMPsInfo from the Helper (DatabaseModel.cs), a datatable is returned by this

                int i = 0;
                int e = 0;

                foreach (DataRow dr in dt.Rows)
                {
                    e = 0;
                    myP.Add(dr[e].ToString());
                    e = e + 1;
                    while (i < 8)
                    {
                        //string name = dr[0].ToString();
                        myP.Add(dr[i].ToString()); //converts each row into a string, then puts it in the myEMps list

                        i = i + 1;
                    }

                    i = 1;

                }

                return Ok(myP); // the OK method returns a 200 status code (indicates request succeeded)
                                // I'm not sure if MORE is being returned there or not
            }
        }

        // THIS IS HARD CODED!!! THAT MONSTER!!!!!
        // GET api/ValuesController/GetValuesById?id=5
        [HttpGet]
        [Route("GetValuesById")]
        public ActionResult<IEnumerable<string>> GetValuesById(int id)
        {
            return new string[] { "value1" };
        }

        // GET api/InfoSecDB/GetChallengeInfo
        [HttpGet]
        [Route("GetChallengeInfo")]
        public ActionResult<IEnumerable<string>> GetChallengeInfo()
        {
            List<string> myP = new List<string>(); // myEMps is a list    //////////THIS IS WHERE I WAS ON THIS ENDPOINT
            DatabaseModel dbm = new DatabaseModel(); // creates a database model

            DataTable dt = dbm.GetChallengeInfo(); // calls the GetEMPsInfo from the Helper (DatabaseModel.cs), a datatable is returned by this
            int i = 0;
            int e = 0;

            foreach (DataRow dr in dt.Rows)
            {
                e = 0;
                myP.Add(dr[e].ToString());
                e = e + 1;
                while (i < 8)
                {
                    //string name = dr[0].ToString();
                    myP.Add(dr[i].ToString()); //converts each row into a string, then puts it in the myEMps list

                    i = i + 1;
                }

                i = 1;

            }
            return Ok(myP);
        }
        // POST api/InfoSecDB/InsertEmployee
        [HttpPost]
        [Route("InsertEmployee")]
        public ActionResult<IEnumerable<string>> InsertEmployee([FromBody] JObject emp)
            // passes in a JSON object...I assume that's all the data to add to the table
        {
            string empName = (string)emp["empName"]; //objects in the JSON must be tuples?  dictionary-like?
            string empLastName = (string)emp["empLastName"];

            DatabaseModel dbm = new DatabaseModel();
            int res = dbm.insertPerson(empName, empLastName);

            return Ok(res);
        }

        // PUT api/InfoSecDB/adminAddsTeamMember
        [HttpPut]
        [Route("adminAddsTeamMember")]
        public ActionResult<IEnumerable<string>> adminAddsTeamMember([FromBody] JObject data)
        {
            string participantName = (string)data["participantName"];
            string teamName = (string)data["teamName"];

            DatabaseModel dbm = new DatabaseModel();
            int value = dbm.adminAddsTeamMember(participantName, teamName);

            return Ok(value);
        }

        // PUT api/InfoSecDB/participantAddsAttendance
        [HttpPut]
        [Route("participantAddsAttendance")]
        public ActionResult<IEnumerable<string>> participantAddsAttendance([FromBody] JObject data)
        {
            string participantName = (string)data["participantName"];
            string meetingDate = (string)data["meetingDate"];

            DatabaseModel dbm = new DatabaseModel();
            int value = dbm.participantAddsAttendance(participantName, meetingDate);

            return Ok(value);
        }

        // PUT api/InfoSecDB/adminCreatesTeam
        [HttpPut]
        [Route("adminCreatesTeam")]
        public ActionResult<IEnumerable<string>> adminCreatesTeam([FromBody] JObject data)
        {
            string teamName = (string)data["teamName"];

            DatabaseModel dbm = new DatabaseModel();
            int value = dbm.adminCreatesTeam(teamName);

            return Ok(value);
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
