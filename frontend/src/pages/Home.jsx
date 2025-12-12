import { useEffect, useState } from 'react';
import axios from 'axios';

function Home() {
  const [sliderItems, setSliderItems] = useState([]);

  useEffect(() => {
    loadSlider();
  }, []);

  const loadSlider = async () => {
    try {
      const response = await axios.get('http://localhost:5000/api/home/slider');
      setSliderItems(response.data);
    } catch (error) {
      console.error('Error loading slider:', error);
    }
  };

  return (
    <>
      <h1 className="home-title">Paris Bakery | Home</h1>
      
      {/* Carousel */}
      <div className="slick-center">
        {sliderItems.map((item, index) => (
          <div key={index} className="photo-slider">
            <a href={`spark?xdt=${encodeURIComponent(item.data)}`} title={item.data}>
              <img src={item.options} alt={item.data} />
            </a>
          </div>
        ))}
      </div>

      {/* Safety Banner - Desktop */}
      <div className="desktop-safety-banner-container" style={{ display: 'none' }}>
        <div className="desktop-safety-banner">
          <img title="safety" src="images/Safety/desktopsafetybanner.png" alt="Safety Banner" className="img-fluid v-align-bottom" />
        </div>
      </div>

      {/* Safety Banner - Mobile */}
      <div className="mobile-safety-banner" style={{ display: 'none' }}>
        <div className="safety-header-container">
          <div className="safety-header-heading">Your Safety is Our Priority</div>
          <div className="safety-header-text">Steps we are taking</div>
        </div>
        <div className="mobile-safety-image">
          <img src="images/Safety/safety-slider1.svg" alt="0" />
          <img src="images/Safety/safety-slider2.svg" alt="1" />
          <img src="images/Safety/safety-slider3.svg" alt="2" />
          <img src="images/Safety/safety-slider4.svg" alt="3" />
        </div>
      </div>

      {/* Featured Categories */}
      <div className="container-fluid ow-services-main">
        <div className="row">
          <div className="col-md-12">
            <div className="ow-section-title text-center">
              <div className="sep"></div>
              <h2>Our Featured Products</h2>
            </div>
          </div>
        </div>
      </div>

      {/* About Section */}
      <div className="container-fluid ow-about-main">
        <div className="container">
          <div className="row">
            <div className="col-md-6">
              <div className="ow-about-content">
                <h3>Welcome to Paris Bakery</h3>
                <p>
                  Paris Bakery is a premium bakery chain offering delicious cakes, pastries, cookies, 
                  and a wide variety of baked goods. We use only the finest ingredients to ensure 
                  quality and taste in every bite.
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default Home;
