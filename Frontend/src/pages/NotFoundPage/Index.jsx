import React from "react";
import "./Index.css";
import { useNavigate } from "react-router";

export default function NotFoundPage() {
  const navigate = useNavigate();
  const handleGoHome = () => {
    navigate("/");
  };

  return (
    <div className="notfound-container">
      <h1 className="notfound-title">404</h1>
      <p className="notfound-message">Oops! Page not found</p>
      <p className="notfound-submessage">
        The page you're looking for doesn't exist.
      </p>
      <button className="home-button" onClick={handleGoHome}>
        Redirect
      </button>
    </div>
  );
}
