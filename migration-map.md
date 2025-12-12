# Migration Map: ASP.NET WebForms to React + ASP.NET Core

**Project:** pyaris-bakery  
**Total Pages:** 45 ASPX files  
**Converted:** 5 pages (11%)  
**Remaining:** 40 pages (89%)  
**User Controls:** 2 ASCX files (both converted)

---

## Phase 0: Repository Analysis

### Project Overview
- **Frontend:** ASP.NET WebForms with master pages and user controls
- **Backend:** C# code-behind files (.aspx.cs) with direct SQL queries
- **Database:** SQL Server (NODEPOINT database at 192.168.0.230)
- **Authentication:** Session-based with plain-text passwords
- **Payment Gateways:** PhonePe, CCAvenue

### Key Business Flows Identified
1. **Product Catalog & Ordering** (High Priority)
   - spark.aspx → product listing by category
   - sparkdetails.aspx → product details with customization
   - sparkcart.aspx → shopping cart management
   - sparknext.aspx → checkout and payment

2. **User Management** (High Priority)
   - login.aspx → authentication
   - Register.aspx → user registration
   - ForgotPassword.aspx → password recovery
   - myprofile.aspx → profile management
   - mya.aspx → order history

3. **Admin Operations** (Medium Priority)
   - Products.aspx → product catalog management
   - EditProducts.aspx → product editing
   - Customers.aspx → customer management
   - PendingOrders.aspx → order processing
   - StoreOrders.aspx → store order management

4. **Payment Processing** (High Priority)
   - PaymentGateway.aspx → payment gateway selection
   - vpayinit.aspx → PhonePe payment initialization
   - vpayverify.aspx → payment verification
   - VerifyChecksum.aspx → payment checksum validation

5. **Content Pages** (Low Priority)
   - Default.aspx → homepage
   - about.aspx → about page
   - contact.aspx → contact form
   - gallery.aspx → image gallery
   - Franchise.aspx → franchise inquiry

---

## Master Page & User Controls

### Converted (✅)

| Original File | New Component | Status | Notes |
|--------------|---------------|--------|-------|
| Pyaris.master | frontend/src/components/Layout.jsx | ✅ Done | Exact HTML structure preserved |
| MenuSideBar.ascx | frontend/src/components/MenuSideBar.jsx | ✅ Done | All category links maintained |
| ProfileSideBar.ascx | frontend/src/components/ProfileSideBar.jsx | ✅ Done | Operations menu preserved |

---

## Page-by-Page Migration Map

### Priority 1: Core Shopping Flow (High Business Impact)

#### 1. spark.aspx - Product Listing
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Session validation, load products by category
- `LoadForSubGroup(string subgroup, string grp, bool isFlavour)` - Load products filtered by category/subgroup
- SQL: `SELECT DISTINCT [ID], [Menu Name], [Sell Price], [Group], [Sub Group], [Active] FROM [XMaster Menu] WHERE [Sub Group] = @subgroup`

**Proposed React Component:** `frontend/src/pages/Spark.jsx`  
**Proposed API Endpoints:**
- `GET /api/products/category/{category}` - Get products by category
- `GET /api/products/search?q={query}&group={group}&flavour={flavour}` - Search products

**Server Controls Used:**
- Repeater for product listing
- Custom HTML rendering with dynamic product cards

**Required Deviations:** None identified yet

---

#### 2. sparkdetails.aspx - Product Details
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Load product details by ID
- `LoadProductDetails(string productId)` - Fetch product from database
- `AddToCart_Click()` - Add product to session cart
- SQL: `SELECT * FROM [XMaster Menu] WHERE [ID] = @productId`

**Proposed React Component:** `frontend/src/pages/SparkDetails.jsx`  
**Proposed API Endpoints:**
- `GET /api/products/{id}` - Get product details
- `POST /api/cart/add` - Add item to cart
- `GET /api/cart/items` - Get cart items

**Server Controls Used:**
- Image control for product image
- Literal controls for dynamic HTML
- Button controls for add to cart

**Required Deviations:** None identified yet

---

#### 3. sparkcart.aspx - Shopping Cart
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Load cart from session
- `UpdateQuantity(string itemId, int quantity)` - Update cart item quantity
- `RemoveItem(string itemId)` - Remove item from cart
- `CalculateTotals()` - Calculate subtotal, tax, total
- SQL: Joins with product table for pricing

