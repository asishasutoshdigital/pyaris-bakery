import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Swal from "sweetalert2";
import { useCartStore } from "../store/useStore";
import { useDiscountStore } from "../store/useDiscountStore";

function CartPage() {
  const navigate = useNavigate();
  const { cart, updateQuantity, removeFromCart, getTotal } = useCartStore();

  const {
    discountAmount,
    discountSource,
    giftCards,
    applyRedeem,
    applyGiftCard,
    removeGiftCard,
    clearDiscount,
  } = useDiscountStore();

  const [showRedeem, setShowRedeem] = useState(false);
  const [showGift, setShowGift] = useState(false);
  const [redeemPoints, setRedeemPoints] = useState("");
  const [giftCode, setGiftCode] = useState("");

  useEffect(() => {
    document.documentElement.style.backgroundColor = "#fbf3f3";
    document.body.style.backgroundColor = "#fbf3f3";
  }, []);

  const subtotal = getTotal();
  const finalTotal = Math.max(subtotal - discountAmount, 0);
  const MIN_ORDER = 300;

  /* ---------- Quantity ---------- */
  const handleUpdateQuantity = (id, change) => {
    const item = cart.find((i) => i.id === id);
    if (!item) return;
    const qty = item.quantity + change;
    if (qty > 0) updateQuantity(id, qty);
  };

  /* ---------- Remove ---------- */
  const handleRemove = (id) => {
    Swal.fire({
      title: "Remove Item?",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Remove",
    }).then((r) => r.isConfirmed && removeFromCart(id));
  };

  /* ---------- Redeem ---------- */
  const handleRedeem = () => {
    if (discountSource === "GiftCard") {
      Swal.fire("Remove gift card first", "", "error");
      return;
    }

    const points = Number(redeemPoints);
    const availablePoints = 500; // TODO: API
    const loyaltyPercent = 1; // TODO: MasterCodes

    if (points < 300) {
      Swal.fire("Minimum 300 points required", "", "error");
      return;
    }

    if (points > availablePoints) {
      Swal.fire(`Maximum ${availablePoints} allowed`, "", "error");
      return;
    }

    applyRedeem(points, loyaltyPercent);
    setRedeemPoints("");
    setShowRedeem(false);
  };

  /* ---------- Gift Card ---------- */
  const handleGiftCard = () => {
    if (discountSource === "Redeem") {
      Swal.fire("Remove redeem points first", "", "error");
      return;
    }

    if (!giftCode) {
      Swal.fire("Enter gift card", "", "error");
      return;
    }

    const giftValue = 200; // TODO: API

    applyGiftCard({
      cardNumber: giftCode,
      value: giftValue,
    });

    setGiftCode("");
    setShowGift(false);
  };

  /* ---------- Place Order (ONLY CHANGE HERE) ---------- */
  const handlePlaceOrder = () => {
    const user = localStorage.getItem("user"); // 🔐 LOGIN CHECK

    if (!user) {
      Swal.fire({
        title: "Login Required",
        text: "Please login to place your order",
        icon: "warning",
        confirmButtonText: "Login",
      }).then(() => {
        navigate("/login?redirect=checkout");
      });
      return;
    }

    if (finalTotal < MIN_ORDER) {
      Swal.fire(
        `Minimum Order Amount : Rs ${MIN_ORDER}`,
        "Please Check Your Cart Total !!",
        "error"
      );
      return;
    }

    navigate("/checkout");
  };

  /* ---------- Empty Cart ---------- */
  if (cart.length === 0) {
    return (
      <div id="midpart">
        <div id="emptycartcontainer" className="row spark-over-container">
          <div className="category-card-heading">Empty Cart !!</div>
          <div className="cartTitle">Add some items to cart to checkout</div>
          <div className="col-md-4">
            <input
              type="button"
              onClick={() => navigate("/")}
              value="CONTINUE SHOPPING"
              className="rounded-btn red-btn"
            />
          </div>
        </div>
      </div>
    );
  }

  /* ---------- UI (UNCHANGED) ---------- */
  return (
    <div id="midpart">
      <div id="cartcontainer" className="cart-container">
        <div className="col-md-12 col-xs-12 no-padding cart-inner-container">
          {/* LEFT */}
          <div className="col-md-8 col-sm-7 cart-item-container">
            <h1 className="cartTitle">Shopping cart</h1>

            <div id="xdata">
              {cart.map((item) => (
                <div key={item.id} className="cart-card">
                  <div className="cart-card-image">
                    <img
                      src={`/images/productimages/${item.menuName}-1.jpg`}
                      alt={item.menuName}
                      className="cart-card-icon"
                      onError={(e) =>
                        (e.target.src =
                          "/images/svg-icons/img-placeholder.svg")
                      }
                    />
                  </div>

                  <div className="cart-card-body">
                    <div className="cart-card-title">{item.menuName}</div>

                    <div className="cart-card-quantity">
                      <span>QTY :</span>
                      <button
                        className="quantity-button"
                        onClick={() => handleUpdateQuantity(item.id, -1)}
                      >
                        -
                      </button>
                      <span style={{ margin: "0 12px", fontWeight: "bold" }}>
                        {item.quantity}
                      </span>
                      <button
                        className="quantity-button"
                        onClick={() => handleUpdateQuantity(item.id, 1)}
                      >
                        +
                      </button>
                    </div>

                    <div className="cart-card-text">
                      PRICE : Rs {item.sellPrice}
                    </div>
                    <div className="cart-card-text">
                      AMOUNT : Rs{" "}
                      {(item.sellPrice * item.quantity).toFixed(0)}
                    </div>
                  </div>

                  <div className="cart-card-footer">
                    <button
                      className="remove-item"
                      onClick={() => handleRemove(item.id)}
                    >
                      ✕
                    </button>
                  </div>
                </div>
              ))}
            </div>
          </div>

          {/* RIGHT */}
          <div className="col-md-3 col-sm-4 cart-button-container">
            <div className="cartTotalBox">
              SUB TOTAL : Rs {subtotal.toFixed(0)}
            </div>

            {discountAmount > 0 && (
              <div className="cartTotalBox">
                Discount : Rs {discountAmount.toFixed(0)}
                <span className="remove-discount-link" onClick={clearDiscount}>
                  [Remove]
                </span>
              </div>
            )}

            <div className="cartTotalBox">
              TOTAL : Rs {finalTotal.toFixed(0)}
            </div>

            <div style={{ textAlign: "center" }}>
              <button
                className="btn pink-btn"
                onClick={() => setShowRedeem(!showRedeem)}
              >
                Redeem Points
              </button>
            </div>

            {showRedeem && (
              <div className="row mt-2">
                <input
                  className="form-control"
                  value={redeemPoints}
                  onChange={(e) => setRedeemPoints(e.target.value)}
                  placeholder="Points to Redeem"
                />
                <button className="redeem-btn pink-btn" onClick={handleRedeem}>
                  Redeem
                </button>
              </div>
            )}

            <div style={{ textAlign: "center" }}>
              <button
                className="btn btn-link"
                onClick={() => setShowGift(!showGift)}
              >
                Do you have a gift card? Click here
              </button>
            </div>

            {showGift && (
              <div className="row mt-2">
                <input
                  className="form-control"
                  value={giftCode}
                  onChange={(e) => setGiftCode(e.target.value)}
                  placeholder="Gift Card"
                />
                <button
                  className="redeem-btn pink-btn"
                  onClick={handleGiftCard}
                >
                  Apply
                </button>
              </div>
            )}

            {giftCards.map((g) => (
              <div key={g.cardNumber} className="redeemed-giftcard-entry">
                <span className="giftcard-code">{g.cardNumber}</span>
                <span
                  className="remove-giftcard-link"
                  onClick={() => removeGiftCard(g.cardNumber)}
                >
                  ✖
                </span>
              </div>
            ))}

            <button
              className="rounded-btn green-btn"
              onClick={handlePlaceOrder}
            >
              PLACE ORDER
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default CartPage;
