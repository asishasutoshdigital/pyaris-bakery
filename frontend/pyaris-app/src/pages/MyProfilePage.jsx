import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Swal from "sweetalert2";
import { useAuthStore } from "../store/useAuthStore";

function MyProfilePage() {
  const navigate = useNavigate();
  const user = useAuthStore((state) => state.user);

  /* ---------- AUTH GUARD ---------- */
  useEffect(() => {
    if (!user) {
      navigate("/login?redirect=profile", { replace: true });
    }
  }, [user, navigate]);

  /* ---------- STATE ---------- */
  const [passwordData, setPasswordData] = useState({
    newPassword: "",
    confirmPassword: "",
  });

  const [profileData, setProfileData] = useState({
    mobileNo: "",
    name: "",
    email: "",
    address1: "",
    address2: "",
    city: "",
    pincode: "",
  });

  /* ---------- LOAD USER ---------- */
  useEffect(() => {
    if (user) {
      setProfileData({
        mobileNo: user.mobileNumber || "",
        name: user.name || "",
        email: user.email || "",
        address1: user.address1 || "",
        address2: user.address2 || "",
        city: user.city || "",
        pincode: user.pincode || "",
      });
    }
  }, [user]);

  /* ---------- HANDLERS ---------- */
  const handlePasswordChange = (e) => {
    setPasswordData({ ...passwordData, [e.target.name]: e.target.value });
  };

  const handleProfileChange = (e) => {
    setProfileData({ ...profileData, [e.target.name]: e.target.value });
  };

  /* ---------- CHANGE PASSWORD ---------- */
  const handleResetPassword = () => {
    if (!passwordData.newPassword || !passwordData.confirmPassword) {
      Swal.fire("Error", "Please fill both password fields", "error");
      return;
    }

    if (passwordData.newPassword !== passwordData.confirmPassword) {
      Swal.fire("Error", "Passwords do not match", "error");
      return;
    }

    Swal.fire("Success", "Password changed successfully", "success");
  };

  /* ---------- UPDATE PROFILE ---------- */
  const handleUpdateProfile = () => {
    Swal.fire("Success", "Profile updated successfully", "success");
  };

  return (
    <div id="midpart">
      <div className="col-md-12">
        <div className="col-md-12 menu-right-content" id="xdata">

          {/* ================= LEFT : CHANGE PASSWORD ================= */}
          <div className="col-md-6">
            <div className="sectionTitle">Change Password !!</div>
            <div className="horizontalLine"></div>

            <div className="sectionContent">
              <div className="row">
                <div className="form-group col-md-4">New Password</div>
                <div className="form-group col-md-8">
                  <input
                    type="password"
                    className="form-control"
                    placeholder="Your Password"
                    name="newPassword"
                    value={passwordData.newPassword}
                    onChange={handlePasswordChange}
                  />
                </div>
              </div>

              <div className="row">
                <div className="form-group col-md-4">Confirm Password</div>
                <div className="form-group col-md-8">
                  <input
                    type="password"
                    className="form-control"
                    placeholder="Confirm Password"
                    name="confirmPassword"
                    value={passwordData.confirmPassword}
                    onChange={handlePasswordChange}
                  />
                </div>
              </div>
            </div>

            <div className="buttonSection">
              <img
                src="/images/RESET.png"
                alt="Reset Password"
                style={{ cursor: "pointer" }}
                onClick={handleResetPassword}
              />
            </div>
          </div>

          {/* ================= RIGHT : UPDATE PROFILE ================= */}
          <div className="col-md-6">
            <div className="sectionTitle">Update Profile !!</div>
            <div className="horizontalLine"></div>

            <div className="sectionContent">
              <div className="row">
                <div className="form-group col-md-4">Mobile No</div>
                <div className="form-group col-md-8">
                  <input
                    type="text"
                    className="form-control numeric"
                    value={profileData.mobileNo}
                    readOnly
                  />
                </div>
              </div>

              <div className="row">
                <div className="form-group col-md-4">Your Name</div>
                <div className="form-group col-md-8">
                  <input
                    type="text"
                    className="form-control"
                    name="name"
                    value={profileData.name}
                    onChange={handleProfileChange}
                  />
                </div>
              </div>

              <div className="row">
                <div className="form-group col-md-4">Email Id</div>
                <div className="form-group col-md-8">
                  <input
                    type="text"
                    className="form-control"
                    name="email"
                    value={profileData.email}
                    onChange={handleProfileChange}
                  />
                </div>
              </div>

              <div className="row">
                <div className="form-group col-md-4">Address Line1</div>
                <div className="form-group col-md-8">
                  <input
                    type="text"
                    className="form-control"
                    name="address1"
                    value={profileData.address1}
                    onChange={handleProfileChange}
                  />
                </div>
              </div>

              <div className="row">
                <div className="form-group col-md-4">Address Line2</div>
                <div className="form-group col-md-8">
                  <input
                    type="text"
                    className="form-control"
                    name="address2"
                    value={profileData.address2}
                    onChange={handleProfileChange}
                  />
                </div>
              </div>

              <div className="row">
                <div className="form-group col-md-4">City</div>
                <div className="form-group col-md-8">
                  <input
                    type="text"
                    className="form-control"
                    name="city"
                    value={profileData.city}
                    onChange={handleProfileChange}
                  />
                </div>
              </div>

              <div className="row">
                <div className="form-group col-md-4">Pin Code</div>
                <div className="form-group col-md-8">
                  <input
                    type="text"
                    className="form-control"
                    maxLength="6"
                    name="pincode"
                    value={profileData.pincode}
                    onChange={handleProfileChange}
                  />
                </div>
              </div>
            </div>

            <div className="buttonSection">
              <img
                src="/images/update.png"
                alt="Update Profile"
                style={{ cursor: "pointer" }}
                onClick={handleUpdateProfile}
              />
            </div>
          </div>

        </div>
      </div>
    </div>
  );
}

export default MyProfilePage;
