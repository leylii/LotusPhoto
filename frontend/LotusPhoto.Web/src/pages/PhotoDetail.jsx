import { useParams, Link } from 'react-router-dom';
import { useEffect, useState } from 'react';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

function PhotoDetail() {
  const { id } = useParams();
  const [photo, setPhoto] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch(`${API_BASE_URL}/api/photo/${id}`)
      .then((res) => {
        if (res.ok) return res.json();
        else throw new Error("Not found");
      })
      .then((data) => {
        setPhoto(data);
        setLoading(false);
      })
      .catch(() => {
        setPhoto(null);
        setLoading(false);
      });
  }, [id]);

  if (loading) return <p className="text-center p-8">Loading...</p>;

  if (!photo) return <div className="p-8 text-center">No Photo</div>;

  return (
    <div className="p-8 text-center">
      <img
        src={`${API_BASE_URL}${photo.previewUrl}`}
        alt={photo.title}
        className="mx-auto rounded-md mb-4 max-w-md"
      />
      <h2 className="text-2xl font-bold">{photo.title}</h2>
      <p className="text-gray-600">{photo.category}</p>
      {photo.description && (
        <p className="text-gray-500 mt-2">{photo.description}</p>
      )}
      <p className="text-sm mt-2 font-medium text-gray-800">Price: ${photo.price}</p>
      <Link to="/">
        <button className="mt-6 px-4 py-2 bg-gray-200 text-gray-800 rounded-md hover:bg-gray-300">
          ← Return to Gallery
        </button>
      </Link>
    </div>
  );
}

export default PhotoDetail;
