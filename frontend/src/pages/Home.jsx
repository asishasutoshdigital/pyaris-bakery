import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { productService } from '../services/productService';
import './Home.css';

const Home = () => {
  const [sliders, setSliders] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadHomepageData();
  }, []);

  const loadHomepageData = async () => {
    try {
      const sliderData = await productService.getHomepageSlider();
      setSliders(sliderData);
    } catch (error) {
      console.error('Error loading homepage data:', error);
    } finally {
      setLoading(false);
    }
  };

  const flavours = [
    { name: 'Chocolate', query: 'Chocolate Cakes', image: '/images/ExperienceFlavours/chocolate.jpg' },
    { name: 'Fruit', query: 'Fruit Cakes', image: '/images/ExperienceFlavours/mixed_fruits.jpg' },
    { name: 'Mango', query: 'Mango Cakes', image: '/images/ExperienceFlavours/exotics_pastry.png' },
    { name: 'Black Forest', query: 'Black Forest Cakes', image: '/images/ExperienceFlavours/black_forest.jpg' },
    { name: 'Butterscotch', query: 'Butterscotch Cakes', image: '/images/ExperienceFlavours/butterscotch.jpg' },
    { name: 'Vanilla', query: 'Vanilla Cakes', image: '/images/ExperienceFlavours/Vanilla.jpg' },
    { name: 'Coffee', query: 'Coffee Cakes', image: '/images/ExperienceFlavours/coffee.png' },
    { name: 'Strawberry', query: 'Strawberry Cakes', image: '/images/ExperienceFlavours/strawberry.jpg' },
  ];

  const designerCakes = [
    { name: 'Spiderman Cakes', query: 'Spiderman Cakes', image: '/images/DesignerCakes/spiderman.jpg' },
    { name: 'Rainbow Cakes', query: 'Rainbow Cakes', image: '/images/DesignerCakes/rainbow.jpg' },
    { name: 'Baby Shower Cakes', query: 'Baby shower Cakes', image: '/images/DesignerCakes/babyshower.jpg' },
    { name: 'Mickey Mouse Cakes', query: 'Mickey Mouse Cakes', image: '/images/DesignerCakes/mickeymouse.jpg' },
    { name: 'PUBG Cakes', query: 'PUBG Cakes', image: '/images/DesignerCakes/pubg.jpg' },
    { name: 'Minion Cakes', query: 'Minion Cakes', image: '/images/DesignerCakes/minion.jpg' },
    { name: 'Chhota Bheem Cakes', query: 'Chota Bheem Cakes', image: '/images/DesignerCakes/bheem.jpg' },
    { name: 'Cartoon Cakes', query: 'Cartoon Cakes', image: '/images/DesignerCakes/cartoon.jpg' },
  ];

  if (loading) {
    return <div className="loading">Loading...</div>;
  }

  return (
    <div className="home-container">
      <h1 className="home-title">Paris Bakery | Home</h1>

      {/* Carousel/Slider */}
      <div className="slider-container">
        {sliders.map((slider, index) => (
          <div key={index} className="photo-slider">
            <Link to={`/products?query=${encodeURIComponent(slider.data)}`}>
              <img src={slider.imageUrl} alt={slider.data} />
            </Link>
          </div>
        ))}
      </div>

      {/* Experience Flavours */}
      <section className="category-section">
        <h2 className="category-heading">Experience Flavours</h2>
        <div className="category-grid">
          {flavours.map((flavour) => (
            <Link
              key={flavour.name}
              to={`/products?flavour=${encodeURIComponent(flavour.query)}&isFlavour=true`}
              className="category-card"
            >
              <img src={flavour.image} alt={flavour.name} loading="lazy" />
              <div className="category-card-title">{flavour.name}</div>
            </Link>
          ))}
        </div>
      </section>

      {/* Designer Cakes */}
      <section className="category-section">
        <h2 className="category-heading">Designer Cakes</h2>
        <div className="category-grid">
          {designerCakes.map((cake) => (
            <Link
              key={cake.name}
              to={`/products?subgroup=${encodeURIComponent(cake.query)}`}
              className="category-card"
            >
              <img src={cake.image} alt={cake.name} loading="lazy" />
              <div className="category-card-title">{cake.name}</div>
            </Link>
          ))}
        </div>
      </section>

      {/* About Us Section */}
      <section className="about-section">
        <div className="about-content">
          <div className="about-text">
            <h2>About us</h2>
            <p>
              We are exactly what you are looking for. Yes, we are an FSSAI certified online cake and Bakery Company 
              that specializes in delivering absolutely lip-smacking delicacies. Currently, we are delivering cakes in Bhuvaneshwar.
              We are not just a bakery, we are not just bakers. In fact, we are just like you, a bunch of food lovers 
              fascinated with sweet indulgence, who dreamt of creating an appetizing fairy land of divine delicacies 
              that relishes the utmost desires.
            </p>
            <Link to="/about" className="read-more-btn">Read More</Link>
          </div>
          <div className="about-image">
            <img src="/images/new-images/aboutusimage.png" alt="About Paris Bakery" />
          </div>
        </div>
      </section>
    </div>
  );
};

export default Home;
