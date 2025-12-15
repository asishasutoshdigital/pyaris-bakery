import { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { productAPI } from '../../services/api';
import Swal from 'sweetalert2';

function AdminEditProductPage() {
  const navigate = useNavigate();
  const { id } = useParams();
  const isNew = id === 'new';
  const [formData, setFormData] = useState({ menuName: '', group: '', subGroup: '', sellPrice: '', active: '1' });

  useEffect(() => {
    if (!isNew) loadProduct();
  }, [id]);

  const loadProduct = async () => {
    try {
      const response = await productAPI.getById(id);
      setFormData(response.data);
    } catch (error) {
      Swal.fire('Error', 'Failed to load product', 'error');
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    Swal.fire('Success', `Product ${isNew ? 'created' : 'updated'}`, 'success');
    navigate('/admin/products');
  };

  return (
    <div className="container py-4">
      <h1>{isNew ? 'Add' : 'Edit'} Product</h1>
      <div className="card mt-4">
        <div className="card-body">
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label className="form-label">Name *</label>
              <input type="text" className="form-control" value={formData.menuName} onChange={(e) => setFormData({...formData, menuName: e.target.value})} required />
            </div>
            <div className="mb-3">
              <label className="form-label">Group *</label>
              <input type="text" className="form-control" value={formData.group} onChange={(e) => setFormData({...formData, group: e.target.value})} required />
            </div>
            <div className="mb-3">
              <label className="form-label">Price *</label>
              <input type="number" className="form-control" value={formData.sellPrice} onChange={(e) => setFormData({...formData, sellPrice: e.target.value})} required />
            </div>
            <button type="submit" className="btn btn-primary">Save</button>
            <button type="button" className="btn btn-secondary ms-2" onClick={() => navigate('/admin/products')}>Cancel</button>
          </form>
        </div>
      </div>
    </div>
  );
}

export default AdminEditProductPage;
