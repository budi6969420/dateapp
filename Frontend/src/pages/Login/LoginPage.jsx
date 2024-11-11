import React, { useEffect, useState } from "react";
import "./index.css";
import useGlobalContext from "../../hooks/useGlobalContext";
import { useNavigate } from "react-router";

const LoginPage = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  let { fetchData, saveToken, isAuthenticated } = useGlobalContext();

  useEffect(() => {
    if (isAuthenticated) navigate("/");
  }, []);

  const handleLogin = async (e) => {
    e.preventDefault();
    let response = await fetchData(
      "user/login",
      "POST",
      {
        username,
        password,
      },
      true
    );
    if (response.token) {
      saveToken(response.token);
      navigate("/");
    }
  };

  return (
    <div className="login-page">
      <h2>Welcome Back!</h2>
      <h3>Login</h3>
      <form onSubmit={handleLogin}>
        <div className="form-group">
          <label htmlFor="username">Username:</label>
          <input
            type="text"
            id="username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
            className="input-field"
            placeholder="Enter your username"
          />
        </div>
        <div className="form-group">
          <label htmlFor="password">Password:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
            className="input-field"
            placeholder="Enter your password"
          />
        </div>
        <div className="button-container">
          <button type="submit" className="btn btn-primary">
            Confirm
          </button>
        </div>
      </form>
    </div>
  );
};

export default LoginPage;
