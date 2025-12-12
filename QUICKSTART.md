# Quick Start Guide

## For End Users

### Running the Application

1. **Download the Executable**
   - Get `PyarisBakery-Setup.exe` or the standalone executable

2. **Install (if using installer)**
   - Run `PyarisBakery-Setup.exe`
   - Follow the installation wizard
   - Choose installation location

3. **Launch the Application**
   - Double-click `Pyaris Bakery` shortcut from Start Menu or Desktop
   - Or run `START.bat` from the installation folder
   - The application will open in your browser

4. **Use the Application**
   - Browse products
   - Add items to cart
   - Proceed to checkout
   - Complete payment
   - Track your order

## For Developers

### Build from Source

```bash
# 1. Navigate to project root
cd pyaris-bakery

# 2. Run the build script
build.bat

# 3. This will:
#    - Install React dependencies
#    - Build React frontend
#    - Restore NuGet packages
#    - Publish ASP.NET Core backend

# 4. Start the application
Backend\PyarisAPI\publish\PyarisAPI.exe
```

### Development Server

```bash
# Terminal 1: Start React dev server
cd Frontend\pyaris-app
npm install
npm run dev

# Terminal 2: Start ASP.NET Core API
cd Backend\PyarisAPI
dotnet run

# Access at: http://localhost:5173 (React) and http://localhost:5000 (API)
```

### Create Distribution Package

```bash
# After building, create the standalone package
create-exe.bat

# This creates:
# - Distribution/ folder with PyarisAPI.exe
# - START.bat launcher
# - README.txt
```

### Create Installer

```bash
# Requires NSIS to be installed
makensis installer.nsi

# Creates: PyarisBakery-Setup.exe
```

## Environment Details

### Database
- **Server**: 192.168.0.230
- **Database**: NODEPOINT
- **User**: pyaris
- **Tables**: XMaster Menu, Orders, Customers, etc.

### Payment Gateway
- **Provider**: PhonePe
- **Merchant ID**: PARISBAKERYONLINE
- **Environment**: Pre-production

### Application Ports
- **Frontend**: http://localhost:5000 (production) / http://localhost:5173 (dev)
- **API**: http://localhost:5000/api
- **Database**: 192.168.0.230:1433

## API Examples

### Get All Products
```
GET http://localhost:5000/api/products
```

### Get Product by ID
```
GET http://localhost:5000/api/products/1
```

### Create Order
```
POST http://localhost:5000/api/orders
Content-Type: application/json

{
  "customerId": "customer123",
  "totalAmount": 1000,
  "deliveryAddress": "123 Main St",
  "phoneNumber": "9876543210",
  "paymentMethod": "PHONEPE",
  "items": [
    {
      "productId": 1,
      "quantity": 2,
      "price": 100,
      "tax": 18
    }
  ]
}
```

### Initiate Payment
```
POST http://localhost:5000/api/payment/phonepe/initiate
Content-Type: application/json

{
  "orderId": "ORDER123",
  "amount": 1000,
  "customerId": "customer123"
}
```

## System Requirements

- **OS**: Windows 7 or later
- **RAM**: 2 GB minimum
- **Disk**: 500 MB
- **Internet**: Required for payment gateway
- **Database**: SQL Server 2019+

## First Time Setup

1. **Verify Database Connection**
   - Ensure SQL Server is accessible
   - Check database NODEPOINT exists
   - Verify user credentials

2. **Verify Payment Gateway**
   - Ensure internet connection
   - Test payment gateway credentials
   - PhonePe account should be active

3. **Access Application**
   - Open http://localhost:5000 in browser
   - Browse products
   - Test shopping cart
   - Create sample order

## Troubleshooting

### Application Won't Start
- Check if port 5000 is available
- Verify database is running
- Check appsettings.json configuration

### Database Connection Error
- Verify SQL Server is running on 192.168.0.230
- Check username/password
- Ensure NODEPOINT database exists

### Frontend Not Loading
- Clear browser cache
- Hard refresh (Ctrl + Shift + R)
- Check browser console for errors

### Payment Gateway Issues
- Verify internet connection
- Check PhonePe credentials
- Check merchant ID configuration

## Common Tasks

### Change Database Server
Edit `Backend\PyarisAPI\appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=YOUR_SERVER;database=NODEPOINT;..."
  }
}
```

### Change Application Port
Edit `Backend\PyarisAPI\appsettings.json`:
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://localhost:5001"
      }
    }
  }
}
```

### Enable Development Logging
Edit `Backend\PyarisAPI\appsettings.Development.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  }
}
```

## Support & Contact

For issues, contact the development team or refer to the MIGRATION_GUIDE.md for detailed documentation.
