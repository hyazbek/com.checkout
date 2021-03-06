# Checkout take home exercise.

com.checkout is a .net api that simulates a payment gateway.

### Live APIs on MS Azure
Api was deployed to MS Azure, you can check the working versions on the below links:

- [Payment API](https://comcheckoutapi.azurewebsites.net/swagger/index.html)
- [Bank API](https://comcheckoutbank.azurewebsites.net/swagger/index.html)


## Deliverables

### com.checkout.api
API that processes a payment through the gateway and ability to check for a payment detail of a previously made payment through its transaction id.
### com.checkout.bank
Bank API that will be called from the payment processing api and returns the status of a payment.

## Assumptions

1- Assumed that all cards added are valid cards, no card number validation or formatting is implemented, we can create more functions with regex to validate card number format, validate expiry date, etc.

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

## Integration Tests

To run the integration tests, you need to restore the database to sql server, and make sure that the machine running the tests has internet access,
and can access the below url
https://comcheckoutbank.azurewebsites.net/BankTransaction/ProcessTransaction

## Process Transaction Code

The process transaction method does the below actions
1- Check if PaymentRequest is null

2- check if Amount <= 0

3- check if the merchant is valid (exists in the database by comparing MerchantID)

4- Check if currency is valid (exists in the DB)

5- Check if card exists in the database (by card number), if it does not, create a new card and add it to the cards table

6- create a transaction object and save it in transactions table with default status

7- create unproccessed transaction object and pass it to the mock bank service

8- bank service will check if user has sufficient funds (hardcoded to 1000)

9- update transaction status and code

10- return Ok message response if all the above completed without errors


i guess i could split all these checks into separate methods for better code readability


## The Extra Mile 
### Docker
- Added docker support for com.checkout.api but was not able to test it (fingers crossed :D )


### Enable App Insights
- Application Insights are enabled on Azure App Service
![Azure App Insights](https://dub01pap003files.storage.live.com/y4mi1DPfdyW89ZrpBb4WJEfxFDwqXaiZla9r5RmA8jKdC4JUOaNUggM9ZgAGqsStHlf-YlTBCYDhXbDxAhbJh4J_bVT9PpCqmzj45_Be6wnani6PSSzfKeV8Q9EuCjJe-ztDaXcfocOukeqrTAxiw8r82RXrMLfXOrv0RUCBaMABlCml8tUkdpmSgICxyMBu0a2H3SY7P33TaB_d3Yy2c1E3raEgqFx4KEU-GAZzk5toSs?encodeFailures=1&width=1101&height=613)


## Articles and Tutorials used
[Clean Architecture](https://www.c-sharpcorner.com/article/implementing-a-clean-architecture-in-asp-net-core-6/)

[Generic Repository](https://www.c-sharpcorner.com/article/generic-repository-pattern-in-asp-net-core/)

[entityframeworktutorial](https://www.entityframeworktutorial.net/efcore/entity-framework-core-migration.aspx)

[Docker Article](https://softchris.github.io/pages/dotnet-dockerize.html#create-a-dockerfile)

[Azure Api Management](https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-azure-api-management-using-vs?view=aspnetcore-6.0)

[API Unit Tests](https://code-maze.com/unit-testing-aspnetcore-web-api/)

[API Integration Tests](https://code-maze.com/aspnet-core-integration-testing/)

