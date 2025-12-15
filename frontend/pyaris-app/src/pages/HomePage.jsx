import { Link } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { productAPI } from '../services/api';

function HomePage() {
  const [groups, setGroups] = useState([]);

  useEffect(() => {
    loadGroups();
  }, []);

  const loadGroups = async () => {
    try {
      const response = await productAPI.getGroups();
      setGroups(response.data);
    } catch (error) {
      console.error('Error loading groups:', error);
    }
  };

  return (
    <div className="home-page">
      {/* Hero Section */}
      <section className="hero-section bg-primary text-white py-5">
        <div className="container text-center">
          <h1 className="display-4">Welcome to Paris Bakery</h1>
          <p className="lead">Freshly baked goods, delivered to your doorstep</p>
          <Link to="/products" className="btn btn-light btn-lg mt-3">
            Order Now
          </Link>
        </div>
      </section>

      {/* Features Section */}
      <section className="features-section py-5">
        <div className="container">
          <h2 className="text-center mb-4">Why Choose Us</h2>
          <div className="row">
            <div className="col-md-4 text-center mb-4">
              <div className="feature-box p-4">
                <h4>Fresh Daily</h4>
                <p>All products baked fresh every day</p>
              </div>
            </div>
            <div className="col-md-4 text-center mb-4">
              <div className="feature-box p-4">
                <h4>Home Delivery</h4>
                <p>Fast and reliable delivery to your home</p>
              </div>
            </div>
            <div className="col-md-4 text-center mb-4">
              <div className="feature-box p-4">
                <h4>Quality Assured</h4>
                <p>Only the finest ingredients used</p>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Categories Section */}
      <section className="categories-section py-5 bg-light">
        <div className="container">
          <h2 className="text-center mb-4">Browse by Category</h2>
          <div className="row">
            {groups.map((group, index) => (
              <div key={index} className="col-md-3 col-6 mb-4">
                <Link
                  to={`/products?group=${group}`}
                  className="category-card text-decoration-none"
                >
                  <div className="card h-100 text-center">
                    <div className="card-body">
                      <h5 className="card-title">{group}</h5>
                    </div>
                  </div>
                </Link>
              </div>
            ))}
          </div>
        </div>
      </section>
    </div>
  );
}

export default HomePage;
