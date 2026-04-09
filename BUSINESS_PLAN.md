# SchoolConnect — Business Plan

**Version**: 2.0 | **Date**: April 2026 | **Status**: Active Development

---

## 1. Executive Summary

SchoolConnect is a cloud-native, multi-tenant **School Management & Learning Platform** purpose-built for the Southern African education market. The platform provides a complete digital operating system for schools — from curriculum management and lesson delivery through to billing and collaboration — offered as a SaaS product with tiered subscription pricing.

The platform currently supports **six national/international examination boards** (ZIMSEC, CAPS/DBE, IEB, Cambridge CAIE, BEC, ECZ), covers **four countries** (Zimbabwe, South Africa, Botswana, Zambia), and is architected for rapid expansion into additional markets.

### Core Value Proposition

> **One platform to run an entire school — curriculum-aligned, multi-country, and affordable.**

SchoolConnect replaces the patchwork of spreadsheets, paper registers, WhatsApp groups, and disconnected tools that schools across Southern Africa rely on today, with a single integrated platform grounded in local curricula.

---

## 2. Problem Statement

Schools in Southern Africa face a unique set of challenges:

1. **Fragmented tools**: Schools use separate, disconnected systems (or paper) for attendance, grading, timetabling, communication, and billing.
2. **No curriculum alignment**: Generic school management tools are not designed around ZIMSEC, CAPS, BEC, or ECZ syllabi — teachers must manually map content.
3. **Poor parent-school communication**: Information flows via printed letters, noticeboard, or unstructured WhatsApp groups.
4. **Billing chaos**: Fee collection, invoicing, and subscription management are manual and error-prone, especially for multi-campus institutions.
5. **No collaboration infrastructure**: Teachers lack digital tools for co-planning lessons, coordinating departments, or managing projects.
6. **Limited data & insight**: School leadership has no real-time dashboards or analytics for decision-making.

---

## 3. Solution: The SchoolConnect Platform

### 3.1 Platform Architecture

SchoolConnect is built as a **microservices platform** using Clean Architecture, Domain-Driven Design, CQRS, and Event Sourcing. Each service is independently deployable, scalable, and maintainable.

| Layer | Technology |
|---|---|
| **Backend Services** | .NET 10.0, Minimal APIs, MediatR, FluentValidation |
| **Database** | MongoDB (per-service databases) |
| **Event Infrastructure** | Event Sourcing (MongoDB), Azure Service Bus |
| **Frontend Portal** | Next.js 15, React 19, TypeScript, MUI 7, TanStack Query, Tailwind CSS |
| **Shared Libraries** | SchoolConnect.Common (Domain, Application, Infrastructure, API) |
| **Containerization** | Docker, Docker Compose |
| **API Documentation** | Swagger/OpenAPI on every service |

### 3.2 Microservices & Feature Map

The platform consists of **eight production-ready microservices** and a **web portal**:

#### 🏫 Education System & Curriculum Service
- Hierarchical data model: Country → Education System → Assessment Board → Program → Subject → Curriculum
- **Six examination boards** with board-specific grading, phases, and assessment policies:
  - **ZIMSEC** (Zimbabwe) — O Level & A Level
  - **CAPS/DBE** (South Africa) — Foundation through FET/NSC
  - **IEB** (South Africa) — Independent schools with PAT support
  - **Cambridge CAIE** (International) — IGCSE, AS & A Level
  - **BEC** (Botswana) — Junior Certificate & BGCSE
  - **ECZ** (Zambia) — Grade 9 & School Certificate
- Curriculum discovery API, content search, learning objective search
- Practical Assessment Task (PAT) support for IT/CAT subjects
- Seed data for 60+ subjects across 10 programs in 4 countries
- Full Event Sourcing with audit trail

#### 🏛️ Institution Management Service
- Multi-campus support: Institute → Centre → Facility → Resource
- 10 domain entities (Institute, Centre, Facility, FacilityBooking, Resource, ResourceAllocation, StaffMember, StaffCentreAssignment, Team, TeamMember)
- Facility booking & resource allocation
- Institute and Centre dashboards
- 14 live API endpoints; 35+ planned

