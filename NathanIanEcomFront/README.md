# NathanIanEcomFront

#Project Fails to Build on Debug
#	If the project is failing to build when you run the debug, go to View => Other Windows => Task Runner Explorer
#	once there, go package.json => Custom => Build, right-click, and make sure it's set to before build.
#	If errors continue, manually execute 'npm run build' in the cmd for the project and make sure that it runs the
#	script that matches "build" under "scripts" in package.json. You can tweak the echo statement to make sure it's
#	updated. If all of that isn't working, then the troubleshooting is beyond the scope of this little FAQ.
