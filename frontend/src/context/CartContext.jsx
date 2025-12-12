import React, { createContext, useContext, useState, useEffect } from 'react';

const CartContext = createContext();

export const useCart = () => {
  const context = useContext(CartContext);
  if (!context) {
    throw new Error('useCart must be used within a CartProvider');
  }
  return context;
};

export const CartProvider = ({ children }) => {
  const [cartItems, setCartItems] = useState([]);
  const [cartCount, setCartCount] = useState(0);

  useEffect(() => {
    // Load cart from localStorage on mount
    const savedCart = localStorage.getItem('cart');
    if (savedCart) {
      const items = JSON.parse(savedCart);
      setCartItems(items);
      const totalQuantity = items.reduce((sum, item) => sum + (item.quantity || 1), 0);
      setCartCount(totalQuantity);
    }
  }, []);

  const addToCart = (item) => {
    const updatedCart = [...cartItems, item];
    setCartItems(updatedCart);
    const totalQuantity = updatedCart.reduce((sum, item) => sum + (item.quantity || 1), 0);
    setCartCount(totalQuantity);
    localStorage.setItem('cart', JSON.stringify(updatedCart));
  };

  const removeFromCart = (itemId) => {
    const updatedCart = cartItems.filter((item) => item.id !== itemId);
    setCartItems(updatedCart);
    const totalQuantity = updatedCart.reduce((sum, item) => sum + (item.quantity || 1), 0);
    setCartCount(totalQuantity);
    localStorage.setItem('cart', JSON.stringify(updatedCart));
  };

  const clearCart = () => {
    setCartItems([]);
    setCartCount(0);
    localStorage.removeItem('cart');
  };

  const updateCartItem = (itemId, updates) => {
    const updatedCart = cartItems.map((item) =>
      item.id === itemId ? { ...item, ...updates } : item
    );
    setCartItems(updatedCart);
    localStorage.setItem('cart', JSON.stringify(updatedCart));
  };

  const getCartTotal = () => {
    return cartItems.reduce((total, item) => total + (item.price * item.quantity), 0);
  };

  return (
    <CartContext.Provider
      value={{
        cartItems,
        cartCount,
        addToCart,
        removeFromCart,
        clearCart,
        updateCartItem,
        getCartTotal,
      }}
    >
      {children}
    </CartContext.Provider>
  );
};
