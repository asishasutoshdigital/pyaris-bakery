import { useEffect, useState, useRef } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { productAPI } from '../services/api';
import { useCartStore } from '../store/useStore';
import Swal from 'sweetalert2';

// EXACT 1:1 clone of sparkdetails.aspx structure
function ProductDetailsPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(true);
  const [quantity, setQuantity] = useState(1);
  const [weight, setWeight] = useState('0.5');
  const [flavour, setFlavour] = useState('');
  const [message, setMessage] = useState('');
  const [currentPrice, setCurrentPrice] = useState(0);
  const [isCake, setIsCake] = useState(false);
  
  const addToCart = useCartStore((state) => state.addToCart);

  useEffect(() => {
    loadProduct();
  }, [id]);

  useEffect(() => {
    if (product) {
      // checkgrp() function from ASPX line 17-24
      if (product.group !== 'Cakes') {
        setIsCake(false);
      } else {
        setIsCake(true);
      }
    }
  }, [product]);

  useEffect(() => {
    if (product && isCake) {
      // multy() function from ASPX lines 34-61
      multy();
    }
  }, [weight, product, isCake]);

  const loadProduct = async () => {
    setLoading(true);
    try {
      const response = await productAPI.getById(id);
      setProduct(response.data);
      setCurrentPrice(parseFloat(response.data.sellPrice));
      
      if (response.data.group === 'Cakes') {
        setWeight('0.5'); // Default weight for cakes
      }
    } catch (error) {
      console.error('Error loading product:', error);
      Swal.fire('Error', 'Failed to load product details', 'error');
    } finally {
      setLoading(false);
    }
  };

  // multy() function from ASPX lines 34-61
  const multy = () => {
    if (!product) return;
    
    const minwt = 0.5; // Minimum weight
    const wt = parseFloat(weight);
    const sp = parseFloat(product.sellPrice);

    if (wt > minwt) {
      const amt = Math.ceil(wt * sp);
      setCurrentPrice(amt);
      document.getElementById('xrate').innerHTML = `Rs ${amt} (Rs ${sp}/Kg)`;
    } else if (wt === minwt) {
      const amt = Math.ceil(wt * sp);
      setCurrentPrice(amt);
      document.getElementById('xrate').innerHTML = `Rs ${amt} (Rs ${sp}/Kg)`;
    } else {
      if (weight !== '') {
        setWeight(minwt.toString());
        const amt = Math.ceil(minwt * sp);
        setCurrentPrice(amt);
        document.getElementById('xrate').innerHTML = `Rs ${amt} (Rs ${sp}/Kg)`;
        Swal.fire(`Minimum Weight : ${minwt} Kg`, 'Please check the weight of the item!', 'error');
      }
    }
  };

  // smallimgclick function from ASPX lines 62-75
  const smallimgclick = (obj) => {
    const newSrc = obj.querySelector('img').getAttribute('src');
    const bigImg = document.getElementById('bigimg');
    const zoomResult = document.getElementById('zoomResult');

    if (bigImg) {
      bigImg.setAttribute('src', newSrc);
    }

    if (zoomResult) {
      zoomResult.style.backgroundImage = `url('${newSrc}')`;
    }
  };

  // Image zoom functionality from ASPX lines 77-104
  useEffect(() => {
    const img = document.getElementById('bigimg');
    const zoomResult = document.getElementById('zoomResult');

    if (!img || !zoomResult) return;

    zoomResult.style.backgroundImage = `url('${img.src}')`;

    const handleLoad = () => {
      zoomResult.style.backgroundSize = `${img.naturalWidth * 1.5}px ${img.naturalHeight * 1.5}px`;
    };

    img.addEventListener('load', handleLoad);

    const moveZoom = (e) => {
      const bounds = img.getBoundingClientRect();
      const x = e.clientX - bounds.left;
      const y = e.clientY - bounds.top;

      const xPercent = (x / bounds.width) * 100;
      const yPercent = (y / bounds.height) * 100;

      zoomResult.style.backgroundPosition = `${xPercent}% ${yPercent}%`;
    };

    img.addEventListener('mousemove', moveZoom);
    img.addEventListener('mouseenter', () => zoomResult.style.display = 'block');
    img.addEventListener('mouseleave', () => zoomResult.style.display = 'none');

    return () => {
      img.removeEventListener('load', handleLoad);
      img.removeEventListener('mousemove', moveZoom);
    };
  }, [product]);

  const handleAddToCart = () => {
    if (!product) return;
    
    const minWeight = 0.5;
    const wt = parseFloat(weight);
    
    if (isCake && wt < minWeight) {
      Swal.fire(`Minimum Weight : ${minWeight} Kg`, 'Please check the weight of the item!', 'error');
      return;
    }
    
    const cartItem = {
      ...product,
      quantity: quantity,
      weight: isCake ? weight : null,
      flavour: flavour || null,
      message: message || null,
      finalPrice: currentPrice
    };
    
    addToCart(cartItem);
    
    Swal.fire({
      title: 'Added to Cart!',
      text: `${quantity} x ${product.menuName} added to cart`,
      icon: 'success',
      timer: 1500,
      showConfirmButton: false
    });
  };

  const allowOnlyNumbers = (e) => {
    const charCode = e.which ? e.which : e.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode !== 46) {
      e.preventDefault();
    }
  };

  if (loading) {
    return (
      <div className="container py-5 text-center">
        <div className="spinner-border" role="status">
          <span className="visually-hidden">Loading...</span>
        </div>
      </div>
    );
  }

  if (!product) {
    return (
      <div className="container py-5 text-center">
        <h3>Product not found</h3>
        <button className="btn btn-primary mt-3" onClick={() => navigate('/products')}>
          Back to Products
        </button>
      </div>
    );
  }

  // ASPX structure starts at line 111
  return (
    <>
      {/* ASPX lines 111-206 */}
      <div id="midpart">
        <div itemScope itemType="https://schema.org/Product" id="xdata" className="col-md-12 product-details-container">
          <meta itemProp="brand" content="Paris Bakery" />
          <meta itemProp="url" content={window.location.href} />
          
          {/* Left section - Images (ASPX lines 116-143) */}
          <div className="col-md-6 login-container">
            <meta itemProp="image" content={`/menupic/big/${product.id}b.png`} />
            
            <div className="imgBox" id="ximgshowser">
              <img 
                alt="Placeholder"
                id="bigimg"
                className="imgBoxImageSize"
                src={`/menupic/big/${product.id}b.png`}
                onError={(e) => e.target.src = '/images/svg-icons/img-placeholder.svg'}
              />
              <div id="zoomResult"></div>
              <div id="lens"></div>
              <div className="veg-symbol"></div>
            </div>

            <div className="row smallimgBox">
              <ul id="prodImages" style={{ display: 'flex', listStyle: 'none', padding: 0, margin: 0 }}>
                <li id="ProductThumbnail_1" style={{ marginRight: '10px' }}>
                  <div className="smallimgBoxImageSize" id="ximgsmall1" onClick={(e) => smallimgclick(e.currentTarget)}>
                    <img 
                      alt="Thumbnail"
                      src={`/menupic/big/${product.id}b.png`}
                      className="smallimgBoxImageSize"
                      onError={(e) => e.target.src = '/images/svg-icons/img-placeholder.svg'}
                    />
                  </div>
                </li>
                <li id="ProductThumbnail_2" style={{ marginRight: '10px' }}>
                  <div className="smallimgBoxImageSize" id="ximgsmall2" onClick={(e) => smallimgclick(e.currentTarget)}>
                    <img 
                      alt="Thumbnail"
                      src={`/menupic/big/${product.id}b.png`}
                      className="smallimgBoxImageSize"
                      onError={(e) => e.target.src = '/images/svg-icons/img-placeholder.svg'}
                    />
                  </div>
                </li>
                <li id="ProductThumbnail_3">
                  <div className="smallimgBoxImageSize" id="ximgsmall3" onClick={(e) => smallimgclick(e.currentTarget)}>
                    <img 
                      alt="Thumbnail"
                      src={`/menupic/big/${product.id}b.png`}
                      className="smallimgBoxImageSize"
                      onError={(e) => e.target.src = '/images/svg-icons/img-placeholder.svg'}
                    />
                  </div>
                </li>
              </ul>
            </div>
          </div>
          
          {/* Right section - Product info (ASPX lines 144-204) */}
          <div className="col-md-6">
            <div className="sectionContent">
              <h1 id="xmenumane" className="xmenuname" itemProp="name">
                {product.menuName}
              </h1>
              
              <div className="rating-box">
                <div>
                  <span className="star">★</span>
                  <span className="rating-value">4.{Math.floor(Math.random() * 9)}</span>
                </div>
              </div>
              
              <div id="xgroupsubgroup" className="xgroupsubgroup">
                {product.group}
              </div>
              
              <img src="/images/out.png" width="180" alt="Out" className="nooutline" />
              
              <div id="xrate" className="xrate" itemProp="offers" itemScope itemType="https://schema.org/Offer">
                Rs {currentPrice}
              </div>
              
              {/* Weight section (ASPX lines 162-172) - Only for cakes */}
              {isCake && (
                <div id="x2" className="row">
                  <div className="col-md-12 product-prop-heading">
                    Weight
                  </div>
                  <div className="col-md-6">
                    <input
                      type="text"
                      id="xweight"
                      className="form-control numeric"
                      value={weight}
                      onChange={(e) => setWeight(e.target.value)}
                      onKeyPress={allowOnlyNumbers}
                      placeholder="Enter weight in Kg"
                    />
                  </div>
                </div>
              )}
              
              {/* Flavour section (ASPX lines 173-181) - Only for cakes */}
              {isCake && (
                <div id="x3" className="row">
                  <div className="col-md-12 product-prop-heading">
                    Flavour
                  </div>
                  <div className="col-md-6">
                    <select
                      id="xflavourname"
                      className="timerx form-control"
                      value={flavour}
                      onChange={(e) => setFlavour(e.target.value)}
                    >
                      <option value="">Select Flavour</option>
                      <option value="Vanilla">Vanilla</option>
                      <option value="Chocolate">Chocolate</option>
                      <option value="Strawberry">Strawberry</option>
                      <option value="Butterscotch">Butterscotch</option>
                      <option value="Pineapple">Pineapple</option>
                      <option value="Black Forest">Black Forest</option>
                      <option value="Red Velvet">Red Velvet</option>
                      <option value="Mixed Fruit">Mixed Fruit</option>
                    </select>
                  </div>
                </div>
              )}
              
              {/* Message section (ASPX lines 182-189) - Only for cakes */}
              {isCake && (
                <div id="x1" className="row">
                  <div className="col-md-12 product-prop-heading">
                    Cake Message
                  </div>
                  <div className="col-md-6">
                    <input
                      id="xwish"
                      type="text"
                      className="form-control"
                      placeholder="Enter message on cake"
                      value={message}
                      onChange={(e) => setMessage(e.target.value)}
                      maxLength="50"
                    />
                  </div>
                </div>
              )}
              
              {/* Quantity section (ASPX lines 190-197) */}
              <div id="x4" className="row">
                <div className="col-md-12 product-prop-heading">
                  Quantity
                </div>
                <div className="col-md-6">
                  <input
                    id="xqty"
                    type="text"
                    className="form-control numeric"
                    maxLength="3"
                    value={quantity}
                    onChange={(e) => setQuantity(parseInt(e.target.value) || 1)}
                    onKeyPress={allowOnlyNumbers}
                  />
                </div>
              </div>
              
              {/* Add to cart button (ASPX lines 198-202) */}
              <div id="x5" className="row">
                <div className="col-md-6">
                  <button
                    className="rounded-btn pink-btn"
                    onClick={handleAddToCart}
                  >
                    Add To Cart
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      
      {/* Description section (ASPX lines 207-210) */}
      <div className="col-md-12 product-details-description">
        <h2>Cake Description</h2>
        <p>Turn your celebration into a sizzling party by ordering this adorable cake. This delicious cake will drivel the taste buds of all the party attendees and most importantly an ideal delicacy to make someone special feel on any special occasion by gifting this delicious cake.</p>
      </div>

      {/* Delivery badges (ASPX lines 212-233) */}
      <div className="col-md-12 time-smile-content">
        <div className="time-smile">
          <div className="thumb-time">
            <div className="thumb-image">
              <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEgAAABKCAYAAAAYJRJMAAAABHNCSVQICAgIfAhkiAAAD/pJREFUeJztW3l4VEW2P9X33t73Jd1JZw8hGyQkIJugkVUWEX0q4LgNo+KMjuvT9/SNvqfO8ukbUZlRYRxFHAYX3J4iooIalSWQkIUkZOnudDok6aS7053k93tv1/tD+qaDIQRNJ3kPft93/zjnnlN16vfVrapbdQrBJAPGWFB/ovk5uUI212q12YUCkYDkI1tigq7MYDR8ihByjWc8aDwrOxcwxpLa6pNlj9z/RFKbtQ3YSAQAAPLTU2H6jHz/siuXdM6YMe1Ng1H39/GKaVIR1Gq27fr1nQ+VmpstZ7VZuWa5/5bb1pfNvKTwV+MRE288KhkNMMZzPv/sq/SRyAEA2Pvx5+KXX3x1RXOjefd4xEWORyWjAU3TeRaTNT0qJyYlwbbtmzu6OrvRvr1fqj7cvUcUffdt2SEQioTFZrP1equals9M3xjGvS9KD6E81FLU0mTr588TzIyMz4rysWX/ovDzx199aXnvOHOvzxb5vcgYGeq+JZ1yThiCEUlU+Fo3PJJ8PU/3MZwZYP/qbPFvbk= " width="100%" height="100%" alt="100% On Time Delivery" />
            </div>
            <div className="time-content"><span className="percent-bold">100%</span>On Time Delivery</div>
          </div>
          <div className="thumb-time">
            <div className="thumb-image">
              <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEcAAABHCAYAAABVsFofAAAABHNCSVQICAgIfAhkiAAAEzNJREFUeJzVXGdY1Mf2fmcbsLuAILBLlSZdBDX2RmwxGmvsojGS3OQmMTf5596Yc9P15" width="100%" height="100%" alt="100% Payment Protection" />
            </div>
            <div className="time-content"><span className="percent-bold">100%</span>Payment Protection</div>
          </div>
          <div className="thumb-time">
            <div className="thumb-image">
              <img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAE0AAABHCAYAAABCksrWAAAABHNCSVQICAgIfAhkiAAAEHNJREFUeJztXE9sG1d6/31vht1Qjsl4jG6lnSGgQONDQ0KSScON1OsGYU5bQBsJ2JvT6NQC1Vo+25BvBaz" width="100%" height="100%" alt="100% Smiles Delivered" />
            </div>
            <div className="time-content"><span className="percent-bold">100%</span>Smiles Delivered</div>
          </div>
        </div>
      </div>
    </>
  );
}

export default ProductDetailsPage;
