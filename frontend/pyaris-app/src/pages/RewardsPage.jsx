import { useState, useEffect } from 'react';
import { useUserStore } from '../store/useStore';
import { useNavigate } from 'react-router-dom';

function RewardsPage() {
  const navigate = useNavigate();
  const { user, isAuthenticated } = useUserStore();
  const [rewardPoints, setRewardPoints] = useState(0);
  const [transactions, setTransactions] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!isAuthenticated) {
      navigate('/login?redirect=rewards');
      return;
    }

    loadRewards();
  }, [isAuthenticated, navigate]);

  const loadRewards = async () => {
    setLoading(true);
    try {
      // TODO: Implement rewards loading from API
      // const response = await rewardsAPI.getByCustomer(user.id);
      // setRewardPoints(response.data.points);
      // setTransactions(response.data.transactions);
      
      // Placeholder data
      setRewardPoints(0);
      setTransactions([]);
    } catch (error) {
      console.error('Error loading rewards:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="rewards-page">
      <div className="container py-5">
        <h1 className="mb-4">My Rewards</h1>
        
        {loading ? (
          <div className="text-center py-5">
            <div className="spinner-border" role="status">
              <span className="visually-hidden">Loading...</span>
            </div>
          </div>
        ) : (
          <>
            <div className="row mb-4">
              <div className="col-md-4">
                <div className="card text-center">
                  <div className="card-body">
                    <h3 className="display-4 text-primary">{rewardPoints}</h3>
                    <p className="lead">Available Points</p>
                  </div>
                </div>
              </div>
              
              <div className="col-md-8">
                <div className="card">
                  <div className="card-body">
                    <h5>How Rewards Work</h5>
                    <ul>
                      <li>Earn points with every purchase</li>
                      <li>1 point = ₹1 spent</li>
                      <li>Redeem points on your next order</li>
                      <li>Points never expire</li>
                    </ul>
                  </div>
                </div>
              </div>
            </div>
            
            <div className="card">
              <div className="card-body">
                <h4 className="mb-4">Transaction History</h4>
                
                {transactions.length === 0 ? (
                  <div className="text-center py-4">
                    <p className="text-muted">No transactions yet</p>
                    <p>Start earning points by making purchases!</p>
                    <button
                      className="btn btn-primary mt-2"
                      onClick={() => navigate('/products')}
                    >
                      Shop Now
                    </button>
                  </div>
                ) : (
                  <div className="table-responsive">
                    <table className="table">
                      <thead>
                        <tr>
                          <th>Date</th>
                          <th>Description</th>
                          <th>Points</th>
                          <th>Type</th>
                        </tr>
                      </thead>
                      <tbody>
                        {transactions.map((transaction, index) => (
                          <tr key={index}>
                            <td>{new Date(transaction.date).toLocaleDateString()}</td>
                            <td>{transaction.description}</td>
                            <td className={transaction.type === 'earned' ? 'text-success' : 'text-danger'}>
                              {transaction.type === 'earned' ? '+' : '-'}{transaction.points}
                            </td>
                            <td>
                              <span className={`badge ${transaction.type === 'earned' ? 'bg-success' : 'bg-info'}`}>
                                {transaction.type}
                              </span>
                            </td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </div>
                )}
              </div>
            </div>
          </>
        )}
      </div>
    </div>
  );
}

export default RewardsPage;
