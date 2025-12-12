import { useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import axios from 'axios';

function Login() {
  const navigate = useNavigate();
  const location = useLocation();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [usernameError, setUsernameError] = useState('');
  const [errorText, setErrorText] = useState('');

  const handleContinue = async () => {
    setUsernameError('');
    
    if (!username || username.length !== 10) {
      setUsernameError('Please enter valid Mobile number');
      return;
    }

    try {
      const response = await axios.post('http://localhost:5000/api/auth/check-user', 
        JSON.stringify(username), 
        { headers: { 'Content-Type': 'application/json' } }
      );

      if (response.data.exists) {
        setShowPassword(true);
      } else {
        navigate('/register', { state: { mobileNo: username } });
      }
    } catch (error) {
      console.error('Error checking user:', error);
      setUsernameError('Error checking user');
    }
  };

  const handleLogin = async () => {
    setErrorText('');

    if (!username || !password) {
      setErrorText('Please Enter Mobile number and Password');
      return;
    }

    try {
      const response = await axios.post('http://localhost:5000/api/auth/login', {
        mobileNo: username,
        password: password
      }, { withCredentials: true });

      if (response.data.success) {
        const searchParams = new URLSearchParams(location.search);
        if (searchParams.get('xs')) {
          navigate('/sparknext');
        } else {
          navigate('/');
        }
      }
    } catch (error) {
      if (error.response?.data?.message) {
        setErrorText(error.response.data.message);
      } else {
        setErrorText('Login failed');
      }
    }
  };

  return (
    <div id="midpart">
      <div className="cupcake-candle">
        <img src="images/new-images/cupcake-candle.jpg" alt="cupcake-background" width="70%" height="70%" />
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
                readOnly={showPassword}
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
                  {errorText && <div className="error-text">{errorText}</div>}
                </div>
              </div>
            )}
          </div>
          
          {!showPassword ? (
            <div id="ContinueButtonSection" className="buttonSection">
              <button className="primary-btn" onClick={handleContinue}>
                CONTINUE
              </button>
            </div>
          ) : (
            <div id="LoginButtonSection">
              <div className="buttonSection">
                <button className="primary-btn" onClick={handleLogin}>
                  LOG IN
                </button>
              </div>
              <div className="buttonSection forgot-password-link">
                <a href="/forgot-password">Forgot Password?</a>
              </div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

export default Login;
