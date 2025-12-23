import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { authAPI } from '../services/api';

function RegisterPage() {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    mobile: '',
    password: '',
    confirmPassword: '',
    name: '',
    email: '',
    address1: '',
    address2: '',
    city: '',
    pincode: ''
  });
  const [errors, setErrors] = useState({});
  const [loading, setLoading] = useState(false);

  // Allow only numbers for specific fields
  const allowOnlyNumbers = (e) => {
    const charCode = e.which ? e.which : e.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      e.preventDefault();
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
    // Clear error for this field
    if (errors[name]) {
      setErrors(prev => ({ ...prev, [name]: '' }));
    }
  };

  const validateForm = () => {
    const newErrors = {};
    
    if (!formData.mobile || formData.mobile.length !== 10) {
      newErrors.mobile = 'Mobile number must be 10 digits';
    }
    if (!formData.password || formData.password.length < 6) {
      newErrors.password = 'Password must be at least 6 characters';
    }
    if (formData.password !== formData.confirmPassword) {
      newErrors.confirmPassword = 'Passwords do not match';
    }
    if (!formData.name) {
      newErrors.name = 'Name is required';
    }
    if (!formData.email || !formData.email.includes('@')) {
      newErrors.email = 'Valid email is required';
    }
    if (!formData.address1) {
      newErrors.address1 = 'Address is required';
    }
    if (!formData.city) {
      newErrors.city = 'City is required';
    }
    if (!formData.pincode || formData.pincode.length !== 6) {
      newErrors.pincode = 'Pin code must be 6 digits';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (!validateForm()) {
      return;
    }

    setLoading(true);
    try {
      const response = await authAPI.register({
        mobile: formData.mobile,
        password: formData.password,
        name: formData.name,
        email: formData.email,
        address1: formData.address1,
        address2: formData.address2,
        city: formData.city,
        pincode: formData.pincode
      });

      if (response.data.success) {
        if (window.swal) {
          window.swal('Success!', 'Registration successful! Please login.', 'success');
        } else {
          alert('Registration successful! Please login.');
        }
        navigate('/login');
      } else {
        if (window.swal) {
          window.swal('Error!', response.data.message || 'Registration failed', 'error');
        } else {
          alert(response.data.message || 'Registration failed');
        }
      }
    } catch (error) {
      console.error('Registration error:', error);
      const errorMessage = error.response?.data?.message || 'Registration failed. Please try again.';
      if (window.swal) {
        window.swal('Error!', errorMessage, 'error');
      } else {
        alert(errorMessage);
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div id="midpart">
      <div className="cupcake-candle">
        <img 
          src="/images/new-images/cupcake-candle.jpg" 
          alt="cupcake-background" 
          style={{ width: '70%', height: '70%' }} 
        />
      </div>
      
      <div className="container login-container">
        <div className="row">
          <div className="col-md-12">
            <div className="sectionTitle">Register</div>
          </div>
        </div>

        <form onSubmit={handleSubmit}>
          <div className="row">
            {/* Column 1 */}
            <div className="col-md-6 col-sm-12">
              <div className="form-group">
                <label>
                  Mobile Number <span style={{ color: 'red' }}>*</span>
                </label>
                <input
                  type="text"
                  name="mobile"
                  className="form-control numeric"
                  maxLength="10"
                  value={formData.mobile}
                  onChange={handleChange}
                  onKeyPress={allowOnlyNumbers}
                  placeholder="10 digit mobile number"
                />
                {errors.mobile && <div className="error-text">{errors.mobile}</div>}
              </div>

              <div className="form-group">
                <label>
                  Password <span style={{ color: 'red' }}>*</span>
                </label>
                <input
                  type="password"
                  name="password"
                  className="form-control"
                  value={formData.password}
                  onChange={handleChange}
                  placeholder="Password"
                />
                {errors.password && <div className="error-text">{errors.password}</div>}
              </div>

              <div className="form-group">
                <label>
                  Confirm Password <span style={{ color: 'red' }}>*</span>
                </label>
                <input
                  type="password"
                  name="confirmPassword"
                  className="form-control"
                  value={formData.confirmPassword}
                  onChange={handleChange}
                  placeholder="Confirm Password"
                />
                {errors.confirmPassword && <div className="error-text">{errors.confirmPassword}</div>}
              </div>

              <div className="form-group">
                <label>
                  Name <span style={{ color: 'red' }}>*</span>
                </label>
                <input
                  type="text"
                  name="name"
                  className="form-control"
                  value={formData.name}
                  onChange={handleChange}
                  placeholder="Full Name"
                />
                {errors.name && <div className="error-text">{errors.name}</div>}
              </div>
            </div>

            {/* Column 2 */}
            <div className="col-md-6 col-sm-12">
              <div className="form-group">
                <label>
                  Email <span style={{ color: 'red' }}>*</span>
                </label>
                <input
                  type="email"
                  name="email"
                  className="form-control"
                  value={formData.email}
                  onChange={handleChange}
                  placeholder="Email Address"
                />
                {errors.email && <div className="error-text">{errors.email}</div>}
              </div>

              <div className="form-group">
                <label>
                  Address Line 1 <span style={{ color: 'red' }}>*</span>
                </label>
                <input
                  type="text"
                  name="address1"
                  className="form-control"
                  value={formData.address1}
                  onChange={handleChange}
                  placeholder="Address Line 1"
                />
                {errors.address1 && <div className="error-text">{errors.address1}</div>}
              </div>

              <div className="form-group">
                <label>Address Line 2</label>
                <input
                  type="text"
                  name="address2"
                  className="form-control"
                  value={formData.address2}
                  onChange={handleChange}
                  placeholder="Address Line 2 (Optional)"
                />
              </div>

              <div className="form-group">
                <label>
                  City <span style={{ color: 'red' }}>*</span>
                </label>
                <input
                  type="text"
                  name="city"
                  className="form-control"
                  value={formData.city}
                  onChange={handleChange}
                  placeholder="City"
                />
                {errors.city && <div className="error-text">{errors.city}</div>}
              </div>

              <div className="form-group">
                <label>
                  Pin Code <span style={{ color: 'red' }}>*</span>
                </label>
                <input
                  type="text"
                  name="pincode"
                  className="form-control numeric"
                  maxLength="6"
                  value={formData.pincode}
                  onChange={handleChange}
                  onKeyPress={allowOnlyNumbers}
                  placeholder="6 digit pin code"
                />
                {errors.pincode && <div className="error-text">{errors.pincode}</div>}
              </div>
            </div>
          </div>

          <div className="row">
            <div className="col-md-12">
              <div className="buttonSection">
                <button 
                  type="submit" 
                  className="primary-btn" 
                  disabled={loading}
                >
                  {loading ? 'REGISTERING...' : 'REGISTER'}
                </button>
              </div>
              
              <div className="text-center" style={{ marginTop: '15px' }}>
                <span>Already have an account? </span>
                <Link to="/login" className="forgot-password-link">Login here</Link>
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
}

export default RegisterPage;
