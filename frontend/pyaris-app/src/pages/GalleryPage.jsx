function GalleryPage() {
  // Placeholder images - in production, these would be loaded from the server
  const galleryImages = [
    { id: 1, title: 'Custom Birthday Cake', category: 'Cakes' },
    { id: 2, title: 'Wedding Cake', category: 'Cakes' },
    { id: 3, title: 'Chocolate Pastries', category: 'Pastries' },
    { id: 4, title: 'French Croissants', category: 'Pastries' },
    { id: 5, title: 'Anniversary Cake', category: 'Cakes' },
    { id: 6, title: 'Cupcake Collection', category: 'Cupcakes' },
    { id: 7, title: 'Fruit Tart', category: 'Pastries' },
    { id: 8, title: 'Chocolate Truffle Cake', category: 'Cakes' },
  ];

  return (
    <div className="gallery-page">
      <div className="container py-5">
        <h1 className="mb-4">Our Gallery</h1>
        <p className="lead mb-5">
          Explore our collection of delicious cakes and pastries
        </p>
        
        <div className="row">
          {galleryImages.map((image) => (
            <div key={image.id} className="col-md-3 col-sm-6 mb-4">
              <div className="card h-100">
                <div 
                  className="card-img-top bg-light" 
                  style={{ height: '200px', display: 'flex', alignItems: 'center', justifyContent: 'center' }}
                >
                  <div className="text-center text-muted">
                    <i className="bi bi-image" style={{ fontSize: '3rem' }}></i>
                    <p className="mb-0">{image.title}</p>
                  </div>
                </div>
                <div className="card-body">
                  <h5 className="card-title">{image.title}</h5>
                  <p className="card-text">
                    <span className="badge bg-primary">{image.category}</span>
                  </p>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

export default GalleryPage;
