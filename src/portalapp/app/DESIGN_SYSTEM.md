# SchoolConnect Design System

## Overview

The SchoolConnect design system provides a comprehensive set of design tokens, components, and patterns to ensure consistency across the portal.

## Design Principles

1. **Consistency**: Uniform experience across all features
2. **Accessibility**: WCAG 2.1 AA compliant
3. **Responsiveness**: Mobile-first design approach
4. **Performance**: Optimized for fast loading
5. **Scalability**: Easy to extend and maintain

## Design Tokens

Design tokens are the foundational elements of our design system. They are defined in `src/components/design-system/tokens/`.

### Colors

#### Primary Colors
Used for main actions, links, and active states.

```typescript
primary: {
  50: '#e3f2fd',   // Lightest
  500: '#2196f3',  // Base
  700: '#1976d2',  // Hover
  900: '#0d47a1',  // Darkest
}
```

#### Secondary Colors
Used for secondary actions and accents.

```typescript
secondary: {
  50: '#f3e5f5',
  500: '#9c27b0',
  900: '#4a148c',
}
```

#### Semantic Colors

- **Success**: Green tones for positive actions
- **Warning**: Orange/yellow for warnings
- **Error**: Red tones for errors and destructive actions
- **Info**: Blue tones for informational messages

#### Neutral Colors
Used for text, backgrounds, and borders.

```typescript
neutral: {
  50: '#fafafa',   // Background
  900: '#212121',  // Text
}
```

### Typography

#### Font Families

```typescript
sans: 'Inter, system-ui, sans-serif'  // Default
mono: 'ui-monospace, monospace'       // Code
```

#### Font Sizes

```typescript
xs: '0.75rem',    // 12px
sm: '0.875rem',   // 14px
base: '1rem',     // 16px
lg: '1.125rem',   // 18px
xl: '1.25rem',    // 20px
2xl: '1.5rem',    // 24px
3xl: '1.875rem',  // 30px
// ... up to 9xl
```

#### Font Weights

```typescript
normal: 400
medium: 500
semibold: 600
bold: 700
```

#### Line Heights

```typescript
tight: 1.25
normal: 1.5
relaxed: 1.75
```

### Spacing

Consistent spacing scale based on 4px (0.25rem) increments.

```typescript
1: '0.25rem',   // 4px
2: '0.5rem',    // 8px
3: '0.75rem',   // 12px
4: '1rem',      // 16px
6: '1.5rem',    // 24px
8: '2rem',      // 32px
// ... up to 96
```

### Shadows

```typescript
sm: '0 1px 2px 0 rgb(0 0 0 / 0.05)'
DEFAULT: '0 1px 3px 0 rgb(0 0 0 / 0.1)'
lg: '0 10px 15px -3px rgb(0 0 0 / 0.1)'
```

### Border Radius

```typescript
sm: '0.125rem',   // 2px
DEFAULT: '0.25rem', // 4px
lg: '0.5rem',     // 8px
xl: '0.75rem',    // 12px
full: '9999px'    // Pill shape
```

## Primitive Components

Located in `src/components/design-system/primitives/`.

### Button

Interactive button component with multiple variants and sizes.

#### Variants
- **primary**: Main call-to-action
- **secondary**: Secondary actions
- **outline**: Subtle actions
- **ghost**: Minimal visual weight
- **danger**: Destructive actions

#### Sizes
- **sm**: Small (px-3 py-1.5)
- **md**: Medium (px-4 py-2)
- **lg**: Large (px-6 py-3)

#### Usage

```tsx
import { Button } from '@/components/design-system/primitives/Button';

<Button variant="primary" size="md">
  Click Me
</Button>

<Button variant="outline" fullWidth loading>
  Processing...
</Button>
```

#### States
- Default
- Hover
- Active
- Disabled
- Loading

### Input

Text input component with label, error, and helper text support.

#### Props
- `label`: Optional label text
- `error`: Error message
- `helperText`: Helper text below input
- `fullWidth`: Full width input

#### Usage

```tsx
import { Input } from '@/components/design-system/primitives/Input';

<Input
  label="Email"
  type="email"
  placeholder="Enter your email"
  error={errors.email}
  fullWidth
/>
```

#### States
- Default
- Focus
- Error
- Disabled

### Additional Primitives (To Be Implemented)

- **Select**: Dropdown selection
- **Checkbox**: Boolean input
- **Radio**: Single choice from options
- **Switch**: Toggle switch
- **Textarea**: Multi-line text input
- **Badge**: Status indicator
- **Avatar**: User avatar
- **Icon**: Icon wrapper
- **Spinner**: Loading indicator
- **Skeleton**: Loading placeholder

## UI Components

Located in `src/components/ui/`.

### Card

Container component for grouping content.

#### Subcomponents
- `Card`: Main container
- `CardHeader`: Header section with border
- `CardBody`: Main content area
- `CardFooter`: Footer section with border

#### Props
- `padding`: none | sm | md | lg
- `shadow`: boolean
- `hover`: boolean (hover effect)

#### Usage

