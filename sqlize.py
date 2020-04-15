#Name,Email,UCID,Meetings Attended,Member Status,,Name,Email,UCID,Meetings Attended,Member Status
#desired format: INSERT INTO participants (name, email, UCID, meetingsAttended) VALUES ('Alexandra Tenney','alexandra.tenney@ucalgary.ca','30042397','8');
#this program will correctly put sql data into correct insert data

def sqlize(member):
    memarr = member.split(",")
    length = len(memarr) - 1 #ignoring score
    formatted = 'INSERT INTO participants (name, email, UCID, meetingsAttended) VALUES (' 
    for i in range(length):
        memarr[i] = '\"' + memarr[i] + '\"'
        if i == length-1:
            formatted = formatted + memarr[i]
        else:
            formatted = formatted + memarr[i] + ","
    formatted = formatted + ");"
    return formatted

#memstr=input('Enter string: ')



with open("participant_data.txt", "r") as a_file:
  for line in a_file:
    stripped_line = line.strip()
    print(sqlize(stripped_line))

