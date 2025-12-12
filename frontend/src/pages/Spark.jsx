import { useEffect, useState } from 'react';
import { useSearchParams, Link } from 'react-router-dom';
import { catalogAPI } from '../services/api';
import MenuSideBar from '../components/MenuSideBar';

function Spark() {
  const [searchParams] = useSearchParams();
  const [products, setProducts] = useState([]);
  const [categoryTitle, setCategoryTitle] = useState('');
  const [loading, setLoading] = useState(true);

  const subgroup = searchParams.get('xdt');
  const group = searchParams.get('grp');
  const isFlavour = searchParams.get('isFlavour') === 'true';

  useEffect(() => {
    loadProducts();
  }, [subgroup, group, isFlavour]);

  const loadProducts = async () => {
    try {
      setLoading(true);
      const response = await catalogAPI.getProductsByCategory({
        subgroup: subgroup,
        grp: group,
        isFlavour: isFlavour
      });

      setProducts(response.data.products || []);
      setCategoryTitle(response.data.subgroup || '');
    } catch (error) {
      console.error('Error loading products:', error);
      setProducts([]);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container-fluid">
      <div className="row">
        <MenuSideBar />
        <div className="col-md-9">
          <div id="xdata">
            <div className="product-category-title" id="xdisplayheader">
              {categoryTitle}
            </div>
            
            {loading ? (
              <div className="text-center" style={{ padding: '50px' }}>
                <div className="loader">Loading...</div>
              </div>
            ) : products.length > 0 ? (
              <div className="product-listing-container row" id="xfdata">
                {products.map((product) => (
                  <Link
                    key={product.id}
                    className="list-productcard"
                    to={`/sparkdetails?id=${product.id}&isFlavour=${isFlavour}`}
                  >
                    <div className="productcard-image">
                      <img
                        className="lazy"
                        src={product.imagePath}
                        alt={`${product.menuName} - Paris bakery`}
                        className="product-image"
                        width="100%"
                        height="100%"
                      />
                    </div>
                    
                    {product.active === '0' ? (
                      <>
                        <div className="veg-symbol"></div>
                        <div className="out-of-stock-text product-card-title">
                          OUT OF STOCK
                        </div>
                        <div className="product-card-title">{product.menuName}</div>
                        <div className="product-card-price">
                          <img
                            src="/images/svg-icons/rupees.svg"
                            alt="Rs"
                            width="7.5px"
                            height="11.2px"
                          />{' '}
                          {product.sellPrice}
                        </div>
                      </>
                    ) : (
                      <>
                        <div className="rating-box">
                          <div>
                            <span className="star"> ★</span>{' '}
                            <span className="rating-text">
                              {product.rating.toFixed(1)}
                            </span>
                          </div>
                        </div>
                        <div className="veg-symbol"></div>
                        <div className="product-card-title">{product.menuName}</div>
                        <div className="product-card-price">
                          <img
                            src="/images/svg-icons/rupees.svg"
                            alt="Rs"
                            width="7.5px"
                            height="11.2px"
                          />{' '}
                          {product.sellPrice}
                        </div>
                      </>
                    )}
                  </Link>
                ))}
              </div>
            ) : (
              <div className="product-listing-container row">
                <h3>NO ITEMS FOUND )-:</h3>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default Spark;
