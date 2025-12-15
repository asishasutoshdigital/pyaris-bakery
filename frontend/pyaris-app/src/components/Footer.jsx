import { Link } from 'react-router-dom';

function Footer() {
  return (
    <footer className="footer bg-dark text-light py-4 mt-5">
      <div className="container">
        <div className="row">
          <div className="col-md-3">
            <h5>Paris Bakery</h5>
            <p>Your favorite bakery for cakes, pastries, and more!</p>
          </div>
          <div className="col-md-3">
            <h5>Quick Links</h5>
            <ul className="list-unstyled">
              <li><Link to="/" className="text-light text-decoration-none">Home</Link></li>
              <li><Link to="/products" className="text-light text-decoration-none">Products</Link></li>
              <li><Link to="/about" className="text-light text-decoration-none">About Us</Link></li>
              <li><Link to="/contact" className="text-light text-decoration-none">Contact</Link></li>
              <li><Link to="/gallery" className="text-light text-decoration-none">Gallery</Link></li>
              <li><Link to="/franchise" className="text-light text-decoration-none">Franchise</Link></li>
            </ul>
          </div>
          <div className="col-md-3">
            <h5>Policies</h5>
            <ul className="list-unstyled">
              <li><Link to="/privacy" className="text-light text-decoration-none">Privacy Policy</Link></li>
              <li><Link to="/terms" className="text-light text-decoration-none">Terms & Conditions</Link></li>
              <li><Link to="/refund" className="text-light text-decoration-none">Refund Policy</Link></li>
            </ul>
          </div>
          <div className="col-md-3">
            <h5>Contact Info</h5>
            <p className="mb-1">
              <i className="bi bi-envelope"></i> info@parisbakery.in
            </p>
            <p className="mb-1">
              <i className="bi bi-telephone"></i> 9600128966
            </p>
            <p className="mb-1">
              <i className="bi bi-telephone"></i> 9600128965
            </p>
            <div className="mt-3">
              <h6>Follow Us</h6>
              <a href="#" className="text-light me-3"><i className="bi bi-facebook"></i></a>
              <a href="#" className="text-light me-3"><i className="bi bi-instagram"></i></a>
              <a href="#" className="text-light me-3"><i className="bi bi-twitter"></i></a>
            </div>
          </div>
        </div>
        <hr className="bg-light mt-4" />
        <div className="text-center">
          <p className="mb-0">&copy; 2024 Paris Bakery. All rights reserved.</p>
        </div>
      </div>
    </footer>
  );
}

export default Footer;
