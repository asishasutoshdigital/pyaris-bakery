# Pyaris Bakery - React + ASP.NET Core Migration

## Project Structure

```
pyaris-bakery/
├── Backend/
│   └── PyarisAPI/                 # ASP.NET Core 8 Web API
│       ├── Controllers/           # API Endpoints
│       │   ├── ProductsController.cs
│       │   ├── OrdersController.cs
│       │   └── PaymentController.cs
│       ├── Services/              # Business Logic
│       │   ├── ProductService.cs
│       │   ├── OrderService.cs
│       │   └── PaymentService.cs
│       ├── Models/                # Data Models
│       │   └── Entities.cs
│       ├── Data/                  # Database Context
│       │   └── PyarisDbContext.cs
│       ├── PyarisAPI.csproj       # Project File
│       ├── Program.cs             # Startup Configuration
│       ├── appsettings.json       # Configuration
│       └── wwwroot/               # Static files (React build output)
│
├── Frontend/
│   └── pyaris-app/                # React 18 + Vite
│       ├── src/
│       │   ├── components/        # React Components
│       │   ├── pages/             # Page Components
│       │   ├── services/          # API Services
│       │   ├── store/             # Zustand Store (State Management)
│       │   ├── styles/            # CSS Files
│       │   ├── main.jsx           # Entry Point
│       │   └── App.jsx            # Root Component
│       ├── vite.config.js         # Vite Configuration
│       ├── package.json           # Dependencies
│       └── index.html             # HTML Template
│
├── build.bat                      # Build Script
├── create-exe.bat                 # EXE Creation Script
└── installer.nsi                  # NSIS Installer Configuration

```

## Features Implemented

### Backend (ASP.NET Core 8)
- ✅ RESTful API for Products
- ✅ Order Management System
- ✅ Payment Gateway Integration (PhonePe)
- ✅ Entity Framework Core with SQL Server
- ✅ CORS Configuration for React
- ✅ Logging Support
- ✅ Static file serving for React app

### Frontend (React 18 + Vite)
- ✅ Home Page
- ✅ Products Listing
- ✅ Shopping Cart
- ✅ State Management (Zustand)
- ✅ API Integration (Axios)
- ✅ Bootstrap Styling

## Installation & Setup

### Prerequisites
- Windows 7 or later
- Node.js 18+ (for development)
- .NET 8 SDK (for development)
- SQL Server 2019+

### Development Setup

1. **Clone/Extract the project**
```bash
cd pyaris-bakery
```

2. **Build the entire project**
```bash
build.bat
```

This will:
- Install React dependencies
- Build the React frontend
- Restore NuGet packages
- Publish ASP.NET Core backend

3. **Run the application**

   **Option A: Run Published Version**
   ```bash
   Backend\PyarisAPI\publish\PyarisAPI.exe
   ```

   **Option B: Run in Development**
   
   Terminal 1 - React Dev Server:
   ```bash
   cd Frontend\pyaris-app
   npm install
   npm run dev
   ```
   
   Terminal 2 - ASP.NET Core API:
   ```bash
   cd Backend\PyarisAPI
   dotnet run
   ```

4. **Access the application**
   - Frontend: http://localhost:5173 (dev) or http://localhost:5000 (production)
   - API: http://localhost:5000/api

## Configuration

### Database Configuration
Edit `Backend/PyarisAPI/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=YOUR_SERVER;database=NODEPOINT;user=YOUR_USER;pwd=YOUR_PASSWORD;"
  }
}
```

### Payment Gateway Configuration
Update the payment settings in `appsettings.json`:

```json
{
  "PaymentGateway": {
    "Provider": "PhonePe",
    "PhonePe": {
      "MerchantId": "YOUR_MERCHANT_ID",
      "SaltKey": "YOUR_SALT_KEY",
      ...
    }
  }
}
```

## Building for Distribution

### Create Standalone EXE

1. **Build everything**
   ```bash
   build.bat
   ```

2. **Create distribution package**
   ```bash
   create-exe.bat
   ```

   This creates a `Distribution` folder with:
   - PyarisAPI.exe
   - All dependencies
   - Configuration files
   - START.bat (launcher script)
   - README.txt

3. **Create Installer (Optional)**
   
   Install NSIS: https://nsis.sourceforge.io/
   
   ```bash
   makensis installer.nsi
   ```
   
   This creates `PyarisBakery-Setup.exe`

## API Endpoints

### Products
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `GET /api/products/group/{group}` - Get by group
- `GET /api/products/subgroup/{subGroup}` - Get by sub-group
- `POST /api/products` - Create product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product

### Orders
- `GET /api/orders` - Get all orders
- `GET /api/orders/{id}` - Get order details
- `GET /api/orders/customer/{customerId}` - Get customer orders
- `POST /api/orders` - Create order
- `PUT /api/orders/{id}/status` - Update order status

### Payment
- `POST /api/payment/phonepe/initiate` - Initiate PhonePe payment
- `POST /api/payment/phonepe/verify` - Verify payment
- `POST /api/payment/phonepe/refund` - Process refund

## Technology Stack

### Backend
- **Framework**: ASP.NET Core 8
- **ORM**: Entity Framework Core 8
- **Database**: SQL Server
- **API Style**: RESTful
- **Logging**: log4net, built-in ILogger

### Frontend
- **Framework**: React 18
- **Build Tool**: Vite
- **State Management**: Zustand
- **HTTP Client**: Axios
- **Styling**: Bootstrap 5
- **Routing**: React Router DOM

### DevOps
- **Build**: PowerShell/Batch scripts
- **Packaging**: NSIS Installer
- **Deployment**: Self-contained executable

## Migration from ASP.NET Web Forms

### Completed Conversions
1. **Default.aspx** → Home.jsx component
2. **Products.aspx** → Products.jsx component
3. **Shopping Cart** → Cart.jsx component
4. **Backend Logic** → Services layer

### Remaining Work
- Convert login/authentication pages
- Implement user profile pages
- Convert payment gateway pages
- Migrate report generation
- Implement admin panel

## Running the Standalone EXE

1. Extract or run the installer
2. Double-click `START.bat` or `Pyaris Bakery.lnk`
3. The application will start and open automatically
4. Access via browser: http://localhost:5000

## Troubleshooting

### Port Already in Use
If port 5000 is already in use, edit `appsettings.json` and change:
```json
"Kestrel": {
  "Endpoints": {
    "Http": {
      "Url": "http://localhost:5001"
    }
  }
}
```

### Database Connection Failed
- Verify SQL Server is running
- Check connection string in appsettings.json
- Ensure database NODEPOINT exists
- Verify user credentials

### Frontend Not Loading
- Ensure React build completed successfully
- Check that wwwroot folder contains index.html
- Verify CORS is enabled in Program.cs

## Next Steps

1. **Complete Authentication**
   - Add login/register pages
   - Implement JWT authentication
   - Add user profile management

2. **Admin Panel**
   - Product management
   - Order management
   - User management
   - Analytics dashboard

3. **Testing**
   - Unit tests for services
   - Integration tests for API
   - E2E tests for React components

4. **Performance**
   - Implement caching
   - Optimize database queries
   - Add pagination for products

5. **Deployment**
   - Set up CI/CD pipeline
   - Configure Azure/AWS deployment
   - Set up monitoring and logging

## Support

For issues or questions, refer to:
- ASP.NET Core Documentation: https://docs.microsoft.com/dotnet/core/
- React Documentation: https://react.dev
- Vite Documentation: https://vitejs.dev

## License

This project is proprietary to Pyaris Bakery.
