import {useEffect, useState} from "react";
import {useSearchParams} from "react-router-dom";
import axios from "axios";
import {motion} from "framer-motion";
import ProductCard from "../components/ProductCard";
import {Product} from "../types/Product.ts";

const SearchResults = () => {
    const [searchParams] = useSearchParams();
    const query = searchParams.get("query");
    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (query) {
            axios
                .get(`http://localhost:7057/api/products/search?query=${query}`)
                .then((response) => setProducts(response.data))
                .catch((error) => console.error(error))
                .finally(() => setLoading(false));
        }
    }, [query]);

    return (
        <div
            className="min-h-screen bg-gradient-to-b from-black via-purple-900 to-gray-900 text-white px-6 py-12 flex flex-col items-center justify-center relative overflow-hidden">
            <motion.div
                className="absolute inset-0 pointer-events-none"
                initial={{opacity: 0}}
                animate={{opacity: 0.4}}
                transition={{duration: 6, repeat: Infinity, repeatType: "mirror"}}
                style={{
                    background: "radial-gradient(circle, rgba(128,0,255,0.4) 20%, rgba(0,0,0,0) 70%)",
                }}
            />

            <motion.h1
                className="text-4xl md:text-5xl font-bold text-center mb-10 z-10 relative"
                initial={{y: -50, opacity: 0}}
                animate={{y: 0, opacity: 1}}
                transition={{duration: 1}}>
                Search Results for{" "}
                <span className="text-pink-400 underline decoration-wavy">{query}</span>
            </motion.h1>

            {loading ? (
                <div className="flex items-center justify-center h-96 animate-pulse">
                    <p className="text-lg font-medium">Fetching awesome deals...</p>
                </div>
            ) : products.length > 0 ? (
                <motion.div
                    className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 z-10"
                    initial={{opacity: 0}}
                    animate={{opacity: 1}}
                    transition={{duration: 1, delay: 0.5}}>
                    {products.map((product) => (
                        <ProductCard key={product.url} product={product}/>
                    ))}
                </motion.div>
            ) : (
                <motion.p
                    className="text-lg font-semibold text-purple-400 mt-16 z-10 relative"
                    initial={{y: 50, opacity: 0}}
                    animate={{y: 0, opacity: 1}}
                    transition={{duration: 1}}>
                    Sorry, no results found. 🕵️
                </motion.p>
            )}

            <motion.div
                className="mt-10 z-10 relative"
                initial={{opacity: 0}}
                animate={{opacity: 1}}
                transition={{duration: 1, delay: 0.8}}>
                <button
                    onClick={() => window.history.back()}
                    className="bg-gradient-to-r from-pink-500 to-purple-600 hover:from-pink-600 hover:to-purple-700 text-white font-bold py-3 px-8 rounded-full shadow-xl hover:scale-105 transition-transform duration-300">
                    Go Back
                </button>
            </motion.div>
        </div>
    );
};

export default SearchResults;
