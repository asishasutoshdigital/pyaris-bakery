function WhatsAppPage() {
  const whatsappNumber = "919600128966";
  const message = "Hello, I would like to place an order from Paris Bakery";
  const whatsappUrl = `https://wa.me/${whatsappNumber}?text=${encodeURIComponent(message)}`;

  return (
    <div className="container py-5">
      <div className="row justify-content-center">
        <div className="col-md-6 text-center">
          <div className="card">
            <div className="card-body p-5">
              <h1 className="mb-4">Order via WhatsApp</h1>
              <p className="lead mb-4">Place your order directly through WhatsApp</p>
              <a href={whatsappUrl} target="_blank" rel="noopener noreferrer" className="btn btn-success btn-lg">
                <i className="bi bi-whatsapp"></i> Open WhatsApp
              </a>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default WhatsAppPage;
