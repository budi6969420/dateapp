import React, { useState, useEffect, useRef } from "react";
import "./Index.css";
import ProfilePicture from "../ProfilePicture/Index";

const Navbar = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const menuRef = useRef(null);

  const handleClickOutside = (event) => {
    if (isMenuOpen && menuRef.current?.contains(event.target)) {
      setIsMenuOpen(false);
    }
  };

  useEffect(() => {
    document.addEventListener("click", handleClickOutside);

    return () => {
      document.removeEventListener("click", handleClickOutside);
    };
  }, [isMenuOpen]);

  return (
    <header>
      <nav className="navbar">
        <div className="logo">
          <h1>{"Budi & Elli"}</h1>
        </div>
        <ul className={`nav-links ${isMenuOpen ? "active" : ""}`} ref={menuRef}>
          <li>
            <a href="/">Home</a>
          </li>
          <li>
            <a href="/dates">Dates</a>
          </li>
          <li>
            <a href="/ideas">Ideas</a>
          </li>
          <li>
            <a href="/users">Users</a>
          </li>
        </ul>
        <div className="navbar-right">
          <div
            className="hamburger"
            onClick={() => setIsMenuOpen((prev) => !prev)}
          >
            &#9776;
          </div>
          <ProfilePicture self isClickable />
        </div>
      </nav>
    </header>
  );
};

export default Navbar;
