import { Link } from 'react-router-dom';
import { useEffect, useState, useRef } from 'react';
import { productAPI } from '../services/api';

function HomePage() {
  const [sliderImages, setSliderImages] = useState([]);
  const sliderInitialized = useRef(false);

  useEffect(() => {
    // Initialize slick slider when component mounts
    const initSlider = () => {
      // Prevent duplicate initialization
      if (sliderInitialized.current) {
        console.log('Slider already initialized');
        return;
      }
      
      // Check if slider element exists
      const sliderElement = document.querySelector('.slick-center');
      if (!sliderElement) {
        console.log('Slider element not found');
        return;
      }
      
      // Check if jQuery and slick are available
      if (window.$ && window.$.fn && window.$.fn.slick) {
        try {
          console.log('Initializing slick slider...');
          window.$('.slick-center').slick({
            centerMode: true,
            centerPadding: '0px',
            slidesToShow: 1,
            autoplay: true,
            autoplaySpeed: 3000,
            arrows: true,
            dots: false
          });
          sliderInitialized.current = true;
          console.log('Slider initialized successfully');
        } catch (error) {
          console.error('Slick slider initialization error:', error);
        }
      } else {
        console.log('jQuery or slick not available');
      }
    };

    // Wait for DOM to be fully loaded
    if (document.readyState === 'complete') {
      // DOM already loaded, wait a bit for jQuery/slick
      setTimeout(initSlider, 100);
    } else {
      // Wait for load event
      window.addEventListener('load', initSlider);
    }

    // Cleanup function - destroy slider on unmount
    return () => {
      if (sliderInitialized.current && window.$ && window.$('.slick-center').length > 0) {
        try {
          if (window.$('.slick-center').hasClass('slick-initialized')) {
            console.log('Destroying slider...');
            window.$('.slick-center').slick('unslick');
            sliderInitialized.current = false;
          }
        } catch (error) {
          console.error('Slick slider cleanup error:', error);
        }
      }
    };
  }, []);

  return (
    <div className="home-page">
      <h1 className="home-title">Paris Bakery | Home</h1>
      
      {/* Carousel */}
      <div className="slick-center">
        <div className="photo-slider">
          <Link to="/products?xdt=Birthday%20Cakes" title="Birthday Cakes">
            <img src="/images/slider/Slider-1.png" alt="Birthday Cakes" />
          </Link>
        </div>
        <div className="photo-slider">
          <Link to="/products?xdt=Anniversary%20Cakes" title="Anniversary Cakes">
            <img src="/images/slider/Slider-2.png" alt="Anniversary Cakes" />
          </Link>
        </div>
        <div className="photo-slider">
          <Link to="/products?xdt=Designer%20Cakes" title="Designer Cakes">
            <img src="/images/slider/Slider-3.png" alt="Designer Cakes" />
          </Link>
        </div>
      </div>

      {/* Safety Banner - Hidden by default */}
      <div className="desktop-safety-banner-container" style={{display: 'none'}}>
        <div className="desktop-safety-banner">
          <img title="safety" src="/images/Safety/desktopsafetybanner.png" alt="Safety Banner" className="img-fluid v-align-bottom" />
        </div>
      </div>
      <div className="mobile-safety-banner" style={{display: 'none'}}>
        <div className="safety-header-container">
          <div className="safety-header-heading">Your Safety is Our Priority</div>
          <div className="safety-header-text">Steps we are taking</div>
        </div>
        <div className="mobile-safety-image">
          <img src="/images/Safety/safety-slider1.svg" alt="0" />
          <img src="/images/Safety/safety-slider2.svg" alt="1" />
          <img src="/images/Safety/safety-slider3.svg" alt="2" />
          <img src="/images/Safety/safety-slider4.svg" alt="3" />
        </div>
      </div>

      {/* Experience Flavours */}
      <div className="category-card-container flavour-card-container">
        <div className="category-card-heading">Experience Flavours</div>
        <div className="category-card-content">
          <Link className="category-card" to="/products?xdt=Chocolate Cakes&isFlavour=true" title="Buy Chocolate Cakes Online | Paris Bakery">
            <img src="/images/ExperienceFlavours/chocolate.jpg" alt="Delicious Chocolate Cakes Online" loading="lazy" />
            <div className="category-card-title">Chocolate</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Fruit Cakes&isFlavour=true" title="Order Fresh Fruit Cakes | Paris Bakery">
            <img src="/images/ExperienceFlavours/mixed_fruits.jpg" alt="Mixed Fruit Cakes - Freshly Baked" loading="lazy" />
            <div className="category-card-title">Fruit</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Mango Cakes&isFlavour=true" title="Mango Cakes for Summer | Paris Bakery">
            <img src="/images/ExperienceFlavours/exotics_pastry.png" alt="Exotic Mango Cakes - Order Now" loading="lazy" />
            <div className="category-card-title">Mango</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Black Forest Cakes&isFlavour=true" title="Classic Black Forest Cakes | Paris Bakery">
            <img src="/images/ExperienceFlavours/black_forest.jpg" alt="Black Forest Cakes with Cherries" loading="lazy" />
            <div className="category-card-title">Black Forest</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Butterscotch Cakes&isFlavour=true" title="Butterscotch Cakes Online Delivery">
            <img src="/images/ExperienceFlavours/butterscotch.jpg" alt="Butterscotch Cakes with Caramel Topping" loading="lazy" />
            <div className="category-card-title">Butterscotch</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Vanilla Cakes&isFlavour=true" title="Classic Vanilla Cakes | Paris Bakery">
            <img src="/images/ExperienceFlavours/Vanilla.jpg" alt="Vanilla Cakes for All Occasions" loading="lazy" />
            <div className="category-card-title">Vanilla</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Coffee Cakes&isFlavour=true" title="Buy Coffee Cakes Online | Paris Bakery">
            <img src="/images/ExperienceFlavours/coffee.png" alt="Coffee Flavored Cakes for Coffee Lovers" loading="lazy" />
            <div className="category-card-title">Coffee</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Strawberry Cakes&isFlavour=true" title="Strawberry Cakes Delivery | Paris Bakery">
            <img src="/images/ExperienceFlavours/strawberry.jpg" alt="Fresh Strawberry Cakes with Cream" loading="lazy" />
            <div className="category-card-title">Strawberry</div>
          </Link>
        </div>
      </div>

      {/* Designer Cakes */}
      <div className="category-card-container">
        <div className="category-card-heading">Designer Cakes</div>
        <div className="category-card-content">
          <Link className="category-card" to="/products?xdt=Spiderman Cakes" title="Order Spiderman Theme Cakes Online | Paris Bakery">
            <img src="/images/DesignerCakes/spiderman.jpg" alt="Spiderman Theme Birthday Cake for Kids" loading="lazy" />
            <div className="category-card-title">Spiderman Cakes</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Rainbow Cakes" title="Colorful Rainbow Cakes for Celebrations">
            <img src="/images/DesignerCakes/rainbow.jpg" alt="Vibrant Rainbow Cakes for Birthdays and Parties" loading="lazy" />
            <div className="category-card-title">Rainbow Cakes</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Baby shower Cakes" title="Baby Shower Cake Designs | Paris Bakery">
            <img src="/images/DesignerCakes/babyshower.jpg" alt="Cute Baby Shower Cakes for Boys and Girls" loading="lazy" />
            <div className="category-card-title">Baby Shower Cakes</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Mickey Mouse Cakes" title="Mickey Mouse Birthday Cakes for Kids">
            <img src="/images/DesignerCakes/mickeymouse.jpg" alt="Mickey Mouse Themed Cakes for Children" loading="lazy" />
            <div className="category-card-title">Mickey Mouse Cakes</div>
          </Link>

          <Link className="category-card" to="/products?xdt=PUBG Cakes" title="PUBG Gamer Birthday Cakes | Paris Bakery">
            <img src="/images/DesignerCakes/pubg.jpg" alt="PUBG Game Inspired Designer Cakes" loading="lazy" />
            <div className="category-card-title">PUBG Cakes</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Minion Cakes" title="Minion Themed Birthday Cakes for Kids">
            <img src="/images/DesignerCakes/minion.jpg" alt="Fun Minion Cakes - Custom Designs Available" loading="lazy" />
            <div className="category-card-title">Minion Cakes</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Chota Bheem Cakes" title="Chhota Bheem Birthday Cakes Online | Paris Bakery">
            <img src="/images/DesignerCakes/bheem.jpg" alt="Chhota Bheem Character Cakes for Children" loading="lazy" />
            <div className="category-card-title">Chhota Bheem Cakes</div>
          </Link>

          <Link className="category-card" to="/products?xdt=Cartoon Cakes" title="Cartoon Character Birthday Cakes | Paris Bakery">
            <img src="/images/DesignerCakes/cartoon.jpg" alt="Cartoon Character Themed Cakes for Kids" loading="lazy" />
            <div className="category-card-title">Cartoon Cakes</div>
          </Link>
        </div>
      </div>

      {/* Something Special */}
      <div className="cup-cakes-container">
        <div className="category-card-heading">Looking for Something Else</div>
        <div className="about-us-card-description">
          <Link to="/products?xdt=Cup%20Cakes" className="cup-cakes" title="Order Fresh Cup Cakes Online | Paris Bakery">
            <div className="cup-cakes-desktop-image">
              <img 
                src="/images/SpecialCakes/cup_cakes_desktop.png" 
                alt="Delicious Cup Cakes - Available in Various Flavors" 
                title="Order Cup Cakes Online" 
                className="img-fluid v-align-bottom" 
                loading="lazy" />
            </div>
            <div className="cup-cakes-mobile-image">
              <img 
                src="/images/SpecialCakes/cup_cakes_mobile.png" 
                alt="Mobile View - Cup Cakes Selection" 
                title="Cup Cakes Mobile View" 
                loading="lazy" />
            </div>
          </Link>

          <Link to="/products?xdt=Jar%20Cakes" className="cup-cakes" title="Buy Jar Cakes Online | Paris Bakery">
            <div className="cup-cakes-desktop-image">
              <img 
                src="/images/SpecialCakes/jar_cakes_desktop.png" 
                alt="Delicious Jar Cakes with Creamy Layers" 
                title="Order Jar Cakes Online" 
                className="img-fluid v-align-bottom" 
                loading="lazy" />
            </div>
            <div className="cup-cakes-mobile-image">
              <img 
                src="/images/SpecialCakes/jar_cakes_mobile.png" 
                alt="Jar Cakes Mobile Display" 
                title="Jar Cakes Mobile View" 
                loading="lazy" />
            </div>
          </Link>

          <Link to="/products?xdt=Pastry" className="cup-cakes" title="Pastries Online Delivery | Paris Bakery">
            <div className="cup-cakes-desktop-image">
              <img 
                src="/images/SpecialCakes/pastry_cakes_desktop.png" 
                alt="Tasty Pastries for Every Occasion" 
                title="Order Pastries Online" 
                className="img-fluid v-align-bottom" 
                loading="lazy" />
            </div>
            <div className="cup-cakes-mobile-image">
              <img 
                src="/images/SpecialCakes/pastry_cakes_mobile.png" 
                alt="Pastry Cakes Mobile View" 
                title="Pastry Cakes Mobile View" 
                loading="lazy" />
            </div>
          </Link>
        </div>
      </div>
    </div>
  );
}

export default HomePage;
