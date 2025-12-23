# FINAL UI FIX REQUIREMENTS - EXACT ASPX REPLICATION

## STATUS: IN PROGRESS

The user requires **pixel-perfect** replication of 6 ASPX pages to React.

## CRITICAL REQUIREMENTS

### Rule #1: EXACT HTML Structure
- Copy HTML structure from ASPX **exactly as-is**
- Same div nesting, same class names, same IDs
- **NO interpretation, NO redesign**

### Rule #2: EXACT CSS Classes
- Use original CSS classes from style.css (94KB)
- **DO NOT** use Bootstrap 5 classes (container, card, row from BS5)
- Use ASPX classes: `product-listing-container`, `imgBox`, `cart-container`, etc.

### Rule #3: EXACT Layout
- Same grid system (Bootstrap 3 from ASPX, not Bootstrap 5)
- Same spacing, margins, padding
- Same responsive breakpoints

---

## 6 PAGES TO FIX

### 1. ProductsPage.jsx ← spark.aspx

**ASPX Structure:**
```html
<div id="xdata">
    <div class="product-category-title" id="xdisplayheader">Category Name</div>
    <div class="product-listing-container row" id="xfdata">
        <!-- Products here -->
        <div class="col-md-3 col-sm-4 col-xs-6 list-productcard">
            <a href="sparkdetails.aspx?id=123">
                <div class="productcard">
                    <div class="productcard-image">
                        <img src="menupic/small/123s.png" class="img-responsive">
                    </div>
                    <div class="product-card-title">Cake Name</div>
                    <div class="product-card-price">₹ 500 / Kg</div>
                </div>
            </a>
        </div>
    </div>
</div>
```

**React Must Use:**
- Same `<div id="xdata">` wrapper
- Same `product-listing-container row` class
- Same `list-productcard`, `productcard`, `productcard-image` classes
- Same image paths: `/menupic/small/{id}s.png`
- Link to `/product/{id}` instead of `sparkdetails.aspx?id={id}`

---

### 2. ProductDetailsPage.jsx ← sparkdetails.aspx

**ASPX Structure:**
```html
<div id="midpart">
    <div class="col-md-12 product-details-container">
        <div class="col-md-6 login-container">
            <div class="imgBox">
                <img id="bigimg" class="imgBoxImageSize" src="menupic/big/1b.png">
                <div id="lens"></div>
                <div class="veg-symbol"></div>
            </div>
            <div class="row smallimgBox">
                <ul id="prodImages">
                    <li><div class="smallimgBoxImageSize" onclick="smallimgclick(this)">
                        <img src="menupic/big/1b.png" class="smallimgBoxImageSize">
                    </div></li>
                </ul>
            </div>
        </div>
        <div class="col-md-6">
            <div class="sectionContent">
                <h1 class="xmenuname">Cake Name</h1>
                <div class="rating-box">
                    <span class="star">★</span>
                    <span class="rating-value">4.5</span>
                </div>
                <div class="xgroupsubgroup">Cakes</div>
                <div class="xrate">₹ 250</div>
                
                <div class="row">
                    <div class="col-md-12 product-prop-heading">Weight</div>
                    <div class="col-md-6">
                        <select class="form-control">...</select>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-12 product-prop-heading">Flavour</div>
                    <div class="col-md-6">
                        <select class="form-control timerx">...</select>
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-12 product-prop-heading">Cake Message</div>
                    <div class="col-md-6">
                        <input type="text" class="form-control" placeholder="Enter message on cake">
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-12 product-prop-heading">Quantity</div>
                    <div class="col-md-6">
                        <input type="text" class="form-control numeric" maxlength="3" value="1">
                    </div>
                </div>
                
                <div class="row">
                    <div class="col-md-6">
                        <button class="rounded-btn pink-btn">Add To Cart</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div class="col-md-12 time-smile-content">
        <div class="time-smile">
            <div class="thumb-time">
                <div class="thumb-image"><img src="[base64 image]"></div>
                <div class="time-content"><span class="percent-bold">100%</span>On Time Delivery</div>
            </div>
            <!-- 2 more badges -->
        </div>
    </div>
</div>
```

