# SchoolConnect Portal - Quick Start Guide

## For New Developers 👋

Welcome to the SchoolConnect Portal project! This guide will help you get started quickly.

## Prerequisites

- Node.js >= 22.12.0
- npm >= 10.9.0
- Git

## Initial Setup (5 minutes)

### 1. Navigate to the project

```bash
cd src/portalapp/app
```

### 2. Install dependencies

```bash
npm install
```

This will install all required packages including:
- React 19
- Next.js 15
- TypeScript
- Tailwind CSS 4
- Material-UI 7
- TanStack Query
- And more...

### 3. Set up environment variables

```bash
cp .env.example .env
```

Edit `.env` and update:
```env
NEXT_PUBLIC_API_URL=http://localhost:5000  # Your backend API URL
NEXTAUTH_SECRET=your-secret-key-here       # Generate a random secret
```

### 4. Start development server

```bash
npm run dev
```

The app will be available at `http://localhost:3000`

## Project Structure Overview

```
src/portalapp/app/
├── src/
│   ├── app/                    # Next.js pages
│   │   ├── (auth)/            # Login, signup, forgot password
│   │   └── (portal)/          # Protected pages (dashboard, etc.)
│   ├── components/            # React components
│   │   ├── design-system/    # Design tokens & primitives
│   │   ├── ui/               # UI components
│   │   └── auth/             # Permission gates
│   ├── api/                   # API client & hooks
│   ├── contexts/              # React contexts (auth, theme)
│   ├── hooks/                 # Custom hooks
│   ├── lib/                   # Utilities & permissions
│   └── styles/                # Global styles
├── README.md
├── ARCHITECTURE.md
├── DESIGN_SYSTEM.md
└── IMPLEMENTATION_STATUS.md
```

## Key Files to Know

### Configuration
- `package.json` - Dependencies and scripts
- `tsconfig.json` - TypeScript configuration
- `next.config.ts` - Next.js configuration
- `eslint.config.mjs` - Linting rules

### Core Files
- `src/styles/index.css` - Global styles with Tailwind
- `src/api/client.ts` - HTTP client (Ky)
- `src/contexts/auth/AuthContext.tsx` - Authentication
- `src/lib/permissions/` - Permission system

## Development Workflow

### 1. Create a new feature

```bash
# Create a new branch
git checkout -b feature/your-feature-name

# Make your changes
# Test your changes
# Commit your changes
git add .
git commit -m "Add your feature"

# Push to GitHub
git push origin feature/your-feature-name
```

### 2. Component Development

Example: Creating a new page

```tsx
// src/app/(portal)/students/page.tsx
'use client';

import { Card } from '@/components/ui/Card';
import { Button } from '@/components/design-system/primitives/Button';

export default function StudentsPage() {
  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-3xl font-bold">Students</h1>
        <Button variant="primary">Add Student</Button>
      </div>
      
      <Card>
        {/* Your content */}
      </Card>
    </div>
  );
}
```

### 3. Using the Permission System

```tsx
import { PermissionGate } from '@/components/auth/PermissionGate';
import { RoleGate } from '@/components/auth/RoleGate';

// Show button only to users with permission
<PermissionGate permissions="student:create">
  <Button>Add Student</Button>
</PermissionGate>

// Show content only to specific roles
<RoleGate roles={['super_admin', 'institute_admin']}>
  <AdminPanel />
</RoleGate>
```

### 4. Using Design System Components

```tsx
import { Button } from '@/components/design-system/primitives/Button';
import { Input } from '@/components/design-system/primitives/Input';
import { Card, CardHeader, CardBody } from '@/components/ui/Card';

// Button variants
<Button variant="primary">Primary</Button>
<Button variant="outline">Outline</Button>
<Button variant="ghost">Ghost</Button>

// Input with label and error
<Input
  label="Email"
  type="email"
  error={errors.email}
  fullWidth
/>

// Card with sections
<Card>
  <CardHeader>
    <h3>Title</h3>
  </CardHeader>
  <CardBody>
    Content
  </CardBody>
</Card>
```

## Available Scripts

```bash
# Development
npm run dev          # Start dev server (with Turbopack)

# Production
npm run build        # Build for production
npm start            # Start production server

# Code Quality
npm run lint         # Check for linting errors
npm run lint:fix     # Fix linting errors automatically
```

## Common Tasks

### Adding a new page

1. Create page file: `src/app/(portal)/your-page/page.tsx`
2. Add to navigation: Update sidebar in `src/app/(portal)/layout.tsx`
3. Add permissions: Use PermissionGate or check in component

### Creating a new component

1. Choose location:
   - Primitive: `src/components/design-system/primitives/`
   - UI: `src/components/ui/`
   - Form: `src/components/forms/`
   - Layout: `src/components/layouts/`

2. Create file with TypeScript:
```tsx
import React from 'react';

export interface YourComponentProps {
  // Define props
}

export function YourComponent({ ...props }: YourComponentProps) {
  return (
    // Your JSX
  );
}
```

### Adding API integration

1. Define types: `src/api/types/yourEntity.ts`
2. Create service: `src/api/services/yourEntityService.ts`
3. Create hooks: `src/api/hooks/useYourEntity.ts`
4. Use in components:

```tsx
import { useYourEntity } from '@/api/hooks/useYourEntity';

function YourComponent() {
  const { data, isLoading, error } = useYourEntity();
  
  if (isLoading) return <div>Loading...</div>;
  if (error) return <div>Error: {error.message}</div>;
  
  return <div>{/* Use data */}</div>;
}
```

## Code Style Guidelines

### TypeScript
- Use strict mode (already configured)
- Define proper types/interfaces
- Avoid `any` type

### Components
- Use functional components
- Use hooks instead of classes
- Export named components

### Styling
- Use Tailwind CSS classes
- Follow mobile-first approach
- Use design system tokens

### Naming
- Components: PascalCase (`UserProfile.tsx`)
- Files: kebab-case or PascalCase
- Variables: camelCase
- Constants: UPPER_SNAKE_CASE

## Helpful Resources

### Documentation
- `README.md` - Project overview and setup
- `ARCHITECTURE.md` - Technical architecture
- `DESIGN_SYSTEM.md` - Design system guide
- `IMPLEMENTATION_STATUS.md` - Implementation roadmap

### External Resources
- [Next.js Documentation](https://nextjs.org/docs)
- [React Documentation](https://react.dev)
- [Tailwind CSS](https://tailwindcss.com/docs)
- [Material-UI](https://mui.com/material-ui/)
- [TanStack Query](https://tanstack.com/query)

## Troubleshooting

### Module not found errors
```bash
npm install  # Reinstall dependencies
```

### TypeScript errors
```bash
npm run lint  # Check for errors
```

### Port already in use
```bash
# Change port in dev command
npm run dev -- -p 3001
```

### Build errors
```bash
rm -rf .next  # Clear Next.js cache
npm run build
```

## Getting Help

1. Check documentation files (README, ARCHITECTURE, DESIGN_SYSTEM)
2. Look at existing code examples
3. Reference the demo app at `src/portalapp/demo`
4. Ask the team

## Next Steps

1. ✅ Complete initial setup
2. Read `ARCHITECTURE.md` to understand the structure
3. Read `DESIGN_SYSTEM.md` to learn about components
4. Check `IMPLEMENTATION_STATUS.md` to see what needs to be built
5. Pick a task and start coding!

## Happy Coding! 🚀

The foundation is solid. Build amazing features on top of it!
