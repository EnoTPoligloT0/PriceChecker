
const SearchBar = () => {
    return (
        <div className="flex flex-col items-center justify-center h-screen bg-gradient-to-b from-black to-purple-900 text-white px-4">
            <h1 className="text-4xl md:text-5xl font-bold text-center mb-6">
                Search with <span className="text-pink-500">Seamless Power</span>
                <span className="ml-1 text-pink-400">✨</span>
            </h1>

            <div className="relative w-full max-w-md">
                <input
                    type="text"
                    placeholder="Ask a question"
                    className="w-full bg-black text-white placeholder-gray-400 rounded-full py-3 px-5 pl-12 text-lg focus:outline-none focus:ring-2 focus:ring-pink-500"
                />
                <div className="absolute left-4 top-1/2 transform -translate-y-1/2">
                </div>

                <button
                    type="submit"
                    className="absolute right-4 top-1/2 transform -translate-y-1/2 bg-pink-500 hover:bg-pink-600 text-white rounded-full p-3 shadow-lg"
                >
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        strokeWidth="2"
                        stroke="currentColor"
                        className="w-6 h-6"
                    >
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="M4.5 12.75l6 6 9-13.5"
                        />
                    </svg>
                </button>
            </div>
        </div>
    );
};

export default SearchBar;
