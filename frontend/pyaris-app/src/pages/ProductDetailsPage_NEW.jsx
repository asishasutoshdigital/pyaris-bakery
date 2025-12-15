import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { productAPI } from '../services/api';
import { useCartStore } from '../store/useStore';
import Swal from 'sweetalert2';

function ProductDetailsPage() {
  const { id } = useParams();
  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(true);
  const [selectedWeight, setSelectedWeight] = useState('');
  const [selectedFlavour, setSelectedFlavour] = useState('');
  const [cakeMessage, setCakeMessage] = useState('');
  const [quantity, setQuantity] = useState(1);
  const [calculatedPrice, setCalculatedPrice] = useState(0);
  
  const addToCart = useCartStore((state) => state.addToCart);

  useEffect(() => {
    loadProduct();
  }, [id]);

  useEffect(() => {
    if (product && selectedWeight) {
      calculatePrice();
    }
  }, [selectedWeight, product]);

  const loadProduct = async () => {
    setLoading(true);
    try {
      const response = await productAPI.getById(id);
      setProduct(response.data);
      setCalculatedPrice(response.data?.sellPrice || 0);
    } catch (error) {
      console.error('Error loading product:', error);
    } finally {
      setLoading(false);
    }
  };

  const calculatePrice = () => {
    if (product && selectedWeight) {
      const weight = parseFloat(selectedWeight);
      const pricePerKg = product.sellPrice;
      const amt = Math.ceil(weight * pricePerKg);
      setCalculatedPrice(amt);
    }
  };

  const handleAddToCart = () => {
    if (!product) return;

    const cartItem = {
      ...product,
      weight: selectedWeight,
      flavour: selectedFlavour,
      message: cakeMessage,
      quantity: quantity,
      totalPrice: calculatedPrice * quantity
    };

    addToCart(cartItem);
    
    Swal.fire({
      title: 'Added to Cart!',
      text: `${product.menuName} added to cart`,
      icon: 'success',
      timer: 1500,
      showConfirmButton: false
    });
  };

  const handleSmallImageClick = (imgSrc) => {
    const bigImg = document.getElementById('bigimg');
    if (bigImg) {
      bigImg.src = imgSrc;
    }
  };

  if (loading) {
    return (
      <div id="midpart">
        <div className="col-md-12 text-center">Loading...</div>
      </div>
    );
  }

  if (!product) {
    return (
      <div id="midpart">
        <div className="col-md-12 text-center">Product not found</div>
      </div>
    );
  }

  return (
    <div id="midpart">
      <div itemScope itemType="https://schema.org/Product" id="xdata" className="col-md-12 product-details-container">
        <meta itemProp="brand" content="Paris Bakery" />
        <meta itemProp="url" content={window.location.href} />
        
        <div className="col-md-6 login-container">
          <meta itemProp="image" content={product.imagePath || `/menupic/big/${product.id}b.png`} />
          <div className="imgBox" id="ximgshowser">
            <img
              id="bigimg"
              alt={product.menuName}
              className="imgBoxImageSize"
              src={product.imagePath || `/menupic/big/${product.id}b.png`}
              onError={(e) => {
                e.target.src = '/images/svg-icons/img-placeholder.svg';
              }}
            />
            <div id="lens"></div>
            <div className="veg-symbol"></div>
          </div>

          <div className="row smallimgBox">
            <ul id="prodImages">
              <li id="ProductThumbnail_1">
                <div
                  className="smallimgBoxImageSize"
                  onClick={(e) => handleSmallImageClick(e.currentTarget.querySelector('img').src)}
                >
                  <img
                    alt="Thumbnail"
                    src={product.imagePath || `/menupic/big/${product.id}b.png`}
                    className="smallimgBoxImageSize"
                    onError={(e) => {
                      e.target.src = '/images/svg-icons/img-placeholder.svg';
                    }}
                  />
                </div>
              </li>
            </ul>
          </div>
        </div>

        <div className="col-md-6">
          <div className="sectionContent">
            <h1 id="xmenumane" className="xmenuname" itemProp="name">
              {product.menuName}
            </h1>
            <div className="rating-box">
              <div>
                <span className="star">★</span>
                <span className="rating-value">4.{Math.floor(Math.random() * 9) + 1}</span>
              </div>
            </div>
            <div id="xgroupsubgroup" className="xgroupsubgroup">
              {product.group} / {product.subGroup}
            </div>
            <div id="xrate" className="xrate" itemProp="offers" itemScope itemType="https://schema.org/Offer">
              ₹ {calculatedPrice}
            </div>

            {product.group === 'Cakes' && (
              <>
                <div id="x2" className="row">
                  <div className="col-md-12 product-prop-heading">Weight</div>
                  <div className="col-md-6">
                    <select
                      className="form-control"
                      value={selectedWeight}
                      onChange={(e) => setSelectedWeight(e.target.value)}
                    >
                      <option value="">Select Weight</option>
                      <option value="0.5">0.5 Kg</option>
                      <option value="1">1 Kg</option>
                      <option value="1.5">1.5 Kg</option>
                      <option value="2">2 Kg</option>
                    </select>
                  </div>
                </div>

                <div id="x3" className="row">
                  <div className="col-md-12 product-prop-heading">Flavour</div>
                  <div className="col-md-6">
                    <select
                      className="form-control timerx"
                      value={selectedFlavour}
                      onChange={(e) => setSelectedFlavour(e.target.value)}
                    >
                      <option value="">Select Flavour</option>
                      <option value="Chocolate">Chocolate</option>
                      <option value="Vanilla">Vanilla</option>
                      <option value="Strawberry">Strawberry</option>
                      <option value="Butterscotch">Butterscotch</option>
                    </select>
                  </div>
                </div>

                <div id="x1" className="row">
                  <div className="col-md-12 product-prop-heading">Cake Message</div>
                  <div className="col-md-6">
                    <input
                      type="text"
                      className="form-control"
                      placeholder="Enter message on cake"
                      value={cakeMessage}
                      onChange={(e) => setCakeMessage(e.target.value)}
                    />
                  </div>
                </div>
              </>
            )}

            <div id="x4" className="row">
              <div className="col-md-12 product-prop-heading">Quantity</div>
              <div className="col-md-6">
                <input
                  type="text"
                  className="form-control numeric"
                  maxLength="3"
                  value={quantity}
                  onChange={(e) => {
                    const val = parseInt(e.target.value) || 1;
                    setQuantity(val > 0 ? val : 1);
                  }}
                />
              </div>
            </div>

            <div id="x5" className="row">
              <div className="col-md-6">
                <button className="rounded-btn pink-btn" onClick={handleAddToCart}>
                  Add To Cart
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>

      <div className="col-md-12 product-details-description">
        <h2>Cake Description</h2>
        <p>
          Turn your celebration into a sizzling party by ordering this adorable cake. This delicious cake will
          drivel the taste buds of all the party attendees and most importantly an ideal delicacy to make someone
          special feel on any special occasion by gifting this delicious cake.
        </p>
      </div>

      <div className="col-md-12 time-smile-content">
        <div className="time-smile">
          <div className="thumb-time">
            <div className="thumb-image">
              <img
                src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEgAAABKCAYAAAAYJRJMAAAABHNCSVQICAgIfAhkiAAAD/pJREFUeJztW3l4VEW2P9X33t73Jd1JZw8hGyQkIJugkVUWEX0q4LgNo+KMjuvT9/SNvqfO8ukbUZlRYRxFHAYX3J4iooIalSWQkIUkZOnudDok6aS7053k93tv1/tD+qaDIQRNJ3kPft93/zjnnlN16vfVrapbdQrBJAPGWFB/ovk5uUI212q12YUCkYDkI1tigq7MYDR8ihByjWc8aDwrOxcwxpLa6pNlj9z/RFKbtQ3YSAQAAPLTU2H6jHz/siuXdM6YMe1Ng1H39/GKaVIR1Gq27fr1nQ+VmpstZ7VZuWa5/5bb1pfNvKTwV+MRE288KhkNMMZzPv/sq/SRyAEA2Pvx5+KXX3x1RXOjefd4xEWORyWjAU3TeRaTNT0qJyYlwbbtmzu6OrvRvr1fqj7cvUcUffdt2SEQioTFZrP1anys9M3xjGvS9KD6E81FLU0mTr588TzIyMz4rysWX/ovDzx899N/e+OFIV3ri31fiRtqG68KBHBqPOOaNATJFDJ1dFAG