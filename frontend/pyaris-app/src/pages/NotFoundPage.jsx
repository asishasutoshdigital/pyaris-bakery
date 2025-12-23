import { useNavigate } from 'react-router-dom';

function NotFoundPage() {
  const navigate = useNavigate();

  return (
    <div className="not-found-page">
      <div className="container py-5">
        <div className="row justify-content-center">
          <div className="col-md-6 text-center">
            <h1 className="display-1">404</h1>
            <h2 className="mb-4">Page Not Found</h2>
            <p className="lead mb-4">
              The page you are looking for doesn't exist or has been moved.
            </p>
            <div className="d-grid gap-2 d-md-flex justify-content-md-center">
              <button
                className="btn btn-primary btn-lg"
                onClick={() => navigate('/')}
              >
                Go to Home
              </button>
              <button
                className="btn btn-outline-primary btn-lg"
                onClick={() => navigate(-1)}
              >
                Go Back
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default NotFoundPage;
