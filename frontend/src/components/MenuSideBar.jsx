import { Link } from 'react-router-dom';

function MenuSideBar() {
  return (
    <div className="onlinesidemenu menuSideBar col-md-3" id="xmenu">
      <div className="menuCardTitle">TOP CATEGORIES</div>
      <div className="menuCardSeperator"></div>
      <div id='flyout_menu'>
        <ul>
          <li>
            <a href='#'><span>CAKES</span></a>
            <ul>
              <li><Link to='/spark?xdt=PARTY CAKE'><span>PARTY CAKE</span></Link></li>
              <li><Link to='/spark?xdt=BIRTHDAY CAKE'><span>BIRTHDAY CAKE</span></Link></li>
              <li><Link to='/spark?xdt=LOVE THEME CAKE'><span>LOVE THEME CAKE</span></Link></li>
              <li><Link to='/spark?xdt=KIDS CAKE'><span>KID'S CAKE</span></Link></li>
              <li><Link to='/spark?xdt=BARBIE CAKE'><span>BARBIE CAKE</span></Link></li>
              <li><Link to='/spark?xdt=CARTOON CHARACTER CAKE'><span>CARTOON CHARACTER CAKE</span></Link></li>
              <li><Link to='/spark?xdt=MULTI-LAYER CAKE'><span>MULTI-LAYER CAKE</span></Link></li>
              <li><Link to='/spark?xdt=SMALL CAKE'><span>SMALL CAKE</span></Link></li>
            </ul>
          </li>
          <li>
            <a href='#'><span>FRESH BAKED</span></a>
            <ul>
              <li><Link to='/spark?xdt=BUN'><span>BUN</span></Link></li>
              <li><Link to='/spark?xdt=DRY CAKE'><span>DRY CAKE</span></Link></li>
              <li><Link to='/spark?xdt=KULCHA'><span>KULCHA</span></Link></li>
              <li><Link to='/spark?xdt=LOAF'><span>LOAF</span></Link></li>
              <li><Link to='/spark?xdt=MUFFIN'><span>MUFFIN</span></Link></li>
              <li><Link to='/spark?xdt=REGULAR BREAD'><span>REGULAR BREAD</span></Link></li>
              <li><Link to='/spark?xdt=RUSK'><span>RUSK</span></Link></li>
              <li><Link to='/spark?xdt=SOUP ITEMS'><span>SOUP ITEMS</span></Link></li>
              <li><Link to='/spark?xdt=SPECIAL BREAD'><span>SPECIAL BREAD</span></Link></li>
              <li><Link to='/spark?xdt=TOAST'><span>TOAST</span></Link></li>
            </ul>
          </li>
          <li>
            <a href='#'><span>SNACKS</span></a>
            <ul>
              <li><Link to='/spark?xdt=READY TO EAT'><span>READY TO EAT</span></Link></li>
              <li><Link to='/spark?xdt=DOUGHNUT'><span>DONUT</span></Link></li>
              <li><Link to='/spark?xdt=PUDDING'><span>PUDDING</span></Link></li>
              <li><Link to='/spark?xdt=PASTRIES'><span>PASTRIES</span></Link></li>
              <li><Link to='/spark?xdt=ROLL'><span>ROLL</span></Link></li>
            </ul>
          </li>
          <li>
            <a href='#'><span>COOKIES</span></a>
            <ul>
              <li><Link to='/spark?xdt=GUJHIYA'><span>GUJHIYA</span></Link></li>
              <li><Link to='/spark?xdt=PUFF'><span>PUFF</span></Link></li>
              <li><Link to='/spark?xdt=SALTY'><span>SALTY</span></Link></li>
              <li><Link to='/spark?xdt=SUGAR FREE'><span>SUGAR FREE</span></Link></li>
              <li><Link to='/spark?xdt=SUGARY'><span>SUGARY</span></Link></li>
            </ul>
          </li>
          <li>
            <a href='#'><span>SWEETS</span></a>
            <ul>
              <li><Link to='/spark?xdt=CHOCOLATE'><span>CHOCOLATE</span></Link></li>
              <li><Link to='/spark?xdt=DRY SWEETS'><span>DRY SWEETS</span></Link></li>
            </ul>
          </li>
          <li>
            <a href='#'><span>BOUQUET</span></a>
            <ul>
              <li><Link to='/spark?xdt=BOUQUET'><span>BOUQUET</span></Link></li>
            </ul>
          </li>
          <li>
            <a href='#'><span>NAMKIN</span></a>
            <ul>
              <li><Link to='/spark?xdt=NAMKIN'><span>NAMKIN</span></Link></li>
            </ul>
          </li>
          <li>
            <a href='#'><span>BEVERAGE</span></a>
            <ul>
              <li><Link to='/spark?xdt=SOFT DRINK'><span>SOFT DRINK</span></Link></li>
            </ul>
          </li>
        </ul>
      </div>
    </div>
  );
}

export default MenuSideBar;
