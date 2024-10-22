### '?' usualy means optional

###
METHOD api/endpoint
Header: value

request

200 (responseCode)
responseAsStringOrAsJson

###
POST api/verification/register

{
  "Nick"
  "Password"
}

200

###
POST api/verification/register-guest

{}

200
token

###
POST api/verification/login

{
  "Nick"
  "Password(optional)"
}

###
GET api/user/profile
Authorization: Bearer {token}

-

200
{
  "Id"
  "Nick"
  "Photo"
  "Email"?
  "CreatedAt"
}

###
POST api/user/set-email
Authorization: Bearer {token}

{
  "email"
}

200
###
POST api/user/set-nick
Authorization: Bearer {token}

{
  "Nick"
}

200
###
POST api/user/set-password
Authorization: Bearer {token}

{
  "password"
}

200
###
POST api/user/set-photo
Authorization: Bearer {token}

fileContentsUsingForm

200
