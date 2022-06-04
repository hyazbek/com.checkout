# Checkout take home exercise.

com.checkout is a .net api that simulates a payment gateway.

### Live APIs on MS Azure
- [Payment API](https://comcheckoutapi.azurewebsites.net/swagger/index.html)
- [Bank API](https://comcheckoutbank.azurewebsites.net/swagger/index.html)


## Deliverables

### com.checkout.api
API that processes a payment through the gateway and ability to check for a payment details of a previously made payment through its transaction id.
### com.checkout.bank
Bank API that will be called from the payment processing api and returns the status of a payment.

## Assumptions

1- Assumed that all cards added are valid cards, no card number validation or formatting is implemented, we can create more functions with regex to validate card number format.

2- More checks and validations need to be implemented in the bank api, but for the sake of simplifying and the demo, only 2 checks are implemented:

- Any transaction with an amount > 1000 will be declined, that is to simulate status changes.

- Check if the card is expired and return the appropriate Status and Status Code.

3- Card Number Masking will display 1st and last digit of the card and replace all other digits with 'x'

## Payment Processing Flow

### Step 1: Post Payment
Note: if a card does not exist in the system, it will be created and added to the Cards Table using the CardService.

The payment request will be added into the Transactions Table with a default TransactionStatus = "Created" and Transaction Code = "C_00001"

The /ProcessTransaction controller accepts a PaymentRequest object that has the below properties
- (int) Currency ID (1-Qatari Riyal, 2-US Dollar, 3-British Pound)
- (decimal) Amount (Transaction Amount)
- (Card Object) Card with the following properties ( Card ID, Number, Name, Cvv, Expiry Month and Year)
- (GUID) Merchant ID ( 3 merchants are added to the system with auto generated guids)
```json
{
  "currencyID": 3,
  "amount": 1444,
  "card": {
    "cardNumber": "683542349756",
    "cvv": "133",
    "holderName": "Hilal Yazbek",
    "expiryMonth": "11",
    "expiryYear": "2043"
  },
  "merchantID": "[merchant guid]"
}
```
### Step 2: Call Bank API
The /BankTransaction Controller is called using HttpClient and passing a UnprocessedTransaction object that has the below properties
- (decimal) Amount
- (string) CardNumber
- (string) Name
- (string) CVV
- (string) Expiry Month
- (string) Expiry Year

and returns a BankResponse Object with the below properties
- (GUID) BankResponseID
- (TransactionStatus) TransactionStatus
- (TransactionCode) TransactionCode
``` C#
public enum TransactionStatus
    {
        Created,
        Accepted,
        Declined,
        FailedInsufficientFunds,
        FailedExpiredCard
    }
public enum TransactionCode
    {
        C_00001,    // Created 
        A_10000,    // Accepted
        SD_20000,   // Soft Decline Declined
        SD_20051,   // Soft Decline Insufficient Funds
        SD_20054    // Soft Decline Expired Card
    }
```
### Step 3: Update Transaction

Update Database with the TransactionStatus and TransactionCode
  
![Database Screenshot](https://dub01pap003files.storage.live.com/y4mzh_dbJ2GOar2qFtL-DeOLEVlyOrWw2yIsSilYHQIkwPcH50PxJ_vBHJvVJnTfoIpM5NjylLABhcB_KptlezEVup_0DTPvTJGTtIzlnfD_os5n78KSgLMU_yY5EgcOziilZ0zdrR9SZXuHe_Nrhkooba2FOyJB0N710fTVQ39GgiD6U3xLKwInjWw3oJHhS2FwDAvQzr8desj8gz3Uz9iKG0-OaYfmprU-VPWVFKd2bE?encodeFailures=1&width=916&height=117)

## Get Payment by Transaction ID
/GetTransactionByID takes a TransactionGUID and gets the transaction details in the form of a TransactionResponse Object:
- (string) Currency
- (decimal) Amount
- (string) Status
- (String) StatusCode
- (CardDetails) with all card details but with a Masked Card Number

## Deployment
- install dotnet ef cli 
```
dotnet tool install --global dotnet-ef
```
- add dotnet ef migrations
```
dotnet ef migrations add [Name]
```

- run database update command to create the database and the dummy data form the Migrations Folder.
```
dotnet ef database update

```
- BankAPI url is hardcoded in the code for now, make sure that com.checkout.bank  App URL (in the project settings is the same as the one in the app.config file of com.checkout.api)

- Make sure set startup projects in the solution to com.checkout.api and com.checkout.bank 

![Multiple Startup Projects](https://dub01pap003files.storage.live.com/y4mSiCjcfmkSBGNlRx4nZXXJyovEPm554w7aIqxdRx4ZsNp-5dTHGj1elOYMHep414-vh4Ny53BZAYS2jSqpQTajsS0HOU15SLQFB-n9F2Ag5G6kQRysE6x0rSCZativVKNohqieJjfjWZPsibXqYLKnLcXNKX6CeCRuTudnH4UU8cFfpiErs6qz_wsY0WGHES67qK75tvezYbo2uzWzTSC1IN3Vr6cyvLhayg2OvTwGLU?encodeFailures=1&width=783&height=539)

## The Extra Mile 
### Docker
- Added docker support for com.checkout.api but was not able to test it (fingers crossed :D )


### Enable App Insights
- Application Insigts are enabled on Azure App Service
![Azure App Insights](https://dub01pap003files.storage.live.com/y4mMEBKLGTGldkDKK4UUS9CsdhuT8CP7hyz1tdly_Db09P7V1FyoFHHeaf0rgymECE0AxtV_ovsC5cK7ysOgCNr3nexFoyumQGykG-y4-iW_GJnO5_MFTapkwQvnSb-hspgtgTHhFTKLIoKP_wmH0y8362QzY-8feF0dK5guxg7ikQOWyMIFmcSTde0Vt3RR1kdOZDMwBXV_kEgbsV488whVSrac8u4tGJdiPGpi-pb4V0?encodeFailures=1&width=1323&height=590)


## Articles and Tutorials used
[Clean Architecture](https://www.c-sharpcorner.com/article/implementing-a-clean-architecture-in-asp-net-core-6/)

[Generic Repository](https://www.c-sharpcorner.com/article/generic-repository-pattern-in-asp-net-core/)

[entityframeworktutorial](https://www.entityframeworktutorial.net/efcore/entity-framework-core-migration.aspx)

[Docker Article](https://softchris.github.io/pages/dotnet-dockerize.html#create-a-dockerfile)

[Azure Api Management](https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-azure-api-management-using-vs?view=aspnetcore-6.0)

[API Unit Tests](https://code-maze.com/unit-testing-aspnetcore-web-api/)