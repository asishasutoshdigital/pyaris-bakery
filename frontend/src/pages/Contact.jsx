import { useState } from 'react';

function Contact() {
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

  const handleSubmit = (e) => {
    e.preventDefault();
    
    if (!formData.name || !formData.phone || !formData.email || !formData.message) {
      alert('Please fill all required fields');
      return;
    }

    // TODO: Implement contact form submission
    alert('Thank you! We will contact you soon.');
  };

  return (
    <div id="midpart">
      <div className="container">
        <div className="row">
          <div className="col-md-12">
            <div className="sectionTitle">
              Contact Us
            </div>
            <div className="sectionContent">
              <div className="row">
                <div className="col-md-6">
                  <h3>Get in Touch</h3>
                  <p>We'd love to hear from you. Send us a message and we'll respond as soon as possible.</p>
                  
                  <form onSubmit={handleSubmit}>
                    <div className="form-group">
                      <label>Name *</label>
                      <input
                        type="text"
                        name="name"
                        className="form-control"
                        placeholder="Your Name"
                        value={formData.name}
                        onChange={handleChange}
                      />
                    </div>
                    
                    <div className="form-group">
                      <label>Phone *</label>
                      <input
                        type="tel"
                        name="phone"
                        className="form-control"
                        placeholder="Your Phone"
                        value={formData.phone}
                        onChange={handleChange}
                      />
                    </div>
                    
                    <div className="form-group">
                      <label>Email *</label>
                      <input
                        type="email"
                        name="email"
                        className="form-control"
                        placeholder="Your Email"
                        value={formData.email}
                        onChange={handleChange}
                      />
                    </div>
                    
                    <div className="form-group">
                      <label>Message *</label>
                      <textarea
                        name="message"
                        className="form-control"
                        rows="5"
                        placeholder="Your Message"
                        value={formData.message}
                        onChange={handleChange}
                      />
                    </div>
                    
                    <button type="submit" className="primary-btn">
                      SEND MESSAGE
                    </button>
                  </form>
                </div>
                
                <div className="col-md-6">
                  <h3>Contact Information</h3>
                  <p><strong>Address:</strong><br />
                  F/3, Chandaka IE, Chandrasekharpur<br />
                  Bhubaneswar, Odisha 751024</p>
                  
                  <p><strong>Phone:</strong><br />
                  +91-8018114444</p>
                  
                  <p><strong>Email:</strong><br />
                  info@parisbakery.in</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Contact;
