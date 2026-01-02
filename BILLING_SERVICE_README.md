# Billing Service

## Overview
The Billing Service manages billing accounts, invoices, payments, payment methods, transactions, and credit notes for the SchoolConnect platform.

## Architecture
Clean Architecture with four layers:
- **Domain**: Core business entities, value objects, and domain events
- **Application**: CQRS commands/queries with MediatR handlers
- **Infrastructure**: MongoDB repositories and database context
- **API**: Minimal API endpoints with Swagger documentation

## Technology Stack
- .NET 10.0
- MongoDB 3.5.2
- MediatR 14.0.0
- AutoMapper 16.0.0
- FluentValidation 12.1.1

## Projects Structure
```
SchoolConnect.Billing.Domain/
├── Entities/          # BillingAccount, Invoice, Payment, PaymentMethod, Transaction, CreditNote
├── Enums/             # InvoiceStatus, PaymentStatus, PaymentMethodType, TransactionType
├── Events/            # Domain events for billing lifecycle
├── Interfaces/        # Repository interfaces
└── ValueObjects/      # Money, BillingAddress, CardDetails, BankDetails, MobileMoneyDetails

SchoolConnect.Billing.Application/
├── Commands/          # CQRS commands for write operations
├── Queries/           # CQRS queries for read operations
├── Handlers/          # MediatR request handlers (implementation pending)
└── DTOs/              # Data transfer objects

SchoolConnect.Billing.Infrastructure/
├── Persistence/       # BillingDbContext
├── Repositories/      # MongoDB repository implementations
└── Extensions/        # DI service registration

SchoolConnect.Billing.Api/
└── Program.cs         # Minimal API endpoints configuration
```

## Domain Entities

### BillingAccount
Represents a billing account for an institute.
- Properties: InstituteId, BillingName, BillingEmail, Address, TaxId, VatNumber, Currency, Balance
- Methods: Create(), Update(), SetDefaultPaymentMethod(), AdjustBalance()

### Invoice
Represents an invoice with line items.
- Properties: InvoiceNumber, BillingAccountId, SubscriptionId, Status, IssueDate, DueDate, LineItems, Total
- Methods: Create(), AddLineItem(), RemoveLineItem(), Send(), MarkAsPaid(), MarkAsOverdue(), Cancel()

### Payment
Represents a payment transaction.
- Properties: InvoiceId, Amount, Status, PaymentMethod, TransactionReference
- Methods: Create(), MarkAsProcessing(), MarkAsCompleted(), MarkAsFailed(), Refund()

### PaymentMethod
Stores payment method information (Card, Bank Account, Mobile Money).
- Properties: BillingAccountId, Type, CardDetails, BankDetails, MobileMoneyDetails, IsDefault
- Methods: CreateCard(), CreateBankAccount(), CreateMobileMoney(), SetAsDefault(), Deactivate()

### Transaction
Records financial transactions against billing accounts.
- Properties: BillingAccountId, Type, Amount, BalanceBefore, BalanceAfter
- Methods: Create()

### CreditNote
Manages credit notes for refunds and adjustments.
- Properties: BillingAccountId, InvoiceId, Amount, Reason, Status
- Methods: Create(), Apply(), Expire()

## API Endpoints

### Billing Accounts
- `POST /api/billing-accounts` - Create billing account
- `GET /api/billing-accounts/{id}` - Get billing account
- `GET /api/institutes/{instituteId}/billing` - Get institute's billing account
- `PUT /api/billing-accounts/{id}` - Update billing account

### Invoices
- `GET /api/invoices` - List invoices with filters
- `POST /api/invoices` - Create invoice
- `GET /api/invoices/{id}` - Get invoice details
- `POST /api/invoices/{id}/send` - Send invoice
- `POST /api/invoices/{id}/cancel` - Cancel invoice
- `GET /api/invoices/{id}/pdf` - Download invoice PDF (placeholder)

### Payments
- `POST /api/payments` - Process payment
- `GET /api/payments/{id}` - Get payment details
- `GET /api/invoices/{invoiceId}/payments` - Get payments for invoice
- `POST /api/payments/{id}/refund` - Refund payment

### Payment Methods
- `GET /api/billing-accounts/{id}/payment-methods` - List payment methods
- `POST /api/billing-accounts/{id}/payment-methods` - Add payment method
- `DELETE /api/payment-methods/{id}` - Remove payment method
- `PUT /api/payment-methods/{id}/default` - Set as default

### Transactions
- `GET /api/billing-accounts/{id}/transactions` - Get transaction history
  
### Credit Notes
- `POST /api/credit-notes` - Issue credit note
- `GET /api/credit-notes/{id}` - Get credit note
- `POST /api/credit-notes/{id}/apply` - Apply credit note to invoice

### Dashboard
- `GET /api/billing-accounts/{id}/dashboard` - Billing dashboard/summary

## MongoDB Collections
- `billing_accounts` - Billing account information
- `invoices` - Invoice records with line items
- `payments` - Payment transactions
- `payment_methods` - Stored payment methods
- `transactions` - Financial transaction history
- `credit_notes` - Credit note records

## Configuration
Configure in `appsettings.json`:
```json
{
  "MongoDB": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "BillingDb"
  }
}
```

## Running the Service
```bash
cd src/SchoolConnect.Billing.Api
dotnet run
```

The API will be available at `https://localhost:5002` with Swagger UI at the root URL.

## Building
```bash
dotnet build SchoolConnect.Billing.sln
```

## Domain Events
The service emits events for:
- Billing account management (Created)
- Invoice lifecycle (Created, Sent, Paid, Overdue, Cancelled)
- Payment processing (Initiated, Completed, Failed, Refunded)
- Payment method management (Added, Removed)
- Credit notes (Issued, Applied)

## Integration with Subscription Service
- When a subscription is created/renewed, an invoice should be generated
- Payment completion should trigger subscription activation
- Usage limits from subscription plans affect billing

## Payment Gateway Integration
The service includes placeholders for integrating with payment gateways:
- Stripe
- PayPal
- Mobile Money providers (M-Pesa, etc.)

Payment method `ExternalId` stores the gateway reference for secure payment processing.

## Notes
- Full MediatR handlers implementation follows the same pattern as Subscription service
- All API endpoints are documented with Swagger/OpenAPI
- Supports multi-currency billing
- Transaction history provides complete audit trail
