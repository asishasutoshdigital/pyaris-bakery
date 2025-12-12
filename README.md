# Paris Bakery - Modern E-Commerce Platform

A modern e-commerce platform for Paris Bakery built with **ASP.NET Core Web API** backend and **React** frontend.

## 🏗️ Architecture

### Backend
- **Framework**: ASP.NET Core 8.0 Web API
- **Database**: SQL Server
- **Features**:
  - RESTful API endpoints
  - CORS enabled for React frontend
  - Swagger/OpenAPI documentation
  - Session management
  - Payment gateway integration (PhonePe, CCAvenue)
  - SMS and Email notifications

### Frontend
- **Framework**: React 18 with Vite
- **Routing**: React Router v6
- **HTTP Client**: Axios
- **State Management**: React Context API
- **Features**:
  - Responsive design
  - Product catalog
  - Shopping cart
  - User authentication
  - Order management
  - Admin panel

## 📁 Project Structure

```
pyaris-bakery/
├── backend/                    # ASP.NET Core Web API
│   ├── Controllers/           # API controllers
│   ├── Models/                # Data models
│   ├── Services/              # Business logic services
│   ├── Data/                  # Database context
│   ├── Utils/                 # Utility classes
│   ├── Program.cs             # Application entry point
│   ├── appsettings.json       # Configuration
│   └── backend.csproj         # Project file
│
├── frontend/                  # React application
│   ├── src/
│   │   ├── components/        # Reusable components
│   │   ├── pages/             # Page components
│   │   ├── services/          # API service layer
│   │   ├── context/           # React context (state)
│   │   ├── utils/             # Utility functions
│   │   ├── App.jsx            # Main app component
│   │   └── main.jsx           # Application entry point
│   ├── public/                # Static assets
│   ├── package.json           # Dependencies
│   └── vite.config.js         # Vite configuration
│
└── README.md                  # This file
```

## 🚀 Getting Started

### Prerequisites

- **.NET 8.0 SDK** or later - [Download](https://dotnet.microsoft.com/download)
- **Node.js 18+** and npm - [Download](https://nodejs.org/)
- **SQL Server** - Connection string configured in `appsettings.json`

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd backend
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "your-connection-string-here"
   }
   ```

4. Build the project:
   ```bash
   dotnet build
   ```

5. Run the backend (default port: 5000):
   ```bash
   dotnet run
   ```

6. Access Swagger UI at: `http://localhost:5000/swagger`

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Update API URL in `.env.development` (if needed):
   ```
   VITE_API_URL=http://localhost:5000/api
   ```

4. Start the development server:
   ```bash
   npm run dev
   ```

5. Access the application at: `http://localhost:5173` (or the port shown in terminal)

## 🔧 Configuration

### Backend Configuration (`appsettings.json`)

- **ConnectionStrings**: Database connection
- **AppSettings**: Application settings including:
  - Admin credentials
  - Payment gateway settings (PhonePe, CCAvenue)
  - SMS configuration
  - Email settings

### Frontend Configuration

- **Environment Variables**: Configure in `.env.development` or `.env.production`
  - `VITE_API_URL`: Backend API URL

## 📡 API Endpoints

### Products
- `GET /api/products` - Get all products (with optional filters)
- `GET /api/products/{id}` - Get product by ID
- `GET /api/products/homepage-slider` - Get homepage slider images

### Users (Coming Soon)
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration
- `GET /api/users/profile` - Get user profile
- `PUT /api/users/profile` - Update user profile

### Cart (Coming Soon)
- `GET /api/cart` - Get cart items
- `POST /api/cart` - Add item to cart
- `PUT /api/cart/{id}` - Update cart item
- `DELETE /api/cart/{id}` - Remove item from cart

### Orders (Coming Soon)
- `GET /api/orders` - Get user orders
- `GET /api/orders/{id}` - Get order details
- `POST /api/orders` - Create new order

## 🛠️ Development

### Backend Development

- **Hot Reload**: Enabled by default with `dotnet watch run`
- **Debugging**: Use Visual Studio or VS Code with C# extension
- **Testing**: Add tests in a separate test project

### Frontend Development

- **Hot Module Replacement**: Enabled by Vite
- **Linting**: Run `npm run lint`
- **Build for Production**: Run `npm run build`

## 🚢 Deployment

### Backend Deployment

1. Publish the backend:
   ```bash
   cd backend
   dotnet publish -c Release -o ./publish
   ```

2. Deploy the `publish` folder to your hosting service (IIS, Azure, etc.)

### Frontend Deployment

1. Build the frontend:
   ```bash
   cd frontend
   npm run build
   ```

2. Deploy the `dist` folder to your web server or CDN

## 🔐 Security

- Configure CORS appropriately for production
- Use HTTPS in production
- Store sensitive data (API keys, passwords) in environment variables
- Implement proper authentication and authorization
- Validate and sanitize all user inputs

## 📝 Features

### Completed ✅
- Backend API structure
- Frontend React application
- Product listing API
- Homepage slider API
- State management with Context API
- Responsive layout

### In Progress 🚧
- User authentication
- Shopping cart management
- Order processing
- Payment gateway integration
- Admin panel
- Product management

## 🤝 Contributing

This is a private project for Paris Bakery. For questions or issues, contact the development team.

## 📄 License

Proprietary - All rights reserved by Paris Bakery

## 📞 Support

For support, email: info@parisbakery.in
