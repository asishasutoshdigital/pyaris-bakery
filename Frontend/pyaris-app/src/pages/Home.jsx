
export const Home = () => {
  return (
    <div>
      <section className="hero-section">
        <div className="container text-center py-5">
          <h1 className="display-4">Welcome to Pyaris Bakery</h1>
          <p className="lead">Delicious baked goods delivered to your door</p>
          <a href="/products" className="btn btn-primary btn-lg">
            Order Now
          </a>
        </div>
      </section>

      <section className="features-section py-5">
        <div className="container">
          <div className="row">
            <div className="col-md-4 text-center">
              <h3>🚚 Fast Delivery</h3>
              <p>Get your orders delivered quickly to your location</p>
            </div>
            <div className="col-md-4 text-center">
              <h3>🍰 Fresh Products</h3>
              <p>All our products are made fresh daily</p>
            </div>
            <div className="col-md-4 text-center">
              <h3>💳 Secure Payment</h3>
              <p>Multiple payment options available</p>
            </div>
          </div>
        </div>
      </section>
    </div>
  )
}
