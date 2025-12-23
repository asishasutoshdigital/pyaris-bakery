import { useNavigate } from 'react-router-dom';

function OrderFailPage() {
  const navigate = useNavigate();

  return (
    <div className="order-fail-page">
      <div className="container py-5">
        <div className="row justify-content-center">
          <div className="col-md-6 text-center">
            <div className="card">
              <div className="card-body p-5">
                <div className="mb-4">
                  <i className="bi bi-x-circle text-danger" style={{ fontSize: '5rem' }}></i>
                </div>
                
                <h1 className="text-danger mb-3">Order Failed</h1>
                <p className="lead mb-4">
                  Unfortunately, your order could not be processed.
                </p>
                
                <div className="alert alert-warning">
                  <p className="mb-0">
                    This could be due to:
                  </p>
                  <ul className="text-start mt-2 mb-0">
                    <li>Payment was not completed</li>
                    <li>Payment gateway error</li>
                    <li>Network issues</li>
                  </ul>
                </div>
                
                <p className="mb-4">
                  Don't worry! Your cart items are still saved. You can try again.
                </p>
                
                <div className="d-grid gap-2">
                  <button
                    className="btn btn-primary btn-lg"
                    onClick={() => navigate('/checkout')}
                  >
                    Try Again
                  </button>
                  <button
                    className="btn btn-outline-primary"
                    onClick={() => navigate('/cart')}
                  >
                    View Cart
                  </button>
                  <button
                    className="btn btn-link"
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

export default OrderFailPage;
