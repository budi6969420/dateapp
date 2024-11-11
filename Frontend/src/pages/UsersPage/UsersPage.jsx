import React, { useEffect, useState } from "react";
import UserCard from "../../components/UserCard/Index";
import "./Index.css";
import useGlobalContext from "../../hooks/useGlobalContext";

export default function UsersPage() {
  const [users, setUsers] = useState([]);
  let { fetchData } = useGlobalContext();

  useEffect(() => {
    const fetchUsers = async () => {
      let users = await fetchData("user", "GET", null);
      setUsers(users);
    };
    fetchUsers();
  }, []);

  return (
    <div>
      <h2>Users:</h2>
      <div className="user-card-grid">
        {users.map((x) => (
          <UserCard key={x.id} id={x.id} navigateEnabled />
        ))}
      </div>
    </div>
  );
}
