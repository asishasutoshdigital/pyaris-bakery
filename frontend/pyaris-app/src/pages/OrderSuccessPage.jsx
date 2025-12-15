import { useEffect } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useCartStore } from '../store/useStore';

function OrderSuccessPage() {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const clearCart = useCartStore((state) => state.clearCart);
  const orderId = searchParams.get('orderId');

  useEffect(() => {
    // Clear cart on successful order
    clearCart();
  }, [clearCart]);

  return (
    <div className="order-success-page">
      <div className="container py-5">
        <div className="row justify-content-center">
          <div className="col-md-6 text-center">
            <div className="card">
              <div className="card-body p-5">
                <div className="mb-4">
                  <i className="bi bi-check-circle text-success" style={{ fontSize: '5rem' }}></i>
                </div>
                
                <h1 className="text-success mb-3">Order Successful!</h1>
                <p className="lead mb-4">
                  Thank you for your order. Your order has been placed successfully.
                </p>
                
                {orderId && (
                  <div className="alert alert-info">
                    <strong>Order ID:</strong> {orderId}
                  </div>
                )}
                
                <p className="mb-4">
                  You will receive a confirmation email shortly with your order details.
                </p>
                
                <div className="d-grid gap-2">
                  <button
                    className="btn btn-primary btn-lg"
                    onClick={() => navigate('/products')}
                  >
                    Continue Shopping
                  </button>
                  <button
                    className="btn btn-outline-primary"
                    onClick={() => navigate('/')}
                  >
                    Go to Home
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default OrderSuccessPage;
