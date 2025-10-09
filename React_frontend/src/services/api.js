import axios from 'axios';

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
    // Add auth token if needed
    // const token = localStorage.getItem('token');
    // if (token) {
    //   config.headers.Authorization = `Bearer ${token}`;
    // }
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
    console.error('API Error:', error);
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
