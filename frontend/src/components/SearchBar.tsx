import { FiSearch } from "react-icons/fi";
import { FiSend } from "react-icons/fi";
import { motion } from "framer-motion";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const SearchBar = () => {
    const [query, setQuery] = useState("");
    const navigate = useNavigate();

    const handleSearch = () => {
        if (query.trim()) {
            navigate(`/search?query=${encodeURIComponent(query)}`);
        }
    };

    return (
        <div className="flex flex-col items-center justify-center h-screen bg-gradient-to-b from-black to-purple-900 text-white px-4 relative overflow-hidden">
            <motion.div
                className="absolute inset-0 pointer-events-none"
                initial={{ opacity: 0 }}
                animate={{ opacity: 0.6 }}
                transition={{ duration: 3, repeat: Infinity, repeatType: "reverse" }}
                style={{
                    background: "radial-gradient(circle, rgba(128,0,255,0.4) 0%, rgba(0,0,0,0) 60%)",
                }}
            />
            <motion.h1
                className="text-4xl md:text-5xl font-bold text-center mb-8 relative z-10"
                initial={{ y: -50, opacity: 0 }}
                animate={{ y: 0, opacity: 1 }}
                transition={{ duration: 1 }}
            >
                Welcome to <span className="text-pink-500">Price Checker</span>
                <motion.span
                    className="ml-1 text-pink-400"
                    initial={{ scale: 0 }}
                    animate={{ scale: 1.2 }}
                    transition={{
                        duration: 0.8,
                        repeat: Infinity,
                        repeatType: "mirror",
                        ease: "easeInOut",
                    }}
                >
                    ✨
                </motion.span>
            </motion.h1>

            <motion.div
                className="relative w-full max-w-md"
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                transition={{ duration: 1, delay: 0.5 }}
            >
                <div className="relative">
                    <input
                        type="text"
                        value={query}
                        onChange={(e) => setQuery(e.target.value)}
                        placeholder="Search for the best prices..."
                        className="w-full bg-gradient-to-r from-gray-900 via-purple-900 to-black text-white placeholder-gray-400 rounded-full py-4 pl-12 pr-16 text-lg shadow-md focus:outline-none focus:ring-2 focus:ring-pink-500"
                    />

                    <motion.div
                        className="absolute inset-y-0 left-4 flex items-center"
                        initial={{ scale: 0 }}
                        animate={{ scale: 1 }}
                        transition={{ duration: 0.5, delay: 0.7 }}
                    >
                        <FiSearch className="w-6 h-6 text-pink-400" />
                    </motion.div>

                    <motion.button
                        onClick={handleSearch}
                        className="absolute right-1.5 top-1.5 transform -translate-y-1/2 bg-gradient-to-r from-pink-500 to-purple-600 hover:from-pink-600 hover:to-purple-700 text-white rounded-full w-12 h-12 shadow-xl transition-all duration-300 flex items-center justify-center z-10"
                        initial={{ x: 50, opacity: 0 }}
                        animate={{ x: 0, opacity: 1 }}
                        whileHover={{ scale: 1.2 }}
                        whileTap={{ scale: 0.9 }}
                        transition={{ duration: 0.5, delay: 0.8 }}
                    >
                        <FiSend className="w-5 h-5" />
                    </motion.button>
                </div>
            </motion.div>
        </div>
    );
};

export default SearchBar;
