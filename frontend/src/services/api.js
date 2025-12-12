import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5000/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  withCredentials: true,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Authentication API
export const authAPI = {
  checkUser: (mobileNo) => api.post('/auth/check-user', JSON.stringify(mobileNo)),
  login: (credentials) => api.post('/auth/login', credentials),
  register: (userData) => api.post('/auth/register', userData),
  logout: () => api.post('/auth/logout'),
  getSession: () => api.get('/auth/session'),
};

// Home API
export const homeAPI = {
  getSlider: () => api.get('/home/slider'),
};

// Products API
export const productsAPI = {
  getByCategory: (category) => api.get(`/products/category/${encodeURIComponent(category)}`),
  getById: (id) => api.get(`/products/${id}`),
  getAll: () => api.get('/products'),
};

// Orders API
export const ordersAPI = {
  create: (orderData) => api.post('/orders', orderData),
  getByUser: () => api.get('/orders/my-orders'),
  getById: (id) => api.get(`/orders/${id}`),
};

// Cart API
export const cartAPI = {
  get: () => api.get('/cart'),
  add: (item) => api.post('/cart/add', item),
  update: (itemId, quantity) => api.put(`/cart/${itemId}`, { quantity }),
  remove: (itemId) => api.delete(`/cart/${itemId}`),
  clear: () => api.delete('/cart'),
};

// Catalog API
export const catalogAPI = {
  getProductsByCategory: (params) => api.get('/catalog/products', { params }),
};

export default api;
