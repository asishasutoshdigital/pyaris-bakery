import axios from 'axios';

// Create axios instance with default config
const api = axios.create({
  baseURL: 'http://localhost:5045/api',
  headers: {
    'Content-Type': 'application/json',
  },
  withCredentials: true
});

// Product API
export const productAPI = {
  getAll: (params) => api.get('/product', { params }),
  getById: (id) => api.get(`/product/${id}`),
  getBySubgroup: (subgroup, params) => api.get(`/product/subgroup/${subgroup}`, { params }),
  getGroups: () => api.get('/product/groups'),
  getSubGroups: (group) => api.get('/product/subgroups', { params: { group } }),
};

// Cart API
export const cartAPI = {
  getCart: (sessionId) => api.get(`/cart/${sessionId}`),
  addToCart: (data) => api.post('/cart/add', data),
  updateCart: (data) => api.put('/cart/update', data),
  removeFromCart: (itemId) => api.delete(`/cart/remove/${itemId}`),
  clearCart: (sessionId) => api.delete(`/cart/clear/${sessionId}`),
};

// Order API
export const orderAPI = {
  create: (data) => api.post('/order', data),
  getById: (id) => api.get(`/order/${id}`),
  getByCustomer: (customerId) => api.get(`/order/customer/${customerId}`),
  updateStatus: (id, status) => api.put(`/order/${id}/status`, { status }),
};

// Payment API
export const paymentAPI = {
  initiate: (data) => api.post('/payment/phonepe/initiate', data),
  verify: (data) => api.post('/payment/phonepe/verify', data),
  refund: (data) => api.post('/payment/phonepe/refund', data),
};

// Auth API
export const authAPI = {
  login: (data) => api.post('/auth/login', data),
  register: (data) => api.post('/auth/register', data),
  forgotPassword: (data) => api.post('/auth/forgot-password', data),
  logout: () => api.post('/auth/logout'),
};

// Customer API
export const customerAPI = {
  getProfile: (userId) => api.get(`/customer/${userId}`),
  updateProfile: (userId, data) => api.put(`/customer/${userId}`, data),
};

export default api;
