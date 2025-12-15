import { useEffect, useState } from 'react';
import { useSearchParams, useNavigate, Link } from 'react-router-dom';
import { productAPI } from '../services/api';

function ProductsPage() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [displayHeader, setDisplayHeader] = useState('');
  
  const group = searchParams.get('group') || '';
  const subgroup = searchParams.get('subgroup') || '';

  useEffect(() => {
    loadProducts();
  }, [group, subgroup]);

  const loadProducts = async () => {
    setLoading(true);
    try {
      let response;
      if (subgroup) {
        response = await productAPI.getBySubgroup(subgroup, { grp: group });
        setDisplayHeader(subgroup);
      } else if (group) {
        response = await productAPI.getByGroup(group);
        setDisplayHeader(group);
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
                      src={product.imagePath || `/menupic/small/${product.id}s.png`}
                      alt={product.menuName}
                      className="img-responsive"
                      onError={(e) => {
                        e.target.src = '/images/svg-icons/img-placeholder.svg';
                      }}
                    />
                  </div>
                  <div className="product-card-title">{product.menuName}</div>
                  <div className="product-card-price">
                    ₹ {product.sellPrice} / {product.unit}
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
