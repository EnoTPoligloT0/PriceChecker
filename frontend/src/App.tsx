import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import SearchBar from "./components/SearchBar.tsx";
import './index.css'
import SearchResults from "./pages/SearchResults.tsx";
function App() {

  return (
      <Router>
          <Routes>
              <Route path="/" element={<SearchBar />} />
              <Route path="/search" element={<SearchResults />} />
          </Routes>
      </Router>
  )
}

export default App
