import { useState, useEffect } from 'react';
import { useUserStore } from '../store/useStore';
import { useNavigate } from 'react-router-dom';
import Swal from 'sweetalert2';

function MyProfilePage() {
  const navigate = useNavigate();
  const { user, isAuthenticated } = useUserStore();
  const [formData, setFormData] = useState({
    mobileNo: '',
    name: '',
    email: '',
    address1: '',
    address2: '',
    city: '',
    pincode: ''
  });

  useEffect(() => {
    if (!isAuthenticated) {
      navigate('/login?redirect=profile');
      return;
    }

    // TODO: Load user profile data from API
    if (user) {
      setFormData({
        mobileNo: user.mobileNo || '',
        name: user.name || '',
        email: user.email || '',
        address1: user.address1 || '',
        address2: user.address2 || '',
        city: user.city || '',
        pincode: user.pincode || ''
      });
    }
  }, [isAuthenticated, navigate, user]);

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    try {
      // TODO: Implement profile update API call
      // await customerAPI.updateProfile(user.id, formData);
      
      Swal.fire({
        title: 'Success!',
        text: 'Profile updated successfully',
        icon: 'success'
      });
    } catch (error) {
      Swal.fire({
        title: 'Error',
        text: 'Failed to update profile',
        icon: 'error'
      });
    }
  };

  return (
    <div className="my-profile-page">
      <div className="container py-5">
        <h1 className="mb-4">My Profile</h1>
        
        <div className="row">
          <div className="col-md-8">
            <div className="card">
              <div className="card-body">
                <form onSubmit={handleSubmit}>
                  <div className="mb-3">
                    <label className="form-label">Mobile Number</label>
                    <input
                      type="text"
                      className="form-control"
                      name="mobileNo"
                      value={formData.mobileNo}
                      onChange={handleChange}
                      disabled
                    />
                  </div>
                  
                  <div className="mb-3">
                    <label className="form-label">Name *</label>
                    <input
                      type="text"
                      className="form-control"
                      name="name"
                      value={formData.name}
                      onChange={handleChange}
                      required
                    />
                  </div>
                  
                  <div className="mb-3">
                    <label className="form-label">Email *</label>
                    <input
                      type="email"
                      className="form-control"
                      name="email"
                      value={formData.email}
                      onChange={handleChange}
                      required
                    />
                  </div>
                  
                  <div className="mb-3">
                    <label className="form-label">Address Line 1 *</label>
                    <input
                      type="text"
                      className="form-control"
                      name="address1"
                      value={formData.address1}
                      onChange={handleChange}
                      required
                    />
                  </div>
                  
                  <div className="mb-3">
                    <label className="form-label">Address Line 2</label>
                    <input
                      type="text"
                      className="form-control"
                      name="address2"
                      value={formData.address2}
                      onChange={handleChange}
                    />
                  </div>
                  
                  <div className="row">
                    <div className="col-md-6 mb-3">
                      <label className="form-label">City *</label>
                      <input
                        type="text"
                        className="form-control"
                        name="city"
                        value={formData.city}
                        onChange={handleChange}
                        required
                      />
                    </div>
                    <div className="col-md-6 mb-3">
                      <label className="form-label">Pincode *</label>
                      <input
                        type="text"
                        className="form-control"
                        name="pincode"
                        value={formData.pincode}
                        onChange={handleChange}
                        required
                      />
                    </div>
                  </div>
                  
                  <button type="submit" className="btn btn-primary">
                    Update Profile
                  </button>
                </form>
              </div>
            </div>
          </div>
          
          <div className="col-md-4">
            <div className="card">
              <div className="card-body">
                <h5>Quick Links</h5>
                <div className="d-grid gap-2">
                  <button
                    className="btn btn-outline-primary"
                    onClick={() => navigate('/my-account')}
                  >
                    My Orders
                  </button>
                  <button
                    className="btn btn-outline-primary"
                    onClick={() => navigate('/rewards')}
                  >
                    My Rewards
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default MyProfilePage;
