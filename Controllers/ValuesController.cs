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

        // GET api/InfoSecDB/adminSelectsParticipant
        [HttpGet]
        [Route("adminSelectsParticipant")]
        public ActionResult<IEnumerable<string>> adminSelectsParticipant([FromBody] JObject data)
        {
            string pName = (string)data["pName"];
            List<string> distinctSpecializations = new List<string>();
            List<string> allTeams = new List<string>();

            List<string> myP = new List<string>();
            DatabaseModel dbm = new DatabaseModel();
            DataTable dt = dbm.adminSelectsParticipant(pName);

            return Ok(dt);
        }


        // GET api/InfoSecDB/GetChallengeInfo
        [HttpGet]
        [Route("GetChallengeInfo")]
        public ActionResult<IEnumerable<string>> GetChallengeInfo()
        {
            List<string> myP = new List<string>(); // myEMps is a list    //////////THIS IS WHERE I WAS ON THIS ENDPOINT
            DatabaseModel dbm = new DatabaseModel(); // creates a database model

            DataTable dt = dbm.GetChallengeInfo(); // calls the GetEMPsInfo from the Helper (DatabaseModel.cs), a datatable is returned by this

            return Ok(dt);
        }

        // GET api/InfoSecDB/GetTeam
        [HttpGet]
        [Route("GetTeam")]
        public ActionResult<IEnumerable<string>> GetTeam([FromBody] JObject data)
        {
            string teamName = (string)data["teamName"];
            List<string> myP = new List<string>(); // myEMps is a list    //////////THIS IS WHERE I WAS ON THIS ENDPOINT
            DatabaseModel dbm = new DatabaseModel(); // creates a database model

            DataTable dt = dbm.GetTeam(teamName); // calls the GetEMPsInfo from the Helper (DatabaseModel.cs), a datatable is returned by this


            return Ok(dt);
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

        // PUT api/InfoSecDB/participantAddsThemselvesToTeam
        [HttpPut]
        [Route("participantAddsThemselvesToTeam")]
        public ActionResult<IEnumerable<string>> participantAddsThemselvesToTeam([FromBody] JObject data)
        {
            string participantName = (string)data["participantName"];
            string teamName = (string)data["teamName"];

            DatabaseModel dbm = new DatabaseModel();
            int value = dbm.participantAddsThemselvesToTeam(participantName, teamName);

            return Ok(value);
        }


        // GET api/ValuesController/SelectCTF
        [HttpGet]
        [Route("SelectCTF")]
        public ActionResult<IEnumerable<string>> SelectCTF([FromBody] JObject data)
        {
            string ctfName = (string)data["ctfName"];

            List<string> selectCTF = new List<string>();
            DatabaseModel dbm = new DatabaseModel();
            DataTable dt = dbm.SelectCTF(ctfName);

            return Ok(dt);
        }
    }

}
