'use client';

import React from 'react';
import { Card, CardHeader, CardBody } from '@/components/ui/Card';
import { useAuth } from '@/contexts/auth';

export default function DashboardPage() {
  const { user } = useAuth();

  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-3xl font-bold text-neutral-900 dark:text-neutral-50">
          Dashboard
        </h1>
        <p className="mt-2 text-neutral-600 dark:text-neutral-400">
          Welcome back, {user?.name || user?.email}
        </p>
      </div>

      <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-4">
        <Card>
          <CardBody>
            <div className="text-sm font-medium text-neutral-600 dark:text-neutral-400">
              Total Students
            </div>
            <div className="mt-2 text-3xl font-bold text-neutral-900 dark:text-neutral-50">
              1,234
            </div>
            <div className="mt-2 text-sm text-success-600 dark:text-success-400">
              +12% from last month
            </div>
          </CardBody>
        </Card>

        <Card>
          <CardBody>
            <div className="text-sm font-medium text-neutral-600 dark:text-neutral-400">
              Total Staff
            </div>
            <div className="mt-2 text-3xl font-bold text-neutral-900 dark:text-neutral-50">
              89
            </div>
            <div className="mt-2 text-sm text-success-600 dark:text-success-400">
              +5% from last month
            </div>
          </CardBody>
        </Card>

        <Card>
          <CardBody>
            <div className="text-sm font-medium text-neutral-600 dark:text-neutral-400">
              Active Classes
            </div>
            <div className="mt-2 text-3xl font-bold text-neutral-900 dark:text-neutral-50">
              45
            </div>
            <div className="mt-2 text-sm text-neutral-600 dark:text-neutral-400">
              Across all centres
            </div>
          </CardBody>
        </Card>

        <Card>
          <CardBody>
            <div className="text-sm font-medium text-neutral-600 dark:text-neutral-400">
              Attendance Rate
            </div>
            <div className="mt-2 text-3xl font-bold text-neutral-900 dark:text-neutral-50">
              94.5%
            </div>
            <div className="mt-2 text-sm text-success-600 dark:text-success-400">
              +2% from last week
            </div>
          </CardBody>
        </Card>
      </div>

      <div className="grid gap-6 lg:grid-cols-2">
        <Card>
          <CardHeader>
            <h3 className="text-lg font-semibold text-neutral-900 dark:text-neutral-50">
              Recent Activity
            </h3>
          </CardHeader>
          <CardBody>
            <div className="space-y-4">
              <div className="flex items-start gap-3">
                <div className="h-2 w-2 rounded-full bg-primary-600 mt-2"></div>
                <div className="flex-1">
                  <p className="text-sm font-medium text-neutral-900 dark:text-neutral-50">
                    New student enrolled
                  </p>
                  <p className="text-xs text-neutral-600 dark:text-neutral-400">
                    John Smith joined Grade 10-A
                  </p>
                  <p className="mt-1 text-xs text-neutral-500 dark:text-neutral-500">
                    2 hours ago
                  </p>
                </div>
              </div>
              <div className="flex items-start gap-3">
                <div className="h-2 w-2 rounded-full bg-success-600 mt-2"></div>
                <div className="flex-1">
                  <p className="text-sm font-medium text-neutral-900 dark:text-neutral-50">
                    Lesson plan approved
                  </p>
                  <p className="text-xs text-neutral-600 dark:text-neutral-400">
                    Mathematics lesson for Grade 11
                  </p>
                  <p className="mt-1 text-xs text-neutral-500 dark:text-neutral-500">
                    5 hours ago
                  </p>
                </div>
              </div>
              <div className="flex items-start gap-3">
                <div className="h-2 w-2 rounded-full bg-warning-600 mt-2"></div>
                <div className="flex-1">
                  <p className="text-sm font-medium text-neutral-900 dark:text-neutral-50">
                    Payment received
                  </p>
                  <p className="text-xs text-neutral-600 dark:text-neutral-400">
                    School fees for Term 2
                  </p>
                  <p className="mt-1 text-xs text-neutral-500 dark:text-neutral-500">
                    1 day ago
                  </p>
                </div>
              </div>
            </div>
          </CardBody>
        </Card>

        <Card>
          <CardHeader>
            <h3 className="text-lg font-semibold text-neutral-900 dark:text-neutral-50">
              Upcoming Events
            </h3>
          </CardHeader>
          <CardBody>
            <div className="space-y-4">
              <div className="flex items-start gap-3">
                <div className="flex h-12 w-12 flex-col items-center justify-center rounded-lg bg-primary-100 dark:bg-primary-900">
                  <div className="text-xs font-medium text-primary-700 dark:text-primary-300">
                    JAN
                  </div>
                  <div className="text-lg font-bold text-primary-700 dark:text-primary-300">
                    15
                  </div>
                </div>
                <div className="flex-1">
                  <p className="text-sm font-medium text-neutral-900 dark:text-neutral-50">
                    Parent-Teacher Meeting
                  </p>
                  <p className="text-xs text-neutral-600 dark:text-neutral-400">
                    All grades, 10:00 AM - 2:00 PM
                  </p>
                </div>
              </div>
              <div className="flex items-start gap-3">
                <div className="flex h-12 w-12 flex-col items-center justify-center rounded-lg bg-secondary-100 dark:bg-secondary-900">
                  <div className="text-xs font-medium text-secondary-700 dark:text-secondary-300">
                    JAN
                  </div>
                  <div className="text-lg font-bold text-secondary-700 dark:text-secondary-300">
                    20
                  </div>
                </div>
                <div className="flex-1">
                  <p className="text-sm font-medium text-neutral-900 dark:text-neutral-50">
                    Mid-Term Exams Begin
                  </p>
                  <p className="text-xs text-neutral-600 dark:text-neutral-400">
                    Grade 9-12, All subjects
                  </p>
                </div>
              </div>
            </div>
          </CardBody>
        </Card>
      </div>
    </div>
  );
}
