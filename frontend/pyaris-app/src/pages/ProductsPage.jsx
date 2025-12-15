import { useEffect, useState } from 'react';
import { useSearchParams, useNavigate } from 'react-router-dom';
import { productAPI } from '../services/api';
import { useCartStore } from '../store/useStore';
import Swal from 'sweetalert2';

function ProductsPage() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [groups, setGroups] = useState([]);
  const [subGroups, setSubGroups] = useState([]);
  const [selectedGroup, setSelectedGroup] = useState(searchParams.get('group') || '');
  const [selectedSubGroup, setSelectedSubGroup] = useState(searchParams.get('subgroup') || '');
  
  const addToCart = useCartStore((state) => state.addToCart);

  useEffect(() => {
    loadGroups();
  }, []);

  useEffect(() => {
    if (selectedGroup) {
      loadSubGroups(selectedGroup);
    }
  }, [selectedGroup]);

  useEffect(() => {
    loadProducts();
  }, [selectedGroup, selectedSubGroup]);

  const loadGroups = async () => {
    try {
      const response = await productAPI.getGroups();
      setGroups(response.data);
    } catch (error) {
      console.error('Error loading groups:', error);
    }
  };

  const loadSubGroups = async (group) => {
    try {
      const response = await productAPI.getSubGroups(group);
      setSubGroups(response.data);
    } catch (error) {
      console.error('Error loading subgroups:', error);
    }
  };

  const loadProducts = async () => {
    setLoading(true);
    try {
      let response;
      if (selectedSubGroup) {
        response = await productAPI.getBySubgroup(selectedSubGroup, {
          grp: selectedGroup || null
        });
      } else {
        response = await productAPI.getAll({
          group: selectedGroup || null
        });
      }
      setProducts(response.data);
    } catch (error) {
      console.error('Error loading products:', error);
      Swal.fire('Error', 'Failed to load products', 'error');
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

  const handleGroupChange = (e) => {
    setSelectedGroup(e.target.value);
    setSelectedSubGroup('');
  };

  const handleSubGroupChange = (e) => {
    setSelectedSubGroup(e.target.value);
  };

  return (
    <div className="products-page">
      <div className="container py-4">
        <h1 className="mb-4">Our Products</h1>

        {/* Filters */}
        <div className="row mb-4">
          <div className="col-md-6">
            <label className="form-label">Filter by Category</label>
            <select
              className="form-select"
              value={selectedGroup}
              onChange={handleGroupChange}
            >
              <option value="">All Categories</option>
              {groups.map((group, index) => (
                <option key={index} value={group}>
                  {group}
                </option>
              ))}
            </select>
          </div>
          {selectedGroup && (
            <div className="col-md-6">
              <label className="form-label">Filter by Sub-Category</label>
              <select
                className="form-select"
                value={selectedSubGroup}
                onChange={handleSubGroupChange}
              >
                <option value="">All Sub-Categories</option>
                {subGroups.map((subGroup, index) => (
                  <option key={index} value={subGroup}>
                    {subGroup}
                  </option>
                ))}
              </select>
            </div>
          )}
        </div>

        {/* Products Grid */}
        {loading ? (
          <div className="text-center py-5">
            <div className="spinner-border" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
          </div>
        ) : (
          <div className="row">
            {products.length === 0 ? (
              <div className="col-12 text-center py-5">
                <h3>NO ITEMS FOUND )-:</h3>
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
        )}
      </div>
    </div>
  );
}

export default ProductsPage;
