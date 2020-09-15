# Voting website backend
Here are just placeholders for now and will added when finished <br>
[Here](http://pollie-api.azurewebsites.net/index.html) is the website for backend.


### First idea
So bascially we first need the user to input the name, will provide an anonymous option. <br>
Then it leads to the home page where u can create public/private votings. <br>
THen u can send the link of the voting to anyone u want. <br><br>

When another person got the link, idealy we would ask the name first and add anonymous option etc, then they can vote and see the results.<br>
They can also comment below or add more options. 


#### Database thoughts
There will be 4 tables. USER, POLL, POLL_OPTION and VOTE. <br>
The USER table contains the USER_ID and their registed name. Not sure what to do with anonymous rn.<br>
The POLL table is like the question. It has the id, question description and user id for checking the owner. <br>
The POLL_OPTION table is the options for the poll. It has an id, text description and poll_id for the reference. <br>
The VOTE table is the votes that users casted. It has a reference to poll_option id, reference to user_id and a COUNT for counting the number (can be resloved with singalR?)<br>

