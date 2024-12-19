import { motion } from "framer-motion";
import { Product } from "../types/Product.ts";

interface ProductCardProps {
    product: Product;
}

const ProductCard: React.FC<ProductCardProps> = ({ product }) => {
    return (
        <motion.div
            className="bg-gradient-to-r from-purple-900 via-purple-800 to-purple-700 rounded-lg shadow-lg overflow-hidden text-white p-6 transform transition-all hover:scale-105 hover:shadow-2xl"
            whileHover={{ scale: 1.05 }}
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            transition={{ duration: 0.5 }}
        >
            <motion.div
                className="w-full h-60 mb-4 flex justify-center items-center relative"
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                transition={{ duration: 0.8 }}
            >
                <img
                    src={product.imageUrl}
                    alt={product.productName}
                    className="object-contain w-full h-full rounded-md shadow-lg"
                />
            </motion.div>

            <motion.h2
                className="text-2xl font-bold mb-2 text-pink-300"
                initial={{ y: 20, opacity: 0 }}
                animate={{ y: 0, opacity: 1 }}
                transition={{ duration: 0.5 }}
            >
                {product.productName}
            </motion.h2>

            <motion.p
                className="text-3xl font-semibold text-yellow-400 border-2 border-yellow-400 rounded-full px-4 py-1 inline-block mb-4"
                initial={{ y: 20, opacity: 0 }}
                animate={{ y: 0, opacity: 1 }}
                transition={{ duration: 0.5, delay: 0.2 }}
            >
                {product.price} PLN
            </motion.p>

            <motion.a
                href={product.url}
                target="_blank"
                rel="noopener noreferrer"
                className="text-lg text-purple-200 hover:text-purple-100 underline transition-colors duration-300"
                whileHover={{ scale: 1.05 }}
                initial={{ y: 20, opacity: 0 }}
                animate={{ y: 0, opacity: 1 }}
                transition={{ duration: 0.5, delay: 0.4 }}
            >
                Buy on {product.siteName}
            </motion.a>
        </motion.div>
    );
};

export default ProductCard;
