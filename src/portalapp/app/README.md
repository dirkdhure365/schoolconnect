# SchoolConnect Administration Portal

A comprehensive React-based administration portal for the SchoolConnect platform, built with Next.js 15, React 19, TypeScript, and Tailwind CSS 4.

## Overview

This portal provides role-based access control for managing educational institutions, centres, students, staff, academics, lessons, assessments, and more.

## Tech Stack

- **Framework**: Next.js 15 (App Router)
- **React**: 19.1.0
- **TypeScript**: 5.8.3
- **Styling**: Tailwind CSS 4.1.4
- **UI Components**: Material-UI 7
- **State Management**: 
  - TanStack Query for server state
  - Zustand for client state
- **Forms**: React Hook Form + Zod validation
- **Authentication**: NextAuth.js
- **API Client**: Ky

## Getting Started

### Prerequisites

- Node.js >= 22.12.0
- npm >= 10.9.0

### Installation

```bash
# Install dependencies
npm install

# Copy environment variables
cp .env.example .env

# Update .env with your values
```

### Development

```bash
# Start development server
npm run dev

# Build for production
npm run build

# Start production server
npm start

# Lint code
npm run lint

# Fix linting issues
npm run lint:fix
```

The application will be available at `http://localhost:3000`.

## Project Structure

```
src/portalapp/app/
├── src/
│   ├── app/                          # Next.js app directory
│   │   ├── (auth)/                   # Authentication routes
│   │   ├── (portal)/                 # Protected portal routes
│   │   └── api/                      # API routes
│   ├── components/                   # React components
│   │   ├── design-system/            # Design system
│   │   │   ├── tokens/               # Design tokens
│   │   │   ├── primitives/           # Primitive components
│   │   │   └── patterns/             # Reusable patterns
│   │   ├── ui/                       # UI components
│   │   ├── forms/                    # Form components
│   │   ├── layouts/                  # Layout components
│   │   └── data-display/             # Data display components
│   ├── contexts/                     # React contexts
│   │   ├── auth/                     # Authentication
│   │   ├── permissions/              # Permissions
│   │   └── theme/                    # Theme
│   ├── hooks/                        # Custom React hooks
│   ├── lib/                          # Utilities and helpers
│   │   ├── utils/                    # Utility functions
│   │   ├── constants/                # Constants
│   │   └── permissions/              # Permission definitions
│   ├── api/                          # API layer
│   │   ├── hooks/                    # TanStack Query hooks
│   │   ├── services/                 # API services
│   │   └── types/                    # API types
│   ├── types/                        # TypeScript types
│   ├── styles/                       # Global styles
│   └── config/                       # App configuration
├── public/                           # Static assets
├── package.json
├── tsconfig.json
├── next.config.ts
└── tailwind.config.ts
```

## Features

### Role-Based Access Control (RBAC)

The portal implements granular permissions for seven user roles:

- **Super Admin**: Full system access
- **Institute Admin**: Institution-wide management
- **Centre Admin**: Centre-level management
- **Principal**: Academic leadership
- **Teacher**: Teaching and assessment
- **Parent**: Student monitoring
- **Student**: Learning and coursework

### Core Modules

1. **Institution Management**: Manage educational institutions
2. **Centre Management**: Manage centres within institutions
3. **Academic Management**: Classes, subjects, and timetables
4. **Student Management**: Student records and profiles
5. **Staff Management**: Staff records and assignments
6. **Lesson Management**: Lesson planning and delivery
7. **Assessment Management**: Tests, exams, and grading
8. **Calendar**: Events and scheduling
9. **Communication**: Messaging and announcements
10. **Collaboration**: Workspaces and boards
11. **Enrolment**: Application and admission management
12. **Billing**: Subscription and payment management
13. **Reports**: Analytics and insights
14. **Settings**: System configuration

## Design System

The portal includes a comprehensive design system with:

- **Design Tokens**: Colors, typography, spacing, shadows, borders, animations
- **Primitive Components**: Button, Input, Select, Checkbox, Radio, etc.
- **UI Components**: Card, Modal, Drawer, Tabs, etc.
- **Form Components**: FormField, DatePicker, FileUpload, etc.
- **Layout Components**: AppShell, Sidebar, Header, etc.
- **Data Display**: DataTable, StatCard, Chart, Calendar, etc.

See [DESIGN_SYSTEM.md](./DESIGN_SYSTEM.md) for detailed documentation.

## Architecture

The application follows a modular architecture with clear separation of concerns:

- **Presentation Layer**: React components and pages
- **State Management**: TanStack Query for server state, Zustand for UI state
- **Data Layer**: API services and hooks
- **Business Logic**: Utilities and helpers

See [ARCHITECTURE.md](./ARCHITECTURE.md) for detailed architecture documentation.

## Environment Variables

```env
NEXT_PUBLIC_API_URL=http://localhost:5000
NEXT_PUBLIC_APP_NAME=SchoolConnect Portal
NEXTAUTH_URL=http://localhost:3000
NEXTAUTH_SECRET=your-secret-key-here
```

## Development Guidelines

### Code Style

- Use TypeScript strict mode
- Follow ESLint configuration
- Use Prettier for formatting
- Write meaningful component and function names
- Add JSDoc comments for complex logic

### Component Guidelines

- Use functional components with hooks
- Implement proper TypeScript types
- Follow accessibility best practices (WCAG 2.1 AA)
- Use design system components
- Implement responsive design (mobile-first)

### State Management

- Use TanStack Query for server data
- Use Zustand for global client state
- Use React state for local component state
- Implement optimistic updates for better UX

### API Integration

- Use ky for HTTP requests
- Implement proper error handling
- Use TypeScript types for requests/responses
- Implement retry logic for failed requests

## Testing

```bash
# Run tests (when implemented)
npm test

# Run tests in watch mode
npm run test:watch

# Generate coverage report
npm run test:coverage
```

## Contributing

1. Follow the existing code style and patterns
2. Write meaningful commit messages
3. Add tests for new features
4. Update documentation as needed
5. Ensure all linting passes before committing

## License

Proprietary - All rights reserved

## Support

For support, please contact the development team or refer to the internal documentation.
