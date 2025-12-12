import { Link } from 'react-router-dom';

function ProfileSideBar({ showPayback = true }) {
  return (
    <div className="onlinesidemenu col-md-3">
      <div className="menuCardTitle">OPERATIONS</div>
      <div className="menuCardSeperator"></div>
      <div id='flyout_menu'>
        <ul>
          <li>
            <Link to='/mya'><span>MY ORDERS</span></Link>
          </li>
          {showPayback && (
            <li>
              <Link to='/myapayback'><span>MY PAYBACK POINTS</span></Link>
            </li>
          )}
          <li>
            <Link to='/myprofile'><span>MY PROFILE</span></Link>
          </li>
          <li>
            <Link to='/mya?logout=yes'><span>LOGOUT</span></Link>
          </li>
        </ul>
      </div>
    </div>
  );
}

export default ProfileSideBar;
