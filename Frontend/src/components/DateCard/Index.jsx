import React, { useEffect, useState } from "react";
import useGlobalContext from "../../hooks/useGlobalContext";

export default function DateCard({ dateId }) {
  const [date, setDate] = useState();
  const [src, setSrc] = useState(
    "https://media.tenor.com/On7kvXhzml4AAAAi/loading-gif.gif"
  );
  const { fetchData } = useGlobalContext();

  useEffect(() => {
    async function fetchDate() {
      return await fetchData(`date/${dateId}`, "GET", null, true);
    }

    async function retrieveSrc(imageId) {
      let imageBlob = await fetchData(
        `image/${imageId}`,
        "GET",
        null,
        false,
        true
      );
      if (imageBlob && imageBlob.size > 0) {
        setSrc(URL.createObjectURL(imageBlob));
      } else {
        console.error("Image data is not available.");
      }
    }

    fetchDate()
      .then((date) => {
        if (date) {
          setDate(date);
          if (date.imageIds && date.imageIds.length > 0) {
            retrieveSrc(date.imageIds[0]);
          }
          console.log(date);
        }
      })
      .catch((error) => {
        console.error("Error fetching date:", error);
      });
  }, [dateId]);

  return (
    <div className="card">
      <img src={src} alt="date picture" />
      <p>{date ? date.description : "Loading..."}</p>
    </div>
  );
}
