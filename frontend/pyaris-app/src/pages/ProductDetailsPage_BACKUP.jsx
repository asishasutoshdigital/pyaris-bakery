import { useEffect, useState } from 'react';
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
  
  const addToCart = useCartStore((state) => state.addToCart);

  useEffect(() => {
    loadProduct();
  }, [id]);

  const loadProduct = async () => {
    setLoading(true);
    try {
      const response = await productAPI.getById(id);
      setProduct(response.data);
    } catch (error) {
      console.error('Error loading product:', error);
      Swal.fire('Error', 'Failed to load product details', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleAddToCart = () => {
    if (product) {
      for (let i = 0; i < quantity; i++) {
        addToCart(product);
      }
      Swal.fire({
        title: 'Added to Cart!',
        text: `${quantity} x ${product.menuName} added to cart`,
        icon: 'success',
        timer: 1500,
        showConfirmButton: false
      });
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

  return (
    <div className="product-details-page">
      <div className="container py-4">
        <button className="btn btn-link mb-3" onClick={() => navigate(-1)}>
          ← Back
        </button>
        
        <div className="row">
          <div className="col-md-6">
            <div className="product-image-placeholder bg-light p-5 text-center">
              <h3>{product.menuName}</h3>
            </div>
          </div>
          
          <div className="col-md-6">
            <h1>{product.menuName}</h1>
            <div className="veg-symbol my-3"></div>
            
            <h3 className="text-primary">₹ {product.sellPrice}</h3>
            
            {product.discountValue && product.discountValue !== '0' && (
              <div className="alert alert-success">
                <strong>Discount:</strong> {product.discountType === 'Percentage' ? `${product.discountValue}%` : `₹${product.discountValue}`} OFF
              </div>
            )}
            
            {product.feature1 && (
              <div className="mt-3">
                <h5>Features:</h5>
                <ul>
                  {product.feature1 && <li>{product.feature1}</li>}
                  {product.feature2 && <li>{product.feature2}</li>}
                  {product.feature3 && <li>{product.feature3}</li>}
                  {product.feature4 && <li>{product.feature4}</li>}
                </ul>
              </div>
            )}
            
            <div className="mt-4">
              <label className="form-label">Quantity:</label>
              <div className="input-group" style={{ maxWidth: '150px' }}>
                <button
                  className="btn btn-outline-secondary"
                  onClick={() => setQuantity(Math.max(1, quantity - 1))}
                >
                  -
                </button>
                <input
                  type="number"
                  className="form-control text-center"
                  value={quantity}
                  onChange={(e) => setQuantity(Math.max(1, parseInt(e.target.value) || 1))}
                  min="1"
                />
                <button
                  className="btn btn-outline-secondary"
                  onClick={() => setQuantity(quantity + 1)}
                >
                  +
                </button>
              </div>
            </div>
            
            <div className="mt-4">
              {product.active === '0' ? (
                <div className="alert alert-danger">OUT OF STOCK</div>
              ) : (
                <button
                  className="btn btn-success btn-lg"
                  onClick={handleAddToCart}
                >
                  Add to Cart
                </button>
              )}
            </div>
            
            <div className="mt-3">
              <p className="text-muted">
                <strong>Category:</strong> {product.group} - {product.subGroup}
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ProductDetailsPage;
