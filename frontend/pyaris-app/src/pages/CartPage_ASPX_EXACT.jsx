import { useEffect } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useCartStore } from '../store/useStore';
import Swal from 'sweetalert2';

// EXACT 1:1 clone of sparkcart.aspx structure
function CartPage() {
  const navigate = useNavigate();
  const { cart, updateQuantity, removeFromCart, getTotal } = useCartStore();

  // Apply pink background from ASPX line 109-111
  useEffect(() => {
    document.documentElement.style.backgroundColor = '#fbf3f3';
    document.body.style.backgroundColor = '#fbf3f3';
    const form = document.querySelector('form');
    if (form) form.style.backgroundColor = '#fbf3f3';
  }, []);

  const handleUpdateQuantity = (productId, change) => {
    const item = cart.find(i => i.id === productId);
    if (item) {
      const newQty = item.quantity + change;
      if (newQty > 0) {
        updateQuantity(productId, newQty);
      }
    }
  };

  const handleRemove = (productId) => {
    Swal.fire({
      title: 'Remove Item?',
      text: 'Are you sure you want to remove this item from cart?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Yes, remove it!'
    }).then((result) => {
      if (result.isConfirmed) {
        removeFromCart(productId);
        Swal.fire('Removed!', 'Item has been removed from cart.', 'success');
      }
    });
  };

  const handlePlaceOrder = () => {
    const total = getTotal();
    const minOrderAmt = 300;
    
    if (total >= minOrderAmt) {
      navigate('/checkout');
    } else {
      Swal.fire(
        `Minimum Order Amount : Rs ${minOrderAmt}`,
        'Please Check Your Cart Total !!',
        'error'
      );
    }
  };

  // Empty cart - ASPX lines 269-275
  if (cart.length === 0) {
    return (
      <div id="midpart">
        <div id="emptycartcontainer" className="row spark-over-container">
          <div className="category-card-heading">Empty Cart !!</div>
          <div className="cartTitle">Add some items to cart to checkout</div>
          <div className="col-md-4">
            <input 
              type="button" 
              onClick={() => navigate('/')} 
              value="CONTINUE SHOPPING" 
              className="rounded-btn red-btn"
            />
          </div>
        </div>
      </div>
    );
  }

  // Cart with items - ASPX lines 276-395
  return (
    <div id="midpart">
      <div id="cartcontainer" className="cart-container">
        <div className="col-md-12 col-xs-12 no-padding cart-inner-container">
          {/* Left section - cart items (ASPX lines 278-304) */}
          <div className="col-md-8 col-sm-7 cart-item-container">
            <h1 className="cartTitle">
              Shopping cart
            </h1>
            
            <div id="xdata">
              {cart.map((item) => (
                <div key={item.id} className="cart-card">
                  <div className="cart-card-image">
                    <img 
                      src={`/menupic/small/${item.id}s.png`} 
                      alt={item.menuName}
                      className="cart-card-icon"
                      onError={(e) => e.target.src = '/images/svg-icons/img-placeholder.svg'}
                    />
                  </div>
                  
                  <div className="cart-card-body">
                    <div className="cart-card-title">{item.menuName}</div>
                    
                    <div className="cart-card-quantity">
                      <span>QTY : </span>
                      <button 
                        className="quantity-button"
                        onClick={() => handleUpdateQuantity(item.id, -1)}
                      >
                        -
                      </button>
                      <span style={{ margin: '0 12px', fontWeight: 'bold' }}>
                        {item.quantity}
                      </span>
                      <button 
                        className="quantity-button"
                        onClick={() => handleUpdateQuantity(item.id, 1)}
                      >
                        +
                      </button>
                    </div>
                    
                    <div className="cart-card-text" style={{ display: 'inline-block', marginRight: '20px' }}>
                      PRICE : Rs {item.sellPrice}
                    </div>
                    <div className="cart-card-text" style={{ display: 'inline-block' }}>
                      AMOUNT : Rs {(parseFloat(item.sellPrice) * item.quantity).toFixed(0)}
                    </div>
                  </div>
                  
                  <div className="cart-card-footer">
                    <button 
                      className="remove-item"
                      onClick={() => handleRemove(item.id)}
                      title="Remove item"
                    >
                      ✕
                    </button>
                  </div>
                </div>
              ))}
            </div>
          </div>
          
          {/* Right section - cart summary (ASPX lines 305-392) */}
          <div className="col-md-3 col-sm-4 cart-button-container">
            <div className="col-md-12">
              {/* Subtotal - ASPX line 309 */}
              <div className="cartTotalBox" id="xsubtotal">
                SUB TOTAL : Rs {getTotal().toFixed(0)}
              </div>
              
              {/* Total - ASPX line 318 */}
              <div className="cartTotalBox" id="xtotal">
                TOTAL : Rs {getTotal().toFixed(0)}
              </div>
            </div>
            
            {/* Available points section - ASPX lines 320-343 */}
            <div id="payBackContainer">
              <div className="col-md-12">
                <div className="subTotalBox availablePointsBox" id="xavailablepoints">
                  AVAILABLE POINTS TO REDEEM: 0
                </div>
                
                {/* Redeem button placeholder - ASPX lines 326-333 */}
                <div style={{ textAlign: 'center' }}>
                  <button 
                    className="btn pink-btn"
                    style={{ marginTop: '10px', color: 'white' }}
                    onClick={() => Swal.fire('Info', 'Redeem points feature coming soon', 'info')}
                  >
                    Redeem Points
                  </button>
                </div>
                
                {/* Gift card button placeholder - ASPX lines 344-351 */}
                <div style={{ textAlign: 'center' }}>
                  <button 
                    className="btn btn-link"
                    style={{ marginTop: '10px', fontSize: '16px' }}
                    onClick={() => Swal.fire('Info', 'Gift card feature coming soon', 'info')}
                  >
                    Do you have a gift card? Click here
                  </button>
                </div>
              </div>
            </div>
            
            {/* Place order button - ASPX line 387 */}
            <div>
              <button 
                className="rounded-btn green-btn"
                onClick={handlePlaceOrder}
                style={{ width: '100%' }}
              >
                PLACE ORDER
              </button>
            </div>
            
            {/* Continue shopping - ASPX line 390 */}
            <div>
              <button 
                className="rounded-btn transparent-btn"
                onClick={() => navigate('/')}
                style={{ width: '100%', marginTop: '10px' }}
              >
                CONTINUE SHOPPING
              </button>
            </div>
          </div>
        </div>
      </div>
      
      {/* Inline styles from ASPX lines 108-265 */}
      <style jsx="true">{`
        html, body, form, .footer-main {
          background-color: #fbf3f3;
        }
        
        .cart-card {
          display: flex;
          background: rgba(255, 255, 255, 0.6);
          border-radius: 20px;
          margin-bottom: 25px;
          box-shadow: 0 10px 30px rgba(0, 0, 0, 0.08);
          overflow: hidden;
          align-items: center;
          transition: transform 0.3s ease, box-shadow 0.3s ease;
          position: relative;
          backdrop-filter: blur(10px);
          border: 1px solid rgba(255, 255, 255, 0.3);
        }
        
        .cart-card:hover {
          transform: translateX(-6px);
          box-shadow: 0 14px 40px rgba(0, 0, 0, 0.15);
        }
        
        .cart-card-image {
          width: 240px;
          height: 100%;
          display: flex;
          align-items: center;
          background: rgba(243, 246, 250, 0.8);
          padding: 30px;
          border-right: 1px solid #e0e0e0;
        }
        
        .cart-card-icon {
          max-width: 100%;
          height: auto;
          max-height: 160px;
          object-fit: contain;
          border-radius: 18px;
          box-shadow: 0 6px 18px rgba(0, 0, 0, 0.08);
        }
        
        .cart-card-body {
          color: #000;
          padding: 28px;
          flex-grow: 1;
          font-family: 'Segoe UI', sans-serif;
        }
        
        .cart-card-title {
          font-size: 1.5rem;
          font-weight: bold;
          color: #1e1e1e;
          margin-bottom: 12px;
        }
        
        .cart-card-text {
          font-size: 1.1rem;
          color: #000;
          margin-bottom: 16px;
        }
        
        .cart-card-quantity {
          display: flex;
          align-items: center;
          gap: 12px;
          margin-bottom: 16px;
        }
        
        .quantity-button {
          padding: 8px 18px;
          background: #ffffff;
          border: 1px solid #ddd;
          border-radius: 10px;
          font-weight: 600;
          font-size: 1.1rem;
          color: #333;
          box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
          transition: all 0.2s ease;
          cursor: pointer;
        }
        
        .quantity-button:hover {
          background: #f0f2f5;
          transform: scale(1.08);
        }
        
        .cart-card-footer {
          position: absolute;
          top: 16px;
          right: 16px;
          opacity: 0;
          transition: opacity 0.3s ease;
        }
        
        .cart-card:hover .cart-card-footer {
          opacity: 1;
        }
        
        .remove-item {
          width: 64px;
          height: 64px;
          background: linear-gradient(135deg, #ff4e4e, #ff7676);
          border: none;
          border-radius: 50%;
          display: flex;
          align-items: center;
          justify-content: center;
          color: #fff;
          font-size: 24px;
          box-shadow: 0 6px 18px rgba(255, 60, 60, 0.4);
          cursor: pointer;
          transition: all 0.3s ease;
        }
        
        .remove-item:hover {
          transform: scale(1.15) rotate(-10deg);
          box-shadow: 0 12px 28px rgba(255, 0, 0, 0.4);
        }
      `}</style>
    </div>
  );
}

export default CartPage;
