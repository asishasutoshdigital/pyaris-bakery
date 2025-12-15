import { Link } from 'react-router-dom';

function Footer() {
  return (
    <footer className="footer-main container-fluid no-padding">
      <div className="footer-main-container row">
        <div className="col-md-3">
          <div className="footerHeader">Quick Links</div>
          <div className="footerText">
            <Link to="/about" className="fg">About Us</Link>
          </div>
          <div className="footerText">
            <Link to="/privacy" className="fg">Privacy Policy</Link>
          </div>
          <div className="footerText">
            <Link to="/terms" className="fg">Terms & Conditions</Link>
          </div>
        </div>
        
        <div className="col-md-3">
          <div className="footerHeader">Help</div>
          <div className="footerText">
            <Link to="/refund" className="fg">Refund & Cancellation</Link>
          </div>
          <div className="footerText">
            <Link to="/contact" className="fg">Contact Us</Link>
          </div>
        </div>

        <div className="col-md-4" style={{display: 'none'}}>
          <div className="footerHeader">Download App</div>
          <div className="playstoreLink">
            <a href="https://play.google.com/store/apps/details?id=com.pyaris.pyarisbakery" target="_blank" rel="noopener noreferrer">
              <img className="navbar-app-link" src="/images/webgoogleplay.png" alt="Download Android App"/>
            </a>
          </div>
        </div>

        <div className="col-md-2">
          <div className="footerHeader">We are Social</div>
          <div className="fbIconContainer">
            <a className="fbImageSize" href="https://www.facebook.com/myparisbakery/" target="_blank" rel="noopener noreferrer" aria-label="facebook"></a>
          </div>
          <div className="instaIconContainer">
            <a className="instaImageSize" href="https://www.instagram.com/helloparisbakery/" target="_blank" rel="noopener noreferrer" aria-label="instagram"></a>
          </div>
          <div className="linkIconContainer">
            <a className="linkImageSize" href="https://www.linkedin.com/company/paris-bakery-pvt-ltd/" target="_blank" rel="noopener noreferrer" aria-label="linkedin"></a>
          </div>
        </div>

        <div className="col-md-4">
          <div className="footerHeader">Contact Us</div>
          <div className="footerText">
            F/3, Chandaka IE, Chandrasekharpur
          </div>
          <div className="footerText">
            Bhubaneswar, Odisha 751024
          </div>
          <div className="footerText">
            <a href="tel:+918018114444" className="fg">+91-8018114444</a>
          </div>
          <div className="footerText">
            <a href="mailto:info@parisbakery.in" className="fg">info@parisbakery.in</a>
          </div>
        </div>
      </div>

      <div className="footer-bottom-main container-fluid no-padding">
        <div className="footer-bottom-container row">
          <div className="col-md-12">
            <div className="footer-copyright">
              &copy; 2024 Paris Bakery. All Rights Reserved.
            </div>
          </div>
        </div>
      </div>

      {/* WhatsApp Icon */}
      <div className="whatsapp-icon">
        <Link to="/whatsapp" target="_blank">
          <img alt="Send to Whatsapp" title="Send to Whatsapp" src="/images/whatsapp-icon.png" width="100%" height="100%"/>
        </Link>
      </div>
    </footer>
  );
}

export default Footer;
