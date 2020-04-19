/* 
Contributers: Alexandra Tenney, Martha Ibarra, Jeremy Stuart
 Date Updated: April 17, 2020
 Description: These are the controllers, that interact with the Front End (or in our case post man) ans receive the incoming HTTP 
 Request paired wiht any input and then process that request by sending it to the Database Model
*/

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
        // grabs input pNaem and sends to Database Model
        [HttpGet]
        [Route("adminSelectsParticipant")]
        public ActionResult<IEnumerable<string>> adminSelectsParticipant([FromBody] JObject data)
        {
            string pName = (string)data["pName"]; //creates a variable for the user input of pName


            DatabaseModel dbm = new DatabaseModel(); //creates a database model
            DataTable dt = dbm.adminSelectsParticipant(pName); // calls the method in the database models

            return Ok(dt); //returns any values produced as a json
        }


        // GET api/InfoSecDB/GetChallengeInfo
        //gets all challenges, no input given
        [HttpGet]
        [Route("GetChallengeInfo")]
        public ActionResult<IEnumerable<string>> GetChallengeInfo()
        {
            DatabaseModel dbm = new DatabaseModel(); // creates a database model

            DataTable dt = dbm.GetChallengeInfo(); // calls the method in the database models

            return Ok(dt); //returns any values produced as a json
        }

        // GET api/InfoSecDB/GetTeam
        // gets all participants on a team, teamName given as input 
        [HttpGet]
        [Route("GetTeam")]
        public ActionResult<IEnumerable<string>> GetTeam([FromBody] JObject data)
        {
            string teamName = (string)data["teamName"]; //variable for the user input of teamName
            DatabaseModel dbm = new DatabaseModel(); // creates a database model

            DataTable dt = dbm.GetTeam(teamName); //calls the method GetTeam in the database Model


            return Ok(dt); //returns any values produced as a json
        }

        // PUT api/InfoSecDB/adminAddsTeamMember
        //adds a participant already existing in a database to a team, participantName and teamName given as input
        [HttpPut]
        [Route("adminAddsTeamMember")]
        public ActionResult<IEnumerable<string>> adminAddsTeamMember([FromBody] JObject data)
        {
            string participantName = (string)data["participantName"]; //variable for the user input of participantName
            string teamName = (string)data["teamName"]; // variable for the user input of teamName

            DatabaseModel dbm = new DatabaseModel(); // creates a new database model
            int value = dbm.adminAddsTeamMember(participantName, teamName); // calls the database model, stored as an int

            return Ok(value); //returns a positive integer for success, a negative integer for failed
        }

        // PUT api/InfoSecDB/participantAddsAttendance
        // an exisitng participant adds attendance to themselves 
        [HttpPut]
        [Route("participantAddsAttendance")]
        public ActionResult<IEnumerable<string>> participantAddsAttendance([FromBody] JObject data)
        {
            string participantName = (string)data["participantName"]; //variable for the user input of participantName
            string meetingDate = (string)data["meetingDate"]; //variable for the user input of meetingDate

            DatabaseModel dbm = new DatabaseModel(); //creates a new database model
            int value = dbm.participantAddsAttendance(participantName, meetingDate); // calls the database model, stored as an int

            return Ok(value); //returns a positive integer for success, a negative integer for failed
        }

        // POST api/InfoSecDB/adminCreatesTeam
        // takes a teamname as an input and creates a team (sends it to the stored procedure)
        [HttpPost]
        [Route("adminCreatesTeam")]
        public ActionResult<IEnumerable<string>> adminCreatesTeam([FromBody] JObject data)
        {
            string teamName = (string)data["teamName"]; //variable for the user input of meetingDate

            DatabaseModel dbm = new DatabaseModel(); //creates a new database model
            int value = dbm.adminCreatesTeam(teamName); // calls the database model, stored as an int

            return Ok(value); //returns a positive integer for success, a negative integer for failed
        }

        // PUT api/InfoSecDB/participantAddsThemselvesToTeam
        // participant gives input of their name and a team name to add themselves to that team
        [HttpPut]
        [Route("participantAddsThemselvesToTeam")]
        public ActionResult<IEnumerable<string>> participantAddsThemselvesToTeam([FromBody] JObject data)
        {
            string participantName = (string)data["participantName"]; //variable for the user input of participantName
            string teamName = (string)data["teamName"]; //variable for the user input of teamName

            DatabaseModel dbm = new DatabaseModel(); //creates a new database model
            int value = dbm.participantAddsThemselvesToTeam(participantName, teamName); // calls the database model, stored as an int

            return Ok(value); //returns a positive integer for success, a negative integer for failed
        }


        // GET api/InfoSecDB/SelectCTF
        //.
        [HttpGet]
        [Route("SelectCTF")]
        public ActionResult<IEnumerable<string>> SelectCTF([FromBody] JObject data)
        {
            string ctfName = (string)data["ctfName"]; //variable for the user input of ctfName

            DatabaseModel dbm = new DatabaseModel(); //creates a new database model
            DataTable dt = dbm.SelectCTF(ctfName); // calls the database model

            return Ok(dt);//returns any values produced as a json
        }

        // GET api/InfoSecDB/GetTeamInfo
        [HttpGet]
        [Route("GetTeamInfo")]
        public ActionResult<IEnumerable<string>> GetTeamInfo([FromBody] JObject data)
        {
            string teamName = (string)data["teamName"]; //variable for the user input of teamName
            List<string> myP = new List<string>();  
            DatabaseModel dbm = new DatabaseModel(); //creates a new database model

            DataTable dt = dbm.CTFTeam(teamName);  // calls the database model
            return Ok(dt);//returns any values produced as a json
        }


        // GET api/InfoSecDB/SelectWhoFoundFlag
        // takes flag as input and calls the model 
        [HttpGet]
        [Route("SelectWhoFoundFlag")]
        public ActionResult<IEnumerable<string>> selectWhoFoundFlag([FromBody] JObject data)
        {
            string flag = (string)data["flag"];//variable for the user input of flag

            DatabaseModel dbm = new DatabaseModel();// creates a database model
            DataTable dt = dbm.selectWhoFoundFlag(flag); // calls the database model

            return Ok(dt);//returns any values produced as a json
        }

        // GET api/InfoSecDB/SelectNotInTeam
        // selects all participant not in a team
        [HttpGet]
        [Route("SelectNotInTeam")]
        public ActionResult<IEnumerable<string>> selectNotInTeam()
        { 
            DatabaseModel dbm = new DatabaseModel(); // creates a database model
            DataTable dt = dbm.selectNotInTeam();  // calls the database model

            return Ok(dt);//returns any values produced as a json
        }

    }

}
