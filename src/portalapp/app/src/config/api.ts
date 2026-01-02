/**
 * API endpoint configuration
 */

export const API_ENDPOINTS = {
  // Identity Service
  auth: {
    login: '/api/auth/login',
    logout: '/api/auth/logout',
    register: '/api/auth/register',
    refreshToken: '/api/auth/refresh',
    forgotPassword: '/api/auth/forgot-password',
    resetPassword: '/api/auth/reset-password',
    me: '/api/auth/me',
  },

  // Institution Service
  institutions: {
    base: '/api/institutes',
    byId: (id: string) => `/api/institutes/${id}`,
  },
  centres: {
    base: '/api/centres',
    byId: (id: string) => `/api/centres/${id}`,
    byInstitution: (institutionId: string) => `/api/institutes/${institutionId}/centres`,
  },
  facilities: {
    base: '/api/facilities',
    byId: (id: string) => `/api/facilities/${id}`,
  },
  resources: {
    base: '/api/resources',
    byId: (id: string) => `/api/resources/${id}`,
  },
  staff: {
    base: '/api/staff',
    byId: (id: string) => `/api/staff/${id}`,
  },
  teams: {
    base: '/api/teams',
    byId: (id: string) => `/api/teams/${id}`,
  },

  // Education System Service
  classes: {
    base: '/api/classes',
    byId: (id: string) => `/api/classes/${id}`,
  },
  subjects: {
    base: '/api/subjects',
    byId: (id: string) => `/api/subjects/${id}`,
  },
  students: {
    base: '/api/students',
    byId: (id: string) => `/api/students/${id}`,
  },

  // Lesson Delivery Service
  lessonPlans: {
    base: '/api/lesson-plans',
    byId: (id: string) => `/api/lesson-plans/${id}`,
  },
  scheduledLessons: {
    base: '/api/scheduled-lessons',
    byId: (id: string) => `/api/scheduled-lessons/${id}`,
  },
  lessonSessions: {
    base: '/api/lesson-sessions',
    byId: (id: string) => `/api/lesson-sessions/${id}`,
  },
  attendance: {
    base: '/api/attendance',
    byId: (id: string) => `/api/attendance/${id}`,
  },
  homework: {
    base: '/api/homework',
    byId: (id: string) => `/api/homework/${id}`,
  },

  // Calendar Service
  events: {
    base: '/api/events',
    byId: (id: string) => `/api/events/${id}`,
  },
  calendars: {
    base: '/api/calendars',
    byId: (id: string) => `/api/calendars/${id}`,
  },

  // Communication Service
  messages: {
    base: '/api/messages',
    byId: (id: string) => `/api/messages/${id}`,
  },
  announcements: {
    base: '/api/announcements',
    byId: (id: string) => `/api/announcements/${id}`,
  },
  notifications: {
    base: '/api/notifications',
    byId: (id: string) => `/api/notifications/${id}`,
  },

  // Collaboration Service
  workspaces: {
    base: '/api/workspaces',
    byId: (id: string) => `/api/workspaces/${id}`,
  },
  boards: {
    base: '/api/boards',
    byId: (id: string) => `/api/boards/${id}`,
  },
  cards: {
    base: '/api/cards',
    byId: (id: string) => `/api/cards/${id}`,
  },

  // Enrolment Service
  applications: {
    base: '/api/applications',
    byId: (id: string) => `/api/applications/${id}`,
  },
  enrolments: {
    base: '/api/enrolments',
    byId: (id: string) => `/api/enrolments/${id}`,
  },

  // Subscription Service
  plans: {
    base: '/api/plans',
    byId: (id: string) => `/api/plans/${id}`,
  },
  subscriptions: {
    base: '/api/subscriptions',
    byId: (id: string) => `/api/subscriptions/${id}`,
  },

  // Billing Service
  billingAccounts: {
    base: '/api/billing-accounts',
    byId: (id: string) => `/api/billing-accounts/${id}`,
  },
  invoices: {
    base: '/api/invoices',
    byId: (id: string) => `/api/invoices/${id}`,
  },
  payments: {
    base: '/api/payments',
    byId: (id: string) => `/api/payments/${id}`,
  },
  paymentMethods: {
    base: '/api/payment-methods',
    byId: (id: string) => `/api/payment-methods/${id}`,
  },
} as const;
