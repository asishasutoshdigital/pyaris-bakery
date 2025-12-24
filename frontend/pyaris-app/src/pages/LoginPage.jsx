import { useState } from "react";
import { Link, useNavigate, useSearchParams } from "react-router-dom";
import Swal from "sweetalert2";
import { authAPI } from "../services/api";
import { useAuthStore } from "../store/useAuthStore";

function LoginPage() {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();

  const login = useAuthStore((state) => state.login);

  const [mobile, setMobile] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [loading, setLoading] = useState(false);

  const [mobileError, setMobileError] = useState("");
  const [passwordError, setPasswordError] = useState("");

  const redirectTo = searchParams.get("redirect"); // 🔥 IMPORTANT

  /* -------- Allow only numbers -------- */
  const allowOnlyNumbers = (e) => {
    const code = e.which ? e.which : e.keyCode;
    if (code < 48 || code > 57) e.preventDefault();
  };

  /* -------- CONTINUE (ASPX ContinueBtnClicked) -------- */
  const handleContinue = async (e) => {
    e.preventDefault();
    setMobileError("");
    setPasswordError("");

    if (mobile.length !== 10) {
      setMobileError("Please enter valid Mobile number");
      return;
    }

    try {
      setLoading(true);

      const res = await authAPI.checkUser({
        MobileNo: mobile,
      });

      if (res.data.exists) {
        setShowPassword(true); // show password section
      } else {
        // same as Response.Redirect("Register.aspx")
        navigate("/register", { state: { mobile } });
      }
    } catch {
      Swal.fire("Error", "Unable to verify the number", "error");
    } finally {
      setLoading(false);
    }
  };

  /* -------- LOGIN (ASPX LoginandProceed) -------- */
  const handleLogin = async (e) => {
    e.preventDefault();
    setPasswordError("");

    if (!password) {
      setPasswordError("Please enter password");
      return;
    }

    try {
      setLoading(true);

      const res = await authAPI.login({
        MobileNo: mobile,
        Password: password,
      });

      if (res.data.success) {
        // ✅ STORE USER
        login(res.data.user);
        localStorage.setItem("user", JSON.stringify(res.data.user));

        Swal.fire("Success", "Login successful", "success");

        // ✅ ASPX REDIRECT LOGIC
        if (redirectTo) {
          navigate("/" + redirectTo, { replace: true });
        } else {
          navigate("/", { replace: true });
        }
      }
    } catch {
      setPasswordError(
        "The mobile number and password combination provided is not valid"
      );
    } finally {
      setLoading(false);
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
            width="70%"
            height="70%"
          />
        </div>

        <div className="container login-container">
          <div className="col-md-4 col-sm-6 col-xs-12">

            <div className="sectionTitle">Login</div>

            <div className="sectionContent">
              <div className="form-group">Username</div>

              <div className="form-group">
                <input
                  className="form-control numeric"
                  type="text"
                  maxLength="10"
                  placeholder="Your Mobile No"
                  value={mobile}
                  readOnly={showPassword}
                  onChange={(e) => setMobile(e.target.value)}
                  onKeyPress={allowOnlyNumbers}
                />
                {mobileError && (
                  <div className="error-text">{mobileError}</div>
                )}
              </div>

              {showPassword && (
                <>
                  <div className="form-group">Password</div>
                  <div className="form-group">
                    <input
                      className="form-control"
                      type="password"
                      placeholder="Your Password"
                      value={password}
                      onChange={(e) => setPassword(e.target.value)}
                    />
                    {passwordError && (
                      <div className="error-text">{passwordError}</div>
                    )}
                  </div>
                </>
              )}
            </div>

            {!showPassword ? (
              <div className="buttonSection">
                <button
                  className="primary-btn"
                  onClick={handleContinue}
                  disabled={loading}
                >
                  CONTINUE
                </button>
              </div>
            ) : (
              <>
                <div className="buttonSection">
                  <button
                    className="primary-btn"
                    onClick={handleLogin}
                    disabled={loading}
                  >
                    LOG IN
                  </button>
                </div>

                <div className="buttonSection forgot-password-link">
                  <Link to="/forgot-password">Forgot Password?</Link>
                </div>
              </>
            )}

          </div>
        </div>
      </div>
    </>
  );
}

export default LoginPage;
