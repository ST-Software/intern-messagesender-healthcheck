# intern-messagesender-healthcheck

**MessageSender** is our internal application for sending emails over diverse providers (simple SMTP, MailGun, ...). This application can be used by our web projects for sending and operating emails.

Develop version of MessageSender (and your test point) is placed on [MessageSender_develop](https://10.0.1.221:9000). This link is available only in our internal network. It has currently invalid ssl certificate!

###Goal
Create application for testing and checking *MessageSender* healthcheck.
###Steps
1. Get the basics of **HTTP** protocol.
2. See the simple structure of *HealthCheckDto* (available on [application root](https://10.0.1.221:9000)).
3. Create a console application for testing Service availability.
  * Application should show deployed version of service, db connection status, service status + status of each workers
  * Shown data should be logged into file (filepath should be defined in application config)
4. ...

___
###Notes
If you will have problems with untrusted connection, you can call this code in your application for fix it:
```
ServicePointManager
    .ServerCertificateValidationCallback += 
    (sender, cert, chain, sslPolicyErrors) => true;
```
