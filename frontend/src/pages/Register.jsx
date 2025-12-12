import { useState, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import axios from 'axios';

function Register() {
  const navigate = useNavigate();
  const location = useLocation();
  const [formData, setFormData] = useState({
    mobileNo: location.state?.mobileNo || '',
    name: '',
    password: '',
    confirmPassword: '',
    email: '',
    address: ''
  });
  const [errorText, setErrorText] = useState('');

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleRegister = async () => {
    setErrorText('');

    if (!formData.mobileNo || !formData.name || !formData.password || !formData.confirmPassword) {
      setErrorText('Please fill all required fields');
      return;
    }

    if (formData.password !== formData.confirmPassword) {
      setErrorText('Passwords do not match');
      return;
    }

    try {
      const response = await axios.post('http://localhost:5000/api/auth/register', {
        mobileNo: formData.mobileNo,
        name: formData.name,
        password: formData.password,
        email: formData.email,
        address: formData.address
      }, { withCredentials: true });

      if (response.data.success) {
        navigate('/');
      }
    } catch (error) {
      if (error.response?.data?.message) {
        setErrorText(error.response.data.message);
      } else {
        setErrorText('Registration failed');
      }
    }
  };

  return (
    <div id="midpart">
      <div className="container login-container">
        <div className="col-md-4 col-sm-6 col-xs-12">
          <div className="sectionTitle">
            Register
          </div>
          <div className="sectionContent">
            <div className="form-group">
              Mobile Number *
            </div>
            <div className="form-group">
              <input
                name="mobileNo"
                className="form-control numeric"
                type="text"
                maxLength="10"
                placeholder="Your Mobile No"
                value={formData.mobileNo}
                onChange={handleChange}
              />
            </div>
            
            <div className="form-group">
              Name *
            </div>
            <div className="form-group">
              <input
                name="name"
                className="form-control"
                type="text"
                placeholder="Your Name"
                value={formData.name}
                onChange={handleChange}
              />
            </div>

            <div className="form-group">
              Password *
            </div>
            <div className="form-group">
              <input
                name="password"
                className="form-control"
                type="password"
                placeholder="Password"
                value={formData.password}
                onChange={handleChange}
              />
            </div>

            <div className="form-group">
              Confirm Password *
            </div>
            <div className="form-group">
              <input
                name="confirmPassword"
                className="form-control"
                type="password"
                placeholder="Confirm Password"
                value={formData.confirmPassword}
                onChange={handleChange}
              />
            </div>

            <div className="form-group">
              Email
            </div>
            <div className="form-group">
              <input
                name="email"
                className="form-control"
                type="email"
                placeholder="Your Email"
                value={formData.email}
                onChange={handleChange}
              />
            </div>

            <div className="form-group">
              Address
            </div>
            <div className="form-group">
              <textarea
                name="address"
                className="form-control"
                placeholder="Your Address"
                value={formData.address}
                onChange={handleChange}
              />
            </div>

            {errorText && <div className="error-text">{errorText}</div>}
          </div>
          
          <div className="buttonSection">
            <button className="primary-btn" onClick={handleRegister}>
              REGISTER
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Register;
