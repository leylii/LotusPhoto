import Masonry from 'react-masonry-css';
import Header from "../components/Header";
import { Link } from 'react-router-dom';
import { useState, useEffect } from "react";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

function Home() {
  const breakpointColumnsObj = {
    default: 3,
    1024: 3,
    768: 2,
    500: 1
  };

  const [selectedCategory, setSelectedCategory] = useState("All");

  const [photos, setPhotos] = useState([]);

  const allCategories = ["All", ...new Set(photos.map((p) => p.category))];

  
  const filteredPhotos =
  selectedCategory === "All"
    ? photos
    : photos.filter((photo) => photo.category === selectedCategory);

  useEffect(() => {
    fetch(`${API_BASE_URL}/api/photo`)
      .then((res) => res.json())
      .then((data) => setPhotos(data))
      .catch((err) => console.error("Error recieving photos:", err));
  }, []);


  return (
    <>
      <Header />

      <div className="text-center p-8">
        <h1 className="text-2xl font-semibold">I'm a photographer</h1>
        <p className="text-gray-600 mt-2 max-w-xl mx-auto">
          Very interested in photography and here I can share my photos with you...
        </p>
      </div>

      <div className="flex gap-4 justify-center mb-6 flex-wrap">
        {allCategories.map((category) => (
          <button
            key={category}
            onClick={() => setSelectedCategory(category)}
            className={`px-4 py-1 rounded-full border transition ${
              selectedCategory === category
                ? "bg-black text-white"
                : "bg-white text-gray-700 hover:bg-gray-100"
            }`}
          >
            {category}
          </button>
        ))}
      </div>

      <section className="p-8">
      <Masonry
          breakpointCols={breakpointColumnsObj}
          className="flex gap-6"
          columnClassName="masonry-column"
        >
            {filteredPhotos.map((photo, index) => (
              <div key={index} className="mb-6 overflow-hidden rounded-md relative">
              <img
                src={`${API_BASE_URL}${photo.previewUrl}`}
                alt={photo.title}
                className="w-full rounded-md"
              />
            
              {/* Text on the photo */}
              <div className="absolute bottom-0 left-0 w-full bg-stone-50 bg-opacity-50 text-white p-2">
                <h3 className="text-sm text-sm font-semibold">{photo.title}</h3>
                <p className="text-xs">{photo.category}</p>
                <Link to={`/photo/${index}`}>
                    <button className="mt-2 px-4 py-1 text-sm bg-gray-400 text-white rounded-md hover:bg-blue-700">
                      Details
                    </button>
                </Link>
              </div>
            </div>
            
            ))}

        </Masonry>
      </section>
    </>
  );
}

export default Home;