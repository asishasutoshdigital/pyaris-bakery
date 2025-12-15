import { useNavigate } from 'react-router-dom';
import { useCartStore } from '../store/useStore';

function CartPage() {
  const navigate = useNavigate();
  const { cart, cartQuantity, updateQuantity, removeFromCart, getTotal, clearCart } = useCartStore();

  const handleUpdateQuantity = (productId, newQuantity) => {
    updateQuantity(productId, newQuantity);
  };

  const handleRemove = (productId) => {
    removeFromCart(productId);
  };

  const handleCheckout = () => {
    if (cart.length > 0) {
      navigate('/checkout');
    }
  };

  if (cart.length === 0) {
    return (
      <div className="container py-5 text-center">
        <h2>Your cart is empty</h2>
        <p>Add some delicious items to your cart!</p>
        <button className="btn btn-primary mt-3" onClick={() => navigate('/products')}>
          Browse Products
        </button>
      </div>
    );
  }

  return (
    <div className="cart-page">
      <div className="container py-4">
        <h1 className="mb-4">Shopping Cart</h1>
        
        <div className="row">
          <div className="col-md-8">
            <div className="card">
              <div className="card-body">
                {cart.map((item) => (
                  <div key={item.id} className="cart-item border-bottom py-3">
                    <div className="row align-items-center">
                      <div className="col-md-6">
                        <h5>{item.menuName}</h5>
                        <p className="text-muted">₹ {item.sellPrice} each</p>
                      </div>
                      <div className="col-md-3">
                        <div className="input-group">
                          <button
                            className="btn btn-outline-secondary btn-sm"
                            onClick={() => handleUpdateQuantity(item.id, item.quantity - 1)}
                          >
                            -
                          </button>
                          <input
                            type="number"
                            className="form-control form-control-sm text-center"
                            value={item.quantity}
                            onChange={(e) => handleUpdateQuantity(item.id, parseInt(e.target.value) || 1)}
                            min="1"
                          />
                          <button
                            className="btn btn-outline-secondary btn-sm"
                            onClick={() => handleUpdateQuantity(item.id, item.quantity + 1)}
                          >
                            +
                          </button>
                        </div>
                      </div>
                      <div className="col-md-2">
                        <strong>₹ {(parseFloat(item.sellPrice) * item.quantity).toFixed(2)}</strong>
                      </div>
                      <div className="col-md-1">
                        <button
                          className="btn btn-danger btn-sm"
                          onClick={() => handleRemove(item.id)}
                        >
                          ✕
                        </button>
                      </div>
                    </div>
                  </div>
                ))}
                
                <div className="mt-3">
                  <button className="btn btn-outline-danger" onClick={clearCart}>
                    Clear Cart
                  </button>
                </div>
              </div>
            </div>
          </div>
          
          <div className="col-md-4">
            <div className="card">
              <div className="card-body">
                <h4>Order Summary</h4>
                <hr />
                <div className="d-flex justify-content-between mb-2">
                  <span>Items:</span>
                  <span>{cartQuantity}</span>
                </div>
                <div className="d-flex justify-content-between mb-2">
                  <span>Subtotal:</span>
                  <span>₹ {getTotal().toFixed(2)}</span>
                </div>
                <hr />
                <div className="d-flex justify-content-between mb-3">
                  <strong>Total:</strong>
                  <strong>₹ {getTotal().toFixed(2)}</strong>
                </div>
                <button
                  className="btn btn-success w-100"
                  onClick={handleCheckout}
                >
                  Proceed to Checkout
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CartPage;
