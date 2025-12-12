import api from './api';

export const productService = {
  // Get all products or filter by subgroup/flavour
  getProducts: async (filters = {}) => {
    const { subgroup, flavour, isFlavour } = filters;
    const params = new URLSearchParams();
    
    if (subgroup) params.append('subgroup', subgroup);
    if (flavour) params.append('flavour', flavour);
    if (isFlavour) params.append('isFlavour', 'true');
    
    const response = await api.get(`/products?${params.toString()}`);
    return response.data;
  },

  // Get product by ID
  getProduct: async (id) => {
    const response = await api.get(`/products/${id}`);
    return response.data;
  },

  // Get homepage slider
  getHomepageSlider: async () => {
    const response = await api.get('/products/homepage-slider');
    return response.data;
  },
};