**React Must Use:**
- Exact same div structure
- `imgBox`, `smallimgBox` for images
- `product-prop-heading` for labels
- `rounded-btn pink-btn` for Add To Cart button
- `numeric` class on quantity input (for jQuery numeric plugin)
- Base64 badge images preserved
- Image zoom effect on mouse hover

---

### 3. CartPage.jsx ← sparkcart.aspx

**ASPX Structure:**
```html
<div id="midpart">
    <!-- Empty cart -->
    <div id="emptycartcontainer" class="row spark-over-container" runat="server">
        <div class="category-card-heading">Empty Cart !!</div>
        <div class="cartTitle">Add some items to cart to checkout</div>
        <div class="col-md-4">
            <input type="button" onclick="redirect();" value="CONTINUE SHOPPING" class="rounded-btn red-btn">
        </div>
    </div>
    
    <!-- Cart with items -->
    <div id="cartcontainer" class="cart-container" runat="server">
        <div class="col-md-12 col-xs-12 no-padding cart-inner-container">
            <div class="col-md-8 col-sm-7 cart-item-container">
                <h1 class="cartTitle">Shopping cart</h1>
                <div id="xdata">
                    <div class="row">
                        <div class="col-md-2">
                            <img src="menupic/small/181s.png" class="cartItemIcon">
                        </div>
                        <div class="col-md-7">
                            <div class="xmenuname">Chicken Biriyani</div>
                            <div id="xqty">
                                QTY: 
                                <a href="?minus=261" class="quantity-button">-</a>
                                5
                                <a href="?plus=261" class="quantity-button">+</a>
                                <div class="col-md-4 itemDetailCard">PRICE: ₹ 450</div>
                                <div class="col-md-5 itemDetailCard">AMOUNT: ₹ 5800</div>
                            </div>
                        </div>
                        <a href="?del=261" class="removeItemImage">
                            <img src="images/remove.png" alt="Remove">
                        </a>
                    </div>
                </div>
            </div>
            
            <div class="col-md-3 col-sm-4 cart-button-container">
                <div class="col-md-12">
                    <div class="cartTotalBox">SUB TOTAL: ₹ 6700</div>
                    <div class="cartTotalBox" id="xtotal">TOTAL: ₹ 6700</div>
                </div>
                
                <div id="payBackContainer">
                    <div class="col-md-12">
                        <div class="subTotalBox availablePointsBox">
                            AVAILABLE POINTS TO REDEEM: 20
                        </div>
                        <button class="btn pink-btn">Redeem Points</button>
                        
                        <div id="redeemSection" style="display: none;">
                            <div class="col-md-8">
                                <input class="redeemTextBox form-control numeric" type="text" placeholder="Points to Redeem">
                            </div>
                            <div class="col-md-4">
                                <button class="redeem-btn pink-btn">Redeem</button>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div>
                    <button class="rounded-btn green-btn">PLACE ORDER</button>
                </div>
                <div>
                    <button class="rounded-btn transparent-btn">CONTINUE SHOPPING</button>
                </div>
            </div>
        </div>
    </div>
</div>
```

**React Must Use:**
- `cart-container`, `cart-item-container`, `cart-button-container`
- `cartTitle`, `cartTotalBox`, `cartItemIcon`
- `quantity-button` for + / - links
- `rounded-btn green-btn` for Place Order
- `rounded-btn transparent-btn` for Continue Shopping
- `redeemSection` with toggle functionality

---

### 4. LoginPage.jsx ← login.aspx

