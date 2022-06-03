# Checkout take home exercise.

com.checkout is a .net api that simulates a payment gateway.

## Deliverables

### com.checkout.api
API that processes a payment through the gateway and ability to check for a payment details of a previously made payment through its transaction id.
### com.checkout.bank
Bank API that will be called from the payment processing api and returns the status of a payment.

## Assumptions

## Some Details

### Step 1: Payment Process
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
### Step 3: Update Transaction in the Database with the TransactionStatus and TransactionCode
  
![alt text](https://dub01pap003files.storage.live.com/y4mzh_dbJ2GOar2qFtL-DeOLEVlyOrWw2yIsSilYHQIkwPcH50PxJ_vBHJvVJnTfoIpM5NjylLABhcB_KptlezEVup_0DTPvTJGTtIzlnfD_os5n78KSgLMU_yY5EgcOziilZ0zdrR9SZXuHe_Nrhkooba2FOyJB0N710fTVQ39GgiD6U3xLKwInjWw3oJHhS2FwDAvQzr8desj8gz3Uz9iKG0-OaYfmprU-VPWVFKd2bE?encodeFailures=1&width=916&height=117)

## Usage

```python
import foobar

# returns 'words'
foobar.pluralize('word')

# returns 'geese'
foobar.pluralize('goose')

# returns 'phenomenon'
foobar.singularize('phenomena')
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)