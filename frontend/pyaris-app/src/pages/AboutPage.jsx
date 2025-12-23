function AboutPage() {
  return (
    <div className="about-page">
      <div className="container py-5">
        <h1 className="mb-4">About Paris Bakery</h1>
        
        <div className="row">
          <div className="col-md-8">
            <section className="mb-5">
              <h2>Our Story</h2>
              <p>
                Paris Bakery has been serving delicious baked goods for years, bringing
                the authentic taste of French pastries to your doorstep. Our commitment
                to quality and freshness has made us a favorite among cake and pastry lovers.
              </p>
            </section>
            
            <section className="mb-5">
              <h2>Our Mission</h2>
              <p>
                We strive to deliver the finest quality cakes, pastries, and baked goods,
                made fresh daily with premium ingredients. Every product is crafted with
                care and attention to detail.
              </p>
            </section>
            
            <section className="mb-5">
              <h2>What We Offer</h2>
              <ul>
                <li>Fresh baked cakes for all occasions</li>
                <li>Delicious pastries and desserts</li>
                <li>Custom cake designs</li>
                <li>Fast home delivery</li>
                <li>Quality ingredients</li>
                <li>Affordable pricing</li>
              </ul>
            </section>
          </div>
          
          <div className="col-md-4">
            <div className="card">
              <div className="card-body">
                <h3>Contact Information</h3>
                <p>
                  <strong>Email:</strong><br />
                  info@parisbakery.in
                </p>
                <p>
                  <strong>Phone:</strong><br />
                  9600128966
                </p>
                <p>
                  <strong>Hours:</strong><br />
                  Open Daily<br />
                  9:00 AM - 9:00 PM
                </p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default AboutPage;
