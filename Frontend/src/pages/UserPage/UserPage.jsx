import React from "react";
import UserCard from "../../components/UserCard/Index";
import "./Index.css";
import { useParams } from "react-router";

export default function UserPage() {
  const { id } = useParams();

  return (
    <div className="card-container">
      <UserCard id={id} />
    </div>
  );
}
