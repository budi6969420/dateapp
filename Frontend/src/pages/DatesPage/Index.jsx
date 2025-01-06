import React, { useEffect, useState } from "react";
import DateCard from "../../components/DateCard/Index";
import useGlobalContext from "../../hooks/useGlobalContext";
import "./Index.css";

export default function DatesPage() {
  const [dateIds, setDateIds] = useState([]);
  const { fetchData } = useGlobalContext();
  useEffect(() => {
    async function getDates() {
      try {
        const dates = await fetchData("date", "GET", null, true);
        if (Array.isArray(dates)) {
          setDateIds(dates.map((x) => x.id));
        } else {
          console.error("Unexpected data format", dates);
        }
      } catch (error) {
        console.error("Error fetching all dates:", error);
      }
    }

    getDates();
  }, []);
  return (
    <div className="container">
      <h2>Dates:</h2>
      <div className="card-container">
        {dateIds.length > 0 ? (
          dateIds.map((id) => <DateCard key={id} dateId={id} />)
        ) : (
          <p>Loading date ideas...</p>
        )}
      </div>
    </div>
  );
}
