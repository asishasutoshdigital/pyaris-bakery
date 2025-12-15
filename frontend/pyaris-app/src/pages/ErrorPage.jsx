import { useNavigate } from 'react-router-dom';

function ErrorPage() {
  const navigate = useNavigate();

  return (
    <div className="error-page">
      <div className="container py-5">
        <div className="row justify-content-center">
          <div className="col-md-6 text-center">
            <h1 className="display-1">500</h1>
            <h2 className="mb-4">Internal Server Error</h2>
            <p className="lead mb-4">
              Something went wrong on our end. We're working to fix it.
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
                onClick={() => window.location.reload()}
              >
                Refresh Page
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ErrorPage;
