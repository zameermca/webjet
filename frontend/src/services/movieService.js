import axios from "axios";

const API_BASE = "http://localhost:5000/api/movies";

export const fetchMovies = async () => {
  const res = await axios.get(API_BASE);
  return res.data;
};

export const fetchMoviePriceDetails = async (title) => {
  const res = await axios.get(`${API_BASE}/${title}`);
  return res.data;
};
