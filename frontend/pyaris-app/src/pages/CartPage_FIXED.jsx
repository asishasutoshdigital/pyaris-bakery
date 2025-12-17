import { useNavigate, Link } from 'react-router-dom';
import { useCartStore } from '../store/useStore';
import Swal from 'sweetalert2';

function CartPage() {
  const navigate = useNavigate();
  const { cart, cartQuantity, updateQuantity, removeFromCart, getTotal } = useCartStore();

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
    const minOrderAmt = 300; // Minimum order amount
    
    if (total >= minOrderAmt) {
      navigate('/checkout');
    } else {
      Swal.fire('Minimum Order Amount : Rs ' + minOrderAmt, 'Please Check Your Cart Total !!', 'error');
    }
  };

  // Apply pink background
  if (typeof document !== 'undefined') {
    document.documentElement.style.backgroundColor = '#fbf3f3';
    document.body.style.backgroundColor = '#fbf3f3';
  }

  // Empty cart state
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

  return (
    <div id="midpart">
      <div id="cartcontainer" className="cart-container">
        <div className="col-md-12 col-xs-12 no-padding cart-inner-container">
          <div className="col-md-8 col-sm-7 cart-item-container">
            <h1 className="cartTitle">Shopping cart</h1>
            
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
                  <div className="cart-card-text">
                    PRICE : Rs {item.sellPrice}
                  </div>
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
                  <div className="cart-card-text">
                    AMOUNT : Rs {(parseFloat(item.sellPrice) * item.quantity).toFixed(0)}
                  </div>
                </div>
                <div className="cart-card-footer">
                  <button 
                    className="remove-item"
                    onClick={() => handleRemove(item.id)}
                  >
                    ✕
                  </button>
                </div>
              </div>
            ))}
          </div>
          
          <div className="col-md-4 col-sm-5 cart-button-container">
            <div className="cartTotalBox">
              <div style={{ marginBottom: '15px' }}>
                <strong>SUBTOTAL</strong>
                <div style={{ fontSize: '1.2rem', marginTop: '5px' }}>
                  Rs {getTotal().toFixed(0)}
                </div>
              </div>
              
              <div style={{ marginBottom: '15px', padding: '10px 0', borderTop: '1px solid #ddd' }}>
                <strong>Available Points</strong>
                <div style={{ marginTop: '5px', color: '#666' }}>
                  0 points
                </div>
              </div>
              
              <div style={{ marginBottom: '20px', paddingTop: '15px', borderTop: '2px solid #333' }}>
                <div style={{ fontSize: '1.5rem', fontWeight: 'bold' }}>
                  TOTAL : Rs {getTotal().toFixed(0)}
                </div>
              </div>
              
              <button 
                className="rounded-btn green-btn"
                onClick={handlePlaceOrder}
                style={{ width: '100%', marginTop: '10px' }}
              >
                PLACE ORDER
              </button>
              
              <Link to="/">
                <button 
                  className="rounded-btn transparent-btn"
                  style={{ width: '100%', marginTop: '10px' }}
                >
                  CONTINUE SHOPPING
                </button>
              </Link>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CartPage;
