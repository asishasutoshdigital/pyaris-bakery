import { useState } from 'react';
import { Link } from 'react-router-dom';
import Swal from 'sweetalert2';

function ForgotPasswordPage() {
  const [username, setUsername] = useState('');
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setMessage('');
    
    try {
      // TODO: Implement forgot password API call
      // const response = await authAPI.forgotPassword({ username });
      
      Swal.fire({
        title: 'Password Reset',
        text: 'If the username exists, a password reset link has been sent to your email.',
        icon: 'success'
      });
      
      setUsername('');
    } catch (error) {
      setMessage('Username is incorrect.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="forgot-password-page">
      <div className="container py-5">
        <div className="row justify-content-center">
          <div className="col-md-6">
            <div className="card">
              <div className="card-body p-5">
                <h2 className="text-center mb-4">Forgot Password</h2>
                <p className="text-center text-muted mb-4">
                  Enter your username (mobile number) to reset your password
                </p>
                
                {message && (
                  <div className="alert alert-danger">{message}</div>
                )}
                
                <form onSubmit={handleSubmit}>
                  <div className="mb-3">
                    <label className="form-label">Username (Mobile Number)</label>
                    <input
                      type="text"
                      className="form-control"
                      value={username}
                      onChange={(e) => setUsername(e.target.value)}
                      required
                      placeholder="Enter your mobile number"
                    />
                  </div>
                  
                  <button 
                    type="submit" 
                    className="btn btn-primary w-100"
                    disabled={loading}
                  >
                    {loading ? 'Sending...' : 'Reset Password'}
                  </button>
                </form>
                
                <div className="text-center mt-3">
                  <p>
                    Remember your password? <Link to="/login">Login</Link>
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ForgotPasswordPage;
