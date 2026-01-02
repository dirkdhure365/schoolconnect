import ky, { type KyInstance } from 'ky';

/**
 * Base API client using ky
 */

const API_BASE_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000';

// Function to get access token from storage
function getAccessToken(): string | null {
  if (typeof window === 'undefined') return null;
  return localStorage.getItem('accessToken');
}

// Function to set access token in storage
function setAccessToken(token: string): void {
  if (typeof window === 'undefined') return;
  localStorage.setItem('accessToken', token);
}

// Function to remove access token from storage
function removeAccessToken(): void {
  if (typeof window === 'undefined') return;
  localStorage.removeItem('accessToken');
}

/**
 * Main API client instance
 */
export const apiClient: KyInstance = ky.create({
  prefixUrl: API_BASE_URL,
  timeout: 30000,
  retry: {
    limit: 2,
    methods: ['get', 'put', 'head', 'delete', 'options', 'trace'],
    statusCodes: [408, 413, 429, 500, 502, 503, 504],
  },
  hooks: {
    beforeRequest: [
      (request) => {
        const token = getAccessToken();
        if (token) {
          request.headers.set('Authorization', `Bearer ${token}`);
        }
        request.headers.set('Content-Type', 'application/json');
      },
    ],
    afterResponse: [
      async (request, options, response) => {
        // Handle 401 Unauthorized - token refresh or logout
        if (response.status === 401) {
          // TODO: Implement token refresh logic
          // For now, just remove the token and redirect to login
          removeAccessToken();
          if (typeof window !== 'undefined') {
            window.location.href = '/login';
          }
        }

        // Handle other error responses
        if (!response.ok) {
          const error = await response.json().catch(() => ({
            message: 'An error occurred',
          }));
          throw new Error(error.message || `HTTP ${response.status}`);
        }

        return response;
      },
    ],
  },
});

/**
 * Helper functions for common HTTP methods
 */

export const api = {
  get: <T>(url: string, options?: Parameters<typeof apiClient.get>[1]) =>
    apiClient.get(url, options).json<T>(),

  post: <T>(url: string, data?: unknown, options?: Parameters<typeof apiClient.post>[1]) =>
    apiClient
      .post(url, {
        ...options,
        json: data,
      })
      .json<T>(),

  put: <T>(url: string, data?: unknown, options?: Parameters<typeof apiClient.put>[1]) =>
    apiClient
      .put(url, {
        ...options,
        json: data,
      })
      .json<T>(),

  patch: <T>(url: string, data?: unknown, options?: Parameters<typeof apiClient.patch>[1]) =>
    apiClient
      .patch(url, {
        ...options,
        json: data,
      })
      .json<T>(),

  delete: <T>(url: string, options?: Parameters<typeof apiClient.delete>[1]) =>
    apiClient.delete(url, options).json<T>(),
};

export { getAccessToken, setAccessToken, removeAccessToken };
