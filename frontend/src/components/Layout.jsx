import { Outlet, Link } from 'react-router-dom';
import { useEffect } from 'react';

function Layout() {
  useEffect(() => {
    // Google Analytics
    window.dataLayer = window.dataLayer || [];
    function gtag() { window.dataLayer.push(arguments); }
    gtag('js', new Date());
    gtag('config', 'G-WGNXTXN4NG');
  }, []);

  return (
    <>
      <header className="header-main container-fluid no-padding">
        <div className="menu-block container-fluid no-padding">
          <div className="container-fluid navbarContainer">
            <nav className="navbar ow-navigation">
              <div className="navbar-header">
                <button type="button" className="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                  <span className="sr-only">Toggle navigation</span>
                  <span className="icon-bar"></span>
                  <span className="icon-bar"></span>
                  <span className="icon-bar"></span>
                </button>
                <Link className="navbar-brand" to="/">
                  <img src="/images/logo-navbar.png" alt="Paris Bakery" title="Paris Bakery" />
                </Link>
              </div>
              <div id="navbar" className="navbar-collapse collapse navbar-right">
                <ul className="nav navbar-nav">
                  <li><Link to="/">HOME</Link></li>
                  <li><Link to="/about">ABOUT US</Link></li>
                  <li><Link to="/gallery">GALLERY</Link></li>
                  <li><Link to="/online">ORDER ONLINE</Link></li>
                  <li><Link to="/contact">CONTACT US</Link></li>
                </ul>
              </div>
            </nav>
          </div>
        </div>
      </header>

      <div id="site-wrapper">
        <Outlet />
      </div>

      <footer className="footer-main container-fluid">
        <div className="footer-bottom">
          <div className="container">
            <div className="row">
              <div className="col-md-12">
                <div className="footer-bottom-text">
                  <p>&copy; 2024 Paris Bakery. All rights reserved.</p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </footer>
    </>
  );
}

export default Layout;
