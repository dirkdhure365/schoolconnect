'use client';

import React from 'react';
import { usePermissions } from '@/hooks/usePermissions';
import type { Role } from '@/lib/permissions/types';

export interface RoleGateProps {
  roles: Role | Role[];
  children: React.ReactNode;
  fallback?: React.ReactNode;
}

/**
 * Component that conditionally renders children based on user role
 */
export function RoleGate({ roles, children, fallback = null }: RoleGateProps) {
  const { hasRole, hasAnyRole } = usePermissions();

  const hasAccess = Array.isArray(roles) ? hasAnyRole(roles) : hasRole(roles);

  if (!hasAccess) {
    return <>{fallback}</>;
  }

  return <>{children}</>;
}
