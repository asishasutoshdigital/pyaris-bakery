import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import Swal from 'sweetalert2';

function LoginPage() {
  const navigate = useNavigate();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [usernameError, setUsernameError] = useState('');
  const [passwordError, setPasswordError] = useState('');

  const allowOnlyNumbers = (e) => {
    const charCode = e.which ? e.which : e.keyCode;
    if (charCode < 48 || charCode > 57) {
      e.preventDefault();
    }
  };

  const handleContinue = (e) => {
    e.preventDefault();
    setUsernameError('');
    
    if (username.length !== 10) {
      setUsernameError('Please enter a valid 10-digit mobile number');
      return;
    }
    
    // Show password section
    setShowPassword(true);
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    setPasswordError('');
    
    if (!password) {
      setPasswordError('Please enter your password');
      return;
    }
    
    try {
      // TODO: Implement actual login API call
      // const response = await authAPI.login({ mobile: username, password });
      
      Swal.fire({
        title: 'Success!',
        text: 'Login successful',
        icon: 'success'
      });
      
      // Navigate to home or previous page
      navigate('/');
    } catch (error) {
      setPasswordError('Invalid username or password');
      Swal.fire({
        title: 'Error',
        text: 'Login failed. Please check your credentials.',
        icon: 'error'
      });
    }
  };

  return (
    <>
      <style>{`
        html, body, #root, .App {
          background-color: #fbf3f3 !important;
        }
      `}</style>
      
      <div id="midpart">
        <div className="cupcake-candle">
          <img 
            src="/images/new-images/cupcake-candle.jpg" 
            alt="cupcake-background" 
            style={{width: '70%', height: '70%'}} 
          />
        </div>
        
        <div className="container login-container">
          <div className="col-md-4 col-sm-6 col-xs-12">
            <div className="sectionTitle">
              Login
            </div>
            
            <div className="sectionContent">
              <div className="form-group">
                Username
              </div>
              <div className="form-group">
                <input 
                  id="Username"
                  className="form-control numeric" 
                  type="text" 
                  maxLength="10" 
                  placeholder="Your Mobile No"
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                  onKeyPress={allowOnlyNumbers}
                />
                {usernameError && <div className="error-text">{usernameError}</div>}
              </div>
              
              {showPassword && (
                <div id="PasswordSection">
                  <div className="form-group">
                    Password
                  </div>
                  <div className="form-group">
                    <input 
                      id="Password"
                      className="form-control" 
                      type="password" 
                      placeholder="Your Password"
                      value={password}
                      onChange={(e) => setPassword(e.target.value)}
                    />
                    {passwordError && <div className="error-text">{passwordError}</div>}
                  </div>
                </div>
              )}
            </div>
            
            {!showPassword ? (
              <div id="ContinueButtonSection" className="buttonSection">
                <button 
                  id="ContinueBtn"
                  className="primary-btn" 
                  onClick={handleContinue}
                >
                  CONTINUE
                </button>
              </div>
            ) : (
              <div id="LoginButtonSection">
                <div className="buttonSection">
                  <button 
                    id="LoginBtn"
                    className="primary-btn" 
                    onClick={handleLogin}
                  >
                    LOG IN
                  </button>
                </div>
                <div className="buttonSection forgot-password-link">
                  <Link to="/forgot-password">Forgot Password?</Link>
                </div>
              </div>
            )}
          </div>
        </div>
      </div>
    </>
  );
}

export default LoginPage;
