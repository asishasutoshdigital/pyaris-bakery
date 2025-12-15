import { Link } from 'react-router-dom';

function Footer() {
  return (
    <footer className="footer bg-dark text-light py-4 mt-5">
      <div className="container">
        <div className="row">
          <div className="col-md-4">
            <h5>Paris Bakery</h5>
            <p>Your favorite bakery for cakes, pastries, and more!</p>
          </div>
          <div className="col-md-4">
            <h5>Quick Links</h5>
            <ul className="list-unstyled">
              <li><Link to="/" className="text-light">Home</Link></li>
              <li><Link to="/products" className="text-light">Products</Link></li>
              <li><Link to="/about" className="text-light">About Us</Link></li>
              <li><Link to="/contact" className="text-light">Contact</Link></li>
            </ul>
          </div>
          <div className="col-md-4">
            <h5>Contact Info</h5>
            <p>
              Email: info@parisbakery.in<br />
              Phone: 9600128966
            </p>
          </div>
        </div>
        <hr className="bg-light" />
        <div className="text-center">
          <p className="mb-0">&copy; 2024 Paris Bakery. All rights reserved.</p>
        </div>
      </div>
    </footer>
  );
}

export default Footer;
