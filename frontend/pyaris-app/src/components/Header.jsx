import { Link } from "react-router-dom";
import { useCartStore, useLocationStore } from "../store/useStore";
import { useAuthStore } from "../store/useAuthStore";

function Header() {
  const cartQuantity = useCartStore((state) => state.cartQuantity);

  const user = useAuthStore((state) => state.user);
  const logout = useAuthStore((state) => state.logout);
  const isAuthenticated = !!user;

  const { selectedPincode, selectedArea, openPopup } = useLocationStore();

  const handleLogout = () => {
    logout();
  };

  const handleLocationClick = () => {
    openPopup();
  };

  return (
    <header className="header-main container-fluid no-padding">
      <div className="menu-block container-fluid no-padding">
        {/* Main Navigation */}
        <div className="container-fluid navbarContainer">
          <nav className="navbar ow-navigation">
            <div className="navbar-header">
              <button
                type="button"
                className="navbar-toggle collapsed"
                data-toggle="collapse"
                data-target="#navbar"
                aria-expanded="false"
                aria-controls="navbar"
              >
                <span className="sr-only">Toggle navigation</span>
                <span className="icon-bar"></span>
                <span className="icon-bar"></span>
                <span className="icon-bar"></span>
              </button>
              <Link className="navbar-brand" to="/">
                <img
                  src="/images/logo-navbar.png"
                  alt="Paris Bakery"
                  title="Paris Bakery"
                />
              </Link>

              {/* Pincode dropdown */}
              <div className="pincode-select">
                <div
                  className="custom-dropdown"
                  onClick={handleLocationClick}
                  style={{ cursor: "pointer" }}
                >
                  <label id="areaLabel">{selectedArea || ""}</label>
                  <i className="fas fa-map-marker-alt icon"></i>
                  <input
                    list="pincodeList"
                    id="pincode"
                    name="pincode"
                    readOnly
                    style={{
                      border: "none",
                      outline: "none",
                      cursor: "pointer",
                    }}
                    placeholder="Search locality"
                    value={selectedPincode || ""}
                    onClick={handleLocationClick}
                  />
                </div>
              </div>
            </div>

            <div id="navbar" className="navbar-collapse collapse navbar-right">
              <ul className="nav navbar-nav">
                <li id="TrackOrders">
                  <Link to="/my-account">
                    <img src="/images/svg-icons/track.svg" alt="Track Order" />
                    <span>Track Order</span>
                  </Link>
                </li>

                {isAuthenticated ? (
                  <li id="loginmenu" className="dropdown onlineMenu">
                    <a
                      href="#"
                      className="dropdown-toggle dropdown-switch"
                      role="button"
                      aria-haspopup="true"
                      aria-expanded="false"
                      onClick={(e) => e.preventDefault()}
                    >
                      <img src="/images/svg-icons/user.svg" alt="My Account" />
                      <span>{user?.name} | My Account</span>
                    </a>

                    <ul className="dropdown-menu">
                      <li>
                        <Link to="/my-account">
                          <span>My Orders</span>
                        </Link>
                      </li>
                      <li>
                        <Link to="/rewards">
                          <span>My Payback Points</span>
                        </Link>
                      </li>
                      <li>
                        <Link to="/profile">
                          <span>My Profile</span>
                        </Link>
                      </li>

                      {user?.isAdmin && (
                        <>
                          <li>
                            <Link to="/admin/store-orders">
                              <span>Store Orders</span>
                            </Link>
                          </li>
                          <li>
                            <Link to="/admin/pending-orders">
                              <span>Pending Orders</span>
                            </Link>
                          </li>
                          <li>
                            <Link to="/admin/products">
                              <span>Products</span>
                            </Link>
                          </li>
                          <li>
                            <Link to="/admin/update-banner">
                              <span>Update Banner</span>
                            </Link>
                          </li>
                          <li>
                            <Link to="/admin/customers">
                              <span>Customers</span>
                            </Link>
                          </li>
                          <li>
                            <Link to="/admin/service-report">
                              <span>Service Report</span>
                            </Link>
                          </li>
                        </>
                      )}

                      <li>
                        <a href="#" onClick={handleLogout}>
                          <span>Logout</span>
                        </a>
                      </li>
                    </ul>
                  </li>
                ) : (
                  <li id="loginmenu" className="dropdown onlineMenu">
                    <Link
                      to="/login"
                      className="dropdown-toggle dropdown-switch"
                    >
                      <img src="/images/svg-icons/user.svg" alt="Login" />
                      <span>Login/Signup</span>
                    </Link>
                  </li>
                )}

                <li id="MyCart">
                  <Link to="/cart">
                    <img src="/images/svg-icons/cart.svg" alt="My Cart" />
                    <span>My Cart</span>
                    <div className="cart-item-circle">
                      <span className="cart-item-count" id="cartqty">
                        {cartQuantity}
                      </span>
                    </div>
                  </Link>
                </li>

                <li>
                  <a href="#" id="main-options-icon">
                    <img src="/images/svg-icons/menu.svg" alt="More Options" />
                    <span>More</span>
                  </a>
                </li>
              </ul>
            </div>
          </nav>
        </div>

        {/* Menu Navigation */}
        <div className="menu-navbar-all">
          <div className="menu-navbar-flex">
            <div className="menu-navbar">
              <div className="menu-subnav">
                <div className="menu-subnavbtn">
                  <div className="menu-title-container">
                    <Link
                      className="menu-category-title"
                      to="/products?xdt=All Cakes"
                    >
                      Cakes
                    </Link>
                    <div className="menu-separator"></div>
                  </div>
                </div>
              </div>

              <div className="menu-subnav">
                <div className="menu-subnavbtn">
                  <div className="menu-title-container">
                    <div className="menu-category-title">By Type</div>
                    <div className="menu-separator"></div>
                  </div>
                </div>
                <div className="menu-subnav-content-column">
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=Photo Cakes"
                  >
                    Photo Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=Jar Cakes"
                  >
                    Jar Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=Cup Cakes"
                  >
                    Cup Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=Pastry"
                  >
                    Pastries
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=Eggless Cakes"
                  >
                    Eggless Cakes
                  </Link>
                </div>
              </div>

              <div className="menu-subnav">
                <div className="menu-subnavbtn">
                  <div className="menu-title-container">
                    <div className="menu-category-title">Designer Cakes</div>
                    <div className="menu-separator"></div>
                  </div>
                </div>
                <div className="menu-subnav-content-column">
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=All Designer Cakes"
                  >
                    All Designer Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=Minion Cakes"
                  >
                    Minion Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=Baby shower Cakes"
                  >
                    Baby shower Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=Spiderman Cakes"
                  >
                    Spiderman Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=PUBG Cakes"
                  >
                    PUBG Cakes
                  </Link>
                </div>
              </div>

              <div className="menu-subnav">
                <div className="menu-subnavbtn">
                  <div className="menu-title-container">
                    <div className="menu-category-title">Anniversary</div>
                    <div className="menu-separator"></div>
                  </div>
                </div>
                <div className="menu-subnav-content-column">
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=All Anniversary Cakes"
                  >
                    All Anniversary Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=1st Anniversary Cakes"
                  >
                    1st Anniversary Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=25th Anniversary Cakes"
                  >
                    25th Anniversary Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=50th Anniversary Cakes"
                  >
                    50th Anniversary Cakes
                  </Link>
                </div>
              </div>

              <div className="menu-subnav">
                <div className="menu-subnavbtn">
                  <div className="menu-title-container">
                    <div className="menu-category-title">Birthday</div>
                    <div className="menu-separator"></div>
                  </div>
                </div>
                <div className="menu-subnav-content-column">
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=All Birthday Cakes"
                  >
                    All Birthday Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=1st Birthday Cakes"
                  >
                    1st Birthday Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=Cakes for Kids"
                  >
                    Cakes for Kids
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=Birthday Photo Cakes"
                  >
                    Birthday Photo Cakes
                  </Link>
                </div>
              </div>

              <div className="menu-subnav">
                <div className="menu-subnavbtn">
                  <div className="menu-title-container">
                    <div className="menu-category-title">Occasions</div>
                    <div className="menu-separator"></div>
                  </div>
                </div>
                <div className="menu-subnav-content-column">
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=Diwali Cakes"
                  >
                    Diwali Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=Christmas Cakes"
                  >
                    Christmas Cakes
                  </Link>
                  <Link
                    className="menu-category-sub-title"
                    to="/products?xdt=New Year Cakes"
                  >
                    New Year Cakes
                  </Link>
                </div>
              </div>
            </div>

            <div className="menu-navbar-flex">
              <Link to="/franchise" className="franchise-link">
                Franchise Inquiry
              </Link>

              <div className="category-mobilecontainer">
                <span className="category-callus">Call Us:</span>
                <a
                  href="tel:+918018114444"
                  style={{ textDecoration: "none" }}
                  className="category-number"
                >
                  +91-8018114444
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </header>
  );
}

export default Header;