#### 📚 Lesson Delivery Service
- **Lesson Planning**: Create, approve, clone, share lesson plans with activities & resources; approval workflow (Draft → Pending → Approved/Rejected)
- **Scheduling**: Schedule lessons with recurrence, reschedule, cancel
- **Session Management**: Start/end lessons, track duration, capture artifacts (photos, audio, documents)
- **Attendance**: Record per-student attendance (Present, Absent, Late, Excused, Left Early) with reasons
- **Homework**: Assign, submit, grade with feedback; multi-attempt support; extension requests
- **Curriculum Coverage Tracking**: Planned vs. actual hours per topic; automatic progress calculation
- 11 domain entities, 22 domain events, 9 MongoDB collections

#### 📅 Calendar & Scheduling Service
- Full calendar events with recurring event support, RSVP, attendee management, reminders
- **Academic timetable management**: periods, slots, conflict detection, publishing workflow
- Substitution management and change tracking
- Multi-channel reminders (In-App, Push, Email, SMS)
- 8 entities, 17 domain events, 7 MongoDB collections, 12+ API endpoints

#### 💬 Communication Service
- **Messaging**: Direct, group, class, and broadcast conversations; attachments; read receipts; priority levels
- **Notifications**: Multi-channel delivery (In-App, Push, Email, SMS); quiet hours; digest options; 11 notification types
- **Announcements**: Institute/centre-wide; targeted audience filtering; scheduled publishing; acknowledgment tracking; analytics
- **Activity Feed**: Personalized streams with prioritization and read tracking
- 7 MongoDB collections

#### 🤝 Collaboration Service (Trello-style)
- **Workspaces**: Multi-tenant, Institute/Centre scoped, role-based membership (Owner, Admin, Member, Guest)
- **Boards**: Kanban-style with customizable backgrounds, templates, cloning, archiving, starring
- **Lists**: Positioned containers with WIP limits and color coding
- **Cards**: Assignees, labels, priorities, due dates, cover images, attachments, checklists with progress, threaded comments with @mentions, watchers, full activity audit trail
- **Educational Use Cases**: Lesson planning boards, project-based learning, curriculum development, event planning, department coordination
- 94 files, 14 entities, 30+ domain events, 10 MongoDB collections, 17 API endpoints

#### 💳 Subscription Service
- **Multi-tier plans**: Free, Basic, Standard, Premium, Enterprise
- Feature-based limits (Students, Staff, Storage, SMS, etc.)
- Trial period management with conversion tracking
- Usage tracking and limit enforcement
- Upgrade/downgrade workflows with auto-renewal
- 18 RESTful API endpoints

#### 💰 Billing Service
- **Multi-currency support** (critical for Southern African markets — ZAR, USD, BWP, ZMW)
- Billing accounts, invoices with line items, payments, payment methods, transactions, credit notes
- **Payment methods**: Card, Bank Account, Mobile Money (M-Pesa, EcoCash, etc.)
- Payment gateway integration placeholders (Stripe, PayPal, Mobile Money providers)
- Refund and credit note support
- Transaction audit trail
- 25 planned API endpoints, 6 MongoDB collections

#### 🖥️ Web Portal (Next.js)
- **Role-Based Access Control**: 7 roles (Super Admin, Institute Admin, Centre Admin, Principal, Teacher, Parent, Student) with 80+ granular permissions
- Design system foundation: tokens, primitives (Button, Input, Card), auth context, permission gates
- API client infrastructure with Ky, TanStack Query hooks, typed endpoints for all backend services
- Dashboard with stat cards, activity feed, upcoming events
- Foundation complete; module pages in development across 18 feature areas

---

## 4. Target Market

### 4.1 Primary Markets (Launch)

| Country | Schools | Opportunity |
|---|---|---|
| **Zimbabwe** | ~9,000 schools | Low digital adoption; ZIMSEC-aligned tools non-existent |
| **South Africa** | ~25,000 schools | CAPS/DBE & IEB; fragmented market with price-sensitive schools |

