import { useNavigate } from 'react-router-dom';

function OnlinePage() {
  const navigate = useNavigate();

  return (
    <div className="container py-5">
      <h1 className="mb-4">Online Ordering</h1>
      <div className="card">
        <div className="card-body">
          <p className="lead">Welcome to our online ordering system</p>
          <button className="btn btn-primary me-2" onClick={() => navigate('/products')}>
            Browse Products
          </button>
          <button className="btn btn-outline-primary" onClick={() => navigate('/cart')}>
            View Cart
          </button>
        </div>
      </div>
    </div>
  );
}

export default OnlinePage;
