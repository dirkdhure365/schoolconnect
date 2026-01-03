'use client';

import React from 'react';
import { usePermissions } from '@/hooks/usePermissions';
import type { Permission } from '@/lib/permissions/types';

export interface PermissionGateProps {
  permissions: Permission | Permission[];
  children: React.ReactNode;
  fallback?: React.ReactNode;
  requireAll?: boolean;
}

/**
 * Component that conditionally renders children based on user permissions
 */
export function PermissionGate({
  permissions,
  children,
  fallback = null,
  requireAll = false,
}: PermissionGateProps) {
  const { hasPermission, hasAllPermissions, hasAnyPermission } = usePermissions();

  const permissionArray = Array.isArray(permissions) ? permissions : [permissions];
  
  const hasAccess = requireAll
    ? hasAllPermissions(permissionArray)
    : Array.isArray(permissions)
    ? hasAnyPermission(permissionArray)
    : hasPermission(permissions);

  if (!hasAccess) {
    return <>{fallback}</>;
  }

  return <>{children}</>;
}