### 4.2 Secondary Markets (Year 2–3)

| Country | Schools | Opportunity |
|---|---|---|
| **Botswana** | ~1,000 schools | BEC curriculum; growing digital infrastructure |
| **Zambia** | ~12,000 schools | ECZ curriculum; strong mobile-money penetration |

### 4.3 Tertiary Markets (Year 3+)

- **Cambridge CAIE international schools** across Africa and beyond
- **Namibia, Mozambique, Malawi, Tanzania** — new curriculum board implementations
- Private school groups operating across multiple countries

### 4.4 Customer Segments

| Segment | Description | Pricing Sensitivity |
|---|---|---|
| **Single-campus private schools** | 1 location, 200–800 students | Medium |
| **Multi-campus school groups** | 2–20+ campuses, 1,000–10,000 students | Low (value integration) |
| **Government/public schools** | Budget-constrained, high volume | High (freemium/subsidized) |
| **International schools** | Cambridge/IEB curriculum, premium expectations | Low |

---

## 5. Business Model & Revenue

### 5.1 SaaS Subscription Tiers

Built-in to the platform via the Subscription Service:

| Tier | Monthly Price (USD) | Target | Key Limits |
|---|---|---|---|
| **Free** | $0 | Trial / small schools | 50 students, 10 staff, 1 centre, 100MB storage |
| **Basic** | $29 | Small private schools | 200 students, 25 staff, 1 centre, 1GB storage |
| **Standard** | $79 | Mid-size schools | 500 students, 50 staff, 3 centres, 5GB storage |
| **Premium** | $199 | Large schools / groups | 2,000 students, 200 staff, 10 centres, 25GB storage |
| **Enterprise** | Custom | School groups / government | Unlimited, dedicated support, SLA, custom integrations |

All tiers include:
- Curriculum-aligned content for selected boards
- Calendar, communication, collaboration
- Billing & invoicing
- Lesson planning & delivery
- Usage tracking with limit enforcement
- Trial period (14–30 days) with conversion tracking

### 5.2 Additional Revenue Streams

| Stream | Description |
|---|---|
| **SMS/Notification Credits** | Pay-as-you-go SMS bundles for parent notifications |
| **Payment Processing Fees** | Commission on fee collection via integrated payment gateways |
| **Premium Add-ons** | Advanced analytics, AI timetable generation, custom report cards |
| **Implementation & Training** | Onboarding, data migration, staff training packages |
| **API Access** | Third-party integration access for school groups with existing systems |
| **Marketplace** | Future: lesson plan templates, assessment banks, teaching resources |

### 5.3 Unit Economics (Target)

| Metric | Target |
|---|---|
| **ARPU** (Average Revenue Per User/School) | $50–$120/month |
| **CAC** (Customer Acquisition Cost) | < $150 |
| **LTV** (Lifetime Value) | > $2,000 (24+ month retention) |
| **LTV:CAC Ratio** | > 10:1 |
| **Gross Margin** | > 80% (SaaS model) |
| **Churn** | < 5% monthly |

---

## 6. Go-To-Market Strategy

### 6.1 Phase 1: Seed & Validate (Months 1–6)

**Goal**: 50 schools on the platform (10 paying)

- **Direct outreach** to private schools in Harare, Bulawayo (Zimbabwe) and Johannesburg, Cape Town (South Africa)
- **Free tier** as entry point; conversion to Basic/Standard after trial
- **Pilot partnerships** with 5–10 schools for product validation and case studies
- **Referral program**: schools that refer others get 1 month free
- Focus on **ZIMSEC and CAPS** boards first (largest addressable market)

### 6.2 Phase 2: Growth (Months 7–18)

**Goal**: 500 schools, $25K MRR

- **Content marketing**: curriculum-aligned blog posts, teacher resource guides, YouTube tutorials
- **Conference presence**: EdTech conferences, teacher union events, school administrator forums
- **School group partnerships**: Target multi-campus groups for Enterprise deals
- **Mobile money integration**: Critical for Zimbabwe (EcoCash) and Zambia (MTN Mobile Money)
- Expand to **Botswana (BEC)** and **Zambia (ECZ)** boards