**Proposed React Component:** `frontend/src/pages/SparkCart.jsx`  
**Proposed API Endpoints:**
- `GET /api/cart` - Get current cart
- `PUT /api/cart/item/{id}` - Update item quantity
- `DELETE /api/cart/item/{id}` - Remove item
- `GET /api/cart/totals` - Get calculated totals

**Server Controls Used:**
- Repeater for cart items
- Button controls for quantity update/remove

**Required Deviations:** None identified yet

---

#### 4. sparknext.aspx - Checkout
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Load cart, validate user session
- `ValidateDeliveryAddress()` - Check pincode availability
- `CreateOrder()` - Insert into [Xsales MAster] table
- `InitiatePayment()` - Redirect to payment gateway
- SQL: Multiple inserts into sales and order detail tables

**Proposed React Component:** `frontend/src/pages/SparkNext.jsx`  
**Proposed API Endpoints:**
- `POST /api/orders/validate-address` - Validate delivery address
- `POST /api/orders/create` - Create order
- `POST /api/payment/initiate` - Start payment flow

**Server Controls Used:**
- TextBox controls for address fields
- DropDownList for time slot selection
- Button for checkout

**Required Deviations:** None identified yet

---

### Priority 2: User Management

#### 5. login.aspx - Login
**Status:** ✅ Done (Component + API)  
**Backend API:** `AuthController.CheckUser()`, `AuthController.Login()`  
**React Component:** `frontend/src/pages/Login.jsx`  
**Notes:** Session-based auth implemented with cookies

---

#### 6. Register.aspx - Registration
**Status:** ✅ Done (Component + API)  
**Backend API:** `AuthController.Register()`  
**React Component:** `frontend/src/pages/Register.jsx`  
**Notes:** Auto-login after registration preserved

---

#### 7. ForgotPassword.aspx - Password Recovery
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Display form
- `SendPasswordResetLink()` - Send SMS/Email with reset link
- SQL: `SELECT * FROM [xUser Details] WHERE [Mobile No] = @mobile`

**Proposed React Component:** `frontend/src/pages/ForgotPassword.jsx`  
**Proposed API Endpoints:**
- `POST /api/auth/forgot-password` - Initiate password reset
- `POST /api/auth/reset-password` - Complete password reset

**Required Deviations:** None identified yet

---

#### 8. myprofile.aspx - User Profile
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Load user profile data
- `UpdateProfile_Click()` - Update user information
- SQL: `SELECT * FROM [xUser Details] WHERE [Mobile No] = @user`
- SQL: `UPDATE [xUser Details] SET [Name] = @name, [Email] = @email WHERE [Mobile No] = @user`

**Proposed React Component:** `frontend/src/pages/MyProfile.jsx`  
**Proposed API Endpoints:**
- `GET /api/profile` - Get user profile
- `PUT /api/profile` - Update user profile

**Required Deviations:** None identified yet

---

#### 9. mya.aspx - Order History
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Load user orders, handle logout, handle cancel
- `LoadOrders()` - Fetch orders from database
- `CancelLoad(string id)` - Cancel order
- SQL: `SELECT * FROM [Xsales MAster] WHERE [User] = @user ORDER BY [ID] DESC`
- SQL: `UPDATE [Xsales MAster] SET [Status] = 'CANCELLED' WHERE [ID] = @id`

**Proposed React Component:** `frontend/src/pages/MyOrders.jsx`  
**Proposed API Endpoints:**
- `GET /api/orders/my-orders` - Get user order history
- `PUT /api/orders/{id}/cancel` - Cancel order
- `GET /api/orders/{id}/details` - Get order details

**Server Controls Used:**
- Repeater for order listing
- Button controls for cancel action

**Required Deviations:** None identified yet

---

#### 10. myapayback.aspx - Payback Points
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Load payback points balance and transactions
- SQL: Custom query for payback calculations

**Proposed React Component:** `frontend/src/pages/MyPayback.jsx`  
**Proposed API Endpoints:**
- `GET /api/payback/balance` - Get current balance
- `GET /api/payback/transactions` - Get transaction history

**Required Deviations:** None identified yet

---

### Priority 3: Admin Operations

#### 11. Products.aspx - Product Management
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Load all products
- `LoadProducts()` - Build HTML table of products
- SQL: `SELECT * FROM [XMaster Menu] ORDER BY [Id] DESC`

**Proposed React Component:** `frontend/src/pages/admin/Products.jsx`  
**Proposed API Endpoints:**
- `GET /api/admin/products` - Get all products (admin view)
- Reuse `ProductController` methods

