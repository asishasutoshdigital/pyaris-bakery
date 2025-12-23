import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { productAPI } from '../../services/api';

function AdminProductsPage() {
  const navigate = useNavigate();
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    loadProducts();
  }, []);

  const loadProducts = async () => {
    setLoading(true);
    try {
      const response = await productAPI.getAll({ activeOnly: false });
      setProducts(response.data);
    } catch (error) {
      console.error('Error loading products:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="admin-products-page">
      <div className="container-fluid py-4">
        <div className="d-flex justify-content-between align-items-center mb-4">
          <h1>Manage Products</h1>
          <button className="btn btn-primary" onClick={() => navigate('/admin/products/edit/new')}>
            Add New Product
          </button>
        </div>
        {loading ? (
          <div className="text-center py-5">
            <div className="spinner-border"></div>
          </div>
        ) : (
          <div className="card">
            <div className="card-body">
              <div className="table-responsive">
                <table className="table table-hover">
                  <thead>
                    <tr>
                      <th>ID</th><th>Name</th><th>Group</th><th>Price</th><th>Active</th><th>Actions</th>
                    </tr>
                  </thead>
                  <tbody>
                    {products.map((p) => (
                      <tr key={p.id}>
                        <td>{p.id}</td>
                        <td>{p.menuName}</td>
                        <td>{p.group}</td>
                        <td>₹{p.sellPrice}</td>
                        <td><span className={`badge ${p.active === '1' ? 'bg-success' : 'bg-danger'}`}>
                          {p.active === '1' ? 'Active' : 'Inactive'}
                        </span></td>
                        <td>
                          <button className="btn btn-sm btn-primary" onClick={() => navigate(`/admin/products/edit/${p.id}`)}>
                            Edit
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}

export default AdminProductsPage;
