## ideas and problems

### prepare match:
1. host server -> server idenyfier
2. join server (server identyfier) -> 204 / 400
3. (optional) edit server
4. start server -> 204 -> gives information everybody on server

### match:
1. ... -> question
2. ... -> end question
3. answer -> answer and !2

### what i will need:
1. many messages in one message (match: request !3)
2. to use sockets and http together
3. attach socket sessions to match

## solutions

### prepare match:
1. http (cannot be websockets)
2. http (cannot be websockets)
3. http
4. websockets

### match:
1. socket (cannot be http)
2. socket (cannot be http)
3. socket

### what i will need:
1. single messages format and queue of executing many things at once
2. single user session token
3. socket pointers or something like this

## other
there will also be needed user creation
we do not need currently email so only password needs to be used
also on start creating permissions is not required


<!-- # quiz rivals

## connect to socket

- ### host private server
**Endpoint:** `/socket/host/private`

- ### join private server

#### request fields:
- [code](#connect-code)

#### response fields:
- (none)

## value objects:
- ### connect code
- **type:** string
- **description:** A 6-digit numerical join code used to connect to the server. The code may include leading zeros (e.g., "000001")


#### response
| name | value object |
|-|-|-|
| `code` | [connect code](#connect-code) | 

```json
{
  "code": "012345"
}
```


### start socket
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
