import { useEffect, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";
import { productAPI } from "../services/api";

function ProductsPage() {
  const [searchParams] = useSearchParams();
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [displayHeader, setDisplayHeader] = useState("");

  const xdt = searchParams.get("xdt") || searchParams.get("subgroup") || "";
  const grp = searchParams.get("grp") || searchParams.get("group") || "";
  const isFlavour = searchParams.get("isFlavour") === "true";

  useEffect(() => {
    loadProducts();
  }, [xdt, grp, isFlavour]);

  const loadProducts = async () => {
    setLoading(true);
    try {
      const response = await productAPI.getBySubgroup(xdt, {
        grp,
        isFlavour,
      });

      setProducts(response.data || []);
      setDisplayHeader(xdt || "All Products");
    } catch (err) {
      console.error(err);
      setProducts([]);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return <div className="product-category-title">Loading...</div>;
  }

  return (
    <div id="xdata">
      <div className="product-category-title" id="xdisplayheader">
        {displayHeader}
      </div>

      <div className="product-listing-container row" id="xfdata">
        {products.length === 0 ? (
          <p>No products found.</p>
        ) : (
          products.map((product) => (
            <div
              key={product.id}
              className="col-md-3 col-sm-4 col-xs-6 list-productcard"
            >
              <Link
                to={`/product/${product.id}`}
                style={{ textDecoration: "none" }}
              >
                <div className="productcard">
                  <div className="productcard-image">
                    <img
                      src={`/images/productimages/${product.menuName}-1.jpg`}
                      alt={product.menuName}
                      className="product-image"
                      onError={(e) => {
                        e.target.onerror = null;
                        e.target.src = "/images/svg-icons/img-placeholder.svg";
                      }}
                    />
                  </div>

                  <div className="veg-symbol"></div>
                  <div className="product-card-title">{product.menuName}</div>
                  <div className="product-card-price">
                    <img src="/images/svg-icons/rupees.svg" alt="Rs" />
                    {product.sellPrice}
                  </div>
                </div>
              </Link>
            </div>
          ))
        )}
      </div>
    </div>
  );
}

export default ProductsPage;
