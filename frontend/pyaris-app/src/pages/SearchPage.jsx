import { useState, useEffect } from 'react';
import { useSearchParams, useNavigate } from 'react-router-dom';
import { productAPI } from '../services/api';
import { useCartStore } from '../store/useStore';
import Swal from 'sweetalert2';

function SearchPage() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const [searchQuery, setSearchQuery] = useState(searchParams.get('q') || '');
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(false);
  const addToCart = useCartStore((state) => state.addToCart);

  useEffect(() => {
    if (searchQuery) {
      handleSearch();
    }
  }, [searchQuery]);

  const handleSearch = async () => {
    if (!searchQuery.trim()) return;
    
    setLoading(true);
    try {
      // TODO: Implement search API
      // For now, get all products and filter client-side
      const response = await productAPI.getAll();
      const filtered = response.data.filter(product =>
        product.menuName.toLowerCase().includes(searchQuery.toLowerCase()) ||
        product.group.toLowerCase().includes(searchQuery.toLowerCase()) ||
        product.subGroup.toLowerCase().includes(searchQuery.toLowerCase())
      );
      setProducts(filtered);
    } catch (error) {
      console.error('Error searching products:', error);
      Swal.fire('Error', 'Failed to search products', 'error');
    } finally {
      setLoading(false);
    }
  };

  const handleAddToCart = (product) => {
    addToCart(product);
    Swal.fire({
      title: 'Added to Cart!',
      text: `${product.menuName} has been added to your cart`,
      icon: 'success',
      timer: 1500,
      showConfirmButton: false
    });
  };

  const handleSearchSubmit = (e) => {
    e.preventDefault();
    handleSearch();
  };

  return (
    <div className="search-page">
      <div className="container py-4">
        <h1 className="mb-4">Search Products</h1>
        
        <form onSubmit={handleSearchSubmit} className="mb-4">
          <div className="input-group input-group-lg">
            <input
              type="text"
              className="form-control"
              placeholder="Search for cakes, pastries..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
            />
            <button className="btn btn-primary" type="submit">
              <i className="bi bi-search"></i> Search
            </button>
          </div>
        </form>

        {loading ? (
          <div className="text-center py-5">
            <div className="spinner-border" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
          </div>
        ) : (
          <>
            {searchQuery && (
              <p className="mb-3">
                {products.length} result{products.length !== 1 ? 's' : ''} for "{searchQuery}"
              </p>
            )}
            
            <div className="row">
              {products.length === 0 && searchQuery ? (
                <div className="col-12 text-center py-5">
                  <h3>No products found</h3>
                  <p>Try searching with different keywords</p>
                </div>
              ) : (
                products.map((product) => (
                  <div key={product.id} className="col-md-3 col-sm-6 mb-4">
                    <div className="card h-100">
                      <div className="card-body">
                        <div className="veg-symbol mb-2"></div>
                        <h5 className="card-title">{product.menuName}</h5>
                        <p className="card-text">
                          <strong>₹ {product.sellPrice}</strong>
                        </p>
                        {product.active === '0' ? (
                          <p className="text-danger">OUT OF STOCK</p>
                        ) : (
                          <div>
                            <button
                              className="btn btn-primary btn-sm me-2"
                              onClick={() => navigate(`/products/${product.id}`)}
                            >
                              View Details
                            </button>
                            <button
                              className="btn btn-success btn-sm"
                              onClick={() => handleAddToCart(product)}
                            >
                              Add to Cart
                            </button>
                          </div>
                        )}
                      </div>
                    </div>
                  </div>
                ))
              )}
            </div>
          </>
        )}
      </div>
    </div>
  );
}

export default SearchPage;
