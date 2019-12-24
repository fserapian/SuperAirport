# superairport
Airport web app clone using asp.net core and angular material design

To start the project:

In SuperAirport/APIAeroport (Asp.net App) run the cestrel server: dotnet run (or) dotnet watch run to watch changes in code.

In SuperAirport/SPAAeroport (Angular App):
- npm install (to get back node_modules folder)
- ng serve --open (to open up the project in the browser)

This project uses darksky api and skycons for the weather info,
and uses twilio service to receive subscription messages.

You should provide your own darksky api key in weather.service.ts in order to make the weather work .
You should provide as well your own twilio sid and authentication token as well
as your twilio number in NotificationsController.cs in order to be able to receive subscription messages.

