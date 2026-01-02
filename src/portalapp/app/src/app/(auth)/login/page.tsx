'use client';

import React, { useState } from 'react';
import { useRouter } from 'next/navigation';
import { Button } from '@/components/design-system/primitives/Button';
import { Input } from '@/components/design-system/primitives/Input';
import { Card } from '@/components/ui/Card';
import { useAuth } from '@/contexts/auth';

export default function LoginPage() {
  const router = useRouter();
  const { login } = useAuth();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setIsLoading(true);

    try {
      await login(email, password);
      router.push('/dashboard');
    } catch (err) {
      setError('Invalid email or password');
      console.error('Login error:', err);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="flex min-h-screen items-center justify-center bg-neutral-100 px-4 dark:bg-neutral-900">
      <Card className="w-full max-w-md" padding="lg">
        <div className="mb-8 text-center">
          <h1 className="text-3xl font-bold text-neutral-900 dark:text-neutral-50">
            SchoolConnect
          </h1>
          <p className="mt-2 text-neutral-600 dark:text-neutral-400">
            Sign in to your account
          </p>
        </div>

        <form onSubmit={handleSubmit} className="space-y-4">
          <Input
            type="email"
            label="Email"
            placeholder="Enter your email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            fullWidth
            required
          />

          <Input
            type="password"
            label="Password"
            placeholder="Enter your password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            fullWidth
            required
          />

          {error && (
            <div className="rounded-lg bg-error-50 p-3 text-sm text-error-700 dark:bg-error-900/20 dark:text-error-400">
              {error}
            </div>
          )}

          <Button type="submit" fullWidth loading={isLoading}>
            Sign In
          </Button>

          <div className="mt-4 text-center">
            <a
              href="/forgot-password"
              className="text-sm text-primary-600 hover:text-primary-700 dark:text-primary-400"
            >
              Forgot password?
            </a>
          </div>

          <div className="mt-4 text-center">
            <span className="text-sm text-neutral-600 dark:text-neutral-400">
              Don't have an account?{' '}
            </span>
            <a
              href="/signup"
              className="text-sm font-medium text-primary-600 hover:text-primary-700 dark:text-primary-400"
            >
              Sign up
            </a>
          </div>
        </form>
      </Card>
    </div>
  );
}
