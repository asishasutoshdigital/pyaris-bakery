import { useState, useEffect } from 'react';
import { useUserStore } from '../store/useStore';
import { useNavigate } from 'react-router-dom';

function MyAccountPage() {
  const navigate = useNavigate();
  const { user, isAuthenticated } = useUserStore();
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!isAuthenticated) {
      navigate('/login?redirect=my-account');
      return;
    }

    loadOrders();
  }, [isAuthenticated, navigate]);

  const loadOrders = async () => {
    setLoading(true);
    try {
      // TODO: Implement order loading from API
      // const response = await orderAPI.getByCustomer(user.id);
      // setOrders(response.data);
      
      // Placeholder data
      setOrders([]);
    } catch (error) {
      console.error('Error loading orders:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="my-account-page">
      <div className="container py-5">
        <h1 className="mb-4">My Orders</h1>
        
        {loading ? (
          <div className="text-center py-5">
            <div className="spinner-border" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
          </div>
        ) : orders.length === 0 ? (
          <div className="card">
            <div className="card-body text-center py-5">
              <h3>No orders yet</h3>
              <p>You haven't placed any orders yet.</p>
              <button
                className="btn btn-primary mt-3"
                onClick={() => navigate('/products')}
              >
                Start Shopping
              </button>
            </div>
          </div>
        ) : (
          <div className="orders-list">
            {orders.map((order) => (
              <div key={order.id} className="card mb-3">
                <div className="card-body">
                  <div className="row">
                    <div className="col-md-8">
                      <h5>Order #{order.orderNo}</h5>
                      <p className="text-muted mb-2">
                        Placed on: {new Date(order.orderDate).toLocaleDateString()}
                      </p>
                      <p className="mb-2">
                        <strong>Status:</strong>{' '}
                        <span className={`badge ${
                          order.status === 'Delivered' ? 'bg-success' :
                          order.status === 'Processing' ? 'bg-info' :
                          order.status === 'Cancelled' ? 'bg-danger' :
                          'bg-warning'
                        }`}>
                          {order.status}
                        </span>
                      </p>
                      <p className="mb-0">
                        <strong>Total:</strong> ₹ {order.totalAmount}
                      </p>
                    </div>
                    <div className="col-md-4 text-end">
                      <button
                        className="btn btn-outline-primary"
                        onClick={() => navigate(`/order/${order.id}`)}
                      >
                        View Details
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}

export default MyAccountPage;