**Server Controls Used:**
- Dynamic HTML table generation
- JavaScript DataTables for sorting/filtering

**Required Deviations:** None identified yet

---

#### 12. EditProducts.aspx - Product Editing
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Load product for editing
- `UpdateProduct_Click()` - Save product changes
- `UploadImage()` - Handle product image upload
- SQL: `UPDATE [XMaster Menu] SET ... WHERE [ID] = @id`

**Proposed React Component:** `frontend/src/pages/admin/EditProducts.jsx`  
**Proposed API Endpoints:**
- `PUT /api/admin/products/{id}` - Update product
- `POST /api/admin/products/{id}/image` - Upload product image
- `DELETE /api/admin/products/{id}` - Delete product

**Required Deviations:** None identified yet

---

#### 13. Customers.aspx - Customer Management
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Load customer list
- `LoadCustomers()` - Fetch from [xUser Details]
- SQL: `SELECT * FROM [xUser Details] ORDER BY [ID] DESC`

**Proposed React Component:** `frontend/src/pages/admin/Customers.jsx`  
**Proposed API Endpoints:**
- `GET /api/admin/customers` - Get all customers
- `GET /api/admin/customers/{id}` - Get customer details

**Required Deviations:** None identified yet

---

#### 14. PendingOrders.aspx - Pending Order Management
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Load pending orders
- `UpdateOrderStatus()` - Change order status
- SQL: `SELECT * FROM [Xsales MAster] WHERE [Status] = 'PENDING'`

**Proposed React Component:** `frontend/src/pages/admin/PendingOrders.jsx`  
**Proposed API Endpoints:**
- `GET /api/admin/orders/pending` - Get pending orders
- `PUT /api/admin/orders/{id}/status` - Update order status

**Required Deviations:** None identified yet

---

#### 15. StoreOrders.aspx - Store Order Management
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Load store orders
- SQL: `SELECT * FROM [Xsales MAster] WHERE [Outlet] = @outlet`

**Proposed React Component:** `frontend/src/pages/admin/StoreOrders.jsx`  
**Proposed API Endpoints:**
- `GET /api/admin/orders/store/{outletId}` - Get orders by store

**Required Deviations:** None identified yet

---

### Priority 4: Payment Integration

#### 16. PaymentGateway.aspx - Payment Selection
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Display payment options
- `SelectGateway_Click()` - Route to selected gateway

**Proposed React Component:** `frontend/src/pages/PaymentGateway.jsx`  
**Proposed API Endpoints:**
- `GET /api/payment/gateways` - Get available payment methods

**Required Deviations:** None identified yet

---

#### 17-21. PhonePe Integration (vpayinit, vpayverify, vpayredirect, vpayhash, vpayinitpaytm)
**Status:** ❌ Not Started  
**Backend Methods:**
- PhonePe API integration
- Payment checksum generation/validation
- Payment status verification
- SQL: Insert/Update payment transactions

**Proposed React Components:**
- `frontend/src/pages/payment/VPayInit.jsx`
- `frontend/src/pages/payment/VPayVerify.jsx`
- `frontend/src/pages/payment/VPayRedirect.jsx`

**Proposed API Endpoints:**
- `POST /api/payment/phonepe/initiate` - Start PhonePe payment
- `POST /api/payment/phonepe/verify` - Verify PhonePe payment
- `POST /api/payment/phonepe/checksum` - Generate checksum

**Required Deviations:** None identified yet

---

#### 22. VerifyChecksum.aspx - Payment Verification
**Status:** ❌ Not Started  
**Backend Methods:**
- `VerifyPaymentChecksum()` - Validate payment checksum
- `UpdateOrderStatus()` - Update order after payment
- SQL: Update order status, insert payment record

**Proposed React Component:** `frontend/src/pages/VerifyChecksum.jsx`  
**Proposed API Endpoints:**
- `POST /api/payment/verify-checksum` - Verify and process payment

**Required Deviations:** None identified yet

---

### Priority 5: Content Pages

#### 23. Default.aspx - Homepage
**Status:** ✅ Done (Component + API)  
**Backend API:** `HomeController.GetSlider()`  
**React Component:** `frontend/src/pages/Home.jsx`  
**Notes:** Slider data loaded from mastercodes table

---

#### 24. about.aspx - About Page
**Status:** ✅ Done (Component only)  
**React Component:** `frontend/src/pages/About.jsx`  
**Notes:** Static content page