```tsx
import { Card, CardHeader, CardBody, CardFooter } from '@/components/ui/Card';

<Card>
  <CardHeader>
    <h3>Card Title</h3>
  </CardHeader>
  <CardBody>
    Content goes here
  </CardBody>
  <CardFooter>
    <Button>Action</Button>
  </CardFooter>
</Card>
```

### Additional UI Components (To Be Implemented)

- **Modal**: Dialog overlay
- **Drawer**: Side panel
- **Dropdown**: Dropdown menu
- **Tabs**: Tab navigation
- **Accordion**: Collapsible sections
- **Breadcrumb**: Navigation breadcrumbs
- **Pagination**: Page navigation
- **Toast**: Notification popup
- **Alert**: Alert messages
- **Tooltip**: Hover tooltip
- **Popover**: Popover content
- **EmptyState**: Empty state placeholder
- **ErrorBoundary**: Error boundary

## Form Components

Located in `src/components/forms/`.

### To Be Implemented

- **FormField**: Form field wrapper with validation
- **FormSection**: Group related form fields
- **SearchInput**: Search with debounce
- **DatePicker**: Date/time selection
- **FileUpload**: File upload with preview
- **RichTextEditor**: TipTap-based editor
- **TagInput**: Multi-tag input

## Layout Components

Located in `src/components/layouts/`.

### To Be Implemented

- **AppShell**: Main app container
- **Sidebar**: Collapsible sidebar
- **Header**: Top header bar
- **PageContainer**: Page wrapper
- **PageHeader**: Page title and actions
- **Section**: Content section
- **Grid**: Responsive grid

## Data Display Components

Located in `src/components/data-display/`.

### To Be Implemented

- **DataTable**: Feature-rich table
- **List**: List views
- **TreeView**: Hierarchical tree
- **StatCard**: Statistics card
- **Chart**: Chart wrapper
- **Timeline**: Activity timeline
- **Calendar**: Calendar view

## Authentication Components

Located in `src/components/auth/`.

### PermissionGate

Conditionally renders children based on permissions.

#### Usage

```tsx
import { PermissionGate } from '@/components/auth/PermissionGate';

<PermissionGate permissions="student:create">
  <Button>Add Student</Button>
</PermissionGate>

<PermissionGate 
  permissions={['student:edit', 'student:delete']}
  requireAll={false}
>
  <Button>Manage Student</Button>
</PermissionGate>
```

### RoleGate

Conditionally renders children based on user role.

#### Usage

```tsx
import { RoleGate } from '@/components/auth/RoleGate';

<RoleGate roles="super_admin">
  <AdminPanel />
</RoleGate>

<RoleGate roles={['teacher', 'principal']}>
  <TeacherTools />
</RoleGate>
```

## Responsive Design

### Breakpoints

```css
xs: 0px      /* Mobile */
sm: 600px    /* Small tablet */
md: 960px    /* Tablet */
lg: 1280px   /* Desktop */
xl: 1920px   /* Large desktop */
```

### Usage

```tsx
// Tailwind classes
<div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4">
  {/* Responsive grid */}
</div>
```

## Dark Mode

Dark mode is supported throughout the design system using Tailwind's dark mode variant.

### Usage

```tsx
<div className="bg-white text-neutral-900 dark:bg-neutral-800 dark:text-neutral-50">
  Content
</div>
```

## Accessibility

### Focus Management

All interactive components include visible focus indicators:

```css
focus:outline-none 
focus:ring-2 
focus:ring-primary-500 
focus:ring-offset-2
```

### Semantic HTML

- Use proper heading hierarchy (h1, h2, h3)
- Use semantic elements (nav, main, aside, article)
- Include proper ARIA labels

### Keyboard Navigation

- All interactive elements are keyboard accessible
- Proper tab order
- Escape to close modals/drawers

### Screen Reader Support

- Meaningful alt text for images
- ARIA labels for icon buttons
- ARIA live regions for dynamic content

## Animation

### Transition Classes

```tsx
transition-colors  // Color transitions
transition-shadow  // Shadow transitions
transition-all     // All properties
```

### Animation Durations

```typescript
fast: '150ms'
normal: '200ms'
slow: '300ms'
```

## Best Practices

### Component Usage

1. **Use design system components**: Always use components from the design system
2. **Customize with props**: Use component props instead of custom CSS
3. **Follow naming conventions**: Use semantic names
4. **Document custom components**: Add JSDoc comments

### Styling

1. **Use Tailwind classes**: Prefer Tailwind over custom CSS
2. **Use design tokens**: Reference color, spacing, etc. from tokens
3. **Maintain consistency**: Use consistent spacing, colors, typography
4. **Mobile-first**: Start with mobile layout, add breakpoints

### Accessibility

1. **Test with keyboard**: Ensure keyboard navigation works
2. **Test with screen reader**: Verify screen reader compatibility
3. **Check color contrast**: Ensure sufficient contrast ratios
4. **Provide alternatives**: Text alternatives for images and icons

## Contributing

When adding new components:

1. Place in appropriate directory (primitives, ui, forms, layouts, data-display)
2. Use TypeScript with proper types
3. Follow existing patterns and conventions
4. Add prop documentation
5. Ensure accessibility
6. Test on multiple devices/browsers
7. Add to this documentation
