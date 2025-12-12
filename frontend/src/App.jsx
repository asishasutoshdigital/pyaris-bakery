import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import { CartProvider } from './context/CartContext';
import Home from './pages/Home';
import './App.css';

function App() {
  return (
    <Router>
      <AuthProvider>
        <CartProvider>
          <div className="app">
            <header className="app-header">
              <div className="container">
                <h1 className="logo">Paris Bakery</h1>
                <nav className="main-nav">
                  <a href="/">Home</a>
                  <a href="/products">Products</a>
                  <a href="/cart">Cart</a>
                  <a href="/login">Login</a>
                </nav>
              </div>
            </header>
            
            <main className="main-content">
              <Routes>
                <Route path="/" element={<Home />} />
                {/* Add more routes here as we create more pages */}
              </Routes>
            </main>
            
            <footer className="app-footer">
              <div className="container">
                <p>&copy; 2024 Paris Bakery. All rights reserved.</p>
              </div>
            </footer>
          </div>
        </CartProvider>
      </AuthProvider>
    </Router>
  );
}

export default App;
