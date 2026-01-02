# Subscription Service

## Overview
The Subscription Service manages subscription plans, subscriptions, trials, and usage tracking for the SchoolConnect platform.

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
SchoolConnect.Subscription.Domain/
├── Entities/          # SubscriptionPlan, Subscription, SubscriptionUsage, SubscriptionTrial, PlanFeature
├── Enums/             # PlanTier, FeatureType, SubscriptionStatus, BillingFrequency, TrialStatus
├── Events/            # Domain events for subscription lifecycle
├── Interfaces/        # Repository interfaces
└── ValueObjects/      # PlanPricing, FeatureLimit, BillingCycle

SchoolConnect.Subscription.Application/
├── Commands/          # CQRS commands for write operations
├── Queries/           # CQRS queries for read operations
├── Handlers/          # MediatR request handlers
└── DTOs/              # Data transfer objects

SchoolConnect.Subscription.Infrastructure/
├── Persistence/       # SubscriptionDbContext
├── Repositories/      # MongoDB repository implementations
└── Extensions/        # DI service registration

SchoolConnect.Subscription.Api/
└── Program.cs         # Minimal API endpoints configuration
```

## Domain Entities

### SubscriptionPlan
Represents a subscription tier/package with pricing and features.
- Properties: Name, Code, Tier, Pricing, Features, IsActive, IsPublic, TrialDays
- Methods: Create(), Update(), AddFeature(), RemoveFeature(), Activate(), Deactivate()

### Subscription
Represents an active subscription for an institute.
- Properties: InstituteId, PlanId, Status, BillingFrequency, StartDate, EndDate, AutoRenew
- Methods: Create(), Activate(), Upgrade(), Downgrade(), Cancel(), Suspend(), Expire(), Renew(), StartTrial()

### SubscriptionUsage
Tracks usage metrics per subscription period.
- Properties: SubscriptionId, InstituteId, Period, UsageMetrics
- Methods: Create(), RecordMetric(), GetMetric()

### SubscriptionTrial
Manages trial periods for subscriptions.
- Properties: SubscriptionId, InstituteId, PlanId, StartDate, EndDate, Status
- Methods: Create(), Convert(), Expire(), Cancel()

## API Endpoints

### Plans
- `GET /api/plans` - Get all active subscription plans
- `GET /api/plans/{id}` - Get plan by ID
- `POST /api/plans` - Create new plan (admin)
- `PUT /api/plans/{id}` - Update plan (admin)
- `DELETE /api/plans/{id}` - Deactivate plan (admin)

### Subscriptions
- `POST /api/subscriptions` - Create new subscription
- `GET /api/subscriptions/{id}` - Get subscription details
- `GET /api/institutes/{instituteId}/subscription` - Get institute's subscription
- `PUT /api/subscriptions/{id}/upgrade` - Upgrade to higher tier
- `PUT /api/subscriptions/{id}/downgrade` - Downgrade to lower tier
- `PUT /api/subscriptions/{id}/cancel` - Cancel subscription
- `PUT /api/subscriptions/{id}/renew` - Renew subscription

### Trials
- `POST /api/subscriptions/{id}/trial` - Start trial period
- `GET /api/subscriptions/{id}/trial` - Get trial status
- `POST /api/subscriptions/{id}/trial/convert` - Convert trial to paid

### Usage
- `GET /api/subscriptions/{id}/usage` - Get usage metrics
- `POST /api/subscriptions/{id}/usage` - Record usage

## MongoDB Collections
- `subscription_plans` - Subscription plan configurations
- `subscriptions` - Active subscriptions
- `subscription_usage` - Usage tracking records
- `subscription_trials` - Trial period management

## Configuration
Configure in `appsettings.json`:
```json
{
  "MongoDB": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "SubscriptionDb"
  }
}
```

## Running the Service
```bash
cd src/SchoolConnect.Subscription.Api
dotnet run
```

The API will be available at `https://localhost:5001` with Swagger UI at the root URL.

## Building
```bash
dotnet build SchoolConnect.Subscription.sln
```

## Domain Events
The service emits events for:
- Subscription lifecycle (Created, Activated, Upgraded, Downgraded, Cancelled, Expired, Renewed)
- Trial management (Started, Converted, Expired)
- Usage tracking (LimitApproaching, LimitExceeded)

These events can be consumed by other services for integration and notifications.
