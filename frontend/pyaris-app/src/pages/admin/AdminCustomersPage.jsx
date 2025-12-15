import { useState, useEffect } from 'react';

function AdminCustomersPage() {
  const [customers, setCustomers] = useState([]);
  const [loading, setLoading] = useState(false);

  return (
    <div className="container-fluid py-4">
      <h1 className="mb-4">Customer Management</h1>
      <div className="card">
        <div className="card-body">
          <div className="table-responsive">
            <table className="table table-hover">
              <thead>
                <tr>
                  <th>ID</th><th>Mobile</th><th>Name</th><th>Email</th><th>City</th><th>Pincode</th>
                </tr>
              </thead>
              <tbody>
                {customers.length === 0 ? (
                  <tr><td colSpan="6" className="text-center py-4">No customers found</td></tr>
                ) : (
                  customers.map((c) => (
                    <tr key={c.userId}>
                      <td>{c.userId}</td><td>{c.mobileNumber}</td><td>{c.name}</td>
                      <td>{c.email}</td><td>{c.city}</td><td>{c.pincode}</td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
  );
}

export default AdminCustomersPage;
