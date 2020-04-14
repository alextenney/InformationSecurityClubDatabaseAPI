
CREATE TABLE leaderboard (
  leaderboardKey int NOT NULL IDENTITY(1, 1),
  points int NOT NULL DEFAULT 0,
  PRIMARY KEY (leaderboardKey)
) ;


CREATE TABLE challenges (
  challengeKey int NOT NULL IDENTITY(1, 1),
  cName varchar(255) DEFAULT NULL,
  cPath varchar(255) DEFAULT NULL,
  difficult int DEFAULT NULL,
  flag varchar(255) DEFAULT NULL,
  is_found tinyint DEFAULT NULL,
  is_club_created tinyint DEFAULT NULL,
  PRIMARY KEY (challengeKey)
) ;

CREATE TABLE adds_points (
  challengeKey int DEFAULT NULL,
  leaderboardKey int DEFAULT NULL,
  PRIMARY KEY(leaderboardKey, challengeKey),
  FOREIGN KEY (challengeKey) REFERENCES challenges(challengeKey),
  FOREIGN KEY (leaderboardKey) REFERENCES leaderboard(leaderboardKey)
);


CREATE TABLE lookup_challenge (
  lookupKey int NOT NULL IDENTITY(1, 1),
  challengeType varchar(255) DEFAULT 'none',
  PRIMARY KEY (lookupKey)
) ;


CREATE TABLE challenge_type_lookup (
  lookupKey int DEFAULT NULL,
  challengeKey int DEFAULT NULL,
  PRIMARY KEY(lookupKey, challengeKey) ,
  CONSTRAINT challengeKey_fk2 FOREIGN KEY (challengeKey) REFERENCES challenges (challengeKey),
  CONSTRAINT lookupKey_fk FOREIGN KEY (lookupKey) REFERENCES lookup_challenge (lookupKey)
);

CREATE TABLE participants (
  participantKey int NOT NULL IDENTITY(1, 1),
  pName varchar(255) NOT NULL,
  email varchar(255) DEFAULT NULL,
  UCID char(8) DEFAULT NULL,
  discordID varchar(255) DEFAULT NULL,
  percentMeetingsAttended varchar(45) DEFAULT '0',
  lifetimeScore int DEFAULT '0',
  PRIMARY KEY (participantKey)
) ;

DROP TABLE IF EXISTS members;
CREATE TABLE members(
  participantKey int DEFAULT NULL,
  member_since date DEFAULT NULL,
  PRIMARY KEY (participantKey),
  CONSTRAINT participantKey FOREIGN KEY (participantKey) REFERENCES participants (participantKey)
);

CREATE TABLE executives (
  expertise varchar(255) DEFAULT NULL,
  position varchar(45) DEFAULT NULL,
  participantKey int DEFAULT NULL,
  PRIMARY KEY (participantKey),
  CONSTRAINT participantKey_fk1 FOREIGN KEY (participantKey) REFERENCES participants (participantKey) ON DELETE CASCADE ON UPDATE CASCADE
) ;

CREATE TABLE meetings (
  meetingKey int NOT NULL IDENTITY(1, 1),
  meetingdate date DEFAULT NULL,
  topic varchar(255) DEFAULT NULL,
  PRIMARY KEY (meetingKey)
) ;

CREATE TABLE participants_goto_meetings (
  participantKey int DEFAULT NULL,
  meetingKey int DEFAULT NULL,
  PRIMARY KEY (participantKey, meetingKey),
  CONSTRAINT meetingKey_fk FOREIGN KEY (meetingKey) REFERENCES meetings (meetingKey),
  CONSTRAINT participantKey_fk2 FOREIGN KEY (participantKey) REFERENCES participants (participantKey)
);

CREATE TABLE ctfs (
  CTFKey int NOT NULL IDENTITY(1, 1),
  ctfName varchar(255) DEFAULT NULL,
  difficulty int DEFAULT NULL,
  ctftype varchar(255) DEFAULT NULL,
  host varchar(255) DEFAULT NULL,
  ctfdate date DEFAULT NULL,
  PRIMARY KEY (CTFKey)
) ;

CREATE TABLE ctfs_have_challenges (
  challengeKey int DEFAULT NULL,
  CTFKey int DEFAULT NULL,
  PRIMARY KEY (challengeKey, CTFKey),
  CONSTRAINT CTFKey_fk FOREIGN KEY (CTFKey) REFERENCES ctfs (CTFKey),
  CONSTRAINT challengeKey_fk5 FOREIGN KEY (challengeKey) REFERENCES challenges (challengeKey)
);

CREATE TABLE special_events (
  specialEventKey int NOT NULL IDENTITY(1, 1),
  eventdate date NOT NULL,
  eventname varchar(255) NOT NULL,
  eventlocation varchar(255) NOT NULL,
  PRIMARY KEY (specialEventKey)
) ;


CREATE TABLE teams (
  teamKey int NOT NULL IDENTITY(1, 1),
  teamname varchar(255) DEFAULT 'none',
  points int DEFAULT NULL,
  PRIMARY KEY (teamKey)
);

CREATE TABLE team_members (
  teamKey int DEFAULT NULL,
  participantKey int DEFAULT NULL,
  PRIMARY KEY (participantKey, teamKey),
  CONSTRAINT particpantKey_fk2 FOREIGN KEY (participantKey) REFERENCES participants (participantKey),
  CONSTRAINT teamKey_fk2 FOREIGN KEY (teamKey) REFERENCES teams (teamKey)
) ;


CREATE TABLE specialization (
  participantKey int NOT NULL,
  specialization text,
  PRIMARY KEY (participantKey)
) ;

CREATE TABLE participants_goto_special_events (
  participantKey int DEFAULT NULL,
  specialEventKey int DEFAULT NULL,
  PRIMARY KEY (participantKey, specialEventKey),
  CONSTRAINT participantKey_fk FOREIGN KEY (participantKey) REFERENCES participants (participantKey) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT specialEventKey_fk FOREIGN KEY (specialEventKey) REFERENCES special_events (specialEventKey) ON DELETE CASCADE ON UPDATE CASCADE
) ;

CREATE TABLE participants_in_teams (
  participantKey int DEFAULT NULL,
  teamKey int DEFAULT NULL,
  PRIMARY KEY (participantKey, teamKey),
  CONSTRAINT participantKey_fk3 FOREIGN KEY (participantKey) REFERENCES participants (participantKey),
  CONSTRAINT teamKey_fk FOREIGN KEY (teamKey) REFERENCES teams (teamKey)
);

CREATE TABLE particpants_solve_challenges (
  participantKey int DEFAULT NULL,
  challengeKey int DEFAULT NULL,
  PRIMARY KEY (participantKey, challengeKey) /*!80000 INVISIBLE */,
  CONSTRAINT challengeKey_fk3 FOREIGN KEY (challengeKey) REFERENCES challenges(challengeKey),
  CONSTRAINT participantKey_fk4 FOREIGN KEY (participantKey) REFERENCES participants(participantKey)
);

CREATE TABLE teams_work_on_ctfs (
  teamKey int DEFAULT NULL,
  challengeKey int DEFAULT NULL,
  PRIMARY KEY (teamKey, challengeKey) /*!80000 INVISIBLE */,
  CONSTRAINT challengeKey_fk1 FOREIGN KEY (challengeKey) REFERENCES challenges (challengeKey),
  CONSTRAINT teamKey_fk1 FOREIGN KEY (teamKey) REFERENCES teams (teamKey)
) ;