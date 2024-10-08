# quiz rivals

## connect to socket
- ### host private server

request fields: none

response fields:
- [code](#connect-code)

- ### join private server

request fields:
- [code](#connect-code)

#### response
fields: none

## value objects:
- ### connect code
- **type:** string
- **description:** A 6-digit numerical join code used to connect to the server. The code may include leading zeros (e.g., "000001")




<!-- ### start socket
join private server
args:
{
  "code": server code
}

### request
modify server
{
  // rules
}

### request
start
{
  // space for rules
}

### response
question
{
  "ends": question_end_date,
  "question": question
}

### request
answer question
args:
{
  "answer": answer
}

### response
enemy answered
args:

### comment

### value object
server_code: 6 numbers

### value objects
question_end_date: date
question: text




### value object
answer: text -->
