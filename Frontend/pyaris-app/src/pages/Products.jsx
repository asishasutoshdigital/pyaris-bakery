import { useEffect, useState } from 'react'
import { productAPI } from '../services/api'
import { useCartStore } from '../store/store'

export const Products = () => {
  const [products, setProducts] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(null)
  const addToCart = useCartStore((state) => state.addToCart)

  useEffect(() => {
    fetchProducts()
  }, [])

  const fetchProducts = async () => {
    try {
      setLoading(true)
      const response = await productAPI.getAll()
      setProducts(response.data)
      setError(null)
    } catch (err) {
      setError('Failed to load products')
      console.error(err)
    } finally {
      setLoading(false)
    }
  }

  const handleAddToCart = (product) => {
    addToCart(product, 1)
    alert(`${product.menuName} added to cart!`)
  }

  if (loading) return <div className="container mt-5"><p>Loading products...</p></div>
  if (error) return <div className="container mt-5"><p className="text-danger">{error}</p></div>

  return (
    <div className="container mt-5">
      <h1>Our Products</h1>
      <div className="row mt-4">
        {products.map((product) => (
          <div key={product.id} className="col-md-4 mb-4">
            <div className="card">
              <div className="card-body">
                <h5 className="card-title">{product.menuName}</h5>
                <p className="card-text">
                  <strong>Group:</strong> {product.group}<br />
                  <strong>Price:</strong> ₹{product.sellPrice}
                </p>
                <p className="card-text">{product.feature1}</p>
                <button
                  className="btn btn-primary"
                  onClick={() => handleAddToCart(product)}
                >
                  Add to Cart
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}
