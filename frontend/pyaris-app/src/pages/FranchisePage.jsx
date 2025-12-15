import { useState } from 'react';
import Swal from 'sweetalert2';

function FranchisePage() {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    phone: '',
    city: '',
    investment: '',
    experience: '',
    message: ''
  });

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    try {
      // TODO: Implement franchise inquiry submission
      Swal.fire({
        title: 'Thank You!',
        text: 'We have received your franchise inquiry. Our team will contact you soon.',
        icon: 'success'
      });
      
      setFormData({
        name: '',
        email: '',
        phone: '',
        city: '',
        investment: '',
        experience: '',
        message: ''
      });
    } catch (error) {
      Swal.fire({
        title: 'Error',
        text: 'Failed to submit inquiry. Please try again.',
        icon: 'error'
      });
    }
  };

  return (
    <div className="franchise-page">
      <div className="container py-5">
        <h1 className="mb-4">Franchise Opportunities</h1>
        
        <div className="row mb-5">
          <div className="col-md-6">
            <div className="card h-100">
              <div className="card-body">
                <h3>Why Partner With Us?</h3>
                <ul className="mt-3">
                  <li>Established brand with strong reputation</li>
                  <li>Comprehensive training and support</li>
                  <li>Proven business model</li>
                  <li>Marketing and operational assistance</li>
                  <li>High-quality products and ingredients</li>
                  <li>Growing customer base</li>
                </ul>
              </div>
            </div>
          </div>
          
          <div className="col-md-6">
            <div className="card h-100">
              <div className="card-body">
                <h3>Investment Details</h3>
                <div className="mt-3">
                  <p><strong>Initial Investment:</strong> Contact for details</p>
                  <p><strong>Space Required:</strong> 500-1000 sq ft</p>
                  <p><strong>Setup Time:</strong> 2-3 months</p>
                  <p><strong>Franchise Fee:</strong> Contact for details</p>
                  <p><strong>Royalty:</strong> Contact for details</p>
                </div>
              </div>
            </div>
          </div>
        </div>
        
        <div className="card">
          <div className="card-body">
            <h3 className="mb-4">Franchise Inquiry Form</h3>
            <form onSubmit={handleSubmit}>
              <div className="row">
                <div className="col-md-6 mb-3">
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
                
                <div className="col-md-6 mb-3">
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
              </div>
              
              <div className="row">
                <div className="col-md-6 mb-3">
                  <label className="form-label">Phone *</label>
                  <input
                    type="tel"
                    className="form-control"
                    name="phone"
                    value={formData.phone}
                    onChange={handleChange}
                    required
                  />
                </div>
                
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
              </div>
              
              <div className="row">
                <div className="col-md-6 mb-3">
                  <label className="form-label">Investment Capacity</label>
                  <select
                    className="form-select"
                    name="investment"
                    value={formData.investment}
                    onChange={handleChange}
                  >
                    <option value="">Select range</option>
                    <option value="10-20L">₹10-20 Lakhs</option>
                    <option value="20-30L">₹20-30 Lakhs</option>
                    <option value="30-50L">₹30-50 Lakhs</option>
                    <option value="50L+">₹50 Lakhs+</option>
                  </select>
                </div>
                
                <div className="col-md-6 mb-3">
                  <label className="form-label">Business Experience</label>
                  <select
                    className="form-select"
                    name="experience"
                    value={formData.experience}
                    onChange={handleChange}
                  >
                    <option value="">Select experience</option>
                    <option value="none">No business experience</option>
                    <option value="1-3">1-3 years</option>
                    <option value="3-5">3-5 years</option>
                    <option value="5+">5+ years</option>
                  </select>
                </div>
              </div>
              
              <div className="mb-3">
                <label className="form-label">Additional Message</label>
                <textarea
                  className="form-control"
                  name="message"
                  rows="4"
                  value={formData.message}
                  onChange={handleChange}
                ></textarea>
              </div>
              
              <button type="submit" className="btn btn-primary btn-lg">
                Submit Inquiry
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}

export default FranchisePage;