### 6.3 Phase 3: Scale (Months 19–36)

**Goal**: 2,000+ schools, $150K+ MRR

- **Government partnerships**: Ministry of Education pilots in 1–2 countries
- **White-label offering** for large school groups
- **Cambridge CAIE international expansion** across Africa
- **Mobile apps** (iOS/Android) for teachers and parents
- **AI-powered features**: auto-timetabling, performance prediction, curriculum gap analysis

---

## 7. Competitive Advantage

### 7.1 Moats

| Advantage | Detail |
|---|---|
| **Curriculum-native** | Only platform with built-in ZIMSEC, CAPS, IEB, Cambridge, BEC, ECZ curricula — not a generic bolt-on |
| **Multi-country from day one** | Architected for multiple education systems, grading scales, and languages |
| **Mobile money support** | Built-in support for EcoCash, M-Pesa, and other local payment methods alongside cards |
| **Event Sourcing** | Complete audit trail and data integrity — critical for exam boards and government compliance |
| **Lesson ↔ Curriculum linkage** | Lesson plans and sessions directly track curriculum coverage — unique to SchoolConnect |
| **Microservices at the core** | Each service can scale independently; new countries/boards added without rewriting |

### 7.2 Competitive Landscape

| Competitor | Geography | Weakness vs. SchoolConnect |
|---|---|---|
| **Sycamore / Fedena** | Global (US/India) | No Southern African curriculum support; no local payment methods |
| **d6 Connect (SA)** | South Africa | Communication-only; no curriculum, lesson delivery, or billing |
| **SchoolTool / OpenSIS** | Global (Open Source) | Self-hosted, no SaaS; no African curriculum; complex setup |
| **Custom school ERP vendors** | Local per-country | Expensive, not scalable, single-tenant, poor UX |

---

## 8. Technical Roadmap

### 8.1 Completed ✅

| Milestone | Status | Details |
|---|---|---|
| Education System & Curriculum API | ✅ Complete | 6 boards, 60+ subjects, Event Sourcing, 40+ endpoints |
| Curriculum Board Implementations | ✅ Complete | ZIMSEC, CAPS, IEB, Cambridge, BEC, ECZ |
| Institution Management Service | ✅ Complete | Institutes, Centres, Facilities, Resources, Staff, Teams |
| Lesson Delivery Service | ✅ Complete | Planning, scheduling, sessions, attendance, homework, coverage |
| Calendar & Scheduling Service | ✅ Complete | Events, timetables, conflict detection, reminders |
| Communication Service | ✅ Complete | Messaging, notifications, announcements, activity feed |
| Collaboration Service | ✅ Complete | Workspaces, boards, lists, cards (Trello-style) |
| Subscription Service | ✅ Complete | Plans, trials, usage tracking, upgrade/downgrade |
| Billing Service | ✅ Complete | Accounts, invoices, payments, credit notes, multi-currency |
| Common Libraries | ✅ Complete | DDD primitives, CQRS, Event Store, Service Bus, API middleware |
| Web Portal Foundation | ✅ Complete | Next.js 15, RBAC (7 roles, 80+ permissions), design system, API client |
| Test Suite | ✅ 115 tests passing | Domain: 87.6% coverage, Application: 96.5% coverage |
| Docker Support | ✅ Complete | Dockerfile, docker-compose with MongoDB |

### 8.2 In Progress 🔧

| Milestone | Priority | Effort |
|---|---|---|
| Portal module pages (18 feature areas) | P0 | 10–12 weeks |
| Complete design system components | P0 | 1–2 weeks |
| Role-specific dashboards (6 roles) | P0 | 2 weeks |
| Billing Service MediatR handlers | P1 | 1 week |
| Full API endpoint implementation for Institution service | P1 | 2 weeks |

### 8.3 Planned 📋

