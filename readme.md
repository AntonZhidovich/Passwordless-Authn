# Web-application implementing FIDO2 for ASP.NET Core
This repository contains the web-application that supports passwordless authentication

## What is FIDO2? 
[FIDO2](https://fidoalliance.org/fido2/) / [WebAuthn](https://www.w3.org/TR/webauthn/) is a new open authentication standard, supported by [browsers](https://www.w3.org/Consortium/Member/List) and [many large tech companies](https://fidoalliance.org/members/) such as Microsoft, Google etc.
The main driver is to allow a user to login without passwords, creating *passwordless flows* or strong MFA for user signup/login on websites.
The standard is not limited to web applications with support coming to Active Directory and native apps.
The technology builds on public/private keys, allowing authentication to happen without sharing a secret between the user & platform.
This brings many benefits, such as easier and safer logins and makes phishing attempts extremely hard.

## Instalation library
To start, install FIDO2 component from NuGet:
```scharp
install-package Rsk.AspNetCore.Fido -pre
```
To get a license, we contacted the company [Rock Solid Knowledge](https://www.identityserver.com/) which is an official commercial partner for IdentityServer4 and Duende IdentityServer. We were given the license for a period of 3 months.
