import { useEffect, useState } from 'react';
import { useSearchParams, useNavigate, Link } from 'react-router-dom';
import { productAPI } from '../services/api';

function ProductsPage() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [displayHeader, setDisplayHeader] = useState('');
  
  // Support both old ASPX parameters (xdt, grp) and new parameters (subgroup, group)
  const xdt = searchParams.get('xdt') || searchParams.get('subgroup') || '';
  const grp = searchParams.get('grp') || searchParams.get('group') || '';
  const isFlavour = searchParams.get('isFlavour') === 'true';

  useEffect(() => {
    loadProducts();
  }, [xdt, grp, isFlavour]);

  const loadProducts = async () => {
    setLoading(true);
    try {
      let response;
      if (xdt) {
        // Use xdt (subgroup) as the primary filter
        response = await productAPI.getBySubgroup(xdt, { grp, isFlavour });
        setDisplayHeader(xdt);
      } else if (grp) {
        // Fallback to group filter if only grp is provided
        response = await productAPI.getByGroup(grp);
        setDisplayHeader(grp);
      } else {
        response = await productAPI.getAll();
        setDisplayHeader('All Products');
      }
      setProducts(response.data || []);
    } catch (error) {
      console.error('Error loading products:', error);
      setProducts([]);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div id="xdata">
        <div className="product-category-title">Loading...</div>
      </div>
    );
  }

  return (
    <div id="xdata">
      <div className="product-category-title" id="xdisplayheader">
        {displayHeader}
      </div>
      <div className="product-listing-container row" id="xfdata">
        {products.length === 0 ? (
          <div className="col-md-12 text-center">
            <p>No products found.</p>
          </div>
        ) : (
          products.map((product) => (
            <div key={product.id} className="col-md-3 col-sm-4 col-xs-6 list-productcard">
              <Link to={`/product/${product.id}`} style={{ textDecoration: 'none' }}>
                <div className="productcard">
                  <div className="productcard-image">
                    <img
                      src={`/menupic/small/${product.id}s.png`}
                      alt={`${product.menuName} - Paris bakery`}
                      className="img-responsive product-image"
                      width="100%"
                      height="100%"
                      onError={(e) => {
                        e.target.src = '/images/svg-icons/img-placeholder.svg';
                      }}
                    />
                  </div>
                  <div className="veg-symbol"></div>
                  <div className="product-card-title">{product.menuName}</div>
                  <div className="product-card-price">
                    <img src="/images/svg-icons/rupees.svg" alt="Rs" width="7.5px" height="11.2px" /> {product.sellPrice}
                  </div>
                </div>
              </Link>
            </div>
          ))
        )}
      </div>
    </div>
  );
}

export default ProductsPage;
