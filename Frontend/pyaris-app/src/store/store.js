import { create } from 'zustand'

export const useCartStore = create((set) => ({
  cart: [],
  
  addToCart: (product, quantity = 1) => set((state) => {
    const existingItem = state.cart.find(item => item.id === product.id)
    
    if (existingItem) {
      return {
        cart: state.cart.map(item =>
          item.id === product.id
            ? { ...item, quantity: item.quantity + quantity }
            : item
        )
      }
    }
    
    return {
      cart: [...state.cart, { ...product, quantity }]
    }
  }),

  removeFromCart: (productId) => set((state) => ({
    cart: state.cart.filter(item => item.id !== productId)
  })),

  updateQuantity: (productId, quantity) => set((state) => ({
    cart: state.cart.map(item =>
      item.id === productId ? { ...item, quantity } : item
    ).filter(item => item.quantity > 0)
  })),

  clearCart: () => set({ cart: [] }),

  getCartTotal: (state) => {
    return state.cart.reduce((total, item) => {
      return total + (item.sellPrice * item.quantity)
    }, 0)
  },

  getCartItemCount: (state) => {
    return state.cart.reduce((count, item) => count + item.quantity, 0)
  }
}))

export const useUserStore = create((set) => ({
  user: null,
  isAuthenticated: false,

  setUser: (user) => set({ user, isAuthenticated: !!user }),
  clearUser: () => set({ user: null, isAuthenticated: false })
}))
