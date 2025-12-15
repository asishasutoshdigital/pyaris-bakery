# UI Rewrite Plan - Fixing All Pages to Match ASPX Design

## Issues Reported
- HomePage: showing only half screen
- ProductsPage: design not matching spark.aspx
- ProductDetailsPage: design not matching sparkdetails.aspx  
- CartPage: design not matching sparkcart.aspx
- LoginPage: design not matching login.aspx
- RegisterPage: design not matching Register.aspx
- FranchisePage: design not matching Franchise.aspx

## Root Cause
All React pages were created with Bootstrap 5 components instead of using the original ASPX HTML structure and CSS classes from style.css.

## Solution
Rewrite each page to use exact ASPX structure:
- Use original CSS class names (category-card-container, product-listing-container, etc.)
- Use original HTML structure from ASPX files
- Remove Bootstrap 5 specific classes (container, row, col-md-*, card, etc.)
- Keep original JavaScript hooks (numeric class, slick-center, etc.)

## Pages to Rewrite (Priority Order)

### Critical (User Reported)
1. ✅ HomePage.jsx - DONE (already fixed in commit bab1606)
2. ProductsPage.jsx - spark.aspx structure
3. ProductDetailsPage.jsx - sparkdetails.aspx structure  
4. CartPage.jsx - sparkcart.aspx structure
5. LoginPage.jsx - login.aspx structure
6. RegisterPage.jsx - Register.aspx structure
7. FranchisePage.jsx - Franchise.aspx structure

### Important (User Flow)
8. CheckoutPage.jsx - sparknext.aspx
9. OrderSuccessPage.jsx - sparkover.aspx
10. OrderFailPage.jsx - sparkfail.aspx

### Secondary
11. AboutPage.jsx - about.aspx
12. ContactPage.jsx - contact.aspx
13. GalleryPage.jsx - gallery.aspx
14. PrivacyPage.jsx - privacy.aspx
15. TermsPage.jsx - terms.aspx
16. RefundPage.jsx - refund.aspx

... (remaining 32 pages)

## Implementation Strategy
1. Open original ASPX file
2. Extract complete HTML structure (ContentPlaceHolder1 content)
3. Convert ASP.NET server controls to React:
   - asp:Repeater → .map()
   - asp:Button → <button>
   - runat="server" → React state/props
   - <%# Eval("field") %> → {item.field}
4. Keep ALL CSS classes exactly as in ASPX
5. Convert URLs: spark.aspx → /products
6. Add React hooks for API calls
7. Preserve original JavaScript (jQuery, slick, etc.)

## Status
- HomePage: ✅ Fixed (commit bab1606)
- Header: ✅ Fixed (commit 8903fa8) 
- Footer: ✅ Fixed (commit 8903fa8)
- ProductsPage: ⏳ IN PROGRESS
- ProductDetailsPage: ⏳ TODO
- CartPage: ⏳ TODO
- LoginPage: ⏳ TODO
- RegisterPage: ⏳ TODO
- FranchisePage: ⏳ TODO
- Remaining 41 pages: ⏳ TODO
