import { useState } from 'react';
import Swal from 'sweetalert2';

function FranchisePage() {
  const [formData, setFormData] = useState({
    fullName: '',
    age: '',
    phone: '',
    email: '',
    occupation: '',
    annualIncome: '',
    address: '',
    city: '',
    state: '',
    pincode: '',
    loan: '',
    loanAmount: '',
    cibilScore: '',
    businessExp: '',
    businessType: '',
    preferredLocation: '',
    ownProperty: '',
    leasePlan: '',
    propertySize: '',
    budget: '',
    comments: ''
  });

  const [message, setMessage] = useState('');

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const allowOnlyNumbers = (e) => {
    const charCode = e.which ? e.which : e.keyCode;
    if (charCode < 48 || charCode > 57) {
      e.preventDefault();
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    try {
      // TODO: Implement franchise inquiry submission to backend API
      setMessage('Thank you for your interest! We will contact you soon.');
      setTimeout(() => setMessage(''), 10000);
      
      Swal.fire({
        title: 'Thank You!',
        text: 'We have received your franchise inquiry. Our team will contact you soon.',
        icon: 'success'
      });
      
      // Reset form
      setFormData({
        fullName: '',
        age: '',
        phone: '',
        email: '',
        occupation: '',
        annualIncome: '',
        address: '',
        city: '',
        state: '',
        pincode: '',
        loan: '',
        loanAmount: '',
        cibilScore: '',
        businessExp: '',
        businessType: '',
        preferredLocation: '',
        ownProperty: '',
        leasePlan: '',
        propertySize: '',
        budget: '',
        comments: ''
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
    <div className="custom-form">
      <style>{`
        .custom-form h2 {
          color: #d63384;
          text-align: center;
          font-size: 32px;
          margin-bottom: 10px;
        }

        .custom-form .intro {
          text-align: center;
          margin-bottom: 40px;
          color: #555;
          font-size: 16px;
        }

        .custom-form .form-section {
          background-color: #ffffff;
          padding: 35px;
          border-radius: 18px;
          max-width: 900px;
          margin: auto;
          box-shadow: 0 8px 24px rgba(0, 0, 0, 0.08);
          transition: all 0.3s ease;
        }

        .custom-form .section-title {
          color: #d63384;
          font-size: 17px;
          border-left: 5px solid #d63384;
          padding-left: 10px;
          margin-top: 25px;
          margin-bottom: 20px;
        }

        .custom-form .form-row {
          display: flex;
          gap: 20px;
          flex-wrap: wrap;
        }

        .custom-form .form-col {
          flex: 1;
          min-width: 280px;
        }

        .custom-form label {
          display: block;
          margin-bottom: 6px;
          font-weight: 600;
          color: #444;
        }

        .custom-form input[type="text"],
        .custom-form input[type="number"],
        .custom-form input[type="email"],
        .custom-form input[type="tel"],
        .custom-form select,
        .custom-form textarea,
        .custom-form .aspnet-control {
          width: 100%;
          padding: 12px 16px;
          font-size: 15px;
          border: 1px solid #ccc;
          border-radius: 10px;
          margin-bottom: 2px;
          transition: border-color 0.2s ease;
        }

        .custom-form input:focus,
        .custom-form select:focus,
        .custom-form textarea:focus {
          border-color: #d63384;
          outline: none;
          box-shadow: 0 0 0 2px rgba(214, 51, 132, 0.15);
        }

        .custom-form textarea {
          resize: vertical;
          min-height: 80px;
        }

        .custom-form .btn-submit {
          background-color: #d63384;
          margin-top: 10px;
          color: white;
          padding: 14px 28px;
          font-size: 16px;
          border: none;
          border-radius: 10px;
          cursor: pointer;
          width: 100%;
          transition: background-color 0.3s ease, transform 0.2s;
        }

        .custom-form .btn-submit:hover {
          background-color: #bd246e;
          transform: translateY(-2px);
        }

        @media (max-width: 600px) {
          .custom-form .form-row {
            flex-direction: column;
          }
        }

        .custom-form .message-label {
          margin-top: 15px;
          display: block;
          font-weight: bold;
          color: green;
        }
      `}</style>

      <h2>Paris Bakery - Franchise Inquiry Form</h2>
      <div className="intro">
        Paris Bakery is one of India's fastest-growing premium bakery chains, delivering joy through handcrafted cakes, pastries, and snacks.
      </div>

      <div className="form-section">
        {message && <div className="message-label">{message}</div>}

        <form onSubmit={handleSubmit}>
          {/* Personal Info */}
          <h3 className="section-title">1. Personal Info</h3>
          <div className="form-row">
            <div className="form-col">
              <label>Full Name <span style={{color:'red'}}>*</span></label>
              <input
                type="text"
                name="fullName"
                className="aspnet-control"
                value={formData.fullName}
                onChange={handleChange}
                required
              />
            </div>
            <div className="form-col">
              <label>Age <span style={{color:'red'}}>*</span></label>
              <input
                type="number"
                name="age"
                className="aspnet-control"
                value={formData.age}
                onChange={handleChange}
                onKeyPress={allowOnlyNumbers}
                min="0"
                required
              />
            </div>
          </div>

          <div className="form-row">
            <div className="form-col">
              <label>Phone Number <span style={{color:'red'}}>*</span></label>
              <input
                type="text"
                name="phone"
                className="aspnet-control"
                value={formData.phone}
                onChange={handleChange}
                onKeyPress={allowOnlyNumbers}
                maxLength="10"
                required
              />
            </div>
            <div className="form-col">
              <label>Email Address <span style={{color:'red'}}>*</span></label>
              <input
                type="email"
                name="email"
                className="aspnet-control"
                value={formData.email}
                onChange={handleChange}
                required
              />
            </div>
          </div>

          <div className="form-row">
            <div className="form-col">
              <label>Occupation <span style={{color:'red'}}>*</span></label>
              <input
                type="text"
                name="occupation"
                className="aspnet-control"
                value={formData.occupation}
                onChange={handleChange}
                required
              />
            </div>
            <div className="form-col">
              <label>Annual Income <span style={{color:'red'}}>*</span></label>
              <select
                name="annualIncome"
                className="aspnet-control"
                value={formData.annualIncome}
                onChange={handleChange}
                required
              >
                <option value="">--Select--</option>
                <option value="Below 10 Lakhs">Below 10 Lakhs</option>
                <option value="10 to 20 Lakhs">10 to 20 Lakhs</option>
                <option value="Above 10 Lakhs">Above 10 Lakhs</option>
              </select>
            </div>
          </div>

          {/* Location */}
          <h3 className="section-title">2. Location</h3>
          
          <label>Street Address <span style={{color:'red'}}>*</span></label>
          <input
            type="text"
            name="address"
            className="aspnet-control"
            value={formData.address}
            onChange={handleChange}
            required
          />
          
          <div className="form-row">
            <div className="form-col">
              <label>City <span style={{color:'red'}}>*</span></label>
              <input
                type="text"
                name="city"
                className="aspnet-control"
                value={formData.city}
                onChange={handleChange}
                required
              />
            </div>
            <div className="form-col">
              <label>State <span style={{color:'red'}}>*</span></label>
              <input
                type="text"
                name="state"
                className="aspnet-control"
                value={formData.state}
                onChange={handleChange}
                required
              />
            </div>
          </div>
          
          <label>Pin Code <span style={{color:'red'}}>*</span></label>
          <input
            type="text"
            name="pincode"
            className="aspnet-control"
            value={formData.pincode}
            onChange={handleChange}
            onKeyPress={allowOnlyNumbers}
            maxLength="6"
            required
          />

          {/* Loan Details */}
          <h3 className="section-title">3. Loan</h3>
          
          <label>Loan(if any)</label>
          <input
            type="text"
            name="loan"
            className="aspnet-control"
            value={formData.loan}
            onChange={handleChange}
          />
          
          <label>Loan Amount (INR)</label>
          <input
            type="text"
            name="loanAmount"
            className="aspnet-control"
            value={formData.loanAmount}
            onChange={handleChange}
            onKeyPress={allowOnlyNumbers}
          />
          
          <label>CIBIL Score</label>
          <input
            type="text"
            name="cibilScore"
            className="aspnet-control"
            value={formData.cibilScore}
            onChange={handleChange}
            onKeyPress={allowOnlyNumbers}
            maxLength="3"
          />

          {/* Investment */}
          <h3 className="section-title">4. Investment</h3>

          <label>Have you operated any business before? <span style={{color:'red'}}>*</span></label>
          <select
            name="businessExp"
            className="aspnet-control"
            value={formData.businessExp}
            onChange={handleChange}
            required
          >
            <option value="">--Select--</option>
            <option value="Yes">Yes</option>
            <option value="No">No</option>
          </select>

          <label>If Yes, please specify</label>
          <input
            type="text"
            name="businessType"
            className="aspnet-control"
            value={formData.businessType}
            onChange={handleChange}
          />

          <label>Preferred Franchise Location <span style={{color:'red'}}>*</span></label>
          <input
            type="text"
            name="preferredLocation"
            className="aspnet-control"
            value={formData.preferredLocation}
            onChange={handleChange}
            required
          />

          <div className="form-row">
            <div className="form-col">
              <label>Do you own the property? <span style={{color:'red'}}>*</span></label>
              <select
                name="ownProperty"
                className="aspnet-control"
                value={formData.ownProperty}
                onChange={handleChange}
              >
                <option value="">--Select--</option>
                <option value="Yes">Yes</option>
                <option value="No">No</option>
              </select>
            </div>
            <div className="form-col">
              <label>If not, are you planning to lease?</label>
              <select
                name="leasePlan"
                className="aspnet-control"
                value={formData.leasePlan}
                onChange={handleChange}
              >
                <option value="">--Select--</option>
                <option value="Yes">Yes</option>
                <option value="No">No</option>
              </select>
            </div>
          </div>

          <label>Property Size (sq. ft.)</label>
          <input
            type="number"
            name="propertySize"
            className="aspnet-control"
            value={formData.propertySize}
            onChange={handleChange}
            onKeyPress={allowOnlyNumbers}
            min="0"
          />

          <label>Approximate Budget for Investment (INR) <span style={{color:'red'}}>*</span></label>
          <select
            name="budget"
            className="aspnet-control"
            value={formData.budget}
            onChange={handleChange}
            required
          >
            <option value="">--Select--</option>
            <option value="10 to 15 Lakhs">₹10 – ₹15 Lakhs</option>
            <option value="15 to 20 Lakhs">₹15 – ₹20 Lakhs</option>
            <option value="Above 20 Lakhs">₹20 Lakhs and above</option>
          </select>

          <label>Additional Comments or Questions</label>
          <textarea
            name="comments"
            className="aspnet-control"
            value={formData.comments}
            onChange={handleChange}
            rows="3"
          ></textarea>

          <button type="submit" className="btn-submit">Submit Inquiry</button>
        </form>
      </div>
    </div>
  );
}

export default FranchisePage;
