import React, { useEffect, useState } from "react";
import "./Index.css";
import DateCard from "../../components/DateCard/Index";
import useGlobalContext from "../../hooks/useGlobalContext";

const HomePage = () => {
  const [dateIds, setDateIds] = useState([]);
  const { fetchData } = useGlobalContext();

  useEffect(() => {
    async function getRandomDates() {
      try {
        const dates = await fetchData("date/random/2", "GET", null, true);
        if (Array.isArray(dates)) {
          setDateIds(dates.map((x) => x.id));
        } else {
          console.error("Unexpected data format", dates);
        }
      } catch (error) {
        console.error("Error fetching random dates:", error);
      }
    }

    getRandomDates();
  }, []);

  return (
    <div className="container">
      <main>
        <section className="intro">
          <h2>Welcome!</h2>
          <p>
            Explore our collection of romantic date ideas, from cozy nights in
            to adventurous outings. Find inspiration for your next magical
            moment.
          </p>
          <button>Get Started</button>
        </section>

        <section className="gallery-preview">
          <h2>Featured Date Ideas</h2>
          <div className="gallery">
            {dateIds.length > 0 ? (
              dateIds.map((id) => <DateCard key={id} dateId={id} />)
            ) : (
              <p>Loading date ideas...</p>
            )}
          </div>
        </section>
      </main>
    </div>
  );
};

export default HomePage;
