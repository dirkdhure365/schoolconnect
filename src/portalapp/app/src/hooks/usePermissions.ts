import { useAuth } from '@/contexts/auth';
import type { Permission, Role } from '@/lib/permissions/types';
import { getRolePermissions } from '@/lib/permissions/rolePermissions';

/**
 * Hook for checking user permissions
 */
export function usePermissions() {
  const { user } = useAuth();

  const hasPermission = (permission: Permission): boolean => {
    if (!user) return false;
    
    // Check custom permissions first
    if (user.permissions && user.permissions.includes(permission)) {
      return true;
    }
    
    // Check role-based permissions
    const rolePerms = getRolePermissions(user.role);
    return rolePerms.includes(permission);
  };

  const hasAnyPermission = (permissions: Permission[]): boolean => {
    return permissions.some((permission) => hasPermission(permission));
  };

  const hasAllPermissions = (permissions: Permission[]): boolean => {
    return permissions.every((permission) => hasPermission(permission));
  };

  const hasRole = (role: Role): boolean => {
    if (!user) return false;
    return user.role === role;
  };

  const hasAnyRole = (roles: Role[]): boolean => {
    if (!user) return false;
    return roles.includes(user.role);
  };

  return {
    hasPermission,
    hasAnyPermission,
    hasAllPermissions,
    hasRole,
    hasAnyRole,
  };
}
