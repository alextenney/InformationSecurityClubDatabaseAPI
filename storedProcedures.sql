CREATE PROCEDURE [dbo].[adminSelectParticipant]
	@participantName VARCHAR(255)
AS
	BEGIN
		WITH MEETINGCOUNT(participantKey, meetingsAttended)  AS (
			SELECT participants_goto_meetings.participantKey, COUNT(meetingKey)
			FROM participants_goto_meetings
			GROUP BY participants_goto_meetings.participantKey, meetingKey
			HAVING  participants_goto_meetings.participantKey IN (
				SELECT participantKey
				FROM participants
				WHERE pName = @participantName)),
		ONTEAM(participantKey, teamName) AS (
			SELECT B.participantKey, C.teamname
			FROM participants AS A
			INNER JOIN
			team_members AS B
			ON A.participantKey = B.participantKey
			INNER JOIN teams AS C
			ON B.teamKey = C.teamKey
			WHERE pName = @participantName),
		MEMBERSHIP(participantKey, memberSince) AS (
			SELECT A.participantKey, B.member_since
			FROM participants AS A
			LEFT JOIN
			members AS B
			ON A.participantKey = B.participantKey
			WHERE pName = @participantName),
		SPECIALIZATIONPART(participantKey, specializationType) AS (
			SELECT A.participantKey, specializationType
			FROM participants AS A
			INNER JOIN
			specialization_type_lookup AS B
			ON A.participantKey = B.participantKey
			INNER JOIN
			lookup_specialization AS C
			ON B.typeKey = C.typeKey
			WHERE pName = @participantName)
        
		SELECT specializationType, memberSince, teamName, meetingsAttended, lifetimeScore
		FROM MEETINGCOUNT AS A
		INNER JOIN
		ONTEAM AS B
		ON A.participantKey = B.participantKey
		INNER JOIN
		MEMBERSHIP AS C
		ON B.participantKey = C.participantKey
		INNER JOIN
		SPECIALIZATIONPART AS D
		ON C.participantKey = D.participantKey
		INNER JOIN
		participants as E
		ON D.participantKey = E.participantKey;
		
	END


CREATE PROCEDURE [dbo].[participantAddsAttendance]
	@participantName VARCHAR(255),
	@meetingDate DATE
AS
	BEGIN
		INSERT INTO participants_goto_meetings
					(meetings.meetingKey,
					participants.participantKey)
		VALUES (
					(SELECT meetingKey FROM meetings WHERE meetingdate = @meetingDate),
					(SELECT participantKey FROM participants WHERE pName = @participantName)
				);
END




CCREATE PROCEDURE [dbo].[selectTeam]
    @teamName VARCHAR(255)
AS
    BEGIN
        WITH TEAMNAMES(teamname, teamKey) AS (
            SELECT teamname, teamKey
            FROM teams
            WHERE teamname = @teamName),
        PARTSONTEAMS(teamKey, participantKey) AS (
            SELECT team_members.teamKey, team_members.participantKey
            FROM team_members, TEAMNAMES
            WHERE team_members.teamKey = TEAMNAMES.teamKey),
        TEAMSPECIALIZATIONS(participantKey, specializationType) AS (
            SELECT participantKey, specializationType
            FROM specialization_type_lookup AS A
            INNER JOIN lookup_specialization AS B ON A.typeKey = B.typeKey, PARTSONTEAMS
            WHERE PARTSONTEAMS.participantKey = participantKey),
        MEMBERNAMES(pName, participantKey) AS (
            SELECT participants.pName, participants.participantKey
            FROM participants, PARTSONTEAMS
            WHERE participants.participantKey = PARTSONTEAMS.participantKey)

        SELECT teamname, pName, specializationType
        FROM TEAMNAMES AS A
        INNER JOIN
        PARTSONTEAMS AS B
        ON A.teamKey = B.teamKey
        INNER JOIN
        TEAMSPECIALIZATIONS AS C
        ON B.participantKey = C.participantKey
        INNER JOIN 
        MEMBERNAMES AS D
        ON B.participantKey = D.participantKey
        WHERE teamname = @teamName
END




CREATE PROCEDURE [dbo].[adminSelectsChallenges]
AS
	BEGIN
		SELECT cName, A.challengeKey, cPath, difficult, author, challengeType
		FROM challenges AS A
		INNER JOIN
		challenge_type_lookup AS B
		ON A.challengeKey = B.challengeKey
		INNER JOIN
		lookup_challenge AS C
		ON B.lookupKey = C.lookupKey

END



CREATE PROCEDURE [dbo].[selectCTF]
	@CTFname VARCHAR(255)
AS
	BEGIN
		SELECT host, pName
		FROM ctfs AS A
		INNER JOIN 
		teams_work_on_ctfs AS B
		ON A.CTFKey = B.CTFKey
		INNER JOIN
		participants_in_teams AS C
		ON B.teamKey = C.teamKey
		INNER JOIN
		participants AS D
		ON C.participantKey = D.participantKey
END



/* Selects all participants not on a team, and their specializations */

CREATE PROCEDURE [dbo].[selectNotInTeam]
AS
	BEGIN
		WITH NOTONTEAM(pName, participantKey, teamKey) AS (
			SELECT pName, A.participantKey, teamKey
			FROM participants AS A
			LEFT JOIN
			participants_in_teams AS B
			ON A.participantKey = B.participantKey)
		
		SELECT pName, specializationType
		FROM NOTONTEAM AS A
		INNER JOIN
		specialization_type_lookup AS B
		ON A.participantKey = B.participantKey
		INNER JOIN 
		lookup_specialization AS C
		ON B.typeKey = C.typeKey
		WHERE teamKey IS NULL
END


CREATE PROCEDURE [dbo].[adminAddsTeamMember]
	@participantName VARCHAR(255),
	@teamName VARCHAR(255)
AS
	BEGIN
		INSERT INTO team_members
					(teamKey,
					participantKey)
		VALUES (
					(SELECT teamKey FROM teams WHERE teamname = @teamName),
					(SELECT participantKey FROM participants WHERE pName = @participantName)
				);
END


CREATE PROCEDURE [dbo].[participantAddsThemselvesToTeam]
	@participantName VARCHAR(255),
	@teamName VARCHAR(255)
AS
	BEGIN
		INSERT INTO team_members
					(teamKey,
					participantKey)
		VALUES (
					(SELECT teamKey FROM teams WHERE teamname = @teamName),
					(SELECT participantKey FROM participants WHERE pName = @participantName)
				);
END




CREATE PROCEDURE [dbo].[Procedure]
	@flag VARCHAR(255)
AS
	BEGIN
		SELECT pName
		FROM participants AS A
		INNER JOIN
		particpants_solve_challenges AS B
		ON A.participantKey = B.participantKey
		INNER JOIN
		challenges AS C
		ON B.challengeKey = C.challengeKey
		WHERE flag = @flag
END




CREATE PROCEDURE [dbo].[selectCTFSTeamParticipatedIn]
	@teamName VARCHAR(255)
AS
	BEGIN
		SELECT ctfName
		FROM teams AS A
		INNER JOIN
		teams_work_on_ctfs AS B
		ON A.teamKey = B.teamKey
		INNER JOIN
		ctfs AS C
		ON B.CTFKey = C.CTFKey
		WHERE A.teamname = @teamName
END



CREATE PROCEDURE [dbo].[adminCreatesTeam]
	@teamName varchar(255)
AS
	BEGIN
		INSERT INTO teams
					(teamname,
					points)
		VALUES (
					@teamName,
					0
				);
END
