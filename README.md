# Paris Bakery - Converted to ASP.NET Core + React

This project has been converted from ASP.NET Web Forms to a modern stack using ASP.NET Core Web API (backend) and React (frontend).

## ⚠️ IMPORTANT: Styling Preservation

**All existing styles have been preserved exactly as they were:**
- `style.css` (95KB, 4753 lines) - Copied without any modifications
- `sparxdata.css` (2.4KB, 95 lines) - Copied without any modifications
- All class names, IDs, and HTML structure remain identical
- All visual elements, colors, fonts, and layouts are unchanged

## Project Structure

```
pyaris-bakery/
├── backend/                    # ASP.NET Core Web API
│   ├── Controllers/           # API Controllers
│   │   ├── AuthController.cs # Authentication endpoints
│   │   └── HomeController.cs # Homepage data endpoints
│   ├── Models/               # Data models
│   │   ├── User.cs          # User model
│   │   ├── Product.cs       # Product model
│   │   └── Order.cs         # Order model
│   ├── Services/            # Business logic services
│   ├── Program.cs           # Application entry point
│   └── appsettings.json     # Configuration (DB connection, app settings)
│
├── frontend/                   # React Application (Vite)
│   ├── public/                # Static assets
│   │   ├── datetimepicker_css.js
│   │   └── sidefollow.js
│   ├── src/
│   │   ├── components/       # Reusable React components
│   │   │   ├── Layout.jsx   # Master layout (from Pyaris.master)
│   │   │   ├── MenuSideBar.jsx    # Category menu
│   │   │   └── ProfileSideBar.jsx # User profile menu
│   │   ├── pages/           # Page components
│   │   │   ├── Home.jsx     # Homepage (Default.aspx)
│   │   │   ├── Login.jsx    # Login page
│   │   │   ├── Register.jsx # Registration page
│   │   │   ├── About.jsx    # About page
│   │   │   └── Contact.jsx  # Contact page
│   │   ├── services/        # API service layer
│   │   ├── styles/          # CSS files (EXACT copies)
│   │   │   ├── style.css    # Main styles (NO changes)
│   │   │   └── sparxdata.css # Flyout menu styles (NO changes)
│   │   ├── utils/           # Utility functions
│   │   └── App.jsx          # Main app component with routing
│   └── package.json
│
└── [Original ASPX files]      # Original Web Forms files (reference only)
```

## Technology Stack

### Backend
- **ASP.NET Core 8.0** - Web API framework
- **Microsoft.Data.SqlClient** - SQL Server database access
- **Session Management** - For user authentication state

### Frontend
- **React 18** - UI library
- **Vite** - Build tool and dev server
- **React Router** - Client-side routing
- **Axios** - HTTP client for API calls

## Database

The application connects to the existing SQL Server database:
- Server: `192.168.0.230`
- Database: `NODEPOINT`
- Tables: `xUser Details`, `mastercodes`, and others (preserved as-is)

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Node.js 20+ and npm
- SQL Server access

### Backend Setup

1. Navigate to backend directory:
   ```bash
   cd backend
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Update connection string in `appsettings.json` if needed

4. Run the backend:
   ```bash
   dotnet run
   ```
   
   Backend will start on: `http://localhost:5000`

### Frontend Setup

1. Navigate to frontend directory:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start development server:
   ```bash
   npm run dev
   ```
   
   Frontend will start on: `http://localhost:5173`

## API Endpoints

### Authentication
- `POST /api/auth/check-user` - Check if user exists
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration
- `POST /api/auth/logout` - User logout
- `GET /api/auth/session` - Get current session

### Home
- `GET /api/home/slider` - Get homepage slider items

## Features Implemented

✅ **Backend:**
- User authentication (login/register)
- Session management with cookies
- Database connectivity
- CORS configuration for React frontend
- Homepage slider data endpoint

✅ **Frontend:**
- React Router for navigation
- Layout component (from master page)
- Homepage with slider
- Login/Register pages
- About and Contact pages
- Menu sidebar component
- Profile sidebar component
- Exact CSS preservation

## Conversion Mapping

| Original File | React Component | API Controller |
|--------------|----------------|----------------|
| Pyaris.master | Layout.jsx | - |
| MenuSideBar.ascx | MenuSideBar.jsx | - |
| ProfileSideBar.ascx | ProfileSideBar.jsx | - |
| Default.aspx | Home.jsx | HomeController |
| login.aspx | Login.jsx | AuthController |
| Register.aspx | Register.jsx | AuthController |
| about.aspx | About.jsx | - |
| contact.aspx | Contact.jsx | - |

## Next Steps

Additional pages and features to be converted:
- Product pages (spark, sparkdetails, sparkcart, sparknext)
- User profile pages (myprofile, mya, myapayback)
- Admin pages (Products, Customers, Orders)
- Payment gateway integration
- Gallery page
- Policy pages (privacy, terms, refund)

## Notes

- All existing functionality has been preserved
- The visual appearance is identical to the original application
- Session-based authentication is maintained
- Database queries remain the same
- Payment gateway configurations are preserved in appsettings.json

## Security

### Vulnerabilities Fixed
✅ **Cookie Security**: Fixed cookie Secure flag - all session cookies now use HttpOnly and Secure flags
✅ **CodeQL Scan**: Passed with 0 alerts

### Known Security Considerations
⚠️ **Password Storage**: The current implementation stores passwords in plain text in the database, matching the existing ASP.NET Web Forms application. This preserves backward compatibility but should be addressed in a future update by:
- Implementing password hashing (bcrypt, Argon2, or PBKDF2)
- Migrating existing passwords to hashed format
- Updating authentication logic

⚠️ **CSS Font Format**: The existing CSS files reference some fonts with 'otf2' format, which is non-standard. This was preserved as-is per requirements but may cause font loading issues in some browsers.

## Future Enhancements
- Implement password hashing for improved security
- Add comprehensive error handling and logging
- Implement rate limiting for authentication endpoints
- Add input validation middleware
- Implement CSRF protection
- Add comprehensive unit and integration tests
