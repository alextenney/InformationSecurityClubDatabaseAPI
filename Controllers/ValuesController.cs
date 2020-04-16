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
            foreach (DataRow dr in dt.Rows)
            {
                string specialization = dr[0].ToString();
                if (!(distinctSpecializations.Contains(specialization)))
                {
                    distinctSpecializations.Add(specialization);
                }

                string team = dr[2].ToString();
                if (!(allTeams.Contains(team)))
                {
                    allTeams.Add(team);
                }
            }
            string specializationList = String.Join(", ", distinctSpecializations);
            string teamsList = String.Join(", ", allTeams);

            foreach (DataRow dr in dt.Rows)
            {
                myP.Add("{");
                myP.Add("SPECIALIZATION : [" + specializationList + "]");
                myP.Add("MEMBER SINCE : " + dr[1].ToString());
                myP.Add("TEAMS : [" + teamsList + "]");
                myP.Add("MEETINGS ATTENDED : " + dr[3].ToString());
                myP.Add("SCORE : " + dr[4].ToString());
                myP.Add("}");


            }
            return Ok(myP);
        }


        // GET api/InfoSecDB/GetChallengeInfo
        [HttpGet]
        [Route("GetChallengeInfo")]
        public ActionResult<IEnumerable<string>> GetChallengeInfo()
        {
            List<string> myP = new List<string>(); // myEMps is a list    //////////THIS IS WHERE I WAS ON THIS ENDPOINT
            DatabaseModel dbm = new DatabaseModel(); // creates a database model

            DataTable dt = dbm.GetChallengeInfo(); // calls the GetEMPsInfo from the Helper (DatabaseModel.cs), a datatable is returned by this

            foreach (DataRow dr in dt.Rows)
            {
                myP.Add("{");
                myP.Add("NAME : "+ dr[0].ToString());
                myP.Add("KEY : " + dr[1].ToString());
                myP.Add("PATH : " + dr[2].ToString());
                myP.Add("DIFFICULTY : " + dr[3].ToString());
                myP.Add("AUTHOR : " + dr[4].ToString());
                myP.Add("TYPE : " + dr[5].ToString());
                myP.Add("}");


            }
            return Ok(myP);
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

            foreach (DataRow dr in dt.Rows)
            {
                myP.Add("{");
                myP.Add("TEAM NAME : " + dr[0].ToString());
                myP.Add("PARTICIPANT NAME : " + dr[1].ToString());
                myP.Add("PARTICIPANT SPECIALIZATION: : " + dr[2].ToString());
                myP.Add("}");


            }
            return Ok(myP);
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
        

    }
}