---

#### 25. contact.aspx - Contact Form
**Status:** ✅ Done (Component only, API needed)  
**React Component:** `frontend/src/pages/Contact.jsx`  
**Backend API Needed:** `POST /api/contact/submit` - Save contact inquiry

---

#### 26. gallery.aspx - Image Gallery
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Load gallery images
- SQL: `SELECT * FROM mastercodes WHERE location = 'gallery'`

**Proposed React Component:** `frontend/src/pages/Gallery.jsx`  
**Proposed API Endpoints:**
- `GET /api/gallery/images` - Get gallery images

**Required Deviations:** None identified yet

---

#### 27. online.aspx - Online Ordering Info
**Status:** ❌ Not Started  
**Proposed React Component:** `frontend/src/pages/Online.jsx`  
**Notes:** Information page about online ordering

---

#### 28. Franchise.aspx - Franchise Inquiry
**Status:** ❌ Not Started  
**Backend Methods:**
- `Page_Load()` - Display form
- `SubmitFranchiseInquiry()` - Save franchise inquiry
- SQL: Insert into franchise inquiries table

**Proposed React Component:** `frontend/src/pages/Franchise.jsx`  
**Proposed API Endpoints:**
- `POST /api/franchise/submit` - Submit franchise inquiry

**Required Deviations:** None identified yet

---

#### 29. privacy.aspx - Privacy Policy
**Status:** ❌ Not Started  
**Proposed React Component:** `frontend/src/pages/Privacy.jsx`  
**Notes:** Static content page

---

#### 30. terms.aspx - Terms & Conditions
**Status:** ❌ Not Started  
**Proposed React Component:** `frontend/src/pages/Terms.jsx`  
**Notes:** Static content page

---

#### 31. refund.aspx - Refund Policy
**Status:** ❌ Not Started  
**Proposed React Component:** `frontend/src/pages/Refund.jsx`  
**Notes:** Static content page

---

### Priority 6: Utility/Support Pages

#### 32-45. Remaining Pages
- 404.aspx → ErrorPage.jsx (404 handler)
- 500.aspx → ErrorPage.jsx (500 handler)
- CustomErrorPage.aspx → ErrorPage.jsx (generic error)
- demo.aspx → (Demo/test page - low priority)
- GenerateChecksum.aspx → API only (payment utility)
- GenerateOutletBill.aspx → API only (billing utility)
- response.aspx → PaymentResponse.jsx
- sales.aspx → (Empty page - verify if needed)
- ServiceReport.aspx → ServiceReport.jsx
- sh.aspx → (Utility page - verify purpose)
- sparkfail.aspx → PaymentFailed.jsx
- sparkover.aspx → StockUnavailable.jsx
- UpdateBanner.aspx → Admin banner upload
- whatsapp.aspx → WhatsAppRedirect.jsx

---

## Backend Controllers Summary

### Existing Controllers (✅)
1. **AuthController** - Login, register, session management
2. **HomeController** - Homepage slider
3. **ProductController** - Product catalog (partial)

### Required Controllers (❌)
4. **OrderController** - Order creation, retrieval, cancellation, status updates
5. **CartController** - Cart management (add, update, remove, clear)
6. **PaymentController** - Payment gateway integration (PhonePe, CCAvenue)
7. **ProfileController** - User profile management
8. **AdminController** - Admin operations (products, customers, orders)
9. **GalleryController** - Gallery image management
10. **ContactController** - Contact form submissions
11. **FranchiseController** - Franchise inquiries
12. **PaybackController** - Payback points management

---

## Database Schema Analysis

### Key Tables Identified
1. **[xUser Details]** - User accounts (Mobile No, Name, Password, Email, Address)
2. **[XMaster Menu]** - Product catalog (ID, Menu Name, Sell Price, Group, Sub Group, etc.)
3. **[Xsales MAster]** - Orders (ID, User, Status, Total, Outlet, Order Date, etc.)
4. **mastercodes** - Configuration data (location, data, options, Enabled)
5. **Payment transaction tables** - (Need to identify exact names)
6. **Payback tables** - (Need to identify exact names)

### Stored Procedures
- No stored procedures identified yet in the scanned code
- All queries are inline SQL in code-behind files
- **Action:** Continue with inline SQL approach for compatibility

---

## CSS & Assets Migration