**ASPX Structure:**
```html
<style>
    html, body, form, .footer-main {
        background-color: #fbf3f3;
    }
</style>

<div id="midpart">
    <div class="cupcake-candle">
        <img src="images/new-images/cupcake-candle.jpg" alt="cupcake-background" width="70%" height="70%">
    </div>
    <div class="container login-container">
        <div class="col-md-4 col-sm-6 col-xs-12">
            <div class="sectionTitle">Login</div>
            <div class="sectionContent">
                <div class="form-group">Username</div>
                <div class="form-group">
                    <input id="Username" class="form-control numeric" type="text" maxlength="10" placeholder="Your Mobile No">
                    <div class="error-text"></div>
                </div>
                <div id="PasswordSection">
                    <div class="form-group">Password</div>
                    <div class="form-group">
                        <input id="Password" class="form-control" type="password" placeholder="Your Password">
                        <div class="error-text"></div>
                    </div>
                </div>
            </div>
            <div class="buttonSection">
                <button class="primary-btn">CONTINUE</button>
            </div>
            <div>
                <div class="buttonSection">
                    <button class="primary-btn">LOG IN</button>
                </div>
                <div class="buttonSection forgot-password-link">
                    <a href="/forgot-password">Forgot Password?</a>
                </div>
            </div>
        </div>
    </div>
</div>
```

**React Must Use:**
- Background color `#fbf3f3` for entire page (html, body, form, .footer-main)
- `cupcake-candle` div with background image
- `login-container`, `sectionTitle`, `sectionContent`, `buttonSection`
- `form-control numeric` on username (triggers jQuery numeric plugin)
- `primary-btn` class on buttons
- `forgot-password-link` styling

---

### 5. RegisterPage.jsx ← Register.aspx

**ASPX Structure:**
```html
<style>
    html, body, form, .footer-main {
        background-color: #fbf3f3;
    }
</style>

<div id="midpart">
    <div class="container login-container">
        <div class="col-md-8 col-xs-12 no-padding">
            <div class="sectionTitle">Register Yourself</div>
            <div class="sectionContent">
                <div class="col-md-6 col-sm-6">
                    <div class="form-group">Mobile No <span class="requiredStar">*</span></div>
                    <input class="form-control numeric" id="txtmob" type="text" maxlength="10" placeholder="Your Mobile No" autocomplete="off">
                    
                    <div class="form-group">Password <span class="requiredStar">*</span></div>
                    <input class="form-control" id="txtpwd" type="password" placeholder="Your Password" autocomplete="off">
                    
                    <div class="form-group">Your Name <span class="requiredStar">*</span></div>
                    <input id="txtname" type="text" class="form-control" placeholder="Your Name" autocomplete="off">
                    
                    <div class="form-group">Email Id <span class="requiredStar">*</span></div>
                    <input id="txtemailid" type="text" class="form-control" placeholder="Your Email" autocomplete="off">
                </div>
                <div class="col-md-6 col-sm-6">
                    <div class="form-group">Address Line1</div>
                    <input id="txtaddress1" type="text" class="form-control" placeholder="Your Address" autocomplete="off">
                    
                    <div class="form-group">Address Line2</div>
                    <input id="txtaddress2" type="text" class="form-control" placeholder="Your Address" autocomplete="off">
                    
                    <div class="form-group">City</div>
                    <input id="txtcity" type="text" class="form-control" placeholder="Your City" autocomplete="off">
                    
                    <div class="form-group">Pin Code</div>
                    <input id="txtpin" maxlength="6" type="text" class="form-control numeric" placeholder="Your Pincode" autocomplete="off">
                </div>
            </div>
        </div>
        <div class="error-text"></div>
        <div class="buttonSection col-md-4 col-sm-6 col-xs-12">
            <button class="primary-btn">REGISTER</button>
        </div>
    </div>
</div>
```

**React Must Use:**
- Same pink background `#fbf3f3`
- 2-column layout (col-md-6 each)
- `requiredStar` class for asterisks
- `form-control numeric` on mobile and pincode
- `primary-btn` on register button
- `autocomplete="off"` on all inputs

