TDC_SaoPaulo_IOT
================

This project was originally designed for the event The Developer Conference - TDC 2014 of São Paulo.


The inteire solution is composed by 4 projects, 3 of then was developed using Visual Studio 2013 and one using Visual Studio 2012.

Tools Prerequisite
Visual Studio 2013:
iot4dx
TDC2014WP
TDCSPMobileService

Visual Studio 2012 (With NetDuino plugin Installed)
TdcIotColorChanger 

Prerequisites:
To execute the project you will need a NetDuino Hardware with the following components.


Microsoft Azure Account
The web part of this project was hosted using a Microsoft Azure Trial Account, you can use the 150,00 USD available for a free account.
If you already have a Microsoft Azure you can use it..

In Azure you will need:

1) A Website to host the iot4dx project.

2) A MobileService using C# backend to host TDCSPMobileService project.

The website can be deployed using a the free tier, for testing propouse... But in product change it to basic, at least!
The mobile service can be deployed to the free tier with small changes to the project, the original project is using two schedule jobs to send push notification messages, and the free tier only allow one, thats why I choose the a basic tier.



