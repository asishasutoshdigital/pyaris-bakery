import { create } from 'zustand';
import { persist, createJSONStorage } from 'zustand/middleware';

// Generate session ID (similar to randompin.Generate(20) in original)
const generateSessionId = () => {
  const chars = '0123456789';
  let result = '';
  for (let i = 0; i < 20; i++) {
    result += chars.charAt(Math.floor(Math.random() * chars.length));
  }
  return result;
};

// Cart Store with persistence
export const useCartStore = create(
  persist(
    (set, get) => ({
      cart: [],
      cartQuantity: 0,
      sessionId: generateSessionId(),
      
      addToCart: (product) => {
        const currentCart = get().cart;
        const existingItem = currentCart.find(item => item.id === product.id);
        
        if (existingItem) {
          set({
            cart: currentCart.map(item =>
              item.id === product.id
                ? { ...item, quantity: item.quantity + 1 }
                : item
            ),
            cartQuantity: get().cartQuantity + 1
          });
        } else {
          set({
            cart: [...currentCart, { ...product, quantity: 1 }],
            cartQuantity: get().cartQuantity + 1
          });
        }
      },
      
      removeFromCart: (productId) => {
        const currentCart = get().cart;
        const item = currentCart.find(i => i.id === productId);
        
        if (item) {
          set({
            cart: currentCart.filter(i => i.id !== productId),
            cartQuantity: get().cartQuantity - item.quantity
          });
        }
      },
      
      updateQuantity: (productId, quantity) => {
        const currentCart = get().cart;
        const oldItem = currentCart.find(i => i.id === productId);
        const oldQuantity = oldItem ? oldItem.quantity : 0;
        
        if (quantity <= 0) {
          get().removeFromCart(productId);
        } else {
          set({
            cart: currentCart.map(item =>
              item.id === productId
                ? { ...item, quantity }
                : item
            ),
            cartQuantity: get().cartQuantity - oldQuantity + quantity
          });
        }
      },
      
      clearCart: () => set({ cart: [], cartQuantity: 0 }),
      
      getTotal: () => {
        return get().cart.reduce((total, item) => {
          return total + (parseFloat(item.sellPrice || 0) * item.quantity);
        }, 0);
      }
    }),
    {
      name: 'cart-storage',
      storage: createJSONStorage(() => localStorage),
    }
  )
);

// User Store with persistence
export const useUserStore = create(
  persist(
    (set) => ({
      user: null,
      isAuthenticated: false,
      sessionId: null,
      
      login: (userData) => set({
        user: userData,
        isAuthenticated: true,
        sessionId: generateSessionId()
      }),
      
      logout: () => set({
        user: null,
        isAuthenticated: false,
        sessionId: null
      }),
      
      updateProfile: (userData) => set((state) => ({
        user: { ...state.user, ...userData }
      }))
    }),
    {
      name: 'user-storage',
      storage: createJSONStorage(() => localStorage),
    }
  )
);

// App Store (for global app state like selected pincode, etc.)
export const useAppStore = create((set) => ({
      selectedPincode: null,
      selectedGroup: '',
      selectedSubGroup: '',
      
      setSelectedPincode: (pincode) => set({ selectedPincode: pincode }),
      setSelectedGroup: (group) => set({ selectedGroup: group }),
      setSelectedSubGroup: (subGroup) => set({ selectedSubGroup: subGroup }),
}));
