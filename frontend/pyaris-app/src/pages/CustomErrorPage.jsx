import { useNavigate } from 'react-router-dom';

function CustomErrorPage() {
  const navigate = useNavigate();

  return (
    <div className="container py-5">
      <div className="row justify-content-center">
        <div className="col-md-6 text-center">
          <h1 className="display-4">Error</h1>
          <p className="lead mb-4">An error occurred while processing your request.</p>
          <button className="btn btn-primary" onClick={() => navigate('/')}>
            Go to Home
          </button>
        </div>
      </div>
    </div>
  );
}

export default CustomErrorPage;
