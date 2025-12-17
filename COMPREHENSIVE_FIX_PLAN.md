# COMPREHENSIVE SYSTEM FIX PLAN

## Reference
**Live Site (Gold Standard)**: https://www.parisbakery.in

## Critical Issues Summary

### 1. Franchise Page - 99% Wrong ❌
**Current State**: Generic Bootstrap form
**Required State**: Exact match to Franchise.aspx structure

**Required Structure (from Franchise.aspx):**
- Pink color scheme (#d63384)
- 4 sections with section-title styling
- 20+ form fields with proper validation
- Custom CSS classes: `custom-form`, `form-section`, `section-title`, `form-row`, `form-col`

**Fields Required:**
- **Section 1 - Personal Info**: Full Name, Age, Phone, Email, Occupation, Annual Income
- **Section 2 - Location**: Street Address, City, State, Pin Code
- **Section 3 - Loan**: Loan details, Loan Amount, CIBIL Score
- **Section 4 - Investment**: Business Experience, Business Type, Preferred Location, Property Ownership, Property Size, Budget, Comments

### 2. Home Page - Multiple Issues ❌
**Issues:**
- Location selector logic broken
- Banner carousel not functioning
- Section alignment issues
- Page width (FIXED but needs verification)

**Required from Default.aspx:**
- Slick carousel with banner images (class: `slick-center`)
- Location/pincode selector modal
- Category cards with proper links
- Experience Flavours section
- Designer Cakes section

### 3. Login Page - Design Mismatch ❌
**Missing:**
- Cupcake-candle background image
- Pink background color (#fbf3f3)
- Proper `login-container` structure
- `sectionTitle` and `sectionContent` divs

### 4. Product Pages - Critical Failures ❌
**Issues:**
- Product click shows ALL cakes (filter not working)
- Product data incorrect
- Product images not loading
- Routing broken

**Required:**
- Product filtering by subgroup/category
- Correct image paths: `/menupic/small/{id}s.png`
- Proper routing with query parameters

### 5. Backend APIs - Data Issues ❌
**Issues:**
- ProductController returning unfiltered data
- Image path resolution failing
- API routing needs validation

## File-by-File Fix Plan

### Frontend Files

#### 1. FranchisePage.jsx
**Status**: Complete rewrite needed
**Source**: Franchise.aspx (lines 1-282)
**Priority**: CRITICAL

**Implementation:**
```jsx
// Required structure
<div className="custom-form">
  <h2>Paris Bakery - Franchise Inquiry Form</h2>
  <div className="intro">...</div>
  <div className="form-section">
    <h3 className="section-title">1. Personal Info</h3>
    <div className="form-row">
      <div className="form-col">...</div>
    </div>
    // ... 3 more sections
  </div>
</div>
```

**CSS Required** (from Franchise.aspx lines 28-141):
- `.custom-form h2` - Pink (#d63384) centered title
- `.form-section` - White background, padding, shadow
- `.section-title` - Pink with left border
- `.form-row` - Flexbox 2-column layout
- `.btn-submit` - Pink button (#d63384)

#### 2. HomePage.jsx
**Status**: Partial fix needed
**Source**: Default.aspx
**Priority**: HIGH

**Issues to Fix:**
- Location selector modal (check if exists)
- Carousel initialization (slick-center class)
- Banner images loading
- Category card links

#### 3. LoginPage.jsx
**Status**: Complete rewrite needed
**Source**: login.aspx
**Priority**: HIGH

**Required:**
```jsx
<div id="midpart">
  <div className="cupcake-candle">
    <img src="/images/new-images/cupcake-candle.jpg" />
  </div>
  <div className="login-container">
    <div className="sectionTitle">Login</div>
    <div className="sectionContent">...</div>
  </div>
</div>
```

#### 4. ProductsPage.jsx
**Status**: Routing fix needed
**Source**: spark.aspx
**Priority**: HIGH

**Issues:**
- Product filtering not working
- Need to read query parameters (subgroup, grp)
- Filter products based on parameters

#### 5. ProductDetailsPage.jsx
**Status**: Image loading fix needed
**Source**: sparkdetails.aspx
**Priority**: HIGH

**Issues:**
- Product images not loading
- Image path: `/menupic/small/{id}s.png` and `/menupic/large/{id}l.png`

### Backend Files

#### 1. ProductController.cs
**Status**: Filter logic validation needed
**Priority**: HIGH

**Issues:**
- GetBySubGroup may be returning all products
- Need to verify SQL WHERE clause
- Image path resolution

#### 2. HomeController.cs
**Status**: Location logic needed
**Priority**: MEDIUM

**Issues:**
- Pincode/location selection logic
- Session management for selected location

## Testing Checklist

### Frontend Tests
- [ ] Franchise page renders with all 20+ fields
- [ ] Franchise form submission works
- [ ] Home page carousel auto-plays
- [ ] Home page location selector works
- [ ] Login page has cupcake background
- [ ] Login form submits correctly
- [ ] Product page filters by category
- [ ] Product details shows correct product
- [ ] Product images load correctly
- [ ] Cart operations work

### Backend Tests
- [ ] GET /api/product returns all products
- [ ] GET /api/product/subgroup/{subgroup} filters correctly
- [ ] POST /api/franchise/submit saves data
- [ ] POST /api/auth/login authenticates
- [ ] Image paths resolve correctly

### Integration Tests
- [ ] Compare Franchise page with live site
- [ ] Compare Home page with live site
- [ ] Compare Login page with live site
- [ ] Test complete shopping flow
- [ ] Test user registration flow

## Implementation Order

1. **FranchisePage.jsx** - Complete rewrite (1-2 hours)
2. **LoginPage.jsx** - UI fix with background (30 min)
3. **HomePage.jsx** - Location logic + carousel (1 hour)
4. **ProductsPage.jsx** - Routing + filtering (1 hour)
5. **ProductDetailsPage.jsx** - Image loading (30 min)
6. **ProductController.cs** - Filter validation (30 min)
7. **Integration testing** - All pages (1 hour)

**Total Estimated Time**: 5-6 hours

## Success Criteria

✅ **Franchise page**: Exact visual match with 20+ fields working
✅ **Home page**: Carousel works, location selector works
✅ **Login page**: Cupcake background, pink theme, form works
✅ **Product pages**: Correct filtering, images load, data accurate
✅ **Backend APIs**: Return filtered data, paths resolve
✅ **Live site match**: All pages look identical to https://www.parisbakery.in

## Reference Assets

### CSS Files
- style.css (94KB) - Main stylesheet
- sparxdata.css - Additional styles
- css/plugins.css - Bootstrap overrides

### Image Paths
- Banners: `/images/banner/`
- Category: `/images/CategoryBackground/`
- Products: `/menupic/small/{id}s.png`, `/menupic/large/{id}l.png`
- Background: `/images/new-images/cupcake-candle.jpg`

### JavaScript Files
- /js/slick.js - Carousel
- /js/functions.js - Custom functions
- /libraries/lib.js - Bootstrap + plugins

## Notes

- All UI must match live site pixel-perfect
- No redesign, no optimization, pure restoration
- Use original ASPX as source of truth
- Test against live site for validation
