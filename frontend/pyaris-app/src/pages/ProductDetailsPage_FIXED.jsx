import { useEffect, useState, useRef } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { productAPI } from '../services/api';
import { useCartStore } from '../store/useStore';
import Swal from 'sweetalert2';

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
  
  const addToCart = useCartStore((state) => state.addToCart);

  useEffect(() => {
    loadProduct();
  }, [id]);

  useEffect(() => {
    if (product) {
      calculatePrice();
    }
  }, [weight, product]);

  const loadProduct = async () => {
    setLoading(true);
    try {
      const response = await productAPI.getById(id);
      setProduct(response.data);
      setCurrentPrice(parseFloat(response.data.sellPrice));
      // Set initial weight based on product group
      if (response.data.group === 'Cakes') {
        setWeight('0.5');
      }
    } catch (error) {
      console.error('Error loading product:', error);
      Swal.fire('Error', 'Failed to load product details', 'error');
    } finally {
      setLoading(false);
    }
  };

  const calculatePrice = () => {
    if (!product) return;
    
    const sellPrice = parseFloat(product.sellPrice);
    const wt = parseFloat(weight);
    const minWeight = 0.5; // Minimum weight in kg
    
    if (wt >= minWeight && product.group === 'Cakes') {
      const amt = Math.ceil(wt * sellPrice);
      setCurrentPrice(amt);
    } else if (wt < minWeight && product.group === 'Cakes') {
      setWeight('0.5');
      const amt = Math.ceil(minWeight * sellPrice);
      setCurrentPrice(amt);
      if (wt > 0) {
        Swal.fire(`Minimum Weight : ${minWeight} Kg`, 'Please check the weight of the item!', 'error');
      }
    } else {
      setCurrentPrice(sellPrice);
    }
  };

  const handleWeightChange = (e) => {
    const value = e.target.value;
    if (value === '' || parseFloat(value) >= 0) {
      setWeight(value);
    }
  };

  const handleAddToCart = () => {
    if (!product) return;
    
    const minWeight = 0.5;
    const wt = parseFloat(weight);
    
    if (product.group === 'Cakes' && wt < minWeight) {
      Swal.fire(`Minimum Weight : ${minWeight} Kg`, 'Please check the weight of the item!', 'error');
      return;
    }
    
    // Add product with customization
    const cartItem = {
      ...product,
      quantity: quantity,
      weight: product.group === 'Cakes' ? weight : null,
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

  const isCake = product.group === 'Cakes';

  return (
    <div id="midpart">
      <div id="xdata" className="col-md-12 product-details-container">
        <div className="col-md-6 login-container">
          <div className="imgBox">
            <img 
              alt={product.menuName}
              id="zoomImage"
              className="imgBoxImageSize"
              src={`/menupic/big/${product.id}b.png`}
              onError={(e) => e.target.src = '/images/svg-icons/img-placeholder.svg'}
            />
            <div id="lens"></div>
            <div className="veg-symbol"></div>
          </div>

          <div className="row smallimgBox">
            <ul style={{ display: 'flex', listStyle: 'none', padding: 0 }}>
              <li style={{ marginRight: '10px' }}>
                <div className="smallimgBoxImageSize">
                  <img 
                    alt="Thumbnail 1"
                    src={`/menupic/big/${product.id}b.png`}
                    className="smallimgBoxImageSize"
                    onError={(e) => e.target.src = '/images/svg-icons/img-placeholder.svg'}
                  />
                </div>
              </li>
              <li style={{ marginRight: '10px' }}>
                <div className="smallimgBoxImageSize">
                  <img 
                    alt="Thumbnail 2"
                    src={`/menupic/big/${product.id}b.png`}
                    className="smallimgBoxImageSize"
                    onError={(e) => e.target.src = '/images/svg-icons/img-placeholder.svg'}
                  />
                </div>
              </li>
              <li>
                <div className="smallimgBoxImageSize">
                  <img 
                    alt="Thumbnail 3"
                    src={`/menupic/big/${product.id}b.png`}
                    className="smallimgBoxImageSize"
                    onError={(e) => e.target.src = '/images/svg-icons/img-placeholder.svg'}
                  />
                </div>
              </li>
            </ul>
          </div>
        </div>

        <div className="col-md-6">
          <div className="sectionContent">
            <h1 id="xmenumane" className="xmenuname">
              {product.menuName}
            </h1>
            
            <div className="rating-box">
              <span>★★★★★</span>
              <span style={{ marginLeft: '10px', color: '#666' }}>
                (Based on reviews)
              </span>
            </div>

            <div id="xrate" style={{ 
              fontSize: '1.8rem', 
              fontWeight: 'bold', 
              color: '#d63384',
              margin: '20px 0' 
            }}>
              Rs {currentPrice} {isCake && `(Rs ${product.sellPrice}/Kg)`}
            </div>

            {product.feature1 && (
              <div style={{ marginBottom: '20px' }}>
                <h5>Features:</h5>
                <ul>
                  {product.feature1 && <li>{product.feature1}</li>}
                  {product.feature2 && <li>{product.feature2}</li>}
                  {product.feature3 && <li>{product.feature3}</li>}
                  {product.feature4 && <li>{product.feature4}</li>}
                </ul>
              </div>
            )}

            {isCake && (
              <>
                <div className="form-group">
                  <label>Weight (in Kg)</label>
                  <input
                    type="text"
                    id="xweight"
                    className="form-control numeric"
                    value={weight}
                    onChange={handleWeightChange}
                    onKeyPress={allowOnlyNumbers}
                    placeholder="Enter weight in Kg"
                    style={{ maxWidth: '200px' }}
                  />
                </div>

                <div className="form-group">
                  <label>Select Flavour</label>
                  <select
                    id="xflavourname"
                    className="form-control"
                    value={flavour}
                    onChange={(e) => setFlavour(e.target.value)}
                    style={{ maxWidth: '300px' }}
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

                <div className="form-group">
                  <label>Message on Cake</label>
                  <textarea
                    id="xmessage"
                    className="form-control"
                    value={message}
                    onChange={(e) => setMessage(e.target.value)}
                    placeholder="Enter message for cake"
                    rows="3"
                    maxLength="50"
                    style={{ maxWidth: '400px' }}
                  />
                  <small className="form-text text-muted">
                    Max 50 characters
                  </small>
                </div>
              </>
            )}

            <div className="form-group">
              <label>Quantity</label>
              <div style={{ display: 'flex', alignItems: 'center', gap: '10px', maxWidth: '150px' }}>
                <button
                  className="quantity-button"
                  onClick={() => setQuantity(Math.max(1, quantity - 1))}
                >
                  -
                </button>
                <span style={{ fontWeight: 'bold', fontSize: '1.2rem' }}>
                  {quantity}
                </span>
                <button
                  className="quantity-button"
                  onClick={() => setQuantity(quantity + 1)}
                >
                  +
                </button>
              </div>
            </div>

            <div style={{ marginTop: '30px' }}>
              {product.active === '0' ? (
                <div className="alert alert-danger">OUT OF STOCK</div>
              ) : (
                <button
                  className="add-to-cart-btn rounded-btn pink-btn"
                  onClick={handleAddToCart}
                  style={{ padding: '12px 40px', fontSize: '1.1rem' }}
                >
                  ADD TO CART
                </button>
              )}
            </div>

            <div className="delivery-badges" style={{ marginTop: '30px' }}>
              <div style={{ 
                background: '#f0f8ff', 
                padding: '15px', 
                borderRadius: '8px',
                marginBottom: '10px'
              }}>
                <strong>🚚 100% On Time Delivery</strong>
                <div style={{ fontSize: '0.9rem', marginTop: '5px' }}>
                  We ensure your order reaches you on time
                </div>
              </div>
              <div style={{ 
                background: '#f0fff0', 
                padding: '15px', 
                borderRadius: '8px'
              }}>
                <strong>✓ Fresh & Quality Products</strong>
                <div style={{ fontSize: '0.9rem', marginTop: '5px' }}>
                  Made with premium ingredients
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ProductDetailsPage;
