import React from "react";
import MovieList from "./components/MovieList";

const App = () => (
  <div className="p-6 max-w-4xl mx-auto">
    <h1 className="text-2xl font-bold mb-4">Compare Movie Prices</h1>
    <MovieList />
  </div>
);

export default App;