| Milestone | Priority | Target |
|---|---|---|
| **Authentication & Identity Service** | P0 | Q2 2026 |
| **Enrolment & Admissions Service** | P0 | Q2 2026 |
| **Assessment & Grading Service** | P0 | Q2 2026 |
| **Student & Parent Portals** | P0 | Q3 2026 |
| **Payment Gateway Integration** (Stripe, PayPal, EcoCash, M-Pesa) | P0 | Q3 2026 |
| **SignalR Real-time** (chat, notifications, board updates) | P1 | Q3 2026 |
| **Report Card Generation** (PDF, board-specific templates) | P1 | Q3 2026 |
| **Mobile App** (React Native or Flutter) | P1 | Q4 2026 |
| **SMS Integration** (Twilio/Africa's Talking) | P1 | Q3 2026 |
| **Redis Caching Layer** | P2 | Q3 2026 |
| **AI Features** (auto-timetabling, performance prediction) | P2 | Q1 2027 |
| **iCal/Google Calendar Sync** | P2 | Q4 2026 |
| **Additional Curriculum Boards** (Namibia, Malawi, Tanzania) | P2 | 2027 |

---

## 9. Team & Hiring Plan

### Current
- Founder / Full-Stack Architect — platform design, backend services, infrastructure

### Immediate Hires (Pre-Revenue / Seed)
| Role | Why |
|---|---|
| **Frontend Developer** (Next.js/React) | Accelerate portal module development |
| **Product Designer** (UX) | User research with schools; polish UI |

### Growth Hires (Post-Revenue / Series A)
| Role | Why |
|---|---|
| **Backend Developer** (.NET) | Assessment service, Identity service, scaling |
| **Mobile Developer** | iOS/Android apps for teachers & parents |
| **DevOps/SRE** | CI/CD, monitoring, infrastructure |
| **Sales/Partnerships** | School group deals, government pilots |
| **Customer Success** | Onboarding, training, retention |

---

## 10. Funding Requirements

### Seed Round Target: $300K–$500K

| Use | Allocation |
|---|---|
| **Engineering** (salaries, 12 months) | 50% |
| **Go-to-market** (sales, marketing, pilots) | 25% |
| **Infrastructure** (cloud, services, tools) | 15% |
| **Legal & Operations** | 10% |

### Key Milestones to Unlock Series A
- 500+ schools on platform
- $25K+ MRR
- Net revenue retention > 110%
- Presence in 3+ countries
- Mobile app launched

---

## 11. Key Metrics & KPIs

| Category | Metric | Target (12 months) |
|---|---|---|
| **Growth** | Total schools | 500 |
| **Growth** | MRR | $25,000 |
| **Growth** | Paid conversion rate | > 20% |
| **Engagement** | DAU/MAU ratio | > 40% |
| **Engagement** | Lessons recorded/week/school | > 20 |
| **Engagement** | Messages sent/week/school | > 50 |
| **Retention** | Monthly churn | < 5% |
| **Retention** | NPS | > 50 |
| **Quality** | API uptime | > 99.5% |
| **Quality** | Avg. response time | < 200ms |

---

## 12. Risk Mitigation

| Risk | Likelihood | Impact | Mitigation |
|---|---|---|---|
| Low internet penetration in rural areas | High | High | Offline-first mobile app; SMS fallback for notifications |
| Schools resistant to change | Medium | High | Free tier; hands-on onboarding; teacher champions program |
| Payment collection difficulties | High | Medium | Mobile money integration; flexible billing cycles |
| Competition from global EdTech | Medium | Medium | Curriculum-native advantage; local pricing; local support |
| Regulatory/data sovereignty | Medium | Medium | In-country data hosting options; POPIA/GDPR compliance |
| Single-founder risk | High | High | Early co-founder/key hire; documented architecture |

---

## 13. Vision

**Year 1**: The default school management platform for private schools in Zimbabwe and South Africa.

**Year 3**: The leading EdTech SaaS across Southern Africa — 2,000+ schools, 5 countries, government partnerships.

**Year 5**: The pan-African school operating system — covering 15+ countries, 10,000+ schools, with AI-powered learning insights and a thriving marketplace for educational content.

---

*SchoolConnect — Built for African schools, by African builders.*
