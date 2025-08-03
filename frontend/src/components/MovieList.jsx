import React, { useEffect, useState } from "react";
import { fetchMovies, fetchMoviePriceDetails } from "../services/movieService";

const MovieList = () => {
  const [movies, setMovies] = useState([]);
  const [priceInfo, setPriceInfo] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  useEffect(() => {
    const loadMovies = async () => {
      try {
        const movies = await fetchMovies();
        setMovies(movies);
      } catch (err) {
        setError("Failed to fetch movies.");
      }
    };
    loadMovies();
  }, []);

  const handleCompare = async (title) => {
    setLoading(true);
    setPriceInfo(null);
    try {
      const data = await fetchMoviePriceDetails(title);
      setPriceInfo(data);
    } catch {
      setPriceInfo([]);
    }
    setLoading(false);
  };

  return (
    <>
      {error && <div className="text-red-500">{error}</div>}
      <ul className="space-y-2">
        {movies.map((m, idx) => (
          <li key={idx} className="border p-4 flex justify-between">
            <span>{m.title}</span>
            <button
              onClick={() => handleCompare(m.title)}
              className="bg-blue-500 text-white px-3 py-1 rounded"
            >
              Compare
            </button>
          </li>
        ))}
      </ul>
      {loading && <p className="mt-4">Loading prices...</p>}
      {priceInfo && (
        <div className="mt-4">
          <h2 className="font-semibold">Price Comparison</h2>
          <ul>
            {priceInfo.map((p, i) => (
              <li key={i}>{p.provider}: ${p.price.toFixed(2)}</li>
            ))}
          </ul>
          {priceInfo.length > 0 && (
            <p className="font-bold mt-2">
              Cheapest: {
                priceInfo.reduce((a, b) => (a.price < b.price ? a : b)).provider
              }
            </p>
          )}
        </div>
      )}
    </>
  );
};

export default MovieList;
