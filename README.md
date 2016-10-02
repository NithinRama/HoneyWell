# HoneyWell
#Server
Location table consists of the router mac address to location mapping. It is assumed that the company fills this table manually.
DetailsMobile table consists of the details of the mobile, user and his last 3 locations based on the mobile numbers.
DetialsLaptop table consists of the details of laptop, user and his last 3 locations based on the router that the laptop is connected to.

# Desktop App
 This is a C# windows form application, on clicking start, a event handler for network changes will be registered which updates the database in the  server if the wifi connection to the machine changes, The GUI was built to demonstrate the working of the application, later if needed it can be removed and the app can be started on system startup and the event handler can be registered. When stop is clicked, the event handler is unregistered. This way an employee can click on start and minimize the window and start working and while leaving office he/she can stop it. This can be automated so that the application starts at system boot. This can be considered as future work

# Android app
Registers using Employee’s name and email id.
Records and sends the router mac address after the Start button is pressed.
Stops when the stop button is pressed.

# Bot Framework
This is a Bot developed using Microsoft Bot Framework (https://docs.botframework.com/en-us/) C# SDK, main objective of this bot was to simplify the lookup logic, so that employees need not install new applications on their phones or laptops to search for other employees and also the ability to search for colleagues just by asking the bot something like “Where is xyz”, “where is xyz sitting”, “Where is xyz located” etc… To add Natural language ability to the bot, we have used https://www.luis.ai/ a natural language understanding service by Microsoft which is free for now. Models to understand the above sentences was built here. Once the user asks the bot, where the person is it will understand and find the name in the text message and replies with a carousel of cards of employees with same name , each card will have image of the employee and email id and a button called “FIND” on clicking that button, the bot replies with the location of that employee’s laptop and mobile. This bot can be deployed to many channels as stated here https://docs.botframework.com/en-us/csharp/builder/sdkreference/gettingstarted.html#channels including skype, Facebook Messenger. Since, we do not store any user data in the bot, user can directly start talking to it (No registration process)

# Instructions to Run
Instructions For Server:

Unzip the server.zip file.
Open terminal/cmd to the server folder's directory.
Execute "npm install" on the terminal/cmd.
Execute "node app" on the terminal/cmd.
PS: Prerequisite - node.js has to be installed on the host machine.

Instructions to run the Android Application:

Open HONEYWELL\app\src\main\java\com\example\nithinrama\honeywell\constants.java in Android studio and change the ip address to the ip address on which your running the NodeJs server. Then connect the phone and run the code on Android studio.

PS: Prerequisite - Android Studio has to be installed on the machine.

Instructions to run the Bot:

Open the Bot application in Visual Studio 2015 and go to Handlers/Constants.cs and change Ip string to the ip of the server where you hosted our node server. Install Bot emulator from https://docs.botframework.com/en-us/csharp/builder/sdkreference/gettingstarted.html#emulator and test it!

Desktop App : 

open the project folder and goto \HoneyWell\bin\Release and run the HoneyWell.exe file to see it in action.
