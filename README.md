# PRHawkDemo
# If solutuon does not work, please try to update username:token string in below given file
https://github.com/tharakeswararaogoru/PRHawkDemo/blob/master/Services/Manager/HttpHandler.cs

# Required Framework
1) Visual Studio 2015

# Dependency Packages 
Dependencies will be taken care by Nuget Package manager in general when user builds the solution using Visual studio

Otherwise refer below package config files for specific vertions
https://github.com/tharakeswararaogoru/PRHawkDemo/blob/master/PRHawkDemo/packages.config
https://github.com/tharakeswararaogoru/PRHawkDemo/blob/master/PRHawkDemo.Tests/packages.config
https://github.com/tharakeswararaogoru/PRHawkDemo/blob/master/Services/packages.config
https://github.com/tharakeswararaogoru/PRHawkDemo/blob/master/Services.Tests/packages.config

# Valid routes when user runs in localhost

1) http://localhost:{portnumber}/User/{username}
2) http://localhost:{portnumber}/User/{username}?page=1&per_page=4
3) http://localhost:{portnumber}/User/{username}?per_page=4
4) http://localhost:{portnumber}/User/{username}?page=1&per_page=4&gridPage=3

