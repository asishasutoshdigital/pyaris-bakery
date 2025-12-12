import { useCartStore } from '../store/store'

export const Cart = () => {
  const cart = useCartStore((state) => state.cart)
  const removeFromCart = useCartStore((state) => state.removeFromCart)
  const updateQuantity = useCartStore((state) => state.updateQuantity)
  const clearCart = useCartStore((state) => state.clearCart)

  const total = cart.reduce((sum, item) => sum + (item.sellPrice * item.quantity), 0)

  if (cart.length === 0) {
    return (
      <div className="container mt-5">
        <h1>Shopping Cart</h1>
        <p>Your cart is empty. <a href="/products">Continue shopping</a></p>
      </div>
    )
  }

  return (
    <div className="container mt-5">
      <h1>Shopping Cart</h1>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>Product</th>
            <th>Price</th>
            <th>Quantity</th>
            <th>Subtotal</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {cart.map((item) => (
            <tr key={item.id}>
              <td>{item.menuName}</td>
              <td>₹{item.sellPrice}</td>
              <td>
                <input
                  type="number"
                  min="1"
                  value={item.quantity}
                  onChange={(e) => updateQuantity(item.id, parseInt(e.target.value))}
                  style={{ width: '50px' }}
                />
              </td>
              <td>₹{(item.sellPrice * item.quantity).toFixed(2)}</td>
              <td>
                <button
                  className="btn btn-danger btn-sm"
                  onClick={() => removeFromCart(item.id)}
                >
                  Remove
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div className="row mt-4">
        <div className="col-md-6"></div>
        <div className="col-md-6">
          <h4>Total: ₹{total.toFixed(2)}</h4>
          <button className="btn btn-success me-2">Proceed to Checkout</button>
          <button className="btn btn-secondary" onClick={clearCart}>
            Clear Cart
          </button>
        </div>
      </div>
    </div>
  )
}
