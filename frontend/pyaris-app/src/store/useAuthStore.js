import { create } from 'zustand';

export const useAuthStore = create((set) => ({
  user: null,

  login: (user) => {
    localStorage.setItem('user', JSON.stringify(user));
    set({ user });
  },

  logout: () => {
    localStorage.removeItem('user');
    set({ user: null });
  },

  loadUser: () => {
    const stored = localStorage.getItem('user');
    if (stored) {
      set({ user: JSON.parse(stored) });
    }
  }
}));
