import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { productAPI } from '../services/api';
import { useCartStore } from '../store/useStore';
import Swal from 'sweetalert2';

/* 🔹 Helper: generate possible image paths (ASPX wildcard replacement) */
const getProductImages = (menuName) => {
  const suffixes = ['-1', '-2', '-3', '-b'];
  return suffixes.map(s => `/images/productimages/${menuName}${s}.jpg`);
};

function ProductDetailsPage() {
  const { id } = useParams();

  const [product, setProduct] = useState(null);
  const [loading, setLoading] = useState(true);

  const [selectedWeight, setSelectedWeight] = useState('');
  const [selectedFlavour, setSelectedFlavour] = useState('');
  const [cakeMessage, setCakeMessage] = useState('');
  const [quantity, setQuantity] = useState(1);
  const [calculatedPrice, setCalculatedPrice] = useState(0);

  const [imageList, setImageList] = useState([]);
  const [bigImage, setBigImage] = useState('');

  const addToCart = useCartStore((state) => state.addToCart);

  /* 🔹 Load product */
  useEffect(() => {
    loadProduct();
  }, [id]);

  /* 🔹 Price recalculation */
  useEffect(() => {
    if (product && selectedWeight) {
      calculatePrice();
    }
  }, [selectedWeight, product]);

  const loadProduct = async () => {
    setLoading(true);
    try {
      const response = await productAPI.getById(id);
      const prod = response.data;

      setProduct(prod);
      setCalculatedPrice(prod.sellPrice || 0);

      // image setup
      const imgs = getProductImages(prod.menuName);
      setImageList(imgs);
      setBigImage(imgs[0]);

    } catch (err) {
      console.error('Error loading product:', err);
    } finally {
      setLoading(false);
    }
  };

  const calculatePrice = () => {
    const weight = parseFloat(selectedWeight);
    const amt = Math.ceil(weight * product.sellPrice);
    setCalculatedPrice(amt);
  };

  const handleAddToCart = () => {
    if (!product) return;

    addToCart({
      ...product,
      weight: selectedWeight,
      flavour: selectedFlavour,
      message: cakeMessage,
      quantity,
      totalPrice: calculatedPrice * quantity
    });

    Swal.fire({
      title: 'Added to Cart!',
      text: `${product.menuName} added to cart`,
      icon: 'success',
      timer: 1500,
      showConfirmButton: false
    });
  };

  const handleImageError = (e, index) => {
    if (index < imageList.length - 1) {
      e.target.src = imageList[index + 1];
    } else {
      e.target.onerror = null;
      e.target.src = '/images/svg-icons/img-placeholder.svg';
    }
  };

  if (loading) {
    return <div className="col-md-12 text-center">Loading...</div>;
  }

  if (!product) {
    return <div className="col-md-12 text-center">Product not found</div>;
  }

  return (
    <div id="midpart">
      <div className="col-md-12 product-details-container">

        {/* LEFT IMAGE PANEL */}
        <div className="col-md-6">
          <div className="imgBox">
            <img
              id="bigimg"
              src={bigImage}
              alt={product.menuName}
              className="imgBoxImageSize"
              onError={(e) => handleImageError(e, imageList.indexOf(bigImage))}
            />
            <div className="veg-symbol"></div>
          </div>

          {/* Thumbnails */}
          <div className="row smallimgBox">
            <ul id="prodImages">
              {imageList.map((img, i) => (
                <li key={i}>
                  <div
                    className="smallimgBoxImageSize"
                    onClick={() => setBigImage(img)}
                  >
                    <img
                      src={img}
                      alt="thumb"
                      onError={(e) => handleImageError(e, i)}
                    />
                  </div>
                </li>
              ))}
            </ul>
          </div>
        </div>

        {/* RIGHT DETAILS PANEL */}
        <div className="col-md-6">
          <h1>{product.menuName}</h1>

          <div className="rating-box">
            ★ 4.{Math.floor(Math.random() * 9) + 1}
          </div>

          <div className="xgroupsubgroup">
            {product.group} / {product.subGroup}
          </div>

          <div className="xrate">₹ {calculatedPrice}</div>

          {/* CAKE OPTIONS */}
          {product.group === 'Cakes' && (
            <>
              <div className="row">
                <div className="col-md-12">Weight</div>
                <div className="col-md-6">
                  <select
                    className="form-control"
                    value={selectedWeight}
                    onChange={(e) => setSelectedWeight(e.target.value)}
                  >
                    <option value="">Select Weight</option>
                    <option value="0.5">0.5 Kg</option>
                    <option value="1">1 Kg</option>
                    <option value="1.5">1.5 Kg</option>
                    <option value="2">2 Kg</option>
                  </select>
                </div>
              </div>

              <div className="row">
                <div className="col-md-12">Flavour</div>
                <div className="col-md-6">
                  <select
                    className="form-control"
                    value={selectedFlavour}
                    onChange={(e) => setSelectedFlavour(e.target.value)}
                  >
                    <option value="">Select Flavour</option>
                    <option>Chocolate</option>
                    <option>Vanilla</option>
                    <option>Strawberry</option>
                    <option>Butterscotch</option>
                  </select>
                </div>
              </div>

              <div className="row">
                <div className="col-md-12">Cake Message</div>
                <div className="col-md-6">
                  <input
                    className="form-control"
                    value={cakeMessage}
                    onChange={(e) => setCakeMessage(e.target.value)}
                  />
                </div>
              </div>
            </>
          )}

          <div className="row">
            <div className="col-md-12">Quantity</div>
            <div className="col-md-6">
              <input
                className="form-control"
                value={quantity}
                onChange={(e) => setQuantity(Math.max(1, parseInt(e.target.value) || 1))}
              />
            </div>
          </div>

          <button className="rounded-btn pink-btn" onClick={handleAddToCart}>
            Add To Cart
          </button>
        </div>
      </div>
    </div>
  );
}

export default ProductDetailsPage;
