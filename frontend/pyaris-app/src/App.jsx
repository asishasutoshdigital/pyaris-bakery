import { BrowserRouter as Router, Routes, Route, useLocation } from 'react-router-dom';
import { useEffect, memo } from 'react';
import './App.css';

// Import components
import Header from './components/Header';
import Footer from './components/Footer';

// Memoize Header and Footer to prevent unnecessary re-renders
const MemoizedHeader = memo(Header);
const MemoizedFooter = memo(Footer);

// ScrollToTop component to handle scroll on route change
function ScrollToTop() {
  const location = useLocation();

  useEffect(() => {
    // Reset scroll position on route change
    window.scrollTo(0, 0);
  }, [location.pathname]);

  return null;
}

// Import pages
import HomePage from './pages/HomePage';
import ProductsPage from './pages/ProductsPage';
import ProductDetailsPage from './pages/ProductDetailsPage';
import CartPage from './pages/CartPage';
import CheckoutPage from './pages/CheckoutPage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import ForgotPasswordPage from './pages/ForgotPasswordPage';
import AboutPage from './pages/AboutPage';
import ContactPage from './pages/ContactPage';
import GalleryPage from './pages/GalleryPage';
import PrivacyPage from './pages/PrivacyPage';
import TermsPage from './pages/TermsPage';
import RefundPage from './pages/RefundPage';
import OrderSuccessPage from './pages/OrderSuccessPage';
import OrderFailPage from './pages/OrderFailPage';
import MyProfilePage from './pages/MyProfilePage';
import MyAccountPage from './pages/MyAccountPage';
import RewardsPage from './pages/RewardsPage';
import FranchisePage from './pages/FranchisePage';
import SearchPage from './pages/SearchPage';
import NotFoundPage from './pages/NotFoundPage';
import ErrorPage from './pages/ErrorPage';
import CustomErrorPage from './pages/CustomErrorPage';
import WhatsAppPage from './pages/WhatsAppPage';
import OnlinePage from './pages/OnlinePage';
import DemoPage from './pages/DemoPage';
import ResponsePage from './pages/ResponsePage';
import SalesPage from './pages/SalesPage';

// Admin pages
import AdminProductsPage from './pages/admin/AdminProductsPage';
import AdminEditProductPage from './pages/admin/AdminEditProductPage';
import AdminCustomersPage from './pages/admin/AdminCustomersPage';
import AdminPendingOrdersPage from './pages/admin/AdminPendingOrdersPage';
import AdminStoreOrdersPage from './pages/admin/AdminStoreOrdersPage';
import AdminUpdateBannerPage from './pages/admin/AdminUpdateBannerPage';
import AdminServiceReportPage from './pages/admin/AdminServiceReportPage';
import AdminGenerateBillPage from './pages/admin/AdminGenerateBillPage';
import AdminSalesPage from './pages/admin/AdminSalesPage';
import AdminStockPage from './pages/admin/AdminStockPage';

// Payment pages
import VPayInitPage from './pages/VPayInitPage';
import VPayRedirectPage from './pages/VPayRedirectPage';
import VPayVerifyPage from './pages/VPayVerifyPage';
import VPayHashPage from './pages/VPayHashPage';
import VPayInitPaytmPage from './pages/VPayInitPaytmPage';
import PaymentGatewayPage from './pages/PaymentGatewayPage';
import GenerateChecksumPage from './pages/GenerateChecksumPage';
import VerifyChecksumPage from './pages/VerifyChecksumPage';

function App() {
  return (
    <Router>
      <ScrollToTop />
      <div className="App">
        <MemoizedHeader />
        <div id="mainBody">
          <Routes>
            {/* Main pages */}
            <Route path="/" element={<HomePage />} />
            <Route path="/products" element={<ProductsPage />} />
            <Route path="/products/:id" element={<ProductDetailsPage />} />
            <Route path="/cart" element={<CartPage />} />
            <Route path="/checkout" element={<CheckoutPage />} />
            
            {/* Auth pages */}
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/forgot-password" element={<ForgotPasswordPage />} />
            
            {/* Info pages */}
            <Route path="/about" element={<AboutPage />} />
            <Route path="/contact" element={<ContactPage />} />
            <Route path="/gallery" element={<GalleryPage />} />
            <Route path="/privacy" element={<PrivacyPage />} />
            <Route path="/terms" element={<TermsPage />} />
            <Route path="/refund" element={<RefundPage />} />
            <Route path="/franchise" element={<FranchisePage />} />
            
            {/* Order pages */}
            <Route path="/order/success" element={<OrderSuccessPage />} />
            <Route path="/order/failed" element={<OrderFailPage />} />
            
            {/* User pages */}
            <Route path="/profile" element={<MyProfilePage />} />
            <Route path="/my-account" element={<MyAccountPage />} />
            <Route path="/rewards" element={<RewardsPage />} />
            
            {/* Search */}
            <Route path="/search" element={<SearchPage />} />
            
            {/* Admin pages */}
            <Route path="/admin/products" element={<AdminProductsPage />} />
            <Route path="/admin/products/edit/:id" element={<AdminEditProductPage />} />
            <Route path="/admin/customers" element={<AdminCustomersPage />} />
            <Route path="/admin/orders/pending" element={<AdminPendingOrdersPage />} />
            <Route path="/admin/orders/store" element={<AdminStoreOrdersPage />} />
            <Route path="/admin/banner" element={<AdminUpdateBannerPage />} />
            <Route path="/admin/reports" element={<AdminServiceReportPage />} />
            <Route path="/admin/billing" element={<AdminGenerateBillPage />} />
            <Route path="/admin/sales" element={<AdminSalesPage />} />
            <Route path="/admin/stock" element={<AdminStockPage />} />
            
            {/* Payment pages */}
            <Route path="/payment/phonepe/init" element={<VPayInitPage />} />
            <Route path="/payment/phonepe/redirect" element={<VPayRedirectPage />} />
            <Route path="/payment/phonepe/verify" element={<VPayVerifyPage />} />
            <Route path="/payment/phonepe/hash" element={<VPayHashPage />} />
            <Route path="/payment/paytm/init" element={<VPayInitPaytmPage />} />
            <Route path="/payment/gateway" element={<PaymentGatewayPage />} />
            <Route path="/payment/checksum/generate" element={<GenerateChecksumPage />} />
            <Route path="/payment/checksum/verify" element={<VerifyChecksumPage />} />
            
            {/* Misc pages */}
            <Route path="/whatsapp" element={<WhatsAppPage />} />
            <Route path="/online" element={<OnlinePage />} />
            <Route path="/demo" element={<DemoPage />} />
            <Route path="/response" element={<ResponsePage />} />
            <Route path="/sales" element={<SalesPage />} />
            <Route path="/custom-error" element={<CustomErrorPage />} />
            
            {/* Error pages */}
            <Route path="/error" element={<ErrorPage />} />
            <Route path="*" element={<NotFoundPage />} />
          </Routes>
        </div>
        <MemoizedFooter />
      </div>
    </Router>
  );
}

export default App;