### Completed (✅)
- ✅ style.css (95KB, 4753 lines) - Copied exactly
- ✅ sparxdata.css (2.4KB, 95 lines) - Copied exactly
- ✅ datetimepicker_css.js - Copied to public/
- ✅ sidefollow.js - Copied to public/

### Pending (❌)
- ❌ Images folder - Need to copy to frontend/public/images/
- ❌ Fonts folder (libraries/fonts/) - Need to copy
- ❌ Other JavaScript files - Identify and copy
- ❌ favicon.ico - Copy to public/

---

## Authentication & Session Strategy

### Current Implementation (✅)
- Session-based authentication using ASP.NET Core sessions
- Session cookies with HttpOnly and Secure flags
- Session keys: `xuser` (mobile number), `xusername` (display name), `xtran` (transaction ID), `xcartqty` (cart quantity)

### Compatibility Considerations
- ⚠️ Plain-text passwords (documented for future migration)
- ⚠️ Case-sensitive password comparison (preserved for compatibility)
- Session timeout: 30 minutes (configurable)

---

## Required Deviations (Currently None)

No technical deviations required at this time. All functionality can be preserved exactly.

**Future Considerations:**
1. Password hashing migration (requires database migration script)
2. CSS font format corrections (inherited from original - non-blocking)

---

## Testing Strategy

### Visual Parity Testing
For each converted page:
1. Side-by-side screenshot comparison (old vs. new)
2. DOM structure verification (same classes, IDs, element hierarchy)
3. CSS application verification (all styles render correctly)
4. Responsive layout testing (mobile, tablet, desktop)

### Functional Testing
1. Unit tests for API controllers (each endpoint)
2. Integration tests for key flows (ordering, payment, admin)
3. E2E tests for critical user journeys
4. Session/authentication testing
5. Payment gateway integration testing (test mode)

### Automated Checks
- Visual regression testing with Percy or Chromatic
- API contract testing with Pact
- Performance testing (page load times)
- Accessibility testing (WCAG compliance)

---

## Next Actions (Phase 1-2)

### Immediate Tasks
1. ✅ Copy all images to frontend/public/images/
2. ✅ Copy fonts to frontend/public/libraries/fonts/
3. ✅ Set up proper asset loading in Layout component
4. ❌ Create OrderController with all order management endpoints
5. ❌ Create CartController with cart management endpoints
6. ❌ Convert spark.aspx (Product Listing) - Priority 1
7. ❌ Convert sparkdetails.aspx (Product Details) - Priority 1
8. ❌ Convert sparkcart.aspx (Shopping Cart) - Priority 1
9. ❌ Convert sparknext.aspx (Checkout) - Priority 1

### Deliverable Timeline
- **Week 1:** Complete Priority 1 (Shopping Flow) - 4 pages
- **Week 2:** Complete Priority 2 (User Management) - 5 pages
- **Week 3:** Complete Priority 3 (Admin Operations) - 5 pages
- **Week 4:** Complete Priority 4 (Payment Integration) - 7 pages
- **Week 5:** Complete Priority 5-6 (Content & Utility) - 19 pages
- **Week 6:** Testing, verification, and sign-off

---

## Sign-Off Checklist (End of Project)

### Page Conversion
- [ ] All 45 ASPX pages converted to React components
- [ ] All user controls converted to React components
- [ ] All server-side methods converted to API endpoints

### Visual Parity
- [ ] Screenshot comparison for all pages (100% match)
- [ ] CSS applied correctly on all pages
- [ ] Responsive design working on all screen sizes
- [ ] All images and fonts loading correctly

### Functional Parity
- [ ] Authentication flow working identically
- [ ] Shopping cart flow working identically
- [ ] Payment gateway integration working
- [ ] Admin operations working identically
- [ ] All forms submitting correctly
- [ ] All validation messages displaying correctly

### Testing
- [ ] Unit tests passing (100% of backend logic)
- [ ] Integration tests passing (all critical flows)
- [ ] E2E tests passing (all user journeys)
- [ ] Visual regression tests passing
- [ ] Performance benchmarks met

### Documentation
- [ ] API documentation complete
- [ ] Setup/deployment guide complete
- [ ] Testing guide complete
- [ ] Parity verification report complete

### Approval
- [ ] Technical lead sign-off
- [ ] Stakeholder sign-off
- [ ] QA sign-off

---

**Document Version:** 1.0  
**Last Updated:** 2025-12-12  
**Status:** Foundation Complete - Ready for Phase 2 Conversion
