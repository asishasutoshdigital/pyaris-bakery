import axios from 'axios'

const API_URL = process.env.REACT_APP_API_URL || 'http://localhost:5000/api'

const apiClient = axios.create({
  baseURL: API_URL,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json'
  }
})

// Product API
export const productAPI = {
  getAll: () => apiClient.get('/products'),
  getById: (id) => apiClient.get(`/products/${id}`),
  getByGroup: (group) => apiClient.get(`/products/group/${group}`),
  getBySubGroup: (subGroup) => apiClient.get(`/products/subgroup/${subGroup}`),
  create: (product) => apiClient.post('/products', product),
  update: (id, product) => apiClient.put(`/products/${id}`, product),
  delete: (id) => apiClient.delete(`/products/${id}`)
}

// Order API
export const orderAPI = {
  getAll: () => apiClient.get('/orders'),
  getById: (id) => apiClient.get(`/orders/${id}`),
  getByCustomer: (customerId) => apiClient.get(`/orders/customer/${customerId}`),
  create: (order) => apiClient.post('/orders', order),
  updateStatus: (id, status) => apiClient.put(`/orders/${id}/status`, { status })
}

// Payment API
export const paymentAPI = {
  initiatePhonePe: (data) => apiClient.post('/payment/phonepe/initiate', data),
  verifyPhonePe: (data) => apiClient.post('/payment/phonepe/verify', data),
  refundPhonePe: (data) => apiClient.post('/payment/phonepe/refund', data)
}

export default apiClient
