import React from 'react';
import { clsx } from 'clsx';

export interface CardProps {
  children: React.ReactNode;
  className?: string;
  padding?: 'none' | 'sm' | 'md' | 'lg';
  shadow?: boolean;
  hover?: boolean;
}

const paddingClasses = {
  none: '',
  sm: 'p-4',
  md: 'p-6',
  lg: 'p-8',
};

export function Card({ children, className, padding = 'md', shadow = true, hover = false }: CardProps) {
  return (
    <div
      className={clsx(
        'rounded-lg border border-neutral-200 bg-white dark:border-neutral-700 dark:bg-neutral-800',
        shadow && 'shadow-sm',
        hover && 'transition-shadow hover:shadow-md',
        paddingClasses[padding],
        className
      )}
    >
      {children}
    </div>
  );
}

export interface CardHeaderProps {
  children: React.ReactNode;
  className?: string;
}

export function CardHeader({ children, className }: CardHeaderProps) {
  return (
    <div className={clsx('border-b border-neutral-200 pb-4 dark:border-neutral-700', className)}>
      {children}
    </div>
  );
}

export interface CardBodyProps {
  children: React.ReactNode;
  className?: string;
}

export function CardBody({ children, className }: CardBodyProps) {
  return <div className={clsx('py-4', className)}>{children}</div>;
}

export interface CardFooterProps {
  children: React.ReactNode;
  className?: string;
}

export function CardFooter({ children, className }: CardFooterProps) {
  return (
    <div className={clsx('border-t border-neutral-200 pt-4 dark:border-neutral-700', className)}>
      {children}
    </div>
  );
}