---

### 6. FranchisePage.jsx ← Franchise.aspx

**ASPX Structure:**
```html
<div class="custom-form">
    <h2>Paris Bakery - Franchise Inquiry Form</h2>
    <div class="intro">
        Paris Bakery is one of India's fastest-growing premium bakery chains...
    </div>
    
    <div class="form-section">
        <h3 class="section-title">1. Personal Info</h3>
        <div class="form-row">
            <div class="form-col">
                <label>Full Name <span style="color:red">*</span></label>
                <input type="text" class="aspnet-control" required>
            </div>
            <div class="form-col">
                <label>Age <span style="color:red">*</span></label>
                <input type="number" class="aspnet-control" required>
            </div>
        </div>
        <!-- More fields -->
        
        <button class="btn-submit">Submit Inquiry</button>
    </div>
</div>
```

**React Must Use:**
- `custom-form` wrapper
- `form-section`, `section-title`, `form-row`, `form-col`
- `aspnet-control` class on all inputs
- `btn-submit` on submit button
- All 4 sections: Personal Info, Location, Loan, Investment
- All dropdowns with exact options
- Red asterisks for required fields

---

## CSS FILES TO USE

1. **style.css** (94KB) - Main stylesheet
2. **sparxdata.css** - Additional styles
3. **css/plugins.css** - Bootstrap 3 + plugins
4. **css/navigation-menu.css** - Menu styles

**DO NOT USE:**
- Bootstrap 5 CSS (already removed from package.json)
- Modern React component libraries
- Tailwind CSS

---

## JAVASCRIPT HOOKS

These CSS classes trigger JavaScript functionality:

1. **`.numeric`** - Only allows numbers (jQuery plugin)
2. **`.imgBox`** + **`#lens`** - Image zoom on hover
3. **`.quantity-button`** - Increment/decrement quantity
4. **`.slick-center`** - Slick carousel initialization

**Must keep these classes for jQuery plugins to work!**

---

## IMAGES

**Path structure:**
- Products small: `/menupic/small/{id}s.png`
- Products big: `/menupic/big/{id}b.png`
- Icons: `/images/*.png`
- Background: `/images/new-images/cupcake-candle.jpg`

**Fallback:**
- If image fails to load: `/images/svg-icons/img-placeholder.svg`

---

## TESTING CHECKLIST

After rewriting each page:

1. ✅ HTML structure matches ASPX exactly
2. ✅ All CSS class names preserved
3. ✅ Image paths correct
4. ✅ No Bootstrap 5 classes used
5. ✅ Background colors match (especially login/register pink)
6. ✅ Button classes match (`rounded-btn`, `pink-btn`, etc.)
7. ✅ Form layouts match (columns, spacing)
8. ✅ JavaScript hooks present (`.numeric`, etc.)
9. ✅ Page builds without errors
10. ✅ Visual inspection matches ASPX

---

## CURRENT STATUS

- ✅ HomePage: Fixed (95% match)
- ✅ Header: Fixed (90% match)
- ✅ Footer: Fixed (95% match)
- ✅ App.css: Fixed (width constraints removed)
- ⏳ ProductsPage: IN PROGRESS (new file created)
- ⏳ ProductDetailsPage: IN PROGRESS (new file created)
- ⏳ CartPage: TODO
- ⏳ LoginPage: TODO
- ⏳ RegisterPage: TODO
- ⏳ FranchisePage: TODO

---

## NEXT STEPS

1. Complete ProductsPage.jsx rewrite
2. Complete ProductDetailsPage.jsx rewrite
3. Complete CartPage.jsx rewrite
4. Complete LoginPage.jsx rewrite
5. Complete RegisterPage.jsx rewrite
6. Complete FranchisePage.jsx rewrite
7. Test each page against ASPX original
8. Commit all changes
9. Verify build success
10. Reply to user with screenshots

**Estimated time: 2-3 hours for all 6 pages**
