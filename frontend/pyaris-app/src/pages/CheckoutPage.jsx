import { useEffect, useState } from "react";
import Swal from "sweetalert2";
import { useCartStore } from "../store/useStore";

function CheckoutPage() {
  const { cart, getTotal } = useCartStore();

  const [deliveryType, setDeliveryType] = useState("pickup");
  const [outlet, setOutlet] = useState("SELECT OUTLET");
  const [name, setName] = useState("");
  const [address1, setAddress1] = useState("");
  const [address2, setAddress2] = useState("");
  const [city, setCity] = useState("");
  const [pincode, setPincode] = useState("");
  const [mobile, setMobile] = useState("");
  const [date, setDate] = useState("");
  const [time, setTime] = useState("CHOOSE YOUR TIME");

  useEffect(() => {
    document.documentElement.style.backgroundColor = "#fbf3f3";
    document.body.style.backgroundColor = "#fbf3f3";
  }, []);

  const subtotal = getTotal();

  /* ---------- PLACE ORDER ---------- */
  const placeOrder = (mode) => {
    if (!date || time === "CHOOSE YOUR TIME") {
      Swal.fire("Please choose date & time", "", "error");
      return;
    }

    Swal.fire(
      "Order Placed",
      mode === "COD" ? "Cash On Delivery" : "Online Payment",
      "success"
    );
  };

  return (
    <div id="midpart" className="container justify-center">
      <div className="col-md-12 checkout-container">

        {/* 1. DELIVERY TYPE */}
        <div className="snSectionTitle">1. Choose Delivery Type</div>

        <div className="col-md-12 checkout-section-container">

          <div className="form-group col-md-12 row">
            <input
              type="radio"
              checked={deliveryType === "pickup"}
              onChange={() => setDeliveryType("pickup")}
            />{" "}
            <b>Pickup From Outlet</b>
          </div>

          <div className="col-md-12 row">
            <div className="col-md-3 checkout-details-title">
              Choose Outlet <span className="requiredStar">*</span>
            </div>
            <div className="col-md-5">
              <select
                className="form-control"
                value={outlet}
                onChange={(e) => setOutlet(e.target.value)}
              >
                <option>SELECT OUTLET</option>
                            <option >BBSR-BARAMUNDA</option>
                            <option >BBSR-SUM HOSPITAL</option>
                            <option >BBSR-VIVEKANANDA MARG</option>
                            <option >BBSR-BOMIKHAL</option>
                            <option >BBSR-SAILASHREE VIHAR</option>
                            <option >BBSR-SAHEED NAGAR</option>
                            <option >BBSR-JAGAMARA</option>
                            <option >BBSR-JANPATH</option>
                            <option >BBSR-JHARPADA</option>
                            <option >CTC-CDA</option>
                            <option >CTC-NAYASARAK</option>
                            <option >CTC-COLLEGESQUARE</option>
                            <option >CTC-SEC6 CDA</option>
                            <option >BAM-GANDHI NAGAR</option>
                            <option >BALESWAR-STATION ROAD</option>
                            <option >JAJPUR-SURYANSH</option>
                            <option >BAM-ASKAROAD</option>
              </select>
            </div>
          </div>

          <div className="form-group col-md-12 row">
            <input
              type="radio"
              checked={deliveryType === "home"}
              onChange={() => setDeliveryType("home")}
            />{" "}
            <b>Home Delivery / Edit Address</b>
          </div>

          <div className="col-md-12 row">
            <div className="col-md-3 checkout-details-title">
              Receiver Name <span className="requiredStar">*</span>
            </div>
            <div className="col-md-5">
              <input
                className="form-control"
                value={name}
                onChange={(e) => setName(e.target.value)}
              />
            </div>
          </div>

          <div className="col-md-12 row">
            <div className="col-md-3 checkout-details-title">
              Address Line 1 <span className="requiredStar">*</span>
            </div>
            <div className="col-md-5">
              <input
                className="form-control"
                value={address1}
                onChange={(e) => setAddress1(e.target.value)}
              />
            </div>
          </div>

          <div className="col-md-12 row">
            <div className="col-md-3 checkout-details-title">Address Line 2</div>
            <div className="col-md-5">
              <input
                className="form-control"
                value={address2}
                onChange={(e) => setAddress2(e.target.value)}
              />
            </div>
          </div>

          <div className="col-md-12 row">
            <div className="col-md-3 checkout-details-title">
              City <span className="requiredStar">*</span>
            </div>
            <div className="col-md-5">
              <input
                className="form-control"
                value={city}
                onChange={(e) => setCity(e.target.value)}
              />
            </div>
          </div>

          <div className="col-md-12 row">
            <div className="col-md-3 checkout-details-title">
              Pincode <span className="requiredStar">*</span>
            </div>
            <div className="col-md-5">
              <input
                className="form-control numeric"
                maxLength="6"
                value={pincode}
                onChange={(e) => setPincode(e.target.value)}
              />
            </div>
          </div>
        </div>

        {/* 3. PAYMENT REVIEW */}
        <div className="snSectionTitle">3. Payment Review</div>

        <div className="col-md-12 checkout-section-container">
          <div className="col-md-4 cartTotalBox">
            TOTAL CART ITEMS : {cart.length}
          </div>
          <div className="col-md-4 cartTotalBox">
            SUB TOTAL : Rs {subtotal.toFixed(0)}
          </div>
          <div className="col-md-4 cartTotalBox">
            TOTAL : Rs {subtotal.toFixed(0)}
          </div>
        </div>

        {/* 4. DATE & PAYMENT */}
        <div className="snSectionTitle">
          4. Choose Date & Payment Mode
        </div>

        <div className="col-md-12 checkout-section-container">
          <div className="col-md-12 row">
            <div className="col-md-3 checkout-details-title">
              Choose Date <span className="requiredStar">*</span>
            </div>
            <div className="col-md-6">
              <input
                type="date"
                className="form-control"
                value={date}
                onChange={(e) => setDate(e.target.value)}
              />
            </div>
          </div>

          <div className="col-md-12 row">
            <div className="col-md-3 checkout-details-title">
              Choose Time <span className="requiredStar">*</span>
            </div>
            <div className="col-md-6">
              <select
                className="form-control"
                value={time}
                onChange={(e) => setTime(e.target.value)}
              >
                <option>CHOOSE YOUR TIME</option>
                <option>9AM-12PM</option>
                <option>12PM-3PM</option>
                <option>3PM-6PM</option>
              </select>
            </div>
          </div>

          <div className="col-md-12 cart-inner-container justify-center">
            <div className="col-md-5 col-sm-12">
              <button
                className="rounded-btn red-btn"
                onClick={() => placeOrder("COD")}
              >
                CASH ON DELIVERY
              </button>
            </div>
            <div className="col-md-5 col-sm-12">
              <button
                className="rounded-btn red-btn"
                onClick={() => placeOrder("ONLINE")}
              >
                MAKE ONLINE PAYMENT
              </button>
            </div>
          </div>
        </div>

      </div>
    </div>
  );
}

export default CheckoutPage;
