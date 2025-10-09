import axios from 'axios';
import { getCSRFToken, defaultRateLimiter } from '../utils/security';

const API_BASE_URL = process.env.NODE_ENV === 'production' ? '/api' : 'http://localhost:5001/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Request interceptor
api.interceptors.request.use(
  (config) => {
    // Rate limiting check
    const userId = localStorage.getItem('currentUser') || 'anonymous';
    if (!defaultRateLimiter.isAllowed(userId)) {
      return Promise.reject(new Error('För många förfrågningar. Försök igen senare.'));
    }

    // Add auth token
    const token = localStorage.getItem('authToken');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    // Add CSRF token for state-changing operations
    if (['post', 'put', 'patch', 'delete'].includes(config.method.toLowerCase())) {
      const csrfToken = getCSRFToken();
      if (csrfToken) {
        config.headers['X-CSRF-Token'] = csrfToken;
      }
    }

    // Add timestamp to prevent caching of sensitive data
    config.headers['X-Requested-At'] = new Date().getTime();
    
    // Add CSRF protection header
    config.headers['X-Requested-With'] = 'XMLHttpRequest';
    
    // Add security headers
    config.headers['Cache-Control'] = 'no-store';
    config.headers['Pragma'] = 'no-cache';
    
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Response interceptor
api.interceptors.response.use(
  (response) => response,
  (error) => {
    // Handle authentication errors
    if (error.response?.status === 401) {
      // Clear authentication data
      localStorage.removeItem('authToken');
      localStorage.removeItem('currentUser');
      localStorage.removeItem('loginTime');
      localStorage.removeItem('isLoggedIn');
      
      // Redirect to login page
      window.location.href = '/login';
    }
    
    // Log errors securely (don't log sensitive data)
    console.error('API Error:', {
      status: error.response?.status,
      message: error.message,
      url: error.config?.url
    });
    
    return Promise.reject(error);
  }
);

export const customerService = {
  // Get all customers
  getAll: () => api.get('/customers'),
  
  // Get customer by ID
  getById: (id) => api.get(`/customers/${id}`),
  
  // Create new customer
  create: (customer) => api.post('/customers', customer),
  
  // Update customer
  update: (id, customer) => api.put(`/customers/${id}`, customer),
  
  // Delete customer
  delete: (id) => api.delete(`/customers/${id}`),
};

export const visitService = {
  // Get all visits
  getAll: () => api.get('/visits'),
  
  // Get visit by ID
  getById: (id) => api.get(`/visits/${id}`),
  
  // Create new visit
  create: (visit) => api.post('/visits', visit),
  
  // Update visit
  update: (id, visit) => api.put(`/visits/${id}`, visit),
  
  // Delete visit
  delete: (id) => api.delete(`/visits/${id}`),
};

export const journalService = {
  // Get all journals
  getAll: () => api.get('/journals'),
  
  // Get journal by ID
  getById: (id) => api.get(`/journals/${id}`),
  
  // Create new journal
  create: (journal) => api.post('/journals', journal),
  
  // Update journal
  update: (id, journal) => api.put(`/journals/${id}`, journal),
  
  // Delete journal
  delete: (id) => api.delete(`/journals/${id}`),
};

export const partService = {
  // Get all parts
  getAll: () => api.get('/parts'),
  
  // Get part by ID
  getById: (id) => api.get(`/parts/${id}`),
  
  // Create new part
  create: (part) => api.post('/parts', part),
  
  // Update part
  update: (id, part) => api.put(`/parts/${id}`, part),
  
  // Delete part
  delete: (id) => api.delete(`/parts/${id}`),
};

export const mechanicService = {
  // Get all mechanics
  getAll: () => api.get('/mechanics'),
  
  // Get mechanic by ID
  getById: (id) => api.get(`/mechanics/${id}`),
};

export default api;
