![http://coesus.googlecode.com/svn/wiki/coesusbanner.png](http://coesus.googlecode.com/svn/wiki/coesusbanner.png)

**coesus** is a thread-safe command-line multiplatform client-server-database server engine. what a sentense. :D

  * encrypted connections to clients (SSL/TLS)
  * built-in login management using MySQL Connector (connects to a database to co-operate with web interface for example)
  * file sharing, events sharing, sync
  * scalable thanks to plugin support; each plugin can
    * access mysql database using own SQL command
    * parse incoming tcp messages
    * process its own commands after it recognizes the query syntax is plugin-specific
    * communicate with other plugins or some allowed core resources/functions
  * coded in .NET 2.0, also multiplatform
  * built-in instant client console where admin should be logged in (credentials can be set in command-line)

