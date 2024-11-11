import React, { useEffect, useState } from "react";
import ProfilePicture from "../ProfilePicture/Index";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMars, faVenus } from "@fortawesome/free-solid-svg-icons";
import "./Index.css";
import useGlobalContext from "../../hooks/useGlobalContext";

const UserCard = ({ id, navigateEnabled }) => {
  const [user, setUser] = useState();
  let { fetchData } = useGlobalContext();

  useEffect(() => {
    const fetchUserData = async () => {
      let user = await fetchData(`user/${id}`, "GET", null, true);
      setUser(user);
    };

    fetchUserData();
  }, [id]);

  if (!user) {
    return <div>Loading...</div>;
  }

  return (
    <div className="card">
      <div className="user-card-content">
        <ProfilePicture userId={user.id} size={"300px"} isClickable={false} />
        <div>
          {navigateEnabled ? (
            <a href={`/user/${id}`} className="user-name">
              {user.username}
            </a>
          ) : (
            <h3 className="user-name">{user.username}</h3>
          )}
          {user.gender === "m" ? (
            <FontAwesomeIcon icon={faMars} className="gender-icon male" />
          ) : user.gender === "f" ? (
            <FontAwesomeIcon icon={faVenus} className="gender-icon female" />
          ) : null}
        </div>
      </div>
    </div>
  );
};

export default UserCard;
