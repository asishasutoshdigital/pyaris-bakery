import { useSearchParams } from 'react-router-dom';

function ResponsePage() {
  const [searchParams] = useSearchParams();
  const status = searchParams.get('status');

  return (
    <div className="container py-5">
      <div className="card">
        <div className="card-body text-center p-5">
          <h2>Response</h2>
          <p className="lead">Status: {status || 'Unknown'}</p>
        </div>
      </div>
    </div>
  );
}

export default ResponsePage;
