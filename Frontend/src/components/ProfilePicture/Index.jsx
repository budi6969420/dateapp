import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router";
import "./Index.css";
import useGlobalContext from "../../hooks/useGlobalContext";

const ProfilePicture = ({ userId, self, isClickable, size }) => {
  const navigate = useNavigate();
  let { fetchData } = useGlobalContext();

  const [src, setSrc] = useState(
    "https://media.tenor.com/On7kvXhzml4AAAAi/loading-gif.gif"
  );
  const [selfId, setSelfId] = useState();

  useEffect(() => {
    async function retrieveSelf() {
      let request = await fetchData("user/self", "GET", null, true);
      return request.id;
    }

    async function retrieveSrc(userId) {
      let imageBlob = await fetchData(
        `image/user/${userId}`,
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

    const loadProfilePicture = async () => {
      let userIdInternal = userId;
      if (self) {
        userIdInternal = await retrieveSelf();
        setSelfId(userIdInternal);
      }
      await retrieveSrc(userIdInternal);
    };

    loadProfilePicture();
  }, [userId, self]);

  const handleClick = () => {
    if (!isClickable) return;
    navigate(`/user/${self ? selfId : userId}`);
  };

  return (
    <div
      className={`profile-picture ${isClickable ? "clickable" : ""}`}
      onClick={handleClick}
      style={{
        width: size ? size : "40px",
        height: size ? size : "40px",
      }}
    >
      <img src={src} alt="Profile" className="profile-picture-img" />
    </div>
  );
};

export default ProfilePicture;
