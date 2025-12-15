import { useState } from 'react';
import Swal from 'sweetalert2';

function ContactPage() {
  const [formData, setFormData] = useState({
    name: '',
    phone: '',
    email: '',
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
    
    if (formData.name && formData.phone && formData.email && formData.message) {
      // TODO: Implement contact form submission
      Swal.fire({
        title: 'Thank You!',
        text: 'We will contact you soon.',
        icon: 'success',
        showCancelButton: true,
        showConfirmButton: false,
        cancelButtonText: 'OK'
      });
      
      setFormData({ name: '', phone: '', email: '', message: '' });
    }
  };

  return (
    <div className="contact-page">
      <div className="container py-5">
        <h1 className="mb-4">Contact Us</h1>
        
        <div className="row">
          <div className="col-md-6">
            <div className="card">
              <div className="card-body">
                <h3>Send us a message</h3>
                <form onSubmit={handleSubmit}>
                  <div className="mb-3">
                    <label className="form-label">Name</label>
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
                    <label className="form-label">Phone</label>
                    <input
                      type="tel"
                      className="form-control"
                      name="phone"
                      value={formData.phone}
                      onChange={handleChange}
                      required
                    />
                  </div>
                  
                  <div className="mb-3">
                    <label className="form-label">Email</label>
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
                    <label className="form-label">Message</label>
                    <textarea
                      className="form-control"
                      name="message"
                      rows="4"
                      value={formData.message}
                      onChange={handleChange}
                      required
                    ></textarea>
                  </div>
                  
                  <button type="submit" className="btn btn-primary w-100">
                    Send Message
                  </button>
                </form>
              </div>
            </div>
          </div>
          
          <div className="col-md-6">
            <div className="card">
              <div className="card-body">
                <h3>Contact Information</h3>
                
                <div className="mb-4">
                  <h5>Email</h5>
                  <p>info@parisbakery.in</p>
                </div>
                
                <div className="mb-4">
                  <h5>Phone</h5>
                  <p>9600128966</p>
                  <p>9600128965</p>
                </div>
                
                <div className="mb-4">
                  <h5>Business Hours</h5>
                  <p>Open Daily: 9:00 AM - 9:00 PM</p>
                </div>
                
                <div>
                  <h5>Customer Care</h5>
                  <p>For inquiries, please email us or call during business hours.</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ContactPage;
