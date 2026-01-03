'use client';

import React from 'react';
import { AuthProvider } from '@/contexts/auth';
import { useAuth } from '@/contexts/auth';
import { useRouter } from 'next/navigation';

function PortalLayoutContent({ children }: { children: React.ReactNode }) {
  const { isAuthenticated, isLoading, user } = useAuth();
  const router = useRouter();

  React.useEffect(() => {
    if (!isLoading && !isAuthenticated) {
      router.push('/login');
    }
  }, [isAuthenticated, isLoading, router]);

  if (isLoading) {
    return (
      <div className="flex min-h-screen items-center justify-center">
        <div className="text-center">
          <div className="mb-4 inline-block h-8 w-8 animate-spin rounded-full border-4 border-solid border-primary-600 border-r-transparent"></div>
          <p className="text-neutral-600 dark:text-neutral-400">Loading...</p>
        </div>
      </div>
    );
  }

  if (!isAuthenticated) {
    return null;
  }

  return (
    <div className="flex min-h-screen bg-neutral-50 dark:bg-neutral-900">
      {/* Sidebar - Placeholder */}
      <aside className="w-64 border-r border-neutral-200 bg-white dark:border-neutral-700 dark:bg-neutral-800">
        <div className="flex h-16 items-center border-b border-neutral-200 px-6 dark:border-neutral-700">
          <h1 className="text-xl font-bold text-neutral-900 dark:text-neutral-50">
            SchoolConnect
          </h1>
        </div>
        <nav className="p-4">
          <div className="space-y-2">
            <a
              href="/dashboard"
              className="block rounded-lg px-3 py-2 text-sm font-medium text-neutral-700 hover:bg-neutral-100 dark:text-neutral-300 dark:hover:bg-neutral-700"
            >
              Dashboard
            </a>
            <a
              href="/institutions"
              className="block rounded-lg px-3 py-2 text-sm font-medium text-neutral-700 hover:bg-neutral-100 dark:text-neutral-300 dark:hover:bg-neutral-700"
            >
              Institutions
            </a>
            <a
              href="/centres"
              className="block rounded-lg px-3 py-2 text-sm font-medium text-neutral-700 hover:bg-neutral-100 dark:text-neutral-300 dark:hover:bg-neutral-700"
            >
              Centres
            </a>
            <a
              href="/students"
              className="block rounded-lg px-3 py-2 text-sm font-medium text-neutral-700 hover:bg-neutral-100 dark:text-neutral-300 dark:hover:bg-neutral-700"
            >
              Students
            </a>
            <a
              href="/staff"
              className="block rounded-lg px-3 py-2 text-sm font-medium text-neutral-700 hover:bg-neutral-100 dark:text-neutral-300 dark:hover:bg-neutral-700"
            >
              Staff
            </a>
          </div>
        </nav>
      </aside>

      {/* Main content */}
      <div className="flex flex-1 flex-col">
        {/* Header */}
        <header className="flex h-16 items-center justify-between border-b border-neutral-200 bg-white px-6 dark:border-neutral-700 dark:bg-neutral-800">
          <div className="flex items-center gap-4">
            <h2 className="text-lg font-semibold text-neutral-900 dark:text-neutral-50">
              Portal
            </h2>
          </div>
          <div className="flex items-center gap-4">
            <span className="text-sm text-neutral-600 dark:text-neutral-400">
              {user?.name || user?.email}
            </span>
            <span className="rounded-full bg-primary-100 px-3 py-1 text-xs font-medium text-primary-700 dark:bg-primary-900 dark:text-primary-300">
              {user?.role}
            </span>
          </div>
        </header>

        {/* Page content */}
        <main className="flex-1 p-6">{children}</main>
      </div>
    </div>
  );
}

export default function PortalLayout({ children }: { children: React.ReactNode }) {
  return (
    <AuthProvider>
      <PortalLayoutContent>{children}</PortalLayoutContent>
    </AuthProvider>
  );
}
