function RefundPage() {
  return (
    <div className="refund-page">
      <div className="container py-5">
        <h1 className="mb-4">Refund Policy</h1>
        
        <div className="content">
          <section className="mb-4">
            <h2>General Policy</h2>
            <p>
              At Paris Bakery, customer satisfaction is our top priority. We strive
              to provide the highest quality products and services. If you are not
              satisfied with your order, we are here to help.
            </p>
          </section>
          
          <section className="mb-4">
            <h2>Eligibility for Refund</h2>
            <p>You may be eligible for a refund if:</p>
            <ul>
              <li>The order was not delivered</li>
              <li>The product was damaged during delivery</li>
              <li>The product received was significantly different from what was ordered</li>
              <li>Quality issues with the product</li>
            </ul>
          </section>
          
          <section className="mb-4">
            <h2>Refund Request Process</h2>
            <p>
              To request a refund, please contact our customer service within 24 hours
              of receiving your order:
            </p>
            <ul>
              <li>Email: info@parisbakery.in</li>
              <li>Phone: 9600128966</li>
            </ul>
            <p>
              Please provide your order number, photos of the product (if applicable),
              and a detailed description of the issue.
            </p>
          </section>
          
          <section className="mb-4">
            <h2>Refund Processing</h2>
            <p>
              Once your refund request is approved, we will process the refund within
              5-7 business days. The refund will be credited to your original payment
              method.
            </p>
          </section>
          
          <section className="mb-4">
            <h2>Non-Refundable Items</h2>
            <p>The following items are non-refundable:</p>
            <ul>
              <li>Custom-made cakes and personalized items</li>
              <li>Orders cancelled after preparation has begun</li>
              <li>Orders that have been delivered and consumed</li>
            </ul>
          </section>
          
          <section className="mb-4">
            <h2>Cancellation Policy</h2>
            <p>
              Orders can be cancelled before they enter the preparation stage.
              Cancellation requests must be made as soon as possible by contacting
              our customer service.
            </p>
          </section>
          
          <section className="mb-4">
            <h2>Contact Us</h2>
            <p>
              For any questions or concerns regarding refunds, please contact us:
              <br />
              Email: info@parisbakery.in
              <br />
              Phone: 9600128966
            </p>
          </section>
        </div>
      </div>
    </div>
  );
}

export default RefundPage;
